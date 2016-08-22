/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - PUBLIC DOMAIN
 * Дата: 09.10.2014
 */
using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NoxShared;

namespace MapEditor.videobag
{
	/// <summary>
	/// Class providing dynamic access to Nox graphical data
	/// </summary>
	public class VideoBagStream
	{
		public int Length;
		public int EntryCount;
        
        private const string VIDEO_BAG = "video8.bag";
        private const string VIDEO_IDX = "video8.idx";
        private const string VIDEO_PAL = "default.pal";
        private const int BUFFER_SIZE = 65535;
        
        /// <summary>
        /// All sections read from .idx file
        /// </summary>
        public Section[] Sections;
        
        /// <summary>
        /// Section cache (really quickens things)
        /// </summary>
        protected UncompressedSectionCache sectionCache;
        
        /// <summary>
        /// Base file stream (read only)
        /// </summary>
        protected BinaryReader videoBagStream;
        
        /// <summary>
        /// All entries from all sections (used for fast indexing)
        /// </summary>
        protected FileEntry[] Entries;
        
        /// <summary>
        /// 8-bit palette contents
        /// </summary>
        protected int[] Palette8Bit = new int[256];
        
        /// <summary>
        /// Used by type 4, 6 images
        /// </summary>
        public int[] Type46Colors = new int[6];
        
        public class Section
        {
        	public uint VideoBagOffset; // смещениe в сжатом виде
        	public int Index;
        	public uint SizeCompressed;
        	public uint SizeUncompressed;
        	public FileEntry[] Subentries;
        	
        	public Section(BinaryReader br)
        	{
        		int length = br.ReadInt32();
        		VideoBagOffset = 0;
        		SizeUncompressed = br.ReadUInt32();
        		SizeCompressed = br.ReadUInt32();
        		int entries = br.ReadInt32();
        		if (entries == -1) entries = 1;
        		Subentries = new FileEntry[entries];
        		
        		long debug = br.BaseStream.Position + length;
        		
        		uint entryBagOffset = 0; // для быстрого поиска
        		for (int i = 0; i < entries; i++)
        		{
        			FileEntry fe = new FileEntry(br);
        			fe.SectionOffset = entryBagOffset;
        			entryBagOffset += fe.SizeUncompressed;
        			fe.BaseSection = this;
        			Subentries[i] = fe;
        		}
        		
        		Debug.Assert(debug == br.BaseStream.Position, "Section read error");
        	}
        }
        
        /// <summary>
        /// Separate entry descriptor for image in video.bag
        /// </summary>
        public struct FileEntry
        {
        	public string Filename;
        	public byte EntryType;
        	public uint SizeUncompressed;
        	private int unknown;
        	public uint SectionOffset;
        	public Section BaseSection; // для индексации
        	
        	public FileEntry(BinaryReader br)
        	{
        		SectionOffset = 0;
        		BaseSection = null;
        		// это все устанавливается при чтении
        		Filename = new string(br.ReadChars(br.ReadByte()));
                Filename.TrimEnd('\0');
                EntryType = br.ReadByte();
                SizeUncompressed = br.ReadUInt32();
                unknown = br.ReadInt32();
        	}
        }

		public VideoBagStream()
		{
			FileStream fs = new FileStream(NoxDb.NoxPath + VIDEO_BAG, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, BUFFER_SIZE);
			videoBagStream = new BinaryReader(fs);
			ReadIDXEntries();
			ReadPalette();
			sectionCache = new UncompressedSectionCache(this);
		}
		
		/// <summary>
		/// Preserves some hard disk input
		/// </summary>
		protected class UncompressedSectionCache
        {
			public byte[][] CachedSections;
			public uint ActiveCachedSections;
			private VideoBagStream videoBagStream;
			
			private const uint CACHED_SECTIONS_LIMIT = 16;
			
			public UncompressedSectionCache(VideoBagStream stream)
			{
				videoBagStream = stream;
				ClearCache();
			}
			
			private void ClearCache()
			{
				CachedSections = new byte[videoBagStream.Sections.Length][];
				// GC should finalize stuff on its own
				ActiveCachedSections = 0;
			}
			
			public byte[] LookupCache(Section section)
			{
				byte[] result = null;
				result = CachedSections[section.Index];
				return result;
			}
			
			public void AddToCache(Section section, byte[] data)
			{
				if (ActiveCachedSections > CACHED_SECTIONS_LIMIT) ClearCache();
				CachedSections[section.Index] = data;
				ActiveCachedSections++;
			}
        }
		
		/// <summary>
		/// Reads all indexer entries, called automatically
		/// </summary>
		private void ReadIDXEntries()
		{
			BinaryReader videoIdxStream = new BinaryReader(File.OpenRead(NoxDb.NoxPath + VIDEO_IDX));
			if (videoIdxStream.ReadUInt32() == 0xFAEDBCEB)
			{
				Length = videoIdxStream.ReadInt32();
				EntryCount = videoIdxStream.ReadInt32();
				videoIdxStream.BaseStream.Seek(12, SeekOrigin.Current);
				Sections = new Section[EntryCount];
				uint compressedOffset = 0;
				for (int i = 0; i < EntryCount; i++)
				{
					Section section = new Section(videoIdxStream);
					section.VideoBagOffset = compressedOffset;
					compressedOffset += section.SizeCompressed;
					section.Index = i;
					Sections[i] = section;
				}
			}
			videoIdxStream.Close();
		}
		
		/// <summary>
		/// Reads 256-bit palette used for decoding images.
		/// </summary>
		private void ReadPalette()
		{
			BinaryReader br = new BinaryReader(File.OpenRead(NoxDb.NoxPath + VIDEO_PAL));
            br.BaseStream.Seek(7, SeekOrigin.Begin);
            byte[] rgb = new byte[4];
            for (int i = 0; i < 256; i++)
            {
            	rgb[3] = 0xFF;
            	rgb[2] = br.ReadByte();
            	rgb[1] = br.ReadByte();
            	rgb[0] = br.ReadByte();
            	Palette8Bit[i] = BitConverter.ToInt32(rgb, 0);
            }
            br.Close();
		}
		
		public void Close()
		{
			videoBagStream.Close();
		}
		
		public FileEntry[] GetAllEntries()
		{
			if (Entries == null)
			{
				List<FileEntry> entries = new List<FileEntry>();
				foreach (Section s in Sections)
				entries.AddRange(s.Subentries);
				Entries = entries.ToArray();
			}
			return Entries;
		}
		
		/// <summary>
		/// Returns ready-to-go Bitmap and offset values for given Entry
		/// </summary>
		public unsafe Bitmap GetBitmap(FileEntry fe, out int offsX, out int offsY)
		{
			int[] bitmap = null;
			PixelFormat format = PixelFormat.Format32bppArgb;
			Bitmap result = null;
			Section sect = fe.BaseSection;
			// look up uncompressed data in cache
			byte[] sectionUncompressedData = sectionCache.LookupCache(sect);
			if (sectionUncompressedData == null)
			{
				videoBagStream.BaseStream.Seek(sect.VideoBagOffset, SeekOrigin.Begin);
				byte[] sectionCompressedData = videoBagStream.ReadBytes((int) sect.SizeCompressed);
				sectionUncompressedData = new byte[sect.SizeUncompressed];
				NoxLzCompression.Decompress(sectionCompressedData, sectionUncompressedData);
				// add data in cache
				sectionCache.AddToCache(sect, sectionUncompressedData);
			}
			
			int width = 0, height = 0; uint moff;
			offsX = 0; offsY = 0;
			
			fixed (byte* imgSectionPtr = sectionUncompressedData)
			{
				switch (fe.EntryType)
				{
					case 0:
						// Tiles
						width = 46; height = 46;
						bitmap = ReadType0Image(fe, imgSectionPtr);
						break;
					case 1:
						// // Tile edges
						width = 46; height = 46;
						throw new InvalidOperationException("Use ApplyEdgeMask() instead");
					case 3:
						// kinda PCX
						moff = fe.SectionOffset;
						width = *((int*) (imgSectionPtr + moff)); moff += 4;
						height = *((int*) (imgSectionPtr + moff)); moff += 4;
						offsX = *((int*) (imgSectionPtr + moff)); moff += 4;
						offsY = *((int*) (imgSectionPtr + moff));
						bitmap = ReadType3Image(fe, imgSectionPtr);
						break;					
	  				case 4:
					case 5:
					case 6:
						// Same as 3 but with dynamic colors
						moff = fe.SectionOffset;
						width = *((int*) (imgSectionPtr + moff)); moff += 4;
						height = *((int*) (imgSectionPtr + moff)); moff += 4;
						offsX = *((int*) (imgSectionPtr + moff)); moff += 4;
						offsY = *((int*) (imgSectionPtr + moff));
						bitmap = ReadType456Image(fe, imgSectionPtr);
						break;
				}
			}
			
			if (width > 0 && height > 0)
			{
				result = new Bitmap(width, height, format);
				int stride = 4 * width;
				// copy bitmap
				BitmapData bd = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, format);
				Marshal.Copy(bitmap, 0, bd.Scan0, bitmap.Length);
				result.UnlockBits(bd);
			}
			
			return result;
		}
		
		private int[] CreateBitArray(int width, int height)
		{
			int[] result = new int[width * height];
			// Alpha channel is already set to zero
			return result;
		}
		
		/// <summary>
		/// Applies edge mask (edgeEntry) on parentTile, result is stored in bit
		/// </summary>
		public void ApplyEdgeMask(Bitmap bit, int edgeID, int coverID)
		{
			FileEntry edgeEntry = Entries[edgeID]; FileEntry coverTile = Entries[coverID];
			// prepare cover
           
			Section sect = coverTile.BaseSection;
			byte[] sectionUncompressedData = sectionCache.LookupCache(sect);
			if (sectionUncompressedData == null)
			{
				videoBagStream.BaseStream.Seek(sect.VideoBagOffset, SeekOrigin.Begin);
				byte[] sectionCompressedData = videoBagStream.ReadBytes((int) sect.SizeCompressed);
				sectionUncompressedData = new byte[sect.SizeUncompressed];
				NoxLzCompression.Decompress(sectionCompressedData, sectionUncompressedData);
				// add data in cache
				sectionCache.AddToCache(sect, sectionUncompressedData);
			}
			int[] coverColorData = ReadType0Image(coverTile, sectionUncompressedData);
			// prepare edge
			sect = edgeEntry.BaseSection;
			sectionUncompressedData = sectionCache.LookupCache(sect);
			if (sectionUncompressedData == null)
			{
				videoBagStream.BaseStream.Seek(sect.VideoBagOffset, SeekOrigin.Begin);
				byte[] sectionCompressedData = videoBagStream.ReadBytes((int) sect.SizeCompressed);
				sectionUncompressedData = new byte[sect.SizeUncompressed];
				NoxLzCompression.Decompress(sectionCompressedData, sectionUncompressedData);
				// add data in cache
				sectionCache.AddToCache(sect, sectionUncompressedData);
			}
			// build up
			TileEdgeMixer edge = new TileEdgeMixer(bit, coverColorData, sectionUncompressedData, Palette8Bit, (int)edgeEntry.SectionOffset, (int)edgeEntry.SizeUncompressed);
			edge.Apply();
		}
		
		private unsafe int[] ReadType0Image(FileEntry fe, byte* data)
		{
			int[] bitmap = CreateBitArray(46, 46);
            int i = 1; int c = 23; int index = 0; int offs;
            
            for (int row = 0; row < 46; row++)
            {
            	// Boundary check
            	if ((data + fe.SectionOffset + i) > (data + fe.SectionOffset + fe.SizeUncompressed)) return null;
            	
            	for (int col = 0; col < i; col++)
            	{
            		offs = (int) fe.SectionOffset + index; index++;
            		bitmap[(col + c) + row * 46] = Palette8Bit[*(offs + data)];
            	}
            	if (row < 22)
            	{
            		i += 2;
            		c--;
            	}
            	else if (row > 22)
            	{
            		i -= 2;
            		c++;
            	}
            }
			return bitmap;
		}
		
		private unsafe int[] ReadType0Image(FileEntry fe, byte[] data)
		{
			fixed (byte* ptrData = data)
			{
				return ReadType0Image(fe, ptrData);
			}
		}

		private unsafe int[] ReadType0Unordered(FileEntry fe, byte[] data)
		{
			int[] bitmap = CreateBitArray(46, 46);
			uint dataOffset = fe.SectionOffset;
			uint dataLen = fe.SizeUncompressed;
			
			fixed (int* ptrBitmap = bitmap)
			{
				fixed (byte* ptrData = data)
				{
					while (dataLen > 0)
					{
						dataLen--;
						*(ptrBitmap + dataLen) = Palette8Bit[*(ptrData + dataOffset + dataLen)];
					}
				}
			}
			return bitmap;
		}
		
		private unsafe int[] ReadType3Image(FileEntry fe, byte* data)
		{
			byte op = 3; uint index = 0, pixLength = 0, pos = fe.SectionOffset, end = 0;
			int[] bitmap = CreateBitArray(*((int*) (data + pos)), *((int*) (data + pos + 4)));
			pos += 16; // Width, Height, OffsetX, OffsetY
			long length = fe.SectionOffset + fe.SizeUncompressed;
			
			while (op != 0 && pos < length)
			{
				op = *(data + pos); pos++;
				// RLE codes
				switch (op)
				{
					case 0:
						// End
						break;
					case 1:
						// Skip x pixels
						index += *(data + pos); pos++;
						break;
					case 2:
						// Read x pixels
						{
							pixLength = *(data + pos); pos++;
							end = pos + pixLength;
							while (pos < end)
							{
								bitmap[index] = Palette8Bit[*(data + pos)];
								pos++;
								index++;
							}
							break;
						}
					case 3:
						// Begin
						break;
					default:
						{
							//throw new NotImplementedException();
							return bitmap;
						}
				}
			}
			return bitmap;
		}
		
		private unsafe int[] ReadType456Image(FileEntry fe, byte* data)
		{
			byte op = 3; uint index = 0, pixLength = 0, pos = fe.SectionOffset, end = 0;
			int width = *((int*) (data + pos));
			int height = *((int*) (data + pos + 4));
			
			int[] bitmap = CreateBitArray(width, height);
			pos += 17; // Width, Height, OffsetX, OffsetY, Unknown
			long length = fe.SectionOffset + fe.SizeUncompressed;
			
			while (op != 0 && pos < length)
			{
				op = *(data + pos); pos++;
				// RLE codes
				switch (op)
				{
					case 0:
						// End
						break;
					case 1:
						// Skip x pixels
						index += *(data + pos); pos++;
						break;
					case 2:
						// Read x pixels
						{
							pixLength = *(data + pos); pos++;
							end = pos + pixLength;
							while (pos < end)
							{
								bitmap[index] = Palette8Bit[*(data + pos)];
								pos++;
								index++;
							}
							break;
						}
					case 3:
						// Begin
						break;
					case 5:
						// Half transparent (Type5)
						{
							pixLength = *(data + pos); pos++;
							
							while (pixLength > 0)
							{
								ushort col = (ushort)(*(data + pos) | (*(data + pos + 1) << 8));
								
								byte a = (byte) (((col >> 12) & 0xF) << 4);
								byte r = (byte) (((col >> 8) & 0xF) << 4);
								byte g = (byte) (((col >> 4) & 0xF) << 4);
								byte b = (byte) ((col & 0xF) << 4);
								
								bitmap[index] =	a << 24 | r << 16 | g << 8 | b;
								
								pos += 2;
								pixLength--;
								index++;
							}
						}
						break;
					default:
						// Dynamic colors (Type46)
						if ((op & 4) >= 4)
						{
							pixLength = *(data + pos); pos++;
							end = pos + pixLength;
							byte r, g, b;
							while (pos < end)
							{
								float intensity = *(data + pos) / 32F;
								
								// 5(last) color is never used, actually
								int ind = ((op >> 2) - 1) / 5;
								int color = Type46Colors[ind];

								r = (byte) (((color >> 16) & 255) * intensity);
								g = (byte) (((color >> 8) & 255) * intensity);
								b = (byte) ((color & 255) * intensity);
				
								bitmap[index] = (int) ((uint)0xFF000000 | (uint)r << 16 | (uint)g << 8 | (uint)b);
								pos++;
								index++;
							}
						}
						break;
				}
			}
			return bitmap;
		}
	}
}
