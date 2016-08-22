/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 12.02.2015
 */
using System;
using NoxShared;
using System.ComponentModel;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Description of Generator.
	/// </summary>
	public static class Generator
	{
		public static Random GenRandom;
		public static GeneratorConfig GenConfig;
		private static IGenerator _GenImpl;
		public static BackgroundWorker Worker;
		public static bool IsGenerating = false;
		public const int BOUNDARY = 252;
		
		public static string GetStatus()
		{
			if (_GenImpl != null) return _GenImpl.GetAction();
			return "";
		}
		
		public static void SetConfig(GeneratorConfig config)
		{
			GenConfig = config;
			GenRandom = new Random(config.RandomSeed);
			SetupGenerator();
		}
		
		private	static void SetupGenerator()
		{
			if (GenRandom == null) return;
			if (GenConfig == null) return;
			
			switch (GenConfig.MapType)
			{
				case GeneratorConfig.MapPreset.Crossroads:
					_GenImpl = new CrossroadGenerator();
					break;
				case GeneratorConfig.MapPreset.Dungeons:
					_GenImpl = new DungeonGenerator();
					break;
				default:
					return;
			}
			Worker = new BackgroundWorker();
			Worker.WorkerReportsProgress = true;
			Worker.DoWork += new DoWorkEventHandler(worker_DoWork);
		}
		
		public static void GenerateMap(Map map)
		{
			if (Worker == null) return;
			IsGenerating = true;
			GeneratorUtil.ClearMap(map);
			Worker.RunWorkerAsync(map);
		}

		static void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			MapHelper hmap = new MapHelper((Map) e.Argument);
			_GenImpl.Generate(hmap, Worker);
			IsGenerating = false;
			_GenImpl = null;
			Worker = null;
		}
	}
}
