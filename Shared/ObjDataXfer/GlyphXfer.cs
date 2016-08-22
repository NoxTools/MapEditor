/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of GlyphXfer.
	/// </summary>
	[Serializable]
	public class GlyphXfer : DefaultXfer
	{
		public byte Angle;
		public float TargX;
		public float TargY;
		public List<String> Spells;
		
		public GlyphXfer()
		{
			Spells = new List<String>();
			TargX = 2885;
			TargY = 2885;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			
			if (ParsingRule < 41) br.ReadInt32();
			Angle = br.ReadByte();
			TargX = br.ReadSingle();
			TargY = br.ReadSingle();
			byte spells = br.ReadByte();
			while (spells > 0)
			{
				Spells.Add(br.ReadString());
				spells--;
			}
			
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			
			bw.Write(Angle);
			bw.Write(TargX);
			bw.Write(TargY);
			bw.Write((byte) Spells.Count);
			foreach (string s in Spells) bw.Write(s);
		}
	}
}
