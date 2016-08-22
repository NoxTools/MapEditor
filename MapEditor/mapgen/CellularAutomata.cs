using System;

namespace MapEditor.mapgen
{
	/// <summary>
	/// Cellular Automata - used by map generator
	/// </summary>
	public class CellularAutomata
	{
		private bool[,] cellMap;
		private Random random;
		public float StartAliveChance = 0.5f;
		public int BirthCellLimit = 5;
		public int DeathCellLimit = 5;
		public bool CountEdgesAsLiving = false;
		private int size;
		
		public CellularAutomata(Random random, int size)
		{
			this.random = random;
			this.size = size;
		}
		
		public void SetupCA(int BCL, int DCL, float SAC)
		{
			BirthCellLimit = BCL;
			DeathCellLimit = DCL;
			StartAliveChance = SAC;
			BuildRandomMap();
		}
		
		/// <summary>
		/// Build new, completely random cellmap depending on SAC
		/// </summary>
		public void BuildRandomMap()
		{
			cellMap = new bool[size, size];
			for (int x = 0; x < size; x++)
			{
				for (int y = 0; y < size; y++)
				{
					if (random.NextDouble() < StartAliveChance)
						cellMap[x, y] = true;
				}
			}
		}
		
		private int CountAliveNeighbours(int x, int y)
		{
			int count = 0;
			int nX, nY;
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					nX = x + i;
					nY = y + j;
					
					if (i == 0 && j == 0) { }
					else if (nX < 0 || nY < 0 || nX >= size || nY >= size)
					{
						if (CountEdgesAsLiving) count++;
					}
					else if (cellMap[nX, nY])
					{
						count++;
					}
				}
			}
			return count;
		}
		
		/// <summary>
		/// Perform simulation tick, update cellmap
		/// </summary>
		public void DoSimulationTick()
		{
			bool[,] tempCellMap = new bool[size, size];
			Array.Copy(cellMap, tempCellMap, tempCellMap.Length);
			int neighbours;
			for (int x = 0; x < size; x++)
			{
				for (int y = 0; y < size; y++)
				{
					neighbours = CountAliveNeighbours(x, y);
					
					if (cellMap[x, y])
					{
						if (neighbours < DeathCellLimit)
						{
							tempCellMap[x, y] = false;
						}
						else
						{
							tempCellMap[x, y] = true;
						}
					}
					else
					{
						if (neighbours > BirthCellLimit)
						{
							tempCellMap[x, y] = true;
						}
						else
						{
							tempCellMap[x, y] = false;
						}
					}
				}
			}
			cellMap = tempCellMap;
		}
		
		public void SetCellMap(bool[,] map)
		{
			cellMap = map;
		}
		
		public bool[,] GetCellMap()
		{
			return cellMap;
		}
	}
}
