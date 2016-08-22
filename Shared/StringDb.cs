using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;


namespace NoxShared
{
	//only 2 TODOs keeping this from being perfect (2 unknowns in header)
	//also: FIXME: order the entries properly when writing them. what is the order? FIFO?
	public class StringDb : NoxDb
	{
		public static StringDb Current;

		static StringDb()
		{
			dbFile = "nox.csf";
			Current = new StringDb(NoxPath + dbFile);
		}

		public class NoxStringEncoding : Encoding
		{
			//decode methods
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				for (int offset = 0; offset < byteCount; offset++)
					bytes[byteIndex + offset] = (byte) ~bytes[byteIndex + offset];

				return Encoding.Unicode.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return count / 2;
			}

			public override int GetMaxCharCount(int byteCount)
			{
				return byteCount / 2;
			}

			//encode methods
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
			{
				for (int offset = 0; offset < charCount; offset++)
					chars[charIndex + offset] = (char) ~chars[charIndex + offset];//FIXME?

				return Encoding.Unicode.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			public override int GetByteCount(char[] chars, int index, int count)
			{
				return count * 2;
			}

			public override int GetMaxByteCount(int charCount)
			{
				return charCount * 2;
			}
		}

		protected static Encoding enc = new NoxStringEncoding();
		public Hashtable Entries = new Hashtable();
		public StringDbHeader Header = new StringDbHeader();

		public StringDb(string filename)
		{
			using (FileStream fs = File.OpenRead(filename))
			{
				Read(fs);
			}
		}

		public string GetEntryFirstVal(string key)
		{
			if (!Entries.ContainsKey(key)) return null;
			StringEntry entry = (StringEntry) Entries[key];
			if (entry.Values.Count < 1) return null;
			return ((StringEntry.StringValue) entry.Values[0]).Value;
		}
		
		public class StringDbHeader
		{
			public string Identifier = "CSF ";
			public int Flags = 2;//according to XCC, these are "flags"
			public int EntryCount;
			public int unknown_count;//TODO

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				Identifier = ReadIntString(rdr);
				Flags = rdr.ReadInt32();
				EntryCount = rdr.ReadInt32();
				unknown_count = rdr.ReadInt32();
				rdr.ReadBytes(8);//nulls -- according to XCC, the second int are also flags of some sort
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write(StringToInt(Identifier));
				wtr.Write((int) Flags);
				wtr.Write((int) EntryCount);
				wtr.Write((int) unknown_count);
				wtr.Write(new byte[8]);//nulls
			}
		}

		public void Read(Stream stream)
		{
			Header.Read(stream);

			//the string entries
			while (Entries.Count < Header.EntryCount)
			{
				StringEntry ent = new StringEntry(stream);
				Entries.Add(ent.Key, ent);
			}
		}

		public void Write(Stream stream)
		{
			Header.EntryCount = Entries.Count;
			Header.Write(stream);
			foreach (StringEntry ent in Entries.Values)
				ent.Write(stream);
		}

		public class StringEntry
		{
			public string Key;
			public IList Values = new ArrayList();

			public StringEntry(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				rdr.ReadChars(4);//"LBL " backwards
				int numStrings = rdr.ReadInt32();
				Key = new string(rdr.ReadChars(rdr.ReadInt32()));
				while (numStrings-- > 0)
					Values.Add(new StringValue(rdr.BaseStream));
				Debug.Assert(numStrings <= 0);
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write(StringToInt("LBL "));
				wtr.Write((int) Values.Count);
				wtr.Write((int) Key.Length);
				wtr.Write(Encoding.ASCII.GetBytes(Key));
				foreach (StringValue val in Values)
					val.Write(wtr.BaseStream);
			}

			public class StringValue
			{
				public string Value = "";
				public string DialogFile = "";

				public StringValue(Stream stream)
				{
					Read(stream);
				}

				public void Read(Stream stream)
				{
					BinaryReader rdr = new BinaryReader(stream);
					string valueCode = ReadIntString(rdr);//"Str " or "StrW" backwards
					Value = enc.GetString(rdr.ReadBytes(rdr.ReadInt32() * 2));	
					if (valueCode == "StrW")//values of type StrW also have another ASCII string associated with them that associate a sound file found in Dialog/
						DialogFile = new string(rdr.ReadChars(rdr.ReadInt32()));
				}

				public void Write(Stream stream)
				{
					BinaryWriter wtr = new BinaryWriter(stream);
					if (DialogFile == "")
						wtr.Write(StringToInt("Str "));
					else
						wtr.Write(StringToInt("StrW"));

					wtr.Write((int) Value.Length);
					wtr.Write(enc.GetBytes(Value));

					if (DialogFile != "")
					{
						wtr.Write((int) DialogFile.Length);
						wtr.Write(Encoding.ASCII.GetBytes(DialogFile));
					}
				}
			}
		}

		protected static string ReadIntString(BinaryReader rdr)
		{
			char[] valArray = rdr.ReadChars(4);
			Array.Reverse(valArray);
			return new string(valArray);
		}

		protected static int StringToInt(string val)
		{
			char[] valArray = val.ToCharArray();
			Array.Reverse(valArray);
			return BitConverter.ToInt32(Encoding.ASCII.GetBytes(valArray), 0);
		}
	}
}
