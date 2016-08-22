/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 09.11.2014
 */
using System;

namespace MapEditor.objgroups
{
	/// <summary>
	/// Description of IncludeRule.
	/// </summary>
	public struct IncludeRule
	{
		public string Pattern;
		public IncludeRuleType Rule;
		public bool Exclude; // если true то обьекты наоборот удаляются
		
		public IncludeRule(string pattern, IncludeRuleType rule)
		{
			Pattern = pattern;
			Rule = rule;
			Exclude = false;
		}
		
		public enum IncludeRuleType
		{
			OBJECT_CLASS = 1, // обьекты добавляются по классу
			NAME_BEGIN, // по началу имени
			NAME_ENDING, // по концу имени
			NAME_CONTAIN, // по наличию вхождения строки в имя
			NAME_EQUAL, // имя равно
			ANYTHING, // добавляются ВСЕ обьекты
		}
	}
}
