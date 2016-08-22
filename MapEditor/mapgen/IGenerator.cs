/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 13.02.2015
 */
using System;
using System.ComponentModel;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Abstract generator class
	/// </summary>
	public abstract class IGenerator
	{
		protected string currentAction = "Initializing...";
		
		public string GetAction()
		{
			lock (currentAction)
			{
				return currentAction;
			}
		}
		public abstract void Generate(MapHelper hmap, BackgroundWorker worker);
	}
}
