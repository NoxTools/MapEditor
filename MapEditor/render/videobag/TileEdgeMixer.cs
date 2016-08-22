/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - PUBLIC DOMAIN
 * Дата: 12.01.2015
 */
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MapEditor.videobag
{
	/// <summary>
	/// Performs tile masking based on edge pattern from video bag
	/// </summary>
	public class TileEdgeMixer
	{
		static PixelFormat UsePixelFormat = PixelFormat.Format32bppArgb;
		static int TransparentColor = Color.Transparent.ToArgb();
		
		private readonly int[] videoColorPalette;
		
		private readonly Bitmap resultBitmap;
		private BitmapData parentBD;
		private readonly int[] coverTileColorArray;
		
		/*** ПЕРЕМЕННЫЕ УПРАВЛЯЮЩИЕ ЧТЕНИЕМ СЕКЦИИ ***/
		private int videoReadOffset;
		private int videoPatternLength;
		private byte[] videoEdgePattern;
		
		/// <param name="baseBit">The base bitmap to draw edge on</param>
		/// <param name="coverdata">Cover tile - raw colors</param>
		/// <param name="pattern">Section data including Edge pattern</param>
		/// <param name="palette">Video.bag 256c palette</param>
		/// <param name="offset">Edge pattern offset</param>
		/// <param name="patternLength">Edge pattern length</param>
		public TileEdgeMixer(Bitmap baseBit, int[] coverdata, byte[] pattern, int[] palette, int offset, int patternLength)
		{
			resultBitmap = baseBit;//new Bitmap(46, 46, PixelFormat.Format32bppArgb);
			coverTileColorArray = coverdata;
			videoEdgePattern = pattern;
			videoColorPalette = palette;
			videoReadOffset = offset;
			videoPatternLength = patternLength;
		}
		
		private void Lock()
		{
			parentBD = resultBitmap.LockBits(new Rectangle(Point.Empty, resultBitmap.Size), ImageLockMode.WriteOnly, UsePixelFormat);
		}
		
		private void Unlock()
		{
			resultBitmap.UnlockBits(parentBD);
		}
		
		/// <summary>
		/// Lazy stream implementation. Faster than BinaryReader whatsoever
		/// </summary>
		private byte NextByte()
		{
			videoReadOffset++;
			return videoEdgePattern[videoReadOffset - 1];
		}
		
		public unsafe Bitmap Apply()
		{
			videoPatternLength += videoReadOffset;
			Lock();
			byte* outptr = (byte*) parentBD.Scan0;
			
			// ** GENERATE IMAGE **
			byte startX = NextByte();
			byte endX = NextByte();
			int[] buffer = new int[256];
			byte buflen = 0;
			byte bufpos = 0;
			int c = 23, ci = 1;
			
			for (int row = 0; row <= endX; row++)
            {
				if (row >= startX)
				{
					for (int col = 0; col < ci; col++)
					{
						// fill buffer if empty
						if (bufpos >= buflen)
						{
							bufpos = 0;
							switch (NextByte())
							{
								case 4:
								case 1: // Skip pixels
									buflen = NextByte();
									for (int i = 0; i < buflen; i++) buffer[i] = 1;
									break;
								case 2: // Fixed colors
									buflen = NextByte();
									for (int i = 0; i < buflen; i++) buffer[i] = videoColorPalette[NextByte()];
									break;
								case 3: // Copy from source
									buflen = NextByte();
									for (int i = 0; i < buflen; i++) buffer[i] = 2;
									break;
							}
						}
						
						// poke color
						int pxndx = (row * 46 + (col + c));
						if (buffer[bufpos] != 1)
						{
							if (buffer[bufpos] == 2) // cover tile pixel
								buffer[bufpos] = coverTileColorArray[pxndx];
	
							*(int*)((outptr + pxndx * 4)) = buffer[bufpos];
							*(outptr + pxndx * 4 + 3) = 255; // fully opaque
						}
						bufpos++;
					}
            	}
				if (row < 22)
            	{
            		ci += 2;
            		c--;
            	}
            	else if (row > 22)
            	{
            		ci -= 2;
            		c++;
            	}
			}
			
			Unlock();
			return resultBitmap;
		}
	}
}
