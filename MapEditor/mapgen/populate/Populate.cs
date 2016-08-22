/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 14.02.2015
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Description of Populate.
	/// </summary>
	public static class Populate
	{	
		const int PLAYERSTARTS_MIN = 15;
		const int PLAYERSTARTS_MAX = 30;
		
		const float CROSSROAD_TREE_DIST = 110f;
		const int CROSSROAD_TREE_LIMIT = 250;
		static string[] CROSSROAD_TREES = 
		{
			"TreePine03",
			"TreeForest03",
			"TreeForest04",
			"TreeForest13",
			"TreeForest14",
			"TreeForest15",
			"TreeForest17",
			"TreeGreen2",
			"TreeGreen3",
			"TreeOgre03",
			"TreePine02",
			"TreePine06",
			"TreePine10",
			"TreeTrunk6",
			"Stump10",
			"Stump12"
		};		
		
		const float CROSSROAD_FLOWER_DIST = 70f;
		const int CROSSROAD_FLOWERD_LIMIT = 200;
		const int CROSSROAD_FLOWERS_LIMIT = 300;
		
		static string[] FLOWERS_DENSE = 
		{
			"FlowersBlueDense",
			"FlowersYellowDense",
			"FlowersWhiteDense",
			"FlowersPurpleDense",
			"FoliageDense1",
			"FoliageDense2"
		};
		
		static string[] FLOWERS_SPARSE = 
		{
			"FlowersBlueSparse",
			"FlowersYellowSparse",
			"FlowersWhiteSparse",
			"FlowersPurpleSparse",
			"FoliageSparse1",
			"FoliageSparse2"
		};
		
		const int PLANT_CLUSTER_RANGE_MIN = 15;
		const int PLANT_CLUSTER_RANGE_MAX = 60;
		const int PLANT_CLUSTER_N_MIN = 1;
		const int PLANT_CLUSTER_N_MAX = 4;
		const int PLANT_LIMIT = 250;
		const float CROSSROAD_BUSH_DIST = 65f;
		
		static string[] PLANT_GROUP1 = 
		{
			"Bush1",
			"Bush2",
			"Bush11",
			"Bush13",
			"Bush3",
			"Bush4",
			"Bush5",
			"Bush6",
			"Bush7"
		};
		
		static string[] PLANT_GROUP2 = 
		{
			"Plant2",
			"Plant2Flowered"
		};
		
		static string[] PLANT_GROUP3 =
		{
			"Plant4",
			"Plant5"
		};
		
		/// <summary>
		/// Populates map with PlayerStart objects
		/// </summary>
		private static void PlacePlayerStarts(MapHelper hmap, int amount)
		{
			List<PointF> playerStarts = new List<PointF>();
			
			float minDist = 400;
			int px, py;
			while (amount > 0)
			{
				px = Generator.GenRandom.Next(1, Generator.BOUNDARY);
				py = Generator.GenRandom.Next(1, Generator.BOUNDARY);
				// There must be a tile underneath this point
				if (hmap.GetTile(px, py) != null)
				{
					// Convert to map coordinates
					float wx = px * 23f + 11.5f;
					float wy = py * 23f + 34.5f;
					// Check distance between already created PlayerStarts
					if (hmap.DistanceToClosestObjectOfType("PlayerStart", wx, wy) > minDist)
						hmap.PlaceNormalObject("PlayerStart", wx, wy);
					// this will never create endless loop (hopefully)
					amount--;
				}
			}
		}
		
		private static void PlaceObjectsOfType(MapHelper hmap, string[] types, float distanceMin, int errorLimit)
		{
			int errors = 0;
			int px, py = 0;
			while (errors < errorLimit)
			{
				px = Generator.GenRandom.Next(1, Generator.BOUNDARY);
				py = Generator.GenRandom.Next(1, Generator.BOUNDARY);
				// There must be a tile underneath this point
				if (hmap.GetTile(px, py) != null)
				{
					// Convert to map coordinates
					float wx = px * 23f + 11.5f;
					float wy = py * 23f + 34.5f;
					// Check distance
					if (hmap.DistanceToClosestObjectOfType(types, wx, wy) > distanceMin)
					{
						int whatKind = Generator.GenRandom.Next(0, types.Length);
						hmap.PlaceNormalObject(types[whatKind], wx, wy);
					}
					else errors++;
				}
			}
		}
		
		private static void PlaceDenseVegCrossroads(MapHelper hmap, string[] types, float distanceMin, int errorLimit)
		{
			int errors = 0;
			int px, py = 0;
			NoxShared.Map.Tile tile;
			while (errors < errorLimit)
			{
				px = Generator.GenRandom.Next(1, Generator.BOUNDARY);
				py = Generator.GenRandom.Next(1, Generator.BOUNDARY);
				// There must be a tile underneath this point
				tile = hmap.GetTile(px, py);
				if (tile != null)
				{
					// Convert to map coordinates
					float wx = px * 23f + 11.5f;
					float wy = py * 23f + 34.5f;
					// Move by random offset
					wx += Generator.GenRandom.Next(-11, 11);
					wy += Generator.GenRandom.Next(-11, 11);
					// spawn only on dense grass tiles
					if (tile.Graphic == "GrassDense")
					{
						// Check distance
						if (hmap.DistanceToClosestObjectOfType(types, wx, wy) > distanceMin)
						{
							int whatKind = Generator.GenRandom.Next(0, types.Length);
							hmap.PlaceNormalObject(types[whatKind], wx, wy);
						}
						else errors++;
					}
				}
			}
		}
		
		public static void PopulateCrossroad(MapHelper hmap)
		{
			System.ComponentModel.BackgroundWorker worker = Generator.Worker;
			
			PlacePlayerStarts(hmap, Generator.GenRandom.Next(PLAYERSTARTS_MIN, PLAYERSTARTS_MAX));
			
			// Make trees
			PlaceDenseVegCrossroads(hmap, CROSSROAD_TREES, CROSSROAD_TREE_DIST, CROSSROAD_TREE_LIMIT);
			// Make flowers
			PlaceDenseVegCrossroads(hmap, FLOWERS_DENSE, CROSSROAD_FLOWER_DIST, CROSSROAD_FLOWERD_LIMIT);
			PlaceObjectsOfType(hmap, FLOWERS_SPARSE, CROSSROAD_FLOWER_DIST, CROSSROAD_FLOWERS_LIMIT);
			// Make plants/bushes
			//PlaceObjectsOfType(hmap, PLANT_GROUP1, NORMAL_BUSH_DIST, PLANT_LIMIT);
			PlaceDenseVegCrossroads(hmap, PLANT_GROUP2, CROSSROAD_BUSH_DIST, PLANT_LIMIT);
			PlaceDenseVegCrossroads(hmap, PLANT_GROUP3, CROSSROAD_BUSH_DIST, PLANT_LIMIT);
		}
	}
}
