/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 05.11.2014
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace NoxShared
{
	/// <summary>
	/// Monster database provider
	/// </summary>
	public class MonsterDb : NoxDb
	{
		public struct MonsterInfo
		{
			public string Name;
			public int Health;
			public float RetreatRatio;
			public float ResumeRatio;
			public string Status;
		}
		
		public static Dictionary<string, MonsterInfo> MonsterDict;
		
		static MonsterDb()
		{
			dbFile = "monster.bin";
			MonsterDict = new Dictionary<string, MonsterInfo>();
            try
            {
				ParseMonsterBin();
			}
            catch (Exception ex) 
            {
            	Logger.Log("Failed to parse monster.bin");
            	Logger.Log(ex.Message);
            }
		}
		
		private static void ParseMonsterBin()
		{
			using (StreamReader rdr = new StreamReader(CryptApi.DecryptStream(GetStream(), CryptApi.NoxCryptFormat.MONSTER)))
			{
				string line;
				MonsterInfo minfo = new MonsterInfo();
				bool monsterBlock = false;
				while (!rdr.EndOfStream)
				{
					line = rdr.ReadLine();
				
					if (!monsterBlock && line.Length > 1)
					{
						minfo = new MonsterInfo();
						minfo.Name = line;
						monsterBlock = true;
						continue;
					}
					if (line == "END")
					{
						monsterBlock = false;
						MonsterDict.Add(minfo.Name, minfo);
						continue;
					}
					string[] split = line.Split(' ');
					
					string type = "", val = "";
					foreach (string s in split)
					{
						if (s.Length > 0)
						{
							if (s == "ARENA") break; // ignore arena entries
							if (s == "SOLO") continue;
							if (type.Length == 0) type = s;
							else val = s;
						}
					}
					
					switch (type)
					{
						case "HEALTH":
							minfo.Health = int.Parse(val);
							break;
						case "RETREAT_RATIO":
							minfo.RetreatRatio = float.Parse(val, NumberFormatInfo.InvariantInfo);
							break;
						case "RESUME_RATIO":
							minfo.ResumeRatio = float.Parse(val, NumberFormatInfo.InvariantInfo);
							break;
						case "STATUS":
							minfo.Status = val;
							break;
					}
				}
			}
		}
	}
}
