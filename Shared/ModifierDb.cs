using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;

namespace NoxShared
{
	public class ModifierDb : NoxDb
	{
		public class Mod
		{
			public string name;
			public string type;

			public string DESC;
			public Color COLOR1;
			public Color COLOR2;
			public Color COLOR3;
			public Color COLOR4;
			public Color COLOR5;
			public Color COLOR6;

			public string COLOR1_DESC;
			public string COLOR2_DESC;
			public string COLOR3_DESC;
			public string COLOR4_DESC;
			public string COLOR5_DESC;
			public string COLOR6_DESC;

			public string EFFECTIVENESS;
			public string MATERIAL;
			public string PRIMARYENCHANTMENT;
			public string SECONDARYENCHANTMENT;

			public string RANGE;
			public string CLASSUSE;

			public string DURABILITY;
			public string REQUIRED_STRENGTH;

			public string DAMAGE_MIN;
			public string DAMAGE_COEFFICIENT;
			public string DAMAGE_TYPE;
			public string ARMOR_VALUE;

			public string IDENTIFY;
			public string WORTH;
			public Color COLOR;
			public string ATTACKEFFECT;
			public string ALLOWED_WEAPONS;
			public string ALLOWED_ARMOR;
			public string DEFENDEFFECT;
			public string DEFENDCOLLIDEEFFECT;

			public string ALLOWED_POSITION;

			public string PRIMARY;
			public string SECONDARY;
			public string ATTACKPREHITEFFECT;
			public string ATTACKPREDAMAGEEFFECT;

			public string ENGAGEEFFECT;
			public string DISENGAGEEFFECT;
			public string UPDATEEFFECT;
			public string IDENTIFYDESC;
			public string PRIMARYDESC;
			public string SECONDARYDESC;

			public void Read(StreamReader rdr)
			{
				while (rdr.BaseStream.Position < rdr.BaseStream.Length)
				{
					string line = "";
					while (true)
					{
						char c = (char)rdr.Read();

						if (c == ';')
							break;

						line = line + c;

						if (line.Trim() == "END")
							return;
					}

					if (line == "")
						continue;

					string[] parts = line.Split(new char[] { '=' });
					string fieldName = parts[0].Trim();

					string fieldValueS = parts[1].TrimEnd(new char[] { ';' }).Trim();

					System.Object fieldValue;
					if (fieldName.StartsWith("COLOR", StringComparison.InvariantCultureIgnoreCase))
					{
						string[] colors = fieldValueS.Split(new char[] { ' ' });

						Color c = Color.FromArgb(
							int.Parse(colors[0]),
							int.Parse(colors[1]),
							int.Parse(colors[2]));

						fieldValue = c;

						if (colors.Length > 3)
						{
							var fielddescName = fieldName + "_DESC";
							FieldInfo descf = typeof(Mod).GetField(fielddescName);

							string str = "";
							for (int i = 3; i < colors.Length; i++)
							{
								str = colors[i] + " ";
							}

							descf.SetValue(this, str.Trim());
						}
					}
					else
					{
						fieldValue = fieldValueS;
					}

					FieldInfo f = typeof(Mod).GetField(fieldName);
					f.SetValue(this, fieldValue);
				}
			}
			public Mod(StreamReader rdr, string name, string type)
			{
				this.name = name;
				this.type = type;

				Read(rdr);
			}
		}

		public static Dictionary<string, Mod> Mods;
		
		static ModifierDb()
		{
			dbFile = "modifier.bin";
			Mods = new Dictionary<string, Mod>();
  			try
            {
  				ParseModifierBin();
			}
            catch (Exception ex) 
            {
            	Logger.Log("Failed to parse modifier.bin");
            	Logger.Log(ex.Message);
            }
		}
		
		private static void ParseModifierBin()
		{
			using (StreamReader rdr = new StreamReader(CryptApi.DecryptStream(GetStream(), CryptApi.NoxCryptFormat.MODIFIER)))
			{
				string type = "";
				while (rdr.BaseStream.Position < rdr.BaseStream.Length)
				{
					string line = rdr.ReadLine().Trim();

					if (line == "")
						continue;

					if (line == "END")
					{
						type = "";
						continue;
					}

					if (line == "WEAPON_DEFINITIONS" || line == "ARMOR_DEFINITIONS" || line == "EFFECTIVENESS" || line == "MATERIAL" || line == "ENCHANTMENT")
					{
						type = line;
						continue;
					}

					Mods.Add(line, new Mod(rdr, line, type));
				}
			}
		}
	}
}
