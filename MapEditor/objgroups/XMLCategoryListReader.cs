/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 09.11.2014
 */
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace MapEditor.objgroups
{
	/// <summary>
	/// Производит загрузку категорий обьектов из XML файла. 
	/// </summary>
	public static class XMLCategoryListReader
	{
		public static ObjectCategory[] ReadCategories(string filepath)
		{
			List<ObjectCategory> result = new List<ObjectCategory>();
			if (File.Exists(filepath))
			{
				StreamReader rdr = new StreamReader(filepath, Encoding.UTF8); // чтобы не ругалось на кириллицу
				XmlTextReader textReader = new XmlTextReader(rdr);
				
				textReader.ReadStartElement("ThemeCategories");
				ObjectCategory current = null;
				while (textReader.Read())
				{
					if (textReader.IsStartElement("Category"))
					{
						if (current != null) result.Add(current);
						// записываем предыдущую категорию и создаем новую
						current = new ObjectCategory(textReader.GetAttribute("Name"));
					}
					else if (textReader.Name == "Include" || textReader.Name == "Exclude")
					{
						IncludeRule rule = new IncludeRule();
						if (textReader.Name == "Exclude") rule.Exclude = true;
						rule.Rule = (IncludeRule.IncludeRuleType) Enum.Parse(typeof(IncludeRule.IncludeRuleType), textReader.GetAttribute("Rule"), true);
						rule.Pattern = textReader.ReadElementString();
						current.Rules.Add(rule);
					}
				}
				textReader.Close();
				result.Add(current); // добавляем последнюю категорию
			}
			return result.ToArray();
		}
	}
}
