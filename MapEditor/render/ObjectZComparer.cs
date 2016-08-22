/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 23.10.2014
 */
using System;
using System.Collections.Generic;
using NoxShared;

namespace MapEditor.render
{
	/// <summary>
	/// Description of ObjectZComparer.
	/// </summary>
	public class ObjectZComparer : IComparer<Map.Object>
	{
		public ObjectZComparer()
		{
		}
	
		public int Compare(Map.Object a, Map.Object b)
		{
			ThingDb.Thing tt1 = ThingDb.Things[a.Name];
			ThingDb.Thing tt2 = ThingDb.Things[b.Name];
			int y = (int) ((a.Location.Y + tt1.Z) - (b.Location.Y + tt2.Z));
			return y;
		}
	}
}
