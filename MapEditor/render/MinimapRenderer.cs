/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 15.11.2014
 */
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using NoxShared;
using System.Collections.Generic;
using System.Windows.Forms;
namespace MapEditor.render
{
    /// <summary>
    /// Класс, обеспечивающий быструю отрисовку миникарты (стен и плиток пола)
    /// </summary>
    public class MinimapRenderer : BitmapShader
    {
        private readonly Map map;

        private Dictionary<Point, Map.Wall> FakeWalls = new Dictionary<Point, Map.Wall>();
        public MinimapRenderer(Bitmap bitmap, Map theMap, Dictionary<Point, Map.Wall> fakewalls)
            : base(bitmap)
        {
            map = theMap;
            FakeWalls.Clear();
            FakeWalls = fakewalls;
            

            /*
            foreach (var wall in fakewalls)
            {
                FakeWalls.Add(wall.Key, wall.Value);
            }
             * */
        }
        
        
        private void makefake()
        {
            FakeWalls.Clear();

            foreach (var wall in FakeWalls)
            {
                FakeWalls.Add(wall.Key, wall.Value);
            }
            
        }
        
        public void DrawMinimap(int minimapZoom, Rectangle clip)
        {
           
            if (locked)
            { 
                bool deleting = MainWindow.Instance.RightDown;
                byte[] bitarray = new byte[bitData.Stride * bitData.Height];
                Marshal.Copy(bitData.Scan0, bitarray, 0, bitarray.Length);
                int pxI, x, y; Map.Tile tile; int tilePlotZoom = minimapZoom + minimapZoom;

                if (deleting)
                {
                    for (int startX = 0; startX < clip.X + clip.Width; startX++)
                    {
                        for (int startY = 0; startY < clip.Y + clip.Height; startY++)
                        {
                            pxI = ((startY * bitData.Width) + startX) * 4;
                            if (pxI + 3 < bitarray.Length)
                            {
                                bitarray[pxI] = 0x00;
                                bitarray[pxI + 1] = 0x00;
                                bitarray[pxI + 2] = 0x00;
                                bitarray[pxI + 3] = 0xFF;
                            }
                        }
                    }
                }
                

                // draw tiles
                foreach (Point loc in map.Tiles.Keys)
                {

                    x = loc.X * minimapZoom;
                    y = loc.Y * minimapZoom;
                    if (!clip.Contains(x, y)) continue;
                    if (x < 0 || y < 0) return;
                    tile = map.Tiles[loc];


                    for (int rx = x; rx < x + tilePlotZoom; rx++)
                    {
                        for (int ry = y; ry < y + tilePlotZoom; ry++)
                        {
                            pxI = ((ry * bitData.Width) + rx) * 4;
                            if (pxI + 3 < bitarray.Length)
                            {
                                bitarray[pxI] = tile.col.B;
                                bitarray[pxI + 1] = tile.col.G;
                                bitarray[pxI + 2] = tile.col.R;
                                bitarray[pxI + 3] = 0xFF;
                            }

                        }
                    }
                }
                

               // draw walls
                foreach (Point loc in map.Walls.Keys)
                {
                    x = loc.X * minimapZoom;
                    y = loc.Y * minimapZoom;
                    //if (!clip.Contains(x, y)) continue;
                    for (int rx = x; rx < x + minimapZoom; rx++)
                    {
                        for (int ry = y; ry < y + minimapZoom; ry++)
                        {
                            pxI = ((ry * bitData.Width) + rx) * 4;
                            if (pxI + 3 < bitarray.Length)
                            {
                                bitarray[pxI] = 0xFF;
                                bitarray[pxI + 1] = 0xFF;
                                bitarray[pxI + 2] = 0xFF;
                                bitarray[pxI + 3] = 0xFF;
                            }
                        }
                    }
                }

              
                foreach (Point wall in FakeWalls.Keys)
                {
                    x = wall.X * minimapZoom;
                    y = wall.Y * minimapZoom;

                    for (int rx = x; rx < x + minimapZoom; rx++)
                    {
                        for (int ry = y; ry < y + minimapZoom; ry++)
                        {
                            pxI = ((ry * bitData.Width) + rx) * 4;
                            if (pxI + 3 < bitarray.Length)
                            {
                                bitarray[pxI] = 0xFF;
                                bitarray[pxI + 1] = 0xFF;
                                bitarray[pxI + 2] = 0xFF;
                                bitarray[pxI + 3] = 0x70;
                            }
                            
                        }
                    }
                }
                
                
                // copy BitmapData
                Marshal.Copy(bitarray, 0, bitData.Scan0, bitarray.Length);

            }
        }
    }
}