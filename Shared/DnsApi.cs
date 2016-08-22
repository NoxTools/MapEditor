using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Runtime.InteropServices;

namespace DnsLib
{
	public class IpHlpApi
	{
		[DllImport("IPHlpApi.dll")]
		protected static extern int GetNetworkParams(byte[] info, ref int bufLen);

		protected class FixedInfo
		{
			protected const int MAX_HOSTNAME_LEN = 128;
			protected const int MAX_DOMAIN_NAME_LEN = 128;
			protected const int MAX_SCOPE_ID_LEN = 256;

			public string HostName;
			public string DomainName;
			private IntPtr CurrentDnsServer;//"reserved", do not use!
			public ArrayList DnsServerList = new ArrayList();
			public uint NodeType;
			public string ScopeId;
			public uint EnableRouting;
			public uint EnableProxy;
			public uint EnableDns;

			public FixedInfo(Stream stream)
			{
				Read(stream);
			}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				HostName = new string(rdr.ReadChars(MAX_HOSTNAME_LEN + 4)).TrimEnd('\0');
				DomainName = new string(rdr.ReadChars(MAX_DOMAIN_NAME_LEN + 4)).TrimEnd('\0');
				CurrentDnsServer = (IntPtr) rdr.ReadInt32();
				DnsServerList.Add(new IpAddrString(stream));//TODO: get the rest of the servers in the list too
				NodeType = rdr.ReadUInt32();
				ScopeId = new string(rdr.ReadChars(MAX_SCOPE_ID_LEN + 4)).TrimEnd('\0');
				EnableRouting = rdr.ReadUInt32();
				EnableProxy = rdr.ReadUInt32();
				EnableDns = rdr.ReadUInt32();
			}
		}

		protected class IpAddrString
		{
			public IntPtr Next;
			public string IpAddress;
			public string IpMask;
			public uint Context;
			
			public IpAddrString(Stream stream)
			{
				Read(stream);
			}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				Next = (IntPtr) rdr.ReadInt32();
				IpAddress = new string(rdr.ReadChars(0x10)).TrimEnd('\0');
				IpMask = new string(rdr.ReadChars(0x10)).TrimEnd('\0');
				Context = rdr.ReadUInt32();
			}
		}
		
		public static ArrayList GetDnsServers()
		{
			byte[] buffer = new byte[0];
			int length = 0;
			GetNetworkParams(buffer, ref length);//call to get the size needed
			buffer = new byte[length];
			GetNetworkParams(buffer, ref length);//now call for real
			FixedInfo info = new FixedInfo(new MemoryStream(buffer));

			ArrayList servers = new ArrayList();
			foreach (IpAddrString entry in info.DnsServerList)
				servers.Add(entry.IpAddress);

			return servers;
		}
	}

	public class MXRecord
	{
		public int preference = -1;
		public string exchange = null;
	}

	public class DnsApi
	{
		private const int DNS_PORT = 53;

		private static byte[] data;
		private static int position, id, length;
		private static string name;
		private static ArrayList dnsServers = new ArrayList();

		static DnsApi()
		{
			id = DateTime.Now.Millisecond * 60;
			dnsServers.AddRange(IpHlpApi.GetDnsServers());
		}

		public static ArrayList GetMXServers(string host)
		{
			ArrayList servers = new ArrayList();

			//TODO: order these by preference
			foreach (MXRecord rec in GetMXRecords(host))
				servers.Add(rec.exchange);

			return servers;
		}

		public static ArrayList GetMXRecords(string host)
		{
			ArrayList mxRecords = null;

			//try all the dns servers and return the results from the first successful query
			foreach (string server in dnsServers)
			{
				try
				{
					mxRecords = GetMXRecords(host, server);
					break;
				}
				catch(IOException) {}
			}

			return mxRecords;
		}

  		private static int getNewId()
		{
			//return a new id
    		return ++id;
  		}

		public static ArrayList GetMXRecords(string host,string serverAddress)
		{
			//opening the UDP socket at DNS server
			//use UDPClient, if you are still with Beta1
			UdpClient dnsClient = new UdpClient(serverAddress, DNS_PORT);

			//preparing the DNS query packet.
			makeQuery(getNewId(),host);

			//send the data packet
			dnsClient.Send(data,data.Length);

			IPEndPoint endpoint = null;
			//receive the data packet from DNS server
			data = dnsClient.Receive(ref endpoint);

    		length = data.Length;

    		//un pack the byte array & makes an array of MXRecord objects.
    		return makeResponse();
		}

		//for packing the information to the format accepted by server
		public static void makeQuery(int id,String name)
		{
			data = new byte[512];

			data[0]	 = (byte) (id >> 8);
			data[1]  = (byte) (id & 0xFF );
			data[2]  = (byte) 1; data[3] = (byte) 0;
			data[4]  = (byte) 0; data[5] = (byte) 1;
			data[6]  = (byte) 0; data[7] = (byte) 0;
	    	data[8]  = (byte) 0; data[9] = (byte) 0;
    		data[10] = (byte) 0; data[11] = (byte) 0;

    		string[] tokens = name.Split(new char[] {'.'});
	  		string label;

  			position = 12;

  			for(int j=0; j<tokens.Length; j++) {

				label = tokens[j];
				data[position++] = (byte) (label.Length & 0xFF);
				byte[] b = Encoding.ASCII.GetBytes(label);

				for(int k=0; k < b.Length; k++) {
					data[position++] = b[k];
				}

 			}

			data[position++] = (byte) 0 ; data[position++] = (byte) 0;
			data[position++] = (byte) 15; data[position++] = (byte) 0 ;
			data[position++] = (byte) 1 ;

		}

		//for unpacking the byte array
		public static ArrayList makeResponse() {

			ArrayList mxRecords = new ArrayList();
			MXRecord mxRecord;

			//NOTE: we are ignoring the unnecessary fields.
			//		and takes only the data required to build
			//		MX records.

    		int qCount = ((data[4] & 0xFF) << 8) | (data[5] & 0xFF);
		    if (qCount < 0) {
      			throw new IOException("invalid question count");
    		}

    		int aCount = ((data[6] & 0xFF) << 8) | (data[7] & 0xFF);
	    	if (aCount < 0) {
    			throw new IOException("invalid answer count");
    		}

	    	position=12;

    		for( int i=0;i<qCount; ++i) {
				name = "";
	    		position = proc(position);
	    		position += 4;
			}

    		for (int i = 0; i < aCount; ++i) {

				name = "";
				position = proc(position);

      			position+=10;

				int pref = (data[position++] << 8) | (data[position++] & 0xFF);

				name="";
				position = proc(position);

				mxRecord = new MXRecord();

				mxRecord.preference = pref;
				mxRecord.exchange = name;

				mxRecords.Add(mxRecord);

			}

			return mxRecords;
		}

		private static int proc(int position) {

			int len = (data[position++] & 0xFF);

    		if(len == 0) {
      			return position;
    		}

    		int offset;

    		do {
      			if ((len & 0xC0) == 0xC0) {
        			if (position >= length) {
          				return -1;
        			}
	        		offset = ((len & 0x3F) << 8) | (data[position++] & 0xFF);
		        	proc(offset);
	        		return position;
      			} else {
        			if ((position + len) > length) {
          				return -1;
        			}
        			name += Encoding.ASCII.GetString(data, position, len);
        			position += len;
      			}

      			if (position > length) {
        			return -1;
      			}

				len = data[position++] & 0xFF;

      			if (len != 0) {
	        		name += ".";
      			}
    		}while (len != 0);

	    	return position;
		}
	}
}