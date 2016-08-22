/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 13.02.2015
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Description of CrossroadGenerator.
	/// </summary>
	public class CrossroadGenerator : IGenerator
	{
		private CellularAutomata baseFloorCA;
		private CellularAutomata denseFloorCA;
		
		// Floor types
		const string BASE_FLOOR = "GrassSparse2";
		const string PATH_FLOOR = "DirtDark2";
		const string DENSE_FLOOR = "GrassDense";
		const string BLEND_EDGE = "BlendEdge";
		// Wall types
		const string WALL1 = "DecidiousWallGreen";
		const string WALL2 = "DecidiousWallBrown";
		
		public override void Generate(MapHelper hmap, BackgroundWorker worker)
		{
			baseFloorCA = new CellularAutomata(Generator.GenRandom, Generator.BOUNDARY);
			denseFloorCA = new CellularAutomata(Generator.GenRandom, Generator.BOUNDARY);
			// Stage1: Base floor
			currentAction = "Generating sparse grass";
			baseFloorCA.SetupCA(2, 1, 0.048f);
			int ticks = 23;// 23-25 
			for (int i = 0; i < ticks; i++)
			{
				baseFloorCA.DoSimulationTick();
				worker.ReportProgress(GeneratorUtil.CalcPercent(i, ticks));
			}
			// Stage2: Copy floor onto map
			currentAction = "Importing generated tiles";
			bool[,] baseFloorMap = baseFloorCA.GetCellMap();
			hmap.SetTileMaterial(BASE_FLOOR);
			for (int x = 0; x < Generator.BOUNDARY; x++)
			{
				worker.ReportProgress(GeneratorUtil.CalcPercent(x, Generator.BOUNDARY));
			    for (int y = 0; y < Generator.BOUNDARY; y++)
				{
					if (baseFloorMap[x, y]) hmap.PlaceTileSnap(x, y);
				}
			}
			// Stage3: Generate sparse grass
			currentAction = "Generating dense grass";
			denseFloorCA.SetupCA(6, 5, 0.75f);
			ticks = 18;
			for (int i = 0; i < ticks; i++)
			{
				denseFloorCA.DoSimulationTick();
				worker.ReportProgress(GeneratorUtil.CalcPercent(i, ticks));
			}
			// Stage4: Import sparse grass
			bool[,] grassMap = denseFloorCA.GetCellMap();
			hmap.SetTileMaterial(DENSE_FLOOR);
			for (int x = 0; x < Generator.BOUNDARY; x++)
			{
				worker.ReportProgress(GeneratorUtil.CalcPercent(x, Generator.BOUNDARY));
			    for (int y = 0; y < Generator.BOUNDARY; y++)
				{
			    	if (grassMap[x, y] && baseFloorMap[x, y]) hmap.PlaceTileSnap(x, y);
				}
			}
			// Stage5: Make pathes
			currentAction = "Generating pathes";
			baseFloorCA.SetCellMap(baseFloorMap);
			baseFloorCA.BirthCellLimit = 9;
			baseFloorCA.DeathCellLimit = 6;
			ticks = 20;
			for (int i = 0; i < ticks; i++)
			{
				baseFloorCA.DoSimulationTick();
				worker.ReportProgress(GeneratorUtil.CalcPercent(i, ticks));
			}
			// Stage6: Import pathes
			currentAction = "Importing generated tiles";
			bool[,] pathesMap = baseFloorCA.GetCellMap();
			hmap.SetTileMaterial(PATH_FLOOR);
			for (int x = 0; x < Generator.BOUNDARY; x++)
			{
				worker.ReportProgress(GeneratorUtil.CalcPercent(x, Generator.BOUNDARY));
			    for (int y = 0; y < Generator.BOUNDARY; y++)
				{
			    	if (pathesMap[x, y] && baseFloorMap[x, y]) hmap.PlaceTileSnap(x, y);
				}
			}
			// Stage7: Blend pathes
			currentAction = "Blending (Path)";
			hmap.SetTileMaterial(PATH_FLOOR);
			hmap.SetEdgeMaterial(BLEND_EDGE);
			hmap.SetAutoblendIgnore(DENSE_FLOOR); // Densegrass overrides
			GeneratorUtil.BlendAllTiles(hmap);
			// Stage8: Blend dense grass
			currentAction = "Blending (Dense grass)";
			hmap.SetTileMaterial(DENSE_FLOOR);
			hmap.SetEdgeMaterial(BLEND_EDGE);
			hmap.SetAutoblendIgnore(null);
			GeneratorUtil.BlendAllTiles(hmap);
			// Stage9: Scan tiles
			currentAction = "Scanning area (floodfill)";
			// This array will be used later for generating polygons
			List<HashSet<Point>> TileGroupedArea = new List<HashSet<Point>>();
			for (int x = 0; x <= Generator.BOUNDARY; x++)
			{
				worker.ReportProgress(GeneratorUtil.CalcPercent(x, Generator.BOUNDARY));
			    for (int y = 0; y <= Generator.BOUNDARY; y++)
				{
			    	if (hmap.GetTile(x, y) != null)
			    	{
			    		bool search = true;
			    		foreach (HashSet<Point> list in TileGroupedArea)
			    		{
			    			if (list.Contains(new Point(x, y)))
			    			{
			    				search = false;
			    				break;
			    			}
			    		}
			    		if (search)
			    		{
				    		FloodFill fill = new FloodFill(hmap);
				    		fill.PerformFloodFill(x, y);
				    		
				    		// Ignore tile groups with less than 200 tiles
				    		if (fill.GetResult().Count < 200)
				    			fill.DeleteResultTiles();
				    		else
				    			TileGroupedArea.Add(fill.GetResult());
			    		}
			    	}
				}
			}
			// Stage10: Build walls
			currentAction = "Building void walls";
			hmap.SetWallMaterial(WALL1);
			GeneratorUtil.MakeWallsAroundTiles(hmap);
			// Stage11: Smooth walls
			currentAction = "Smoothing walls";
			GeneratorUtil.ReorientWalls2(hmap, Generator.GenConfig.Allow3SideWalls);
			// Stage12: Populate (optional)
			if (Generator.GenConfig.PopulateMap)
			{
				currentAction = "Populating (objects)";
				Populate.PopulateCrossroad(hmap);
			}
			// Finish: return
		}
	}
}
