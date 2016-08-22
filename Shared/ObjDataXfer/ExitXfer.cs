/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;
using System.Text;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of ExitXfer.
	/// </summary>
	[Serializable]
	public class ExitXfer : DefaultXfer
	{
		public string MapName;
		// Last two fields are likely never used
		public float ExitX;
		public float ExitY;
		
		public ExitXfer()
		{
			MapName = "";
			ExitX = 2944f;
			ExitY = 2944f;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryReader br = new BinaryReader(mstream);
			
			int nameLen = br.ReadInt32();
			MapName = Encoding.ASCII.GetString(br.ReadBytes(nameLen)).TrimEnd('\0');
			if (ParsingRule >= 31)
			{
				ExitX = br.ReadSingle();
				ExitY = br.ReadSingle();
			}
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			
			bw.Write(MapName.Length + 1);
			bw.Write(Encoding.ASCII.GetBytes(MapName));
			bw.Write((byte) 0); // null terminator
			bw.Write(ExitX);
			bw.Write(ExitY);
		}
		
		public override short MaxVersion
		{
			get
			{
				return 0x3c;
			}
		}
	}
}
