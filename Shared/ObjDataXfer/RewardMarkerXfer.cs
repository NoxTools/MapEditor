/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of RewardMarkerXfer.
	/// </summary>
	[Serializable]
	public class RewardMarkerXfer : DefaultXfer
	{
		public RewardFlags RewardType;
		public int Unknown1;
		// short count; byte prefixed strings
		public List<string> Spells; 
		public List<string> Abilities; // AbilityBook
		public List<string> Monsters; // FieldGuide
		// 5 unknown integers
		public int Unknown2;
		public int Unknown3;
		public int Unknown4;
		public int Unknown5;
		public int Unknown6;
		// chance switch table
		// 1: 25% 2: 50% 3: 75% 4: 95% else: 100%
		public int ActivateChance;
		public bool RareOrSpecial;
		
		[Flags]
		public enum RewardFlags : uint
		{
			SPELL_BOOK = 1,
			ABILITY_BOOK = 2,
			FIELD_GUIDE = 4,
			WEAPON = 8,
			ARMOR = 0x10,
			GEM = 0x20,
			POTION = 0x40,
			GEM2 = 0x80
		}
		
		public RewardMarkerXfer()
		{
			Spells = new List<string>();
			Abilities = new List<string>();
			Monsters = new List<string>();
			RewardType = (RewardFlags) 0xFF;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			
			RewardType = (RewardFlags) br.ReadUInt32();
			Unknown1 = br.ReadInt32();
			// spells
			short count = br.ReadInt16();
			Spells = new List<string>(count);
			while (count > 0)
			{
				Spells.Add(br.ReadString());
				count--;
			}
			// abilities
			count = br.ReadInt16();
			Abilities = new List<string>(count);
			while (count > 0)
			{
				Abilities.Add(br.ReadString());
				count--;
			}
			// monster scrolls
			count = br.ReadInt16();
			Monsters = new List<string>(count);
			while (count > 0)
			{
				Monsters.Add(br.ReadString());
				count--;
			}
			Unknown2 = br.ReadInt32();
			Unknown3 = br.ReadInt32();
			Unknown4 = br.ReadInt32();
			Unknown5 = br.ReadInt32();
			Unknown6 = br.ReadInt32();
			if (ParsingRule >= 62)
				ActivateChance = br.ReadInt32();
			if (ParsingRule >= 63)
				RareOrSpecial = br.ReadBoolean();
			
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			
			bw.Write((uint) RewardType);
			bw.Write(Unknown1);
			bw.Write((short) Spells.Count);
			foreach (string spell in Spells)
				bw.Write(spell);
			bw.Write((short) Abilities.Count);
			foreach (string ability in Abilities)
				bw.Write(ability);
			bw.Write((short) Monsters.Count);
			foreach (string scroll in Monsters)
				bw.Write(scroll);
			bw.Write(Unknown2);
			bw.Write(Unknown3);
			bw.Write(Unknown4);
			bw.Write(Unknown5);
			bw.Write(Unknown6);
			bw.Write(ActivateChance);
			bw.Write(RareOrSpecial);
		}
		
		public override short MaxVersion 
		{
			get
			{ 
				return 0x3f;
			}
		}
	}
}
