/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 13.02.2015
 */
using System;
using System.ComponentModel;
using NoxShared;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Various utility functions used by map generator
	/// </summary>
	public static class GeneratorUtil
	{
		public static int CalcPercent(int a, int max)
		{
			float coef = (float)a / (float)max;
			coef *= 100f;
			return (int) coef;
		}
		
		public static void BlendAllTiles(MapHelper hmap)
		{
			BackgroundWorker worker = Generator.Worker;
			for (int x = 0; x <= Generator.BOUNDARY; x++)
			{
				worker.ReportProgress(CalcPercent(x, Generator.BOUNDARY));
			    for (int y = 0; y <= Generator.BOUNDARY; y++)
				{
			    	hmap.TileAutoBlend(x, y);
				}
			}
		}
		
		/// <summary>
		/// Magically rotates walls that they look like normal.
		/// </summary>
		public static void ReorientWallsOld(MapHelper hmap, BackgroundWorker worker)
		{
			Map.Wall wall, nUp, nDown, nRight, nLeft;
			for (int x = 0; x < Generator.BOUNDARY; x++)
			{
				worker.ReportProgress(CalcPercent(x, Generator.BOUNDARY));
				for (int y = 0 ; y < Generator.BOUNDARY; y++)
				{
					wall = hmap.GetWall(x, y);
					
					if (wall != null)
					{
						nLeft = hmap.GetWall(x - 1, y - 1);
						nRight = hmap.GetWall(x + 1, y + 1);
						nUp = hmap.GetWall(x + 1, y - 1);
						nDown = hmap.GetWall(x - 1, y + 1);
						
						int neighbors = 0;
						if (nLeft != null) neighbors++;
						if (nRight != null) neighbors++;
						if (nUp != null) neighbors++;
						if (nDown != null) neighbors++;
						if (neighbors == 4) continue; // CROSS
						
						else if (nUp != null && nDown != null)
						{
 							if (nLeft != null) wall.Facing = (Map.Wall.WallFacing) 3;
							else if (nRight != null) wall.Facing = (Map.Wall.WallFacing) 5;
							else wall.Facing = (Map.Wall.WallFacing) 0;
						}
						else if (nLeft != null && nRight != null)
						{
							if (nUp != null) wall.Facing = (Map.Wall.WallFacing) 4;
							else if (nDown != null) wall.Facing = (Map.Wall.WallFacing) 6;
							else wall.Facing = (Map.Wall.WallFacing) 1;
						}
						else if (nLeft != null && nUp != null) wall.Facing = (Map.Wall.WallFacing) 7;
						else if (nRight != null && nUp != null) wall.Facing = (Map.Wall.WallFacing) 8;
						else if (nRight != null && nDown != null) wall.Facing = (Map.Wall.WallFacing) 9;
						else if (nLeft != null && nDown != null) wall.Facing = (Map.Wall.WallFacing) 10;
					}
				}
			}
		}
		
		/// <summary>
		/// Orient/Direct all walls. Much more clever version
		/// </summary>
		public static void ReorientWalls2(MapHelper hmap, bool allowTri)
		{
			BackgroundWorker worker = Generator.Worker;
			Map.Wall wall;
			for (int x = 0; x <= Generator.BOUNDARY + 1; x++)
			{
				worker.ReportProgress(CalcPercent(x, Generator.BOUNDARY));
				for (int y = 0 ; y <= Generator.BOUNDARY + 1; y++)
				{
					wall = hmap.GetWall(x, y);
					
					if (wall != null)
					{
						bool tileLeft = (hmap.GetTile(x - 1, y - 1) != null);
						bool tileRight = (hmap.GetTile(x + 1, y - 1) != null);
						bool tileUp = (hmap.GetTile(x, y - 2) != null);
						bool tileDown = (hmap.GetTile(x, y) != null);
						int facing = -1;
						
						if (!tileLeft)
						{
							if (tileUp && tileRight && tileDown) facing = 10;
							else if (tileDown && tileRight)
							{
								if (hmap.GetWall(x - 1, y - 1) != null && allowTri) facing = 3;
								else facing = 0;
							}
							else if (tileUp && tileRight)
							{
								if (hmap.GetWall(x - 1, y + 1) != null && allowTri) facing = 6;
								else facing = 1;
							}
							else if (!tileUp && !tileDown) facing = 8;
						}
						if (!tileUp && facing < 0)
						{
							if (tileLeft && tileRight && tileDown) facing = 7;
							else if (tileDown && tileLeft)
							{
								if (hmap.GetWall(x + 1, y - 1) != null && allowTri) facing = 4;
								else facing = 1;
							}
							else if (tileDown && tileRight)
							{
								if (hmap.GetWall(x + 1, y + 1) != null && allowTri) facing = 5;
								else facing = 0;
							}
							else if (!tileLeft && !tileRight) facing = 9;
						}
						if (!tileDown && facing < 0)
						{
							if (tileLeft && tileRight && tileUp) facing = 9;
							else if (tileUp && tileLeft)
							{
								if (hmap.GetWall(x + 1, y + 1) != null && allowTri) facing = 5;
								else facing = 0;
							}
							else if (tileUp && tileRight)
							{
								if (hmap.GetWall(x - 1, y + 1) != null && allowTri) facing = 6;
								else facing = 1;
							}
							else if (!tileLeft && !tileRight) facing = 7;
						}
						if (!tileRight && facing < 0)
						{
							if (tileUp && tileLeft && tileDown) facing = 8;
							else if (tileDown && tileLeft)
							{
								if (hmap.GetWall(x + 1, y - 1) != null && allowTri) facing = 4;
								else facing = 1;
							}
							else if (tileUp && tileLeft)
							{
								if (hmap.GetWall(x + 1, y + 1) != null && allowTri) facing = 5;
								else facing = 0;
							}
							else if (!tileUp && !tileDown) facing = 10;
						}
						
						if (facing < 0) facing = 2;
						wall.Facing = (Map.Wall.WallFacing) facing;
					}
				}
			}
		}
		
		/// <summary>
		/// Makes walls between each tile and void
		/// </summary>
		public static void MakeWallsAroundTiles(MapHelper hmap)
		{
			BackgroundWorker worker = Generator.Worker;
			Map.Tile tile;
			for (int x = 0; x <= Generator.BOUNDARY; x++)
			{
				worker.ReportProgress(CalcPercent(x, Generator.BOUNDARY));
				for (int y = 0 ; y <= Generator.BOUNDARY; y++)
				{
					tile = hmap.GetTile(x, y);
					if (tile != null)
					{
						// Simple logic: if there is no tile from specified direction, place wall
						bool tileLeft = (hmap.GetTile(x - 2, y) == null);
						bool tileRight = (hmap.GetTile(x + 2, y) == null);
						bool tileUp = (hmap.GetTile(x, y - 2) == null);
						bool tileDown = (hmap.GetTile(x, y + 2) == null);
						
						if (tileLeft) hmap.PlaceWall(x - 1, y + 1);
						if (tileRight) hmap.PlaceWall(x + 1, y + 1);
						if (tileUp) hmap.PlaceWall(x, y);
						if (tileDown) hmap.PlaceWall(x, y + 2);
						
						// There are some few special cases however that need attention
						if (!tileLeft && hmap.GetTile(x - 1, y + 1) == null && hmap.GetTile(x - 1, y - 1) == null)
							hmap.PlaceWall(x - 1, y + 1);
						if (!tileDown && hmap.GetTile(x - 1, y + 1) == null && hmap.GetTile(x + 1, y + 1) == null)
							hmap.PlaceWall(x, y + 2);
					}
				}
			}
		}
		
		public static void ClearMap(Map map)
		{
			// Only walls and tiles are being generated
			map.Tiles.Clear();
			map.Walls.Clear();
			// And (optional) objects
			if (Generator.GenConfig.PopulateMap) map.Objects.Clear();
		}
		
		public static float DistanceSq(float x1, float y1, float x2, float y2)
		{
			float dx = x1 - x2;
   			float dy = y1 - y2;
			return dx * dx + dy * dy;
		}
		
		public static float Distance(float x1, float y1, float x2, float y2)
		{
			return (float) Math.Sqrt(DistanceSq(x1, y1, x2, y2));
		}
	}
}
