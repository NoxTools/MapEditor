/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - PUBLIC DOMAIN
 * Дата: 09.10.2014
 */
using System;
using System.Drawing;
using System.Collections.Generic;

namespace MapEditor.videobag
{
	/// <summary>
	/// Layer2 cache for prepared Bitmap objects
	/// </summary>
	public class VideoBagCachedProvider
	{
		readonly VideoBagStream videoBag;
		
		/// <summary>
		/// Array of prepared Bitmaps
		/// </summary>
		Bitmap[] cachedBitmapArray;
		
		/// <summary>
		/// Array of stored offsets
		/// </summary>
		public int[][] DrawOffsets;
		
		/// <summary>
		/// Currently cached bitmaps counter
		/// </summary>
		uint activeCachedBitmaps = 0;
		
		/// <summary>
		/// Limit of max cached bitmaps
		/// </summary>
		const uint CACHED_BITMAPS_MAX_ACTIVE = 3200;
		
		/// <summary>
		/// List of cached dynamic-color Bitmaps
		/// </summary>
		List<CachedDynamicBitmap> cachedDynamicBitmaps = new List<CachedDynamicBitmap>();
		
		/// <summary>
		/// Placeholder for dynamic-color requests
		/// </summary>
		static int[] PLACEHOLDER_COLORS = { 0x00FF0000, 0x00FF0000, 0x00FF0000, 0x00FF0000, 0x00FF0000, 0x00FF0000 };
		
		private struct CachedDynamicBitmap : IEquatable<CachedDynamicBitmap>
		{
			public readonly int index;
			public Bitmap result;
			public int[] colors;
			
			public CachedDynamicBitmap(int index, Bitmap result, int[] colors)
			{
				this.index = index;
				this.result = result;
				this.colors = colors;
			}
			
			public bool Equals(CachedDynamicBitmap other)
			{
				if (other.index != index) return false;
				
				for (int i = 0; i < 6; i++)
				{
					if (other.colors[i] != colors[i])
						return false;
				}
				
				return true;
			}
		}
		
		public VideoBagCachedProvider()
		{
			videoBag = new VideoBagStream();
			cachedBitmapArray = new Bitmap[videoBag.GetAllEntries().Length];
			DrawOffsets = new int[cachedBitmapArray.Length][];
		}
		
		private void ClearCachedLists()
		{
			activeCachedBitmaps = 0;
			// Clear dynamic-color bitmaps list
			cachedDynamicBitmaps.Clear();
			// Remove pointers to normal bitmaps
			for (int i = 0; i < cachedBitmapArray.Length; i++)
			{
				//cachedBitmapArray[i].Dispose();
				// GC should finalize everything on its own
				cachedBitmapArray[i] = null;
			}
			GC.Collect(1);
		}
		
		private Bitmap CacheBitmap(int index)
		{
			// Check for overflow
			if (activeCachedBitmaps > CACHED_BITMAPS_MAX_ACTIVE) ClearCachedLists();
			activeCachedBitmaps++;
			
			VideoBagStream.FileEntry[] entries = videoBag.GetAllEntries();
			// Read bitmap from stream
			int ox, oy;
			Bitmap bit = videoBag.GetBitmap(entries[index], out ox, out oy);
			
			// Store offset and bitmap pointer
			DrawOffsets[index] = new int[] { ox, oy };
			cachedBitmapArray[index] = bit;
			
			return bit;
		}
		
		/// <summary>
		/// Retrieves a normal Bitmap from videobag by its index, using cached approach.
		/// </summary>
		public Bitmap GetBitmap(int index)
		{
			Bitmap result = cachedBitmapArray[index];
			if (result == null)
			{
				videoBag.Type46Colors = PLACEHOLDER_COLORS;
				result = CacheBitmap(index);
			}

			return result;
		}
		
		/// <summary>
		/// Retrieves a dynamic-color Bitmap from videobag by its index, using cached approach.
		/// </summary>
		public Bitmap GetBitmapDynamic(int index, int[] cols)
		{
			CachedDynamicBitmap newc = new CachedDynamicBitmap(index, null, cols);
			
			// Check if same bitmap is already present
			foreach (CachedDynamicBitmap bit in cachedDynamicBitmaps)
			{
				if (bit.Equals(newc)) return bit.result;
			}
			
			// Else add new entry
			videoBag.Type46Colors = cols;
			newc.result = CacheBitmap(index);
			cachedDynamicBitmaps.Add(newc);
			
			return newc.result;
		}
		
		/// <summary>
		/// Builds an tile+edge pair on specified Bitmap.
		/// </summary>
		public void ApplyEdgeMask(Bitmap bit, int edgeEntryID, int coverEntryID)
		{
			videoBag.ApplyEdgeMask(bit, edgeEntryID, coverEntryID);
		}
	}
}
