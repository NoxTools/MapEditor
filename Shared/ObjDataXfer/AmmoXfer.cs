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
	/// Description of AmmoXfer.
	/// </summary>
	[Serializable]
	public class AmmoXfer : DefaultXfer
	{
		// these never have C null terminators
		public string[] Enchantments;
		
		public byte AmmoCurrent;
		public byte AmmoLimit;
		
		public AmmoXfer()
		{
			// One of these is normally MaterialTeam$
			Enchantments = new string[4] { "", "" , "", "" };
			AmmoCurrent = 20;
			AmmoLimit = 20;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			// 4 enchantments
			for (int i = 0; i < 4; i++) Enchantments[i] = br.ReadString();
			AmmoCurrent = br.ReadByte();
			AmmoLimit = br.ReadByte();
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			
			for (int i = 0; i < 4; i++) bw.Write(Enchantments[i]);
			bw.Write(AmmoCurrent);
			bw.Write(AmmoLimit);
		}
	}
}
