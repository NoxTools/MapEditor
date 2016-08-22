/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 13.02.2015
 */
using System;
using System.Drawing;
using System.Collections.Generic;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Description of DungeonGenerator.
	/// </summary>
	public class DungeonGenerator : IGenerator
	{
		const string FLOOR1 = "CobbleDirt";
		const string WALL1 = "CathedralBlue";
		
		// assume this rectangle is expanding downwards
		public Rectangle WorkArea = new Rectangle(5, 5, 245, 245);
		private Point currentPos;
		private Point lastGeneratedPos;
		private Random rng;
		private MapHelper hmap;
		
		private bool[,] Array2D;
		
		private bool PlaceRandomRoom()
		{
			Point size = new Point(rng.Next(-10, 10), rng.Next(-10, 10));
			if (currentPos.X <= lastGeneratedPos.X && currentPos.Y >= lastGeneratedPos.Y)
			{
				// adjust coordinates
				currentPos.X += (currentPos.X - lastGeneratedPos.X);
				currentPos.Y += (lastGeneratedPos.X - currentPos.Y);
			}
			// fail if area is already occupied
			if (hmap.CheckTilesExistIsom(currentPos, size)) return false;
			hmap.FillTilesIsom(currentPos, size);
			// Find unused edge point
			List<Point> edges = hmap.FindUnusedTilesIsom(new Point(currentPos.X - 2, currentPos.Y), new Point(size.X + 2, size.Y + 2));
			if (edges.Count <= 0) 
				return false; // FIXME remove placed tiles
			lastGeneratedPos = currentPos;
			currentPos = edges[rng.Next(0, edges.Count - 1)];
			return true;
		}
		
		public override void Generate(MapHelper hmap, System.ComponentModel.BackgroundWorker worker)
		{
			this.hmap = hmap;
			rng = Generator.GenRandom;
			Array2D = new bool[WorkArea.Width - WorkArea.X, WorkArea.Height - WorkArea.Y];
			
			// Single floor type for now
			hmap.SetTileMaterial(FLOOR1);
			
		}
	}
}
