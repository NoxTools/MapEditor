/*
 * MapEditor
 * 
 * 
 * Пользователь: AngryKirC
 * Copyleft - PUBLIC DOMAIN
 * Дата: 09.10.2014
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows;
using System.Collections.Generic;
using NoxShared;
using MapEditor.videobag;
using MapEditor.MapInt;
using WallFacing = NoxShared.Map.Wall.WallFacing;
using MapEditor.XferGui;
namespace MapEditor.render
{
    /// <summary>
    /// Class responsible for drawing the Nox map.
    /// </summary>
    public class MapViewRenderer
    {
        const int squareSize = MapView.squareSize;
        private readonly MapView mapView;
        internal newgui.MapObjectCollection SelectedObjects
        {
            get
            {
                return mapView.SelectedObjects;
            }
        }
        internal Map Map
        {
            get
            {
                return MapInterface.TheMap;
            }
        }

        private readonly Font drawFont;
        private VideoBagCachedProvider videoBagProvider = null;
        public VideoBagCachedProvider VideoBag
        {
            get
            {
                if (videoBagProvider == null)
                    videoBagProvider = new VideoBagCachedProvider();
                return videoBagProvider;
            }
        }
        private readonly ObjectRenderer objectRenderer;
        private readonly TileRenderer floorRenderer;
        public Dictionary<Point, Map.Wall> FakeWalls = new Dictionary<Point, Map.Wall>();
        public bool proDefault = false;
        public bool proHand = false;

        public MapViewRenderer(MapView mapView)
        {
            this.mapView = mapView;
            this.drawFont = new Font("Arial", 9.4F);
            this.objectRenderer = new ObjectRenderer(this);
            this.floorRenderer = new TileRenderer(this);
        }

        public ColorLay ColorLayout = new ColorLay();
        public class ColorLay
        {
            public Pen Tiles;
            public Pen Tiles2;
            public Brush Walls;
            public Color Background;
            public Pen Objects;
            public Color WallsBreakable;
            public Color WallsWindowed;
            public Color WallsSecret;
            public Color Selection;
            public Color Removing;
            public Pen WaypointNorm;
            public Pen WaypointSel;
            public Pen WaypointTwoPath;
            public Pen WaypointDis;
            public ColorLay()
            {
                ResetColors();
            }

            public void InvertColors()
            {
                Tiles2 = Pens.Blue;
                Tiles = Pens.Green;
                Walls = Brushes.Black;
                Background = Color.White;
                Objects = Pens.Red;
            }

            public void ResetColors()
            {
                Tiles2 = Pens.Yellow;
                Tiles = Pens.Gray;
                Walls = Brushes.White;
                Background = Color.Black;
                Objects = Pens.Blue;
                WallsBreakable = Color.Red;
                WallsWindowed = Color.Orange;
                WallsSecret = Color.Green;
                Selection = Color.Green;
                Removing = Color.Red;
                WaypointDis = Pens.Olive;
                WaypointNorm = new Pen(Color.FromArgb(255, 185, 185, 0));
                WaypointSel = Pens.Aqua;
                WaypointTwoPath = Pens.Orange;
            }
        }

        /// <summary>
        /// Wall texture rendering routine
        /// </summary>
        public void DrawTexturedWall(Graphics g, Map.Wall wall, bool transparent, bool beingSelected)
        {
            ThingDb.Wall tt = ThingDb.Walls[wall.matId];
            try
            {
                int actualVari = (int)wall.Variation * 2;
                // для обычных стен все данные берутся отсюда
                ThingDb.Wall.WallRenderInfo wri = tt.RenderNormal[(int)wall.Facing][actualVari];
                // если стену можно сломать
                if (wall.Destructable)
                    wri = tt.RenderBreakable[(int)wall.Facing][actualVari];
                // если стена содержит окошко
                if (wall.Window)
                {
                    switch (wall.Facing)
                    {
                        case WallFacing.NORTH:
                            wri = tt.RenderNormal[11][actualVari];
                            break;
                        case WallFacing.WEST:
                            wri = tt.RenderNormal[12][actualVari];
                            break;
                    }
                }

                // достаем картинку
                Bitmap bitmap = VideoBag.GetBitmap(wri.SpriteIndex);
                BitmapShader shader = null;
                // тонируем если стена необычная

                if (wall.Destructable || wall.Secret || transparent || beingSelected || mapView.picking)
                {
                    shader = new BitmapShader(bitmap);
                    shader.LockBitmap();
                    if (wall.Destructable)
                        shader.ColorShade(ColorLayout.WallsBreakable, 0.30F);
                    if (wall.Secret)
                        shader.ColorShade(ColorLayout.WallsSecret, 0.40F);
                    if (wall.Secret_WallState == 4)
                        shader.MakeSemitransparent(135);
                    if (beingSelected)
                    {
                        Color selCol = Color.GhostWhite;
                        if (MapInterface.CurrentMode == EditMode.WALL_PLACE && !mapView.picking)
                            selCol = ColorLayout.Removing;

                        if (MapInterface.CurrentMode == EditMode.WALL_CHANGE && !mapView.picking)
                            selCol = Color.Purple;


                        shader.ColorGradWaves(selCol, 4F, Environment.TickCount);

                    }



                    if (transparent)
                        shader.MakeSemitransparent();
                    bitmap = shader.UnlockBitmap();
                }

                // допускается что стена одновременно и секретная, и разрушаемая, и с окном
                int x, y;
                if (bitmap != null)
                {
                    x = 23 * wall.Location.X;
                    y = 23 * wall.Location.Y;
                    // коррекция координат
                    int offX = (0 - wri.unknown1) - videoBagProvider.DrawOffsets[wri.SpriteIndex][0];
                    int offY = wri.unknown2 - videoBagProvider.DrawOffsets[wri.SpriteIndex][1];
                    x -= offX + 50;
                    y -= offY + 72;
                    // собственно рисуем
                    g.DrawImage(bitmap, x, y, bitmap.Width, bitmap.Height);
                    // сразу чистим память если картинка не кэшируется
                    if (shader != null) bitmap.Dispose();
                }
            }
            catch (Exception) { }
        }

        private void RenderPostSelRect(Graphics g)
        {
            if (MapInterface.CurrentMode != EditMode.OBJECT_SELECT) return;
            Point MouseKeep = mapView.mouseKeep;
            if (mapView.picking && !MouseKeep.IsEmpty) return;
            if (mapView.Get45RecSize() < 5) return;


            Pen selectAreaPen = new Pen(Color.LightGreen, 1);
            Color newColor = Color.FromArgb(20, Color.LightGreen);
            SolidBrush blueBrush = new SolidBrush(newColor);


            g.DrawPolygon(selectAreaPen, MapInterface.selected45Area);
            if (!EditorSettings.Default.Edit_PreviewMode) return;
            g.FillPolygon(blueBrush, MapInterface.selected45Area);


        }

        private void RenderPostWalls(Graphics g)
        {

            if (MapInterface.CurrentMode == EditMode.WALL_PLACE && EditorSettings.Default.Edit_PreviewMode && !mapView.picking)
            {

                Point pt = MapView.GetNearestWallPoint(mapView.mouseLocation);

                // Render the wall being created (if there is no other in place)
                if (mapView.WallMakeNewCtrl != null && !Map.Walls.ContainsKey(pt))
                {
                    Map.Wall fakeWall = mapView.WallMakeNewCtrl.NewWall(pt, true);
                    DrawTexturedWall(g, fakeWall, true, false);
                }

            }
        }
        private void RenderPostLineWalls(Graphics g)
        {
            Point MouseKeep = mapView.mouseKeep;
            if (MapInterface.CurrentMode == EditMode.WALL_BRUSH && EditorSettings.Default.Edit_PreviewMode && !mapView.picking && !MouseKeep.IsEmpty)
            {
                foreach (var wall in FakeWalls)
                {
                    //Point pt = wall.Value.Location;
                    DrawTexturedWall(g, wall.Value, true, false);
                }
            }
        }
        private void RenderHelpMark(Graphics g)
        {
            Point highlightUndoRedo = mapView.highlightUndoRedo;

            if (highlightUndoRedo.IsEmpty) return;

            int higlightRad = mapView.higlightRad;
            Pen pen = new Pen(Color.FromArgb(130, 255, 255, 255), 3);
            Rectangle rect = new Rectangle(highlightUndoRedo.X, highlightUndoRedo.Y, higlightRad, higlightRad);
            rect.X -= higlightRad / 2;
            rect.Y -= higlightRad / 2;
            /*
            Point a = rect.Location;
            Point b = new Point(a.X + 35, a.Y);
            Point c = new Point(b.X, b.Y + 35);
            Point d = new Point(a.X, b.Y + 35);
           
            //g.DrawLine(pen, a, c);
            //g.DrawLine(pen, b, d);
            */
            g.DrawEllipse(pen, rect);

        }
        /*
        private void RenderPostTiles(Graphics g)
        {
        	if (MapInterface.CurrentMode == EditMode.FLOOR_PLACE && MapEditorSettings.DrawTextured)
        	{
        		Point point = MapView.GetNearestTilePoint(mapView.mouseLocation);
        		// Render the tile being created
        		if (!Map.Tiles.ContainsKey(point))
        		{
	                Map.Tile tile = mapView.TileMakeNewCtrl.GetTile(point,true);
	                DrawTexturedTile(g, tile, true);
        		}
        	}
        }
        */




        private void RenderPostObjects(Graphics g)
        {
            if (MapInterface.CurrentMode == EditMode.OBJECT_PLACE && !mapView.picking)
            {
                string obj = mapView.cboObjCreate.Text;
                if (ThingDb.Things.ContainsKey(obj))
                {
                    Point pt = mapView.mouseLocation;

                    Point ptAligned = pt;

                    if (EditorSettings.Default.Edit_SnapGrid || ThingDb.Things[obj].Xfer == "DoorXfer")
                        ptAligned = new Point((int)Math.Round((decimal)(pt.X / squareSize)) * squareSize, (int)Math.Round((decimal)(pt.Y / squareSize)) * squareSize);
                    if (EditorSettings.Default.Edit_SnapHalfGrid)
                        ptAligned = new Point((int)Math.Round((decimal)((pt.X / (squareSize)) * squareSize) + squareSize / 2), (int)Math.Round((decimal)((pt.Y / (squareSize)) * squareSize) + squareSize / 2));
                    if (EditorSettings.Default.Edit_SnapCustom)
                    {
                        int snap = (int)mapView.customSnapValue.Value;
                        ptAligned = new Point((int)Math.Round((decimal)(pt.X / snap)) * snap, (int)Math.Round((decimal)(pt.Y / snap)) * snap);
                    }

                    Map.Object result = new Map.Object();
                    result.Name = obj;
                    result.Location = ptAligned;


                    if (ThingDb.Things[obj].Xfer == "DoorXfer")
                    {

                        //XferEditor editor = XferEditors.GetEditorForXfer(ThingDb.Things[obj].Xfer);
                        //if (editor != null) editor.SetDefaultData(result);
                        //else result.NewDefaultExtraData();
                        result.NewDefaultExtraData();
                        NoxShared.ObjDataXfer.DoorXfer door = result.GetExtraData<NoxShared.ObjDataXfer.DoorXfer>();
                        int delta = (int)mapView.delta;
                        door.Direction = (NoxShared.ObjDataXfer.DoorXfer.DOORS_DIR)delta;
                    }
                    else if (ThingDb.Things[obj].Xfer == "MonsterXfer")
                    {
                        result.NewDefaultExtraData();
                        NoxShared.ObjDataXfer.MonsterXfer monster = result.GetExtraData<NoxShared.ObjDataXfer.MonsterXfer>();
                        int delta = (int)mapView.delta;
                        monster.DirectionId = (byte)delta;

                    }
                    else if (ThingDb.Things[obj].Xfer == "NPCXfer")
                    {
                        result.NewDefaultExtraData();
                        NoxShared.ObjDataXfer.NPCXfer npc = result.GetExtraData<NoxShared.ObjDataXfer.NPCXfer>();
                        int delta = (int)mapView.delta;
                        npc.DirectionId = (byte)delta;

                    }
                    else if (ThingDb.Things[obj].Xfer == "SentryXfer")
                    {
                        result.NewDefaultExtraData();
                        NoxShared.ObjDataXfer.SentryXfer s = result.GetExtraData<NoxShared.ObjDataXfer.SentryXfer>();
                        float delta = mapView.delta;
                        s.BasePosRadian = (float)delta;

                    }
                    List<Map.Object> listone = new List<Map.Object>();
                    listone.Add(result);
                    objectRenderer.TheHell(g, listone);
                }
            }
        }


        bool updCanvasObjects = true;
        bool updCanvasTiles = true;

        public void UpdateCanvas(bool objects, bool tiles)
        {
            // This is correct logic because we won't override canvas status in the same frame
            if (objects)
                updCanvasObjects = true;
            if (tiles)
                updCanvasTiles = true;
        }

        /// <summary>
        /// Renders the map using current settings and editing stuff. 
        /// </summary>
        public void RenderTo(Graphics g, bool ToImage = false)
        {
            PointF nwCorner, neCorner, seCorner, swCorner, center;
            bool DrawTextured = EditorSettings.Default.Edit_PreviewMode;
            bool DrawExtents3D = EditorSettings.Default.Draw_Extents_3D;
            bool DrawText = EditorSettings.Default.Draw_AllText;
            // enable transparency only in textured preview mode
            if (DrawTextured) g.CompositingMode = CompositingMode.SourceOver;
            else g.CompositingMode = CompositingMode.SourceCopy;
            // optimizations
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.Low;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            proDefault = (!mapView.picking && mapView.mapPanel.Cursor != Cursors.SizeAll && Form.ActiveForm == MainWindow.Instance);
            proHand = (!mapView.picking && !MapInterface.KeyHelper.ShiftKey && MapInterface.SelectedObjects.Items.Count > 1 && !mapView.contextMenuOpen);
            // expand clip rectangle a bit
            const int sqSize2 = squareSize * 2;
           
            Rectangle clip = new Rectangle((int)g.ClipBounds.X - sqSize2, (int)g.ClipBounds.Y - sqSize2, (int)g.ClipBounds.Width + sqSize2, (int)g.ClipBounds.Height + sqSize2);
            if (ToImage)
            {
                clip = new Rectangle(0, 0, 5880, 5880);
                updCanvasObjects = true;
                updCanvasTiles = true;
            }
            if (updCanvasObjects)
            {
                objectRenderer.UpdateCanvas(clip);
                updCanvasObjects = false;
            }
            if (updCanvasTiles)
            {
                floorRenderer.UpdateCanvas(clip);
                updCanvasTiles = false;
            }
            // Paint it black
            Size size = mapView.mapPanel.Size;
            g.Clear(ColorLayout.Background);
            Point mouseLocation = mapView.mouseLocation;
            Pen pen;

            // Draw tiles and edges
            if (MapInterface.CurrentMode == EditMode.FLOOR_PLACE || EditorSettings.Default.Draw_FloorTiles || MapInterface.CurrentMode == EditMode.EDGE_PLACE)
                floorRenderer.Render(g);

            // Draw grid
            if (EditorSettings.Default.Draw_Grid)
            {
                using (pen = new Pen(Color.Gray, 1F))
                {
                    //draw the grid sloppily (an extra screen's worth of lines along either axis)
                    for (int x = -squareSize * (size.Width / squareSize) - 3 * squareSize / 2 % (2 * squareSize); x < 2 * size.Width; x += 2 * squareSize)
                    {
                        int y = -3 * squareSize / 2 % (2 * squareSize);
                        g.DrawLine(pen, new Point(x - 1, y), new Point(y, x - 1));
                        g.DrawLine(pen, new Point(x, y), new Point(size.Width + x, size.Width + y));
                    }
                }
            }

            if (MapInterface.CurrentMode >= EditMode.FLOOR_PLACE && MapInterface.CurrentMode <= EditMode.EDGE_PLACE)
            {
                // Draw the overlay to show tile location
                Point pt = new Point(mouseLocation.X, mouseLocation.Y);
                PointF tilePt = (PointF)MapView.GetNearestTilePoint(pt);
                int squareSize2 = squareSize;

                int bs = (int)MainWindow.Instance.mapView.TileMakeNewCtrl.BrushSize.Value;

                if ((MapInterface.CurrentMode == EditMode.FLOOR_BRUSH || MapInterface.CurrentMode == EditMode.FLOOR_PLACE) && !mapView.picking)
                {
                    squareSize2 *= bs;
                    if (bs > 1)
                    {
                        tilePt.X -= (float)(-0.5 + 0.5 * bs);
                        tilePt.Y -= (float)(1.5 + 0.5 * bs);
                        //tilePt.Y -= 1;
                        tilePt.Y -= ((bs - 1) + ((bs % 2 == 0) ? 1 : 0));
                        tilePt.Y += 2;
                    }
                }

                // Change overlay color depending on editor EditMode
                Color tileOverlayCol = Color.Yellow;
                if (MapInterface.CurrentMode == EditMode.EDGE_PLACE)
                    tileOverlayCol = MainWindow.Instance.mapView.EdgeMakeNewCtrl.AutoEgeBox.Checked ? Color.Green : Color.Aqua;
                if (MapInterface.CurrentMode == EditMode.FLOOR_BRUSH) tileOverlayCol = Color.LawnGreen;

                if (mapView.picking)
                    tileOverlayCol = Color.GhostWhite;

                tilePt.X *= squareSize;
                tilePt.Y *= squareSize;

                center = new PointF(tilePt.X + squareSize / 2f, tilePt.Y + (3 / 2f) * squareSize);
                nwCorner = new PointF(tilePt.X - squareSize2 / 2f, tilePt.Y + (3 / 2f) * squareSize2);
                neCorner = new PointF(nwCorner.X + squareSize2, nwCorner.Y - squareSize2);
                swCorner = new PointF(nwCorner.X + squareSize2, nwCorner.Y + squareSize2);
                seCorner = new PointF(neCorner.X + squareSize2, neCorner.Y + squareSize2);

                g.DrawPolygon(new Pen(tileOverlayCol, 2), new PointF[] { nwCorner, neCorner, seCorner, swCorner });
            }

            Pen destructablePen = new Pen(ColorLayout.WallsBreakable, 2);
            Pen windowPen = new Pen(ColorLayout.WallsWindowed, 2);
            Pen secretPen = new Pen(ColorLayout.WallsSecret, 2);
            Pen invisiblePen = new Pen(Color.DarkGray, 2);
            Pen FakeWallPen = new Pen(Color.LightGray, 2);
            Pen OpenPen = new Pen(Color.FromArgb(255, 110, 170, 110), 2);
            Pen wallPen = new Pen(ColorLayout.Walls, 2);

            // Draw [BELOW] objects
            objectRenderer.RenderBelow(g);

            // Draw walls
            if (EditorSettings.Default.Draw_Walls)
            {

                Map.Wall removing = mapView.GetWallUnderCursor();

                Point highlightUndoRedo = mapView.highlightUndoRedo;




                if (MapInterface.CurrentMode == EditMode.WALL_BRUSH && !mapView.picking)
                    removing = null;


                if (FakeWalls.Count > 0 && !EditorSettings.Default.Edit_PreviewMode)
                {
                    foreach (Map.Wall wall in FakeWalls.Values)
                    {
                        Point pt = wall.Location;
                        int x = pt.X * squareSize, y = pt.Y * squareSize;
                        Point txtPoint = new Point(x, y);
                        pen = invisiblePen;
                        center = new PointF(x + squareSize / 2f, y + squareSize / 2f);
                        Point nCorner = new Point(x, y);
                        Point sCorner = new Point(x + squareSize, y + squareSize);
                        Point wCorner = new Point(x + squareSize, y);
                        Point eCorner = new Point(x, y + squareSize);

                        Point nCornerUp = new Point(x, y - 40);
                        Point sCornerUp = new Point(x + squareSize, y + squareSize - 40);
                        Point wCornerUp = new Point(x + squareSize, y - 40);
                        Point eCornerUp = new Point(x, y + squareSize - 40);
                        PointF centerUp = new PointF(x + squareSize / 2f, (y + squareSize / 2f) - 40);

                        switch (wall.Facing)
                        {
                            case Map.Wall.WallFacing.NORTH:
                                g.DrawLine(pen, wCorner, eCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);

                                g.DrawLine(pen, wCornerUp, wCorner);
                                g.DrawLine(pen, eCornerUp, eCorner);

                                break;
                            case Map.Wall.WallFacing.WEST:
                                g.DrawLine(pen, nCorner, sCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, nCornerUp, sCornerUp);

                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                break;
                            case Map.Wall.WallFacing.CROSS:
                                g.DrawLine(pen, wCorner, eCorner);//north wall
                                g.DrawLine(pen, nCorner, sCorner);//south wall
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);//north wall
                                g.DrawLine(pen, nCornerUp, sCornerUp);//south wall

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);

                                break;
                            case Map.Wall.WallFacing.NORTH_T:
                                g.DrawLine(pen, wCorner, eCorner);//north wall
                                g.DrawLine(pen, center, sCorner);//tail towards south
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);//north wall
                                g.DrawLine(pen, centerUp, sCornerUp);//tail towards south

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, center, centerUp);

                                break;
                            case Map.Wall.WallFacing.SOUTH_T:
                                g.DrawLine(pen, wCorner, eCorner);//north wall
                                g.DrawLine(pen, center, nCorner);//tail towards north
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);//north wall
                                g.DrawLine(pen, centerUp, nCornerUp);//tail towards south

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);
                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, center, centerUp);
                                break;
                            case Map.Wall.WallFacing.WEST_T:
                                g.DrawLine(pen, nCorner, sCorner);//west wall
                                g.DrawLine(pen, center, eCorner);//tail towards east
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, nCornerUp, sCornerUp);//north wall
                                g.DrawLine(pen, centerUp, eCornerUp);//tail towards south

                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);
                                g.DrawLine(pen, center, centerUp);
                                break;
                            case Map.Wall.WallFacing.EAST_T:
                                g.DrawLine(pen, nCorner, sCorner);//west wall
                                g.DrawLine(pen, center, wCorner);//tail towards west
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, nCornerUp, sCornerUp);//north wall
                                g.DrawLine(pen, centerUp, wCornerUp);//tail towards south

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, center, centerUp);
                                break;
                            case Map.Wall.WallFacing.NE_CORNER:
                                g.DrawLine(pen, center, eCorner);
                                g.DrawLine(pen, center, sCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, eCornerUp);
                                g.DrawLine(pen, centerUp, sCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, eCornerUp, eCorner);
                                g.DrawLine(pen, sCornerUp, sCorner);
                                break;
                            case Map.Wall.WallFacing.NW_CORNER:
                                g.DrawLine(pen, center, wCorner);
                                g.DrawLine(pen, center, sCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, wCornerUp);
                                g.DrawLine(pen, centerUp, sCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, wCornerUp, wCorner);
                                g.DrawLine(pen, sCornerUp, sCorner);
                                break;
                            case Map.Wall.WallFacing.SW_CORNER:
                                g.DrawLine(pen, center, nCorner);
                                g.DrawLine(pen, center, wCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, nCornerUp);
                                g.DrawLine(pen, centerUp, wCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, wCornerUp, wCorner);
                                g.DrawLine(pen, nCornerUp, nCorner);
                                break;
                            case Map.Wall.WallFacing.SE_CORNER:
                                g.DrawLine(pen, center, nCorner);
                                g.DrawLine(pen, center, eCorner);

                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, nCornerUp);
                                g.DrawLine(pen, centerUp, eCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, eCornerUp, eCorner);
                                g.DrawLine(pen, nCornerUp, nCorner);
                                break;
                            default:
                                g.DrawRectangle(pen, x, y, squareSize, squareSize);
                                if (DrawText) TextRenderer.DrawText(g, "?", drawFont, nCorner, Color.Red);
                                break;
                        }


                    }
                }

                foreach (Map.Wall wall in Map.Walls.Values)
                {
                    Point pt = wall.Location;
                    int x = pt.X * squareSize, y = pt.Y * squareSize;
                    Point txtPoint = new Point(x, y);
                    if (clip.Contains(x, y))
                    {
                        if (DrawTextured && !wall.Material.Contains("Invisible"))
                        {

                            DrawTexturedWall(g, wall, false, removing == wall);
                            continue;
                        }

                        //TODO: how to draw if a destructable window? is this even possible?

                        if (wall.Secret)
                            pen = wall.Secret_WallState == 4 ? OpenPen : secretPen;
                        else if (wall.Destructable)//if (wall.Destructable || MapInterface.GetLastWalls(wall))
                            pen = destructablePen;
                        else if (wall.Window)
                            pen = windowPen;
                        else if (wall.Material.Contains("Invisible"))
                            pen = invisiblePen;
                        else
                            pen = wallPen;

                        if (removing == wall)
                        {
                            if (mapView.picking) pen = new Pen(Color.Aqua, 3);
                            else if (MapInterface.CurrentMode == EditMode.WALL_CHANGE) pen = new Pen(Color.Purple, 3);

                        }
                        center = new PointF(x + squareSize / 2f, y + squareSize / 2f);
                        Point nCorner = new Point(x, y);
                        Point sCorner = new Point(x + squareSize, y + squareSize);
                        Point wCorner = new Point(x + squareSize, y);
                        Point eCorner = new Point(x, y + squareSize);

                        Point nCornerUp = new Point(x, y - 40);
                        Point sCornerUp = new Point(x + squareSize, y + squareSize - 40);
                        Point wCornerUp = new Point(x + squareSize, y - 40);
                        Point eCornerUp = new Point(x, y + squareSize - 40);
                        PointF centerUp = new PointF(x + squareSize / 2f, (y + squareSize / 2f) - 40);

                        switch (wall.Facing)
                        {
                            case Map.Wall.WallFacing.NORTH:
                                g.DrawLine(pen, wCorner, eCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);

                                g.DrawLine(pen, wCornerUp, wCorner);
                                g.DrawLine(pen, eCornerUp, eCorner);

                                break;
                            case Map.Wall.WallFacing.WEST:
                                g.DrawLine(pen, nCorner, sCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, nCornerUp, sCornerUp);

                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                break;
                            case Map.Wall.WallFacing.CROSS:
                                g.DrawLine(pen, wCorner, eCorner);//north wall
                                g.DrawLine(pen, nCorner, sCorner);//south wall
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);//north wall
                                g.DrawLine(pen, nCornerUp, sCornerUp);//south wall

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);

                                break;
                            case Map.Wall.WallFacing.NORTH_T:
                                g.DrawLine(pen, wCorner, eCorner);//north wall
                                g.DrawLine(pen, center, sCorner);//tail towards south
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);//north wall
                                g.DrawLine(pen, centerUp, sCornerUp);//tail towards south

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, center, centerUp);

                                break;
                            case Map.Wall.WallFacing.SOUTH_T:
                                g.DrawLine(pen, wCorner, eCorner);//north wall
                                g.DrawLine(pen, center, nCorner);//tail towards north
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, wCornerUp, eCornerUp);//north wall
                                g.DrawLine(pen, centerUp, nCornerUp);//tail towards south

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);
                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, center, centerUp);
                                break;
                            case Map.Wall.WallFacing.WEST_T:
                                g.DrawLine(pen, nCorner, sCorner);//west wall
                                g.DrawLine(pen, center, eCorner);//tail towards east
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, nCornerUp, sCornerUp);//north wall
                                g.DrawLine(pen, centerUp, eCornerUp);//tail towards south

                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, eCorner, eCornerUp);
                                g.DrawLine(pen, center, centerUp);
                                break;
                            case Map.Wall.WallFacing.EAST_T:
                                g.DrawLine(pen, nCorner, sCorner);//west wall
                                g.DrawLine(pen, center, wCorner);//tail towards west
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, nCornerUp, sCornerUp);//north wall
                                g.DrawLine(pen, centerUp, wCornerUp);//tail towards south

                                g.DrawLine(pen, wCorner, wCornerUp);
                                g.DrawLine(pen, nCorner, nCornerUp);
                                g.DrawLine(pen, sCorner, sCornerUp);
                                g.DrawLine(pen, center, centerUp);
                                break;
                            case Map.Wall.WallFacing.NE_CORNER:
                                g.DrawLine(pen, center, eCorner);
                                g.DrawLine(pen, center, sCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, eCornerUp);
                                g.DrawLine(pen, centerUp, sCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, eCornerUp, eCorner);
                                g.DrawLine(pen, sCornerUp, sCorner);
                                break;
                            case Map.Wall.WallFacing.NW_CORNER:
                                g.DrawLine(pen, center, wCorner);
                                g.DrawLine(pen, center, sCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, wCornerUp);
                                g.DrawLine(pen, centerUp, sCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, wCornerUp, wCorner);
                                g.DrawLine(pen, sCornerUp, sCorner);
                                break;
                            case Map.Wall.WallFacing.SW_CORNER:
                                g.DrawLine(pen, center, nCorner);
                                g.DrawLine(pen, center, wCorner);
                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, nCornerUp);
                                g.DrawLine(pen, centerUp, wCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, wCornerUp, wCorner);
                                g.DrawLine(pen, nCornerUp, nCorner);
                                break;
                            case Map.Wall.WallFacing.SE_CORNER:
                                g.DrawLine(pen, center, nCorner);
                                g.DrawLine(pen, center, eCorner);

                                if (!DrawExtents3D)
                                    break;
                                g.DrawLine(pen, centerUp, nCornerUp);
                                g.DrawLine(pen, centerUp, eCornerUp);

                                g.DrawLine(pen, centerUp, center);
                                g.DrawLine(pen, eCornerUp, eCorner);
                                g.DrawLine(pen, nCornerUp, nCorner);
                                break;
                            default:
                                g.DrawRectangle(pen, x, y, squareSize, squareSize);
                                if (DrawText) TextRenderer.DrawText(g, "?", drawFont, nCorner, Color.Red);
                                break;
                        }
                        if (DrawText)

                            TextRenderer.DrawText(g, wall.Minimap.ToString(), drawFont, txtPoint, Color.Red);
                    }
                }
            }

            RenderPostLineWalls(g);

            RenderPostWalls(g);
            RenderPostObjects(g);
            RenderPostSelRect(g);

            // Draw objects
            objectRenderer.RenderNormal(g);
            RenderHelpMark(g);
            // Draw polygons
            if (EditorSettings.Default.Draw_Polygons)
            {
                foreach (Map.Polygon poly in Map.Polygons)
                {
                    pen = Pens.PaleGreen;
                    // Highlight the polygon being edited
                    if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                    {
                        if (mapView.PolygonEditDlg.SelectedPolygon == poly || mapView.PolygonEditDlg.SuperPolygon == poly)
                        {
                            pen = Pens.PaleVioletRed;

                            foreach (PointF pt in poly.Points)
                            {
                                center = new PointF(pt.X - MapView.objectSelectionRadius, pt.Y - MapView.objectSelectionRadius);
                                Pen pen2 = MapInterface.SelectedPolyPoint == pt ? Pens.DodgerBlue : Pens.DeepPink;
                                g.DrawEllipse(pen2, new RectangleF(center, new Size(2 * MapView.objectSelectionRadius, 2 * MapView.objectSelectionRadius)));

                            }
                        }
                    }
                    if (poly.Points.Count > 2)
                    {


                        poly.Points.Add(poly.Points[0]);

                        if (MainWindow.Instance.mapView.PolygonEditDlg.ambientColors.Checked)
                        {

                            int alphaa = ((poly.AmbientLightColor.R + poly.AmbientLightColor.R + poly.AmbientLightColor.B + poly.AmbientLightColor.B + poly.AmbientLightColor.G + poly.AmbientLightColor.G) / 6);
                            FillMode newFillMode = FillMode.Alternate;
                            Color newColor = Color.FromArgb(Math.Abs(255 - alphaa), poly.AmbientLightColor);
                            SolidBrush blueBrush = new SolidBrush(newColor);
                            g.DrawPolygon(pen, poly.Points.ToArray());
                            g.FillPolygon(blueBrush, poly.Points.ToArray(), newFillMode);
                        }

                        if (mapView.PolygonEditDlg.SuperPolygon == poly && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                            pen = new Pen(Color.PaleVioletRed, 2);
                        g.DrawLines(pen, poly.Points.ToArray());
                        poly.Points.RemoveAt(poly.Points.Count - 1);
                    }
                }
            }

            // Draw waypoints
            if (EditorSettings.Default.Draw_Waypoints)
            {
                foreach (Map.Waypoint wp in Map.Waypoints)
                {
                    // highlight selected waypoint
                    pen = wp.Flags == 1 ? ColorLayout.WaypointNorm : ColorLayout.WaypointDis;
                    pen = MapInterface.SelectedWaypoint == wp ? ColorLayout.WaypointSel : pen;
                    // draw waypoint and related pathes
                    center = new PointF(wp.Point.X - MapView.objectSelectionRadius, wp.Point.Y - MapView.objectSelectionRadius);
                    g.DrawEllipse(pen, new RectangleF(center, new Size(2 * MapView.objectSelectionRadius, 2 * MapView.objectSelectionRadius)));
                    pen = ColorLayout.WaypointDis;
                    // Draw paths (code/idea from UndeadZeus)
                    foreach (Map.Waypoint.WaypointConnection wpc in wp.connections)
                    {

                        g.DrawLine(pen, wp.Point.X, wp.Point.Y, wpc.wp.Point.X, wpc.wp.Point.Y);
                        foreach (Map.Waypoint.WaypointConnection wpwp in wpc.wp.connections)//Checks if the waypoint connection is connecting to wp
                        {
                            if (wpwp.wp.Equals(wp))
                            {
                                // if there is two way connection
                                /*
                                 if (MapInterface.SelectedWaypoint != wp) pen = ColorLayout.WaypointTwoPath;
                                 g.DrawLine(pen, wp.Point.X, wp.Point.Y, wpc.wp.Point.X, wpc.wp.Point.Y);
                                 break;
                                 */

                                if (MapInterface.SelectedWaypoint != null && wp == MapInterface.SelectedWaypoint)
                                    g.DrawLine(ColorLayout.WaypointSel, wp.Point.X, wp.Point.Y, wpc.wp.Point.X, wpc.wp.Point.Y);
                                else
                                    g.DrawLine(ColorLayout.WaypointTwoPath, wp.Point.X, wp.Point.Y, wpc.wp.Point.X, wpc.wp.Point.Y);
                                break;

                            }
                        }
                    }
                    Point wayPoint = new Point(Convert.ToInt32(center.X), Convert.ToInt32(center.Y));
                    // text rendering is slow - as such don't label unseen waypoints
                    if (DrawText && clip.Contains(wayPoint))
                    {
                        if (wp.Name.Length > 0)
                            TextRenderer.DrawText(g, wp.Number + ":" + wp.ShortName, drawFont, wayPoint, Color.YellowGreen);
                        else
                            TextRenderer.DrawText(g, wp.Number + "", drawFont, wayPoint, Color.MediumPurple);
                    }
                }
            }
        }
    }
}
