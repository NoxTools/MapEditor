using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;
using System.Text;

using NoxShared.NoxType;

namespace NoxShared
{
	/// <summary>
	/// PlayerFile represents the player data contained within a .plr file. This class handles the decryption/encryption when given a filename via the constructor.
	/// </summary>
	public class PlayerFile
	{
		protected string filename;

		//TODO: change these to inner classes as necessary
		protected ArrayList spellset;

		public string Name;
		public string SavePath;
		public DateTime FileTime;
		public string LastMapPlayed;
		public int ActiveSpellset;
		public int ActiveTrap;
		public CharacterClass Class;
		public PlrFileType FileType;

		//colors
		public Color HairColor;
		public Color SkinColor;
		public Color BeardColor;
		public Color MustacheColor;
		public Color SideburnsColor;

		public UserColor PantsColor;
		public UserColor ShirtColor;
		public UserColor ShirtTrimColor;
		public UserColor ShoesColor;
		public UserColor ShoesTrimColor;

		public enum CharacterClass
		{
			WAR = 0x00,
			WIZ = 0x01,
			CON = 0x02
		}

		public PlayerFile()
		{
			spellset = new ArrayList();
		}

		public PlayerFile(string filename) : this()
		{
			Load(filename);
		}

		public void Load(string filename)
		{
			this.filename = filename;
			ReadFile();
		}


		public enum PlrFileType
		{
			SOLO = 0x02,//same as QUEST too
			MULTI = 0x07
		};

		public enum SectionID
		{
			SOLO = 0x02,
			MULTI = 0x07,
			ITEMS = 0x15
		}

		#region Read Methods
		public void ReadFile()
		{
			NoxBinaryReader rdr = new NoxBinaryReader(File.Open(filename, FileMode.Open), CryptApi.NoxCryptFormat.PLR);

			//find out what kind of file we got
			FileType = (PlrFileType) rdr.ReadInt64();

			//switch for different formats
			switch (FileType)
			{
				case PlrFileType.SOLO:
					System.Windows.Forms.MessageBox.Show("Sorry, solo/quest player files are not yet supported.", "Error");
					ReadSPHeader(rdr);
					//ReadSpellset(rdr);
					rdr.BaseStream.Seek(0, SeekOrigin.End);
					break;
				case PlrFileType.MULTI:
					ReadSpellset(rdr);
					rdr.ReadInt32();//TODO: dunno what this int is doing hangin out here, always 0x00000001?
					rdr.SkipToNextBoundary();
					ReadCharacterInfo(rdr);
					rdr.ReadBytes(4);//(TODO) 0c 00 00 00 -- this occupies unaccounted-for space and is post-padded
					rdr.SkipToNextBoundary();
					rdr.ReadBytes((int) rdr.ReadInt64());//skip the next section (the 3byte one)
					rdr.SkipToNextBoundary();
					break;
				default:
					throw new IOException("Unknown player file format");
			}

			System.Diagnostics.Debug.Assert(rdr.BaseStream.Position == rdr.BaseStream.Length, "Wrong number of total bytes read.");
			rdr.Close();
		}

		protected void ReadSpellset(BinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

			rdr.ReadInt16();//UNKNOWN: seems to always be 0x0003

			while (rdr.BaseStream.Position < finish)
			{
				byte flags = rdr.ReadByte();
				byte strLen = rdr.ReadByte();

				//the list ends with a 00 (00-04) (00-02) 04 entry
				// that describes active spellset, trap, and <one other thing?>
				if (strLen <= 0x04)
				{
					ActiveSpellset = strLen;
					ActiveTrap = rdr.ReadByte();
					rdr.ReadByte();//UNKNOWN: dunno what this is for, always 0x04?
					break;//the loop should terminate anyway since these should be the last bytes of the section
				}
				else
					spellset.Add(new Spell(flags, new string(rdr.ReadChars(strLen))));
			}

			System.Diagnostics.Debug.Assert(rdr.BaseStream.Position == finish, "Bad SpellList length");
		}

		public struct Spell
		{
			public byte Flags;//TODO: what's this mean? inverted/not inverted?
			public string Name;

			public Spell(byte flags, string name)
			{
				Flags = flags;
				Name = name;
			}
		}

		protected void ReadCharacterInfo(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
			
			rdr.ReadBytes(6);//UNKNOWN header seems to always = 0c 00 02 00 00 00 
			SavePath = rdr.ReadString(System.Type.GetType("System.Int16"));
			rdr.ReadByte();//terminating null not included in length

			//the time this file was written
			FileTime = new DateTime(
				rdr.ReadInt16(),//year
				rdr.ReadInt32() & 0xFFFF,//month --skip the next 16bytes
				//rdr.ReadInt16(),//day of the week (not needed)
				rdr.ReadInt16(),//day
				rdr.ReadInt16(),//hour
				rdr.ReadInt16(),//minute
				rdr.ReadInt16(),//seconds
				rdr.ReadInt16()//milliseconds
			);

			//these colors are RGB
			HairColor = rdr.ReadColor();
			SkinColor = rdr.ReadColor();
			MustacheColor = rdr.ReadColor();
			BeardColor = rdr.ReadColor();
			SideburnsColor = rdr.ReadColor();

			PantsColor = rdr.ReadUserColor();
			ShirtColor = rdr.ReadUserColor();
			ShirtTrimColor = rdr.ReadUserColor();
			ShoesColor = rdr.ReadUserColor();
			ShoesTrimColor = rdr.ReadUserColor();

			Name = rdr.ReadUnicodeString();
			Class = (CharacterClass) rdr.ReadByte();
			rdr.ReadBytes(2);//UNKNOWN: always 00 0A?
			LastMapPlayed = rdr.ReadString();
			rdr.ReadByte();//null terminator not included in string length

			System.Diagnostics.Debug.Assert(rdr.BaseStream.Position == finish, "Bad CharacterInfo length");
		}

		protected void ReadSPHeader(NoxBinaryReader rdr)
		{
			//long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
			//TODO: skip for now
			rdr.ReadBytes((int) rdr.ReadInt64());
		}
		#endregion

		#region Write Methods
		//precondition: all members are properly initialized
		public void WriteFile()
		{
			NoxBinaryWriter wtr = new NoxBinaryWriter(File.Open(filename, FileMode.Create), CryptApi.NoxCryptFormat.PLR);

			wtr.Write((long) FileType);
			switch (FileType)
			{
				case PlrFileType.MULTI:
					WriteSpells(wtr);
					wtr.Write((int) 0x00000001);//UNKOWN, constant
					wtr.SkipToNextBoundary();

					WriteCharacterInfo(wtr);
					wtr.Write((int) 0x0000000c);//UNKNOWN, constant
					wtr.SkipToNextBoundary();

					//write the last short section
					wtr.Write((long) 3);
					wtr.Write((short) 0x000B);
					wtr.Write((byte) 0x00);

					wtr.SkipToNextBoundary();
					break;
			}

			wtr.Close();
		}

		protected void WriteSpells(NoxBinaryWriter wtr)
		{
			wtr.Write((long) 0);//save a spot for the length
			long startPos = wtr.BaseStream.Position;

			wtr.Write((short) 0x0003);//UNKNOWN, but always the same

			foreach (Spell spell in spellset)
			{
				wtr.Write((byte) spell.Flags);
				wtr.Write(spell.Name);
			}

			wtr.Write((byte) 0x00);//first byte of last entry is null
			wtr.Write((byte) ActiveSpellset);
			wtr.Write((byte) ActiveTrap);
			wtr.Write((byte) 0x04);//UNKNOWN, but constant

			//go back and write in the length
			long length = wtr.BaseStream.Position - startPos;
			wtr.Seek((int) startPos - 8, SeekOrigin.Begin);
			wtr.Write((long) length);
			wtr.Seek(0, SeekOrigin.End);
		}

		protected void WriteCharacterInfo(NoxBinaryWriter wtr)
		{
			wtr.Write((long) 0);//save a spot for the length
			long startPos = wtr.BaseStream.Position;

			wtr.Write((short) 0x000C);//UNKNOWN, but constant
			wtr.Write((int) 0x00000002);//UNKOWN, but constant

			wtr.Write((short) SavePath.Length);
			wtr.Write(SavePath.ToCharArray());
			wtr.Write((byte) 0x00);//extra terminator

			//TODO: allow user specified datetime?
			DateTime time = DateTime.Now;
			wtr.Write((short) time.Year);
			wtr.Write((short) time.Month);
			wtr.Write((short) time.DayOfWeek);
			wtr.Write((short) time.Day);
			wtr.Write((short) time.Hour);
			wtr.Write((short) time.Minute);
			wtr.Write((short) time.Second);
			wtr.Write((short) time.Millisecond);

			wtr.WriteColor(HairColor);
			wtr.WriteColor(SkinColor);
			wtr.WriteColor(MustacheColor);
			wtr.WriteColor(BeardColor);
			wtr.WriteColor(SideburnsColor);
			wtr.WriteUserColor(PantsColor);
			wtr.WriteUserColor(ShirtColor);
			wtr.WriteUserColor(ShirtTrimColor);
			wtr.WriteUserColor(ShoesColor);
			wtr.WriteUserColor(ShoesTrimColor);

			wtr.Write((byte) (Encoding.Unicode.GetByteCount(Name) / 2));
			wtr.Write(Encoding.Unicode.GetBytes(Name));
			wtr.Write((byte) Class);
			wtr.Write((short) 0x0A00);//UNKNOWN, but constant -- may be character level, but this is always reset to 10, so what's the point of storing it in the file?!
			wtr.Write(LastMapPlayed);
			wtr.Write((byte) 0x00);//null terminator not counted in stringlength

			//go back and write in the length
			long length = wtr.BaseStream.Position - startPos;
			wtr.Seek((int) startPos - 8, SeekOrigin.Begin);
			wtr.Write((long) length);
			wtr.Seek(0, SeekOrigin.End);
		}
		#endregion
	}
}
