/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of ArmorXfer.
	/// </summary>
	[Serializable]
	public class ArmorXfer : DefaultXfer
	{
		// these never have C null terminators
		public string[] Enchantments;
		// 0 = Unbreakable
		public short Durability;
		
		// I have never seen this used personally but it exists in code, purpose unknown
		public int Unknown;
		
		public ArmorXfer()
		{
			Enchantments = new string[4] { "", "" , "", "" };
			Durability = 0;
			Unknown = 0;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			// 4 enchantments
			for (int i = 0; i < 4; i++) Enchantments[i] = br.ReadString();
			if (ParsingRule > 41)
			{
				if (ParsingRule >= 42) Durability = br.ReadInt16();
				if (ParsingRule == 61) br.ReadByte();
				if (ParsingRule >= 62) Unknown = br.ReadInt32();
			}
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			
			for (int i = 0; i < 4; i++) bw.Write(Enchantments[i]);
			bw.Write(Durability);
			if (ParsingRule == 61) bw.Write((byte) 0);
			if (ParsingRule >= 62) bw.Write(Unknown);
		}
		
		public override short MaxVersion
		{
			get
			{
				// HACK ignoring Unknown value for now
				return 0x3d; // 0x3e
			}
		}
	}
}
