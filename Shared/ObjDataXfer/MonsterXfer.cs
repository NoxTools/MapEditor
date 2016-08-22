/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Структура содержит данные, хранимые MonsterXFer, в упорядоченной форме.
	/// </summary>
	[Serializable]
	public class MonsterXfer : DefaultXfer
	{
		public byte DirectionId; // направление
		public string[] ScriptEvents; // 10
		public ushort DetectEventTimeout; // задержка во фреймах между вызовами скрипта
		public byte ActionRoamPathFlag; // монстр будет следовать только вейпоинтам с этим флагом
		public NoxEnums.MonsterStatus StatusFlags; // статус флаги
		public float HealthMultiplier; // множитель запаса здоровья
		public float RetreatRatio; // коэфф. здоровья при котором кастеры будут убегать
		public float ResumeRatio; // коэфф. здоровья при котором после отступления кастеры будут снова нападать
		public float SightRange; // радиус (дистанция) обзора монстра
		public float Aggressiveness; // агрессивность монстра, от 0.0F до 0.83F
		public int DefaultAction; // что будет делать монстр сразу после загрузки карты
		public string EscortObjName; // "скриптовое" имя сопровождаемого монстра 
		// описываем логику кастования (вызова заклинаний)
		public List<SpellEntry> KnownSpells; // список заклинаний которые может кастовать монстр (в общем то тоже флаги)
		public ushort ReactionCastingDelayMin; // ждёт столько фреймов перед Inversion/Counterspell
		public ushort ReactionCastingDelayMax; 
		public ushort BuffCastingDelayMin; // ждёт столько фреймов между между защитными заклинаниями
		public ushort BuffCastingDelayMax;
		public ushort DebuffCastingDelayMin; // ждёт столько фреймов между атакующими заклинаниями
		public ushort DebuffCastingDelayMax;
		public ushort OffensiveCastingDelayMin; // ждёт столько фреймов между вызовом монстров?
		public ushort OffensiveCastingDelayMax;
		public ushort BlinkCastingDelayMin; // ждёт столько фреймов между телепортацией
		public ushort BlinkCastingDelayMax;
		public float LockPathDistance; 
		public int SpellPowerLevel; // от 1 до 3
		public float AimSkillLevel; // от 1.0F до 0F - влияет на точность заклинаний
		public bool Immortal; // делает монстра неубиваемым (точнее ставит ему максхп на 0)
		public string TrapSpell1; // бомберы при взрыве их активируют
		public string TrapSpell2;
		public string TrapSpell3;
		public ShopkeeperInfoStruct ShopkeeperInfo; // торговцы
		public uint MagicNumber; // 0xDEADFACE
		public uint AddedSubclass;
		public short Health; // вроде как здоровье
		public bool SetDefaultResumeRatio; 
		public bool SetDefaultRetreatRatio;
        public bool SetDefaultMonsterStatus;
		public bool LearnDefaultSpells; // если true монстр получает стандартные заклинания и spellpower
		public Color[] MaidenBodyColors;
		public string MaidenVoiceSet;
		public string WoundedNPCVoiceSet;
		public BuffEntry[] BuffList;
		public byte PoisonLevel; // сила яда, обычно 0 (не отравлен)
		private const string ENCHANT_SHIELD = "ENCHANT_SHIELD";

        public static ulong[] NOX_DIRECT_LONG = 
		{
            0xFFFFFFFFFFFFFFFF, // 225
			0x0000000100000001, // 270
			0xFFFFFFFF00000001, // 315
			0x00000000FFFFFFFF, // 180
			0x0000000100000000, // 0
			0x00000001FFFFFFFF, // 135
			0xFFFFFFFF00000000, // 90
			0x0000000000000001 // 45
		};

        public static string[] NOX_DIRECT_NAMES =
        {
        	"North", 
        	"South", 
        	"East", 
        	"North-West",
        	"South-West",
        	"West",
        	"North-East",
        	"South-East"
		};

        public static byte[] NOX_DIRECT_VALS = 
		{

			160, // 225
			32, // 45
			224, // 315
			128, // 180
			64, // 90
			96, // 135
			192, // 270
			0, // 0
		};

        public static int[] NOX_DIRECT_ANIM = 
		{
			0, // 225
			7, // 45
			2, // 315
			3, // 180
			6, // 90
			5, // 135
			1, // 270
			4, // 0
		};


		[Serializable]
		public struct ShopkeeperInfoStruct
		{
			public float BuyValueMultiplier; // 0x6B4
			public float SellValueMultiplier; // 0x6B8
			public string ShopkeeperGreetingText; // 0x694
			public ShopItemInfo[] ShopItems;
		}
		
		[Serializable]
		public struct ShopItemInfo
		{
			public string Name;
			public byte Count;
			// AbilityRewardXfer/SpellRewardXfer/FieldGuideXfer
			public string SpellID;
			public string Ench1;
			public string Ench2;
			public string Ench3;
			public string Ench4;
			
			public override string ToString()
			{
				return string.Format("x{0} {1}", Count, Name);
			}
		}
		
		[Serializable]
		public struct BuffEntry
		{
			public string Name;
			public byte Power;
			public int Duration;
			public int ShieldHealth; // only for ENCHANT_SHIELD (0x1A)
		}
		
		[Serializable]
		public struct SpellEntry
		{
			public string SpellName;
			public uint UseFlags;
			public NoxEnums.NPCSpellCastFlags UseFlagsEnum
			{
				get
				{
					return (NoxEnums.NPCSpellCastFlags) UseFlags;
				}
			}
			
			public SpellEntry(string name, uint flags)
			{
				SpellName = name; UseFlags = flags;
			}
			
			public override string ToString()
			{
				if (ThingDb.Spells.ContainsKey(SpellName))
				{
					ThingDb.Spell spell = ThingDb.Spells[SpellName];
					string result = spell.NameString;
					int e = result.IndexOf(':') + 1;
					result = result.Remove(0, e);
					return result;
				}
				return base.ToString();
			}
		}
		
		private void InitDefault()
		{
			DirectionId = 0;
			ScriptEvents = new string[10];
			for (int i = 0; i < 10; i++) ScriptEvents[i] = "";
			DetectEventTimeout = 1;
			ActionRoamPathFlag = 0xFF;
			StatusFlags = (NoxEnums.MonsterStatus) 0;
			HealthMultiplier = 1F;
			RetreatRatio = 0.05F;
			ResumeRatio = 0.5F;
			SightRange = 300F;
			Aggressiveness = 0F;
			DefaultAction = 0;
			EscortObjName = "";
			KnownSpells = new List<MonsterXfer.SpellEntry>();
			ReactionCastingDelayMin = 15;
			ReactionCastingDelayMax = 30;
			BuffCastingDelayMin = 120;
			BuffCastingDelayMax = 180;
			DebuffCastingDelayMin = 45;
			DebuffCastingDelayMax = 60;
			OffensiveCastingDelayMin = 30;
			OffensiveCastingDelayMax = 60;
			BlinkCastingDelayMin = 150;
			BlinkCastingDelayMax = 300;
			LockPathDistance = 30F;
			SpellPowerLevel = 3;
			AimSkillLevel = 0.5F;
			Immortal = false;
			TrapSpell1 = "SPELL_INVALID";
			TrapSpell2 = "SPELL_INVALID";
			TrapSpell3 = "SPELL_INVALID";
			ShopkeeperInfo = new MonsterXfer.ShopkeeperInfoStruct();
			MagicNumber = 0xDEADFACE;
			AddedSubclass = 0;
			Health = 1;
			SetDefaultResumeRatio = true;
			SetDefaultRetreatRatio = true;
			SetDefaultMonsterStatus = true;
			LearnDefaultSpells = true;
			MaidenBodyColors = new Color[6];
			MaidenVoiceSet = "Maiden";
			BuffList = new MonsterXfer.BuffEntry[0];
			PoisonLevel = 0;
		}
		
		public MonsterXfer()
		{
			InitDefault();
		}
		
		/// <summary>
		/// Returns default XFer value for specified monster
		/// </summary>
		public void InitForMonsterName(string monsterID)
		{
			ThingDb.Thing tt = ThingDb.Things[monsterID];
			bool isShopkeeper = tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.SHOPKEEPER];
			bool isFemaleNPC = tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.FEMALE_NPC];
			bool isWoundNPC = tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.WOUNDED_NPC];
			bool isFriendly = isShopkeeper || isFemaleNPC || isWoundNPC || (tt.Name == "AirshipCaptain");
			
			InitDefault();
			if (!isFriendly) Aggressiveness = 0.83F;
			if (isShopkeeper)
			{
				ShopkeeperInfo.ShopkeeperGreetingText = "";
				ShopkeeperInfo.ShopItems = new MonsterXfer.ShopItemInfo[0];
			}
			if (isFemaleNPC)
			{
				for (int i = 0; i < 6; i++) MaidenBodyColors[i] = Color.FromArgb(0xD2, 0xAE, 0x79);
			}
			WoundedNPCVoiceSet = monsterID;
			if (MonsterDb.MonsterDict.ContainsKey(monsterID))
			{
				MonsterDb.MonsterInfo info = MonsterDb.MonsterDict[monsterID];
				//StatusFlags = gui.StatusCheckList.GetFlagsFromString(info.Status);
				Health = (short) info.Health;
				RetreatRatio = info.RetreatRatio;
				ResumeRatio = info.ResumeRatio;
			}
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			DirectionId = (byte) Array.IndexOf(NOX_DIRECT_LONG, br.ReadUInt64());
			ScriptEvents = new string[10];
			// Read script event handler names
			for (int i = 0; i < 10; i++)
			{
				if (i == 2)
					DetectEventTimeout = br.ReadUInt16();
				
				ScriptEvents[i] = br.ReadScriptEventString();
			}
			// Skip (0)
			if (ParsingRule >= 11)
				br.ReadInt32();
			if (ParsingRule >= 31)
			{
				ActionRoamPathFlag = br.ReadByte();
				if (ParsingRule < 51)
					StatusFlags = (NoxEnums.MonsterStatus) br.ReadUInt16();
				else
					StatusFlags = (NoxEnums.MonsterStatus) br.ReadUInt32();
				
				HealthMultiplier = br.ReadSingle();
				RetreatRatio = br.ReadSingle();
				ResumeRatio = br.ReadSingle();
				SightRange = br.ReadSingle();
				if (ParsingRule < 33)
					br.BaseStream.Seek(2, SeekOrigin.Current);
				Aggressiveness = br.ReadSingle();
				if (ParsingRule < 34)
					DefaultAction = br.ReadInt32();
				EscortObjName = br.ReadString();
				if (ParsingRule >= 34)
				{
					int spells = br.ReadInt32();
					KnownSpells = new List<SpellEntry>();
					string spellName = null;
					uint spellFlags = 0;
					
					for (int i = 0; i < spells; i++)
					{
						spellName = br.ReadString();
						spellFlags = br.ReadUInt32();
						if (ThingDb.Spells.Keys.Contains(spellName))
							KnownSpells.Add(new SpellEntry(spellName, spellFlags));
					}
				}
				else
					br.BaseStream.Seek(0x224, SeekOrigin.Current);
				// Spell usage delay values
				if (ParsingRule < 46)
				{
					ReactionCastingDelayMin = (ushort) br.ReadByte();
					ReactionCastingDelayMax = (ushort) br.ReadByte();
					if (ParsingRule <= 32) br.ReadInt32();
					BuffCastingDelayMin = (ushort) br.ReadByte();
					BuffCastingDelayMax = (ushort) br.ReadByte();
					if (ParsingRule <= 32) br.ReadInt32();
					DebuffCastingDelayMin = (ushort) br.ReadByte();
					DebuffCastingDelayMax = (ushort) br.ReadByte();
					if (ParsingRule <= 32) br.ReadInt32();
					OffensiveCastingDelayMin = (ushort) br.ReadByte();
					OffensiveCastingDelayMax = (ushort) br.ReadByte();
					if (ParsingRule <= 32) br.ReadInt32();
					BlinkCastingDelayMin = (ushort) br.ReadByte();
					BlinkCastingDelayMax = (ushort) br.ReadByte();
					if (ParsingRule <= 32) br.ReadInt32();
				}
				else
				{
					ReactionCastingDelayMin = br.ReadUInt16();
					ReactionCastingDelayMax = br.ReadUInt16();
					BuffCastingDelayMin = br.ReadUInt16();
					BuffCastingDelayMax = br.ReadUInt16();
					DebuffCastingDelayMin = br.ReadUInt16();
					DebuffCastingDelayMax = br.ReadUInt16();
					OffensiveCastingDelayMin = br.ReadUInt16();
					OffensiveCastingDelayMax = br.ReadUInt16();
					BlinkCastingDelayMin = br.ReadUInt16();
					BlinkCastingDelayMax = br.ReadUInt16();
				}
				if (ParsingRule > 32)
					LockPathDistance = br.ReadSingle();

				if (ParsingRule >= 33)
				{
					SpellPowerLevel = br.ReadInt32();
					AimSkillLevel = br.ReadSingle();
					if (ParsingRule < 42)
					{
						if (br.ReadInt16() == 0)
							Immortal = true;
					}
					if (ParsingRule < 53)
					{
						int spellIndex = br.ReadInt32();
						TrapSpell1 = ThingDb.Spells.Values[spellIndex].Name;
						spellIndex = br.ReadInt32();
						TrapSpell2 = ThingDb.Spells.Values[spellIndex].Name;
						spellIndex = br.ReadInt32();
						TrapSpell3 = ThingDb.Spells.Values[spellIndex].Name;
					}
					else
					{
						TrapSpell1 = br.ReadString();
						TrapSpell2 = br.ReadString();
						TrapSpell3 = br.ReadString();
					}
				}
				if (ParsingRule >= 34)
				{
					string action = br.ReadString();
					DefaultAction = Array.IndexOf(NoxEnums.AIActionStrings, action);
				}
				if (ParsingRule >= 41)
				{
					// large, complex structure dealing with AI-specific data
					// it is only used on saved games, normally entryType = 4; forced = 0
					short entryType = br.ReadInt16();
					if (entryType <= 4)
					{
						byte forced = 1;
						if (entryType >= 2)
							forced = br.ReadByte();
						if (forced == 1 || entryType < 2)
						{
							return false;
							// TODO implement for full compatibility
						}
					}
				}
				if (ParsingRule >= 42)
				{
                    Immortal = br.ReadBoolean();
				}
				if (ParsingRule >= 43 && thing.Subclass[(int) ThingDb.Thing.SubclassBitIndex.SHOPKEEPER])
				{
					// Shop contents
					ShopkeeperInfoStruct si = new ShopkeeperInfoStruct();
					if (ParsingRule >= 50)
						si.BuyValueMultiplier = br.ReadSingle();
					if (ParsingRule >= 61)
						si.SellValueMultiplier = br.ReadSingle();
					if (ParsingRule >= 48)
						si.ShopkeeperGreetingText = br.ReadString();
					byte items = br.ReadByte();
					si.ShopItems = new ShopItemInfo[items];
					for (int i = 0; i < items; i++)
					{
						ShopItemInfo item = new ShopItemInfo();
						if (ParsingRule < 50) br.ReadInt32();
						item.Count = br.ReadByte();
						item.Name = br.ReadString();
						if (ParsingRule >= 47)
						{
							item.SpellID = br.ReadString();
							item.Ench1 = br.ReadString();
							item.Ench2 = br.ReadString();
							item.Ench3 = br.ReadString();
							item.Ench4 = br.ReadString();
						}
						si.ShopItems[i] = item;
					}
					ShopkeeperInfo = si;
				}
				if (ParsingRule >= 44)
					MagicNumber = br.ReadUInt32();
				if (ParsingRule >= 45)
					AddedSubclass = br.ReadUInt32();
				if (ParsingRule >= 49)
					Health = br.ReadInt16();
				if (ParsingRule >= 51)
				{
					SetDefaultResumeRatio = br.ReadBoolean();
					SetDefaultRetreatRatio = br.ReadBoolean();
                    SetDefaultMonsterStatus = br.ReadBoolean();
					LearnDefaultSpells = br.ReadBoolean();
				}
				if (ParsingRule >= 54 && thing.Subclass[(int) ThingDb.Thing.SubclassBitIndex.FEMALE_NPC])
				{
					MaidenBodyColors = new Color[6];
					byte R, G, B;
					for (int i = 0; i < 6; i++)
					{
						R = br.ReadByte();
						G = br.ReadByte();
						B = br.ReadByte();
						MaidenBodyColors[i] = Color.FromArgb(R, G, B);
					}
					if (ParsingRule >= 55)
						MaidenVoiceSet = br.ReadString();
				}
				if (ParsingRule >= 62)
				{
					short entryType = br.ReadInt16();
					if (entryType > 2 || entryType <= 0) return false;
						
					byte count = br.ReadByte();
					BuffList = new BuffEntry[count];
					while (count > 0)
					{
						BuffEntry be = new BuffEntry();
						be.Name = br.ReadString();
						be.Power = br.ReadByte();
						be.Duration = br.ReadInt32();
						if (be.Name == ENCHANT_SHIELD && entryType >= 2)
							be.ShieldHealth = br.ReadInt32();
							
						BuffList[count] = be;
						count--;
					}
					Array.Reverse(BuffList);
				}
				if (ParsingRule >= 63 && thing.Subclass[(int) ThingDb.Thing.SubclassBitIndex.WOUNDED_NPC])
					WoundedNPCVoiceSet = br.ReadString();
				if (ParsingRule >= 64)
					PoisonLevel = br.ReadByte();
			}
			return true;
		}
		
		/// <summary>
		/// Преобразует структуру обратно в массив байтов, и сохраняет в Map.Object
		/// </summary>
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			ThingDb.Thing tt = thing;
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			// Направление
			bw.Write(NOX_DIRECT_LONG[DirectionId]);
            //bw.Write(Array.IndexOf(NOX_DIRECT_LONG, DirectionId));
            
			// Записываем обработчики
			for (int i = 0; i < 10; i++)
			{
				if (i == 2)
					bw.Write(DetectEventTimeout);
				
				bw.WriteScriptEvent(ScriptEvents[i]);
			}
			bw.Write((int) 0);
			// Основная инфа
			bw.Write(ActionRoamPathFlag);
			bw.Write((uint) StatusFlags);
			bw.Write(HealthMultiplier);
			bw.Write(RetreatRatio);
			bw.Write(ResumeRatio);
			bw.Write(SightRange);
			bw.Write(Aggressiveness);
			bw.Write(EscortObjName);
			// Записываем заклинания
			int knownSpellsCount = KnownSpells.Count;
			bw.Write(knownSpellsCount);
			foreach (SpellEntry se in KnownSpells)
			{
				bw.Write(se.SpellName);
				bw.Write(se.UseFlags);
			}
			bw.Write(ReactionCastingDelayMin);
			bw.Write(ReactionCastingDelayMax);
			bw.Write(BuffCastingDelayMin);
			bw.Write(BuffCastingDelayMax);
			bw.Write(DebuffCastingDelayMin);
			bw.Write(DebuffCastingDelayMax);
			bw.Write(OffensiveCastingDelayMin);
			bw.Write(OffensiveCastingDelayMax);
			bw.Write(BlinkCastingDelayMin);
			bw.Write(BlinkCastingDelayMax);
			//
			bw.Write(LockPathDistance);
			bw.Write(SpellPowerLevel);
			bw.Write(AimSkillLevel);
			bw.Write(TrapSpell1);
			bw.Write(TrapSpell2);
			bw.Write(TrapSpell3);
			bw.Write(NoxEnums.AIActionStrings[DefaultAction]);
			// данные ИИ - пропускаем
			bw.Write((short) 4);
			bw.Write((byte) 0);
			// Бессмертие - в общем то bool
			bw.Write(Immortal);
			// Магазин
			if (tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.SHOPKEEPER])
			{
				bw.Write(ShopkeeperInfo.BuyValueMultiplier);
				bw.Write(ShopkeeperInfo.SellValueMultiplier);
				bw.Write(ShopkeeperInfo.ShopkeeperGreetingText);
				byte itemsCount = (byte) ShopkeeperInfo.ShopItems.Length;
				ShopItemInfo item;
				bw.Write(itemsCount);
				for (int i = 0; i < itemsCount; i++)
				{
					item = ShopkeeperInfo.ShopItems[i];
					bw.Write(item.Count);
					bw.Write(item.Name);
					bw.Write(item.SpellID);
					bw.Write(item.Ench1);
					bw.Write(item.Ench2);
					bw.Write(item.Ench3);
					bw.Write(item.Ench4);
				}
			}
			//
			bw.Write(MagicNumber);
			bw.Write(AddedSubclass);
			bw.Write(Health);
			bw.Write(SetDefaultResumeRatio);
			bw.Write(SetDefaultRetreatRatio);
			bw.Write(SetDefaultMonsterStatus);
			//
			bw.Write(LearnDefaultSpells);
			if (tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.FEMALE_NPC])
			{
				Color color;
				for (int i = 0; i < 6; i++)
				{
					color = MaidenBodyColors[i];
					bw.Write(color.R);
					bw.Write(color.G);
					bw.Write(color.B);
				}
				bw.Write(MaidenVoiceSet);
			}
			// Список бафов
			bw.Write((short) 2);
			byte buffsNum = (byte) BuffList.Length;
			BuffEntry buff;
			bw.Write(buffsNum);
			for (int i = 0; i < buffsNum; i++)
			{
				buff = BuffList[i];
				bw.Write(buff.Name);
				bw.Write(buff.Power);
				bw.Write(buff.Duration);
				if (buff.Name == ENCHANT_SHIELD)
					bw.Write(buff.ShieldHealth);
			}
			//
			if (tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.WOUNDED_NPC])
				bw.Write(WoundedNPCVoiceSet);
			bw.Write(PoisonLevel);
		}
		
		public override short MaxVersion
		{
			get
			{
				return 64;
			}
		}
	}
}
