/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 15.02.2015
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using NoxShared;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Description of FloodFill.
	/// </summary>
	public class FloodFill
	{
		private MapHelper hmap;
		private HashSet<Point> tilesScanned;
		
		public FloodFill(MapHelper hmap)
		{
			this.hmap = hmap;
			tilesScanned = new HashSet<Point>();
		}
		
		public HashSet<Point> GetResult()
		{
			return tilesScanned;
		}
		
		public void DeleteResultTiles()
		{
			foreach (Point pt in tilesScanned)
			{
				hmap.RemoveTile(pt.X, pt.Y);
			}
		}
		
		public void PerformFloodFill(int x, int y)
		{
			Queue<Point> toscan = new Queue<Point>();
			Map.Tile leftUp = null;
			Map.Tile downRight = null;
			Point pt, pt2;
			toscan.Enqueue(new Point(x, y));
			
			while (toscan.Count > 0)
			{
				Point deq = toscan.Dequeue();
				if (tilesScanned.Contains(deq)) continue;
				
				tilesScanned.Add(deq);
				
				pt = deq;
				while (true)
				{
					pt.Offset(-1, -1);
					leftUp = hmap.GetTile(pt.X, pt.Y);
					
					if (leftUp != null)
					{
						pt2 = new Point(pt.X + 1, pt.Y - 1);
						if (hmap.GetTile(pt2.X, pt2.Y) != null) toscan.Enqueue(pt2);
						pt2 = new Point(pt.X - 1, pt.Y + 1);
						if (hmap.GetTile(pt2.X, pt2.Y) != null) toscan.Enqueue(pt2);
					}
					else break;
				}
				
				pt = deq;
				while (true)
				{
					pt.Offset(1, 1);
					downRight = hmap.GetTile(pt.X, pt.Y);
					
					if (downRight != null) 
					{
						pt2 = new Point(pt.X + 1, pt.Y - 1);
						if (hmap.GetTile(pt2.X, pt2.Y) != null) toscan.Enqueue(pt2);
						pt2 = new Point(pt.X - 1, pt.Y + 1);
						if (hmap.GetTile(pt2.X, pt2.Y) != null) toscan.Enqueue(pt2);
					}
					else break;
				}
			}
		}
	}
}
