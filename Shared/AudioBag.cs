using System;
using System.IO;
using System.Collections;

namespace NoxShared
{
	public class AudioBag : Bag
	{
		public class Header
		{
			public int Entries;

			public Header(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				rdr.ReadInt32();//GABA
				rdr.ReadInt32();//0x00000002
				Entries = rdr.ReadInt32();
			}
		}

		public class Entry
		{
			private const int NAME_LENGTH = 0x10;
			public string Name;
			public uint SampleRate;
			public uint Offset;
			public uint Length;
			public int Flags;//according to xccu/xcc/misc/cc_structures.h
			public int ChunkSize;//according to xccu/xcc/misc/cc_structures.h

			public Entry(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Name = new string(rdr.ReadChars(NAME_LENGTH));
				Name = Name.TrimEnd(new char[] {'\0'});
				Offset = rdr.ReadUInt32();
				Length = rdr.ReadUInt32();
				SampleRate = rdr.ReadUInt32();
				Flags = rdr.ReadInt32();
				ChunkSize = rdr.ReadInt32();
			}
		}

		public class SoundFile
		{
			public class Header
			{
				private byte[] header = {0x52, 0x49, 0x46, 0x46, 0x00, 0x00, 0x00, 0x00, 0x57, 0x41, 0x56, 0x45, 0x66, 0x6d, 0x74, 0x20, 0x14, 0x00, 0x00, 0x00, 0x11, 0x00, 0x01, 0x00, 0x22, 0x56, 0x00, 0x00, 0x5c, 0x2b, 0x00, 0x00, 0x00, 0x02, 0x04, 0x00, 0x02, 0x00, 0xf9, 0x03, 0x66, 0x61, 0x63, 0x74, 0x04, 0x00, 0x00, 0x00, 0xa7, 0x5e, 0x00, 0x00, 0x64, 0x61, 0x74, 0x61, 0x00, 0x00, 0x00, 0x00};
				protected uint length;
				public Header(uint length)
				{
					this.length = length;
				}

				public void Write(Stream stream)
				{
					BinaryWriter wtr = new BinaryWriter(stream);

					wtr.Write(header);
					wtr.BaseStream.Seek(0x04, SeekOrigin.Begin);
					wtr.Write((uint) header.Length-8 + length);
					wtr.BaseStream.Seek(0x38, SeekOrigin.Begin);
					wtr.Write((uint) length);
				}
			}

			protected Header header;
			protected byte[] data;

			public SoundFile(Entry entry, Stream bagStream)
			{
				Read(entry, bagStream);
			}

			public void Read(Entry entry, Stream bagStream)
			{
				BinaryReader rdr = new BinaryReader(bagStream);
				header = new Header(entry.Length);
				rdr.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
				data = rdr.ReadBytes((int) entry.Length);
			}

			public void Write(Stream stream)
			{
				header.Write(stream);
				stream.Write(data, 0, data.Length);
			}
		}

		public ArrayList Entries = new ArrayList();
		protected Header header;

		public AudioBag(string path) : base(path)
		{
			Read();
		}

		protected override bool Read()
		{
            if (!base.Read())
                return false;
			header = new Header(idx);

			int count;
			for (count = 0; count < header.Entries; count++)
			{
				Entries.Add(new Entry(idx));
			}

			System.Diagnostics.Debug.Assert(count == header.Entries, "ERROR: Wrong number of entries read.");
            return true;
		}

		public override void ExtractAll(string path)
		{
			Directory.CreateDirectory(path);
			
			foreach (Entry entry in Entries)
			{
				FileStream stream = File.Create(path + "\\" + entry.Name + ".wav");
				SoundFile soundFile = new SoundFile(entry, bag);
				soundFile.Write(stream);
				stream.Close();
			}
		}
	}
}