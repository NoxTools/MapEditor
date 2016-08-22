/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 09.11.2014
 */
using System;
using System.Collections.Generic;
using NoxShared;
using IncludeType = MapEditor.objgroups.IncludeRule.IncludeRuleType;
using ObjectClass = NoxShared.ThingDb.Thing.ClassFlags;

namespace MapEditor.objgroups
{
	/// <summary>
	/// Description of ObjectCategory.
	/// </summary>
	public class ObjectCategory
	{
		public string Name;
		public List<IncludeRule> Rules;
		
		public ObjectCategory(string name)
		{
			Name = name;
			Rules = new List<IncludeRule>();
		}
		
		/// <summary>
		/// Возвращает список имен обьектов, соответствующих набору заданных IncludeRule
		/// </summary>
		public string[] GetThings()
		{
			List<string> result = new List<string>();
			foreach (IncludeRule rule in Rules)
			{
				ObjectClass objClass = ObjectClass.NULL;
				if (rule.Rule == IncludeType.OBJECT_CLASS)
					objClass = (ObjectClass) Enum.Parse(typeof(ObjectClass), rule.Pattern);
				
				// чтобы не делать 99 проверок для очень простой операции
				if (rule.Rule == IncludeType.ANYTHING)
				{
					result.AddRange(ThingDb.Things.Keys);
					return result.ToArray();
				}
				
				foreach (string name in ThingDb.Things.Keys)
				{
					bool _add = false; // добавлять или нет
					
					switch (rule.Rule)
					{
						case IncludeType.NAME_BEGIN:
							// по началу строки
							if (name.StartsWith(rule.Pattern)) _add = true;
							break;
						case IncludeType.NAME_ENDING:
							// по окончанию строки
							if (name.EndsWith(rule.Pattern)) _add = true;
							break;
						case IncludeType.NAME_CONTAIN:
							// по наличию строки
							if (name.Contains(rule.Pattern)) _add = true;
							break;
						case IncludeType.OBJECT_CLASS:
							// по наличию классфлага
							if (ThingDb.Things[name].HasClassFlag(objClass)) _add = true;
							break;
						case IncludeType.NAME_EQUAL:
							// только если имя равно
							if (name == rule.Pattern) _add = true;
							break;
					}
					
					if (_add)
					{
						// если Exclude то обьекты наоборот УДАЛЯЮТСЯ
						if (rule.Exclude) result.Remove(name);
						// иначе добавляем
						else if (!result.Contains(name)) result.Add(name);
					}
				}
			}
			result.Sort(); // сортируем по алфавиту
			return result.ToArray();
		}
		
		public override string ToString()
		{
			// для ComboBox
			return Name;
		}
	}
}
