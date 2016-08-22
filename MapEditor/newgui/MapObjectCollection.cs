/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 08.11.2014
 */
using System;
using System.Collections;
using System.Collections.Generic;
using NoxShared;

namespace MapEditor.newgui
{
	/// <summary>
	/// Представляет коллекцию Map.Object
	/// </summary>
	[Serializable] // Необходимо для поддержки буфера обмена (Clipboard)
	public class MapObjectCollection : IEnumerable, ICloneable
	{
		public List<Map.Object> Items;
		private Map.Object _origin = null;
		public Map.Object Origin // обьект за который держимся мышкой
		{
			get
			{
				if (!Items.Contains(_origin)) return null;
				return _origin;
			}
			set
			{
				_origin = value;
			}
		}
		
		public bool IsMultiSelection
		{
			get
			{
				return (Items.Count > 1);
			}
		}
		
		public bool IsEmpty
		{
			get
			{
				return (Items.Count == 0);
			}
		}
		
		public Map.Object GetFirst()
		{
			if (IsEmpty) return null;
			return Items[0];
		}
		
		public MapObjectCollection()
		{
			Items = new List<Map.Object>();
		}
		
		public IEnumerator GetEnumerator()
		{
			return Items.GetEnumerator();
		}
		
		public object Clone()
		{
			MapObjectCollection clone = new MapObjectCollection();
			clone.Items = new List<Map.Object>(Items);
			clone.Origin = Origin;
			return clone;
		}
	}
}
