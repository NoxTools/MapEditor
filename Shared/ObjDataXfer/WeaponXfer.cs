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
	/// Description of WeaponXfer.
	/// </summary>
	[Serializable]
	public class WeaponXfer : DefaultXfer
	{
		// these never have C null terminators
		public string[] Enchantments;
		// 0 = Unbreakable
		public short Durability;
		
		// Wand charges
		public byte WandChargesCurrent;
		public byte WandChargesLimit;
		public int WandChargePercent;
		
		// I have never seen this used personally but it exists in code, purpose unknown
		public int Unknown;
		
		public WeaponXfer()
		{
			Enchantments = new string[] { "", "" , "", "" };
			Durability = 0;
			WandChargesCurrent = 1;
			WandChargesLimit = 1;
			Unknown = 0;
		}
		
		public void DefaultsFor(ThingDb.Thing thing)
		{
			// Durability
			Durability = (short) thing.Health;
			// Handle magic staves
			if (thing.HasClassFlag(ThingDb.Thing.ClassFlags.WAND) && !thing.Subclass[(int) ThingDb.Thing.SubclassBitIndex.STAFF])
			{
				WandChargesCurrent = thing.WandCharges;
				WandChargesLimit = thing.WandCharges;
			}
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			// 4 enchantments
			for (int i = 0; i < 4; i++) Enchantments[i] = br.ReadString();
			if (ParsingRule > 41)
			{
				// StaffWooden is considered a WAND but without special casting wand structure
				if (thing.HasClassFlag(ThingDb.Thing.ClassFlags.WAND) && !thing.Subclass[(int) ThingDb.Thing.SubclassBitIndex.STAFF])
				{
					WandChargesCurrent = br.ReadByte();
					WandChargesLimit = br.ReadByte();
					if (ParsingRule >= 61) WandChargePercent = br.ReadInt32();
				}
				if (ParsingRule >= 42) Durability = br.ReadInt16();
				if (ParsingRule == 63) br.ReadByte();
				if (ParsingRule >= 64) Unknown = br.ReadInt32();
			}
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			
			for (int i = 0; i < 4; i++) bw.Write(Enchantments[i]);
			if (thing.HasClassFlag(ThingDb.Thing.ClassFlags.WAND) && !thing.Subclass[(int) ThingDb.Thing.SubclassBitIndex.STAFF])
			{
				bw.Write(WandChargesCurrent);
				bw.Write(WandChargesLimit);
				float recalculated = (float) (WandChargesCurrent / WandChargesLimit) * 100f;
				bw.Write((int) recalculated);
			}
			bw.Write(Durability);
			if (ParsingRule == 63) bw.Write((byte) 0);
			if (ParsingRule >= 64) bw.Write(Unknown);
		}
		
		public override short MaxVersion
		{
			get
			{
				// HACK ignoring Unknown value for now
				return 0x3f; // 0x40
			}
		}
	}
}
