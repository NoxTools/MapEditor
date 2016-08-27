/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 06.07.2015
 */

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using MapEditor.XferGui;
using MapEditor.newgui;
using MapEditor.mapgen;
using NoxShared;
namespace MapEditor.MapInt
{
    /// <summary>
    /// Wrapper providing complex Map-related operations
    /// </summary>
    public class MapInterface
    {
        // Singleton
        //private Point? mousePos = null;
        private static MapInterface _instance = new MapInterface();
        /*
        const int HISTORY_LIMIT = 8;
        private Queue<Operation> history;
        */
        public static ArrayList RecSelected = new ArrayList();
        public static PointF SelectedPolyPoint;
        public static Rectangle selectedArea;
        public static Point[] selected45Area = new Point[4];
        protected Map _Map;
        /// <summary>
        /// The map currently being edited.
        /// </summary>
        public static Map TheMap
        {
            get
            {
                return _instance._Map;
            }
            set
            {
                _instance._Map = value;
            }
        }
        protected MapHelper _mapHelper;

        /// <summary>
        /// Enables auto-removal of already existing tiles/walls in placement mode.
        /// </summary>
        public static bool AllowPlaceOverride
        {
            get
            {
                return EditorSettings.Default.Edit_AllowOverride;
            }
        }

        private EditMode _currentMode;

        /// <summary>
        /// Current editing mode/operation.
        /// </summary>
        public static EditMode CurrentMode
        {
            get
            {
                return _instance._currentMode;
            }
            set
            {
                _instance._currentMode = value;
                _instance._ModeIsUpdated = true;
            }
        }


        private bool _ModeIsUpdated = false;

        /// <summary>
        /// Forces MapView to update current mode in statusbar.
        /// </summary>
        public static bool ModeIsUpdated
        {
            get
            {
                return _instance._ModeIsUpdated;
            }
            set
            {
                _instance._ModeIsUpdated = value;
            }
        }

        private KeyHelper _keyHelper = new KeyHelper();


        /// <summary>
        /// Instance of class that stores key states.
        /// </summary>
        public static KeyHelper KeyHelper
        {
            get
            {
                return _instance._keyHelper;
            }
        }

        private MapView _mapView = null;

        /// <summary>
        /// Reference to MapView class with editor tools. TODO: remove redundant calls to this
        /// </summary>
        private static MapView mapView
        {
            get
            {
                if (_instance._mapView == null)
                    _instance._mapView = MainWindow.Instance.mapView;

                return _instance._mapView;
            }
        }

        const string BLANK_MAP_NAME = "blankmap.map";

        public MapInterface()
        {
            //CurrentMode = Mode.OBJECT_SELECT;
            //history = new Queue<Operation>();
        }

        /// <summary>
        /// Attempts to load a map by its filename. Passing null string will result in loading a blank map
        /// </summary>
        public static void SwitchMap(String fileName)
        {   mapView.done = false;
            Stream stream = null;
            
            // Check if requested loading a blank map
            if (fileName == null)
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                // Find BlankMap resource name.
                string name = null;
                foreach (string file in asm.GetManifestResourceNames())
                {
                    if (file.EndsWith(BLANK_MAP_NAME, StringComparison.InvariantCultureIgnoreCase))
                    {
                        name = file;
                        break;
                    }
                }
                // Open resource stream
                stream = asm.GetManifestResourceStream(name);
                
            }
            else
            {
                MainWindow.Instance.Cursor = Cursors.WaitCursor;
                //MainWindow.Instance.Invalidate();
                // Open filestream from name
                stream = new FileStream(fileName, FileMode.Open);
                // attempt to decompress .NXZ (if specified nxz file)
                if (Path.GetExtension(fileName).Equals(".nxz", StringComparison.InvariantCultureIgnoreCase))
                {
                    int length = 0; byte[] data = null;
                    using (var br = new BinaryReader(stream))
                    {
                        length = br.ReadInt32();
                        data = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
                    }
                    byte[] mapData = new byte[length];
                    NoxLzCompression.Decompress(data, mapData);
                    stream = new MemoryStream(mapData);
                }
            }

            // load the map
            var map = new Map();
            map.FileName = fileName;
            using (var rdr = new NoxBinaryReader(stream, CryptApi.NoxCryptFormat.MAP))
                map.Load(rdr);
            // create helper class
            _instance._mapHelper = new MapHelper(map);
            _instance._Map = map;
            // update mapinfo
            MainWindow.Instance.UpdateMapInfo();
            mapView.TimeManager.Clear();
            mapView.currentStep = 0;
            
            mapView.undo.Enabled = false;
            mapView.redo.Enabled = false;
            MainWindow.Instance.undo.Enabled = false;
            MainWindow.Instance.redo.Enabled = false;
            SelectedWaypoint = null;
            MainWindow.Instance.Cursor = Cursors.Default;
            if (mapView.PolygonEditDlg.Visible) mapView.PolygonEditDlg.Visible = false;
            MainWindow.Instance.Reload();

            //mapView
        }

        #region Wall operations
        public static bool WallPlace(int x, int y)
        {
            return WallPlace(new Point(x, y));
        }

        public static bool WallPlace(Point pt, bool fromBrush = false)
        {
            if (pt.X <= 0 || pt.Y <= 0 || pt.X >= 255 || pt.Y >= 255) return false;
            Map.Wall oldWall = null;
            bool remove = false;
            if (TheMap.Walls.ContainsKey(pt))
            {
                oldWall = TheMap.Walls[pt];
                if (!AllowPlaceOverride) return false;
                remove = true;
            }
            Map.Wall newWall = mapView.WallMakeNewCtrl.NewWall(pt);


            if (oldWall != null)
            {
               

                if (!mapView.WallMakeNewCtrl.smartDraw.Checked)
                {
                    if (oldWall.Material == newWall.Material && oldWall.Facing == newWall.Facing && oldWall.Variation == newWall.Variation)
                        return false;
                }
                else
                {
                    if ((oldWall.Material == newWall.Material && oldWall.Facing == newWall.Facing) || mapView.LastWalls.Contains(oldWall))
                        return false;
                }
            }
            if (remove) TheMap.Walls.Remove(pt);
            TheMap.Walls.Add(pt, newWall);
            if (!fromBrush) OpUpdatedWalls = true;
            return true;
        }

        public static bool WallRemove(int x, int y)
        {
            return WallRemove(new Point(x, y));
        }

        public static bool WallRemove(Point pt)
        {
            if (TheMap.Walls.ContainsKey(pt))
            {
                TheMap.Walls.Remove(pt);
                OpUpdatedWalls = true;
                return true;
            }
            return false;
        }

        public static bool WallChange(int x, int y)
        {
            return WallChange(new Point(x, y));
        }

        public static bool WallChange(Point pt, bool removeProp = false)
        {
            if (TheMap.Walls.ContainsKey(pt))
            {

                if (removeProp)
                {

                    if (!TheMap.Walls[pt].Destructable && TheMap.Walls[pt].Secret_ScanFlags == 0 && TheMap.Walls[pt].Secret_WallState == 0)
                        return false;

                    TheMap.Walls[pt].Destructable = false;
                    TheMap.Walls[pt].Secret_ScanFlags = 0;
                    TheMap.Walls[pt].Secret_WallState = 0;
                    OpUpdatedWalls = true;
                }
                else
                {
                    Map.Wall wall = TheMap.Walls[pt];
                    //MessageBox.Show(wall.Secret_WallState.ToString());
                    bool ok = true;
                    int flagsSelected = 0;
                    int flagsChecked = mapView.WallMakeNewCtrl.openWallBox.Checked ? 4 : 0;
                    if (mapView.WallMakeNewCtrl.checkListFlags.GetItemChecked(0)) flagsSelected += 1;
                    if (mapView.WallMakeNewCtrl.checkListFlags.GetItemChecked(1)) flagsSelected += 2;
                    if (mapView.WallMakeNewCtrl.checkListFlags.GetItemChecked(2)) flagsSelected += 4;
                    if (mapView.WallMakeNewCtrl.checkListFlags.GetItemChecked(3)) flagsSelected += 8;



                    if (mapView.WallMakeNewCtrl.checkDestructable.Checked == wall.Destructable &&
                        mapView.WallMakeNewCtrl.polygonGroup.Value == wall.Minimap &&
                        mapView.WallMakeNewCtrl.numericCloseDelay.Value == wall.Secret_OpenWaitSeconds &&
                        flagsSelected == wall.Secret_ScanFlags &&
                        wall.Secret_WallState == flagsChecked) ok = false;

                    if (ok)
                    {
                        mapView.WallMakeNewCtrl.SetWall(TheMap.Walls[pt], KeyHelper.ShiftKey);

                        if (!KeyHelper.ShiftKey)
                            OpUpdatedWalls = true;
                        return true;
                    }
                }
            }
            return false;
        }

        /// ////////////////////////////////////
        /// <summary>

        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static Map.Wall WallGet(Point pt)
        {
            if (!TheMap.Walls.ContainsKey(pt)) return null;
            return TheMap.Walls[pt];
        }

        public static Point WallSnap(Point pt)
        {
            return new Point((pt.X / 23) * 23, (pt.Y / 23) * 23);
        }


        public static Point Rotate(Point point, Point pivot, double angleDegree)
        {

            double angle = angleDegree * Math.PI / 180;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            float dx = point.X - pivot.X;
            float dy = point.Y - pivot.Y;
            double x = cos * dx - sin * dy + pivot.X;
            double y = sin * dx + cos * dy + pivot.Y;

            Point rotated = new Point((int)Math.Round(x), (int)Math.Round(y));
            return rotated;
        }

        public static void WallRectangle(Point pt)
        {
            // Point MouseKeepOff = mapView.mouseKeepOff;
            // Point MousePoint = mapView.mouseKeep;
            Point MouseKeepOff = MapView.GetNearestWallPoint(mapView.mouseKeepOff, true);
            Point MousePoint = MapView.GetNearestWallPoint(mapView.mouseKeep, true);


            if (!MousePoint.IsEmpty)
                MousePoint = mapView.mouseKeep;
            else
            {
                if (MouseKeepOff.IsEmpty)
                    return;

                MousePoint = mapView.mouseKeepOff;
            }
            // pt = MapView.GetCenterPoint(pt);
            pt = Rotate(pt, MousePoint, -45);
            pt = MapView.GetCenterPoint(pt);


            Point a = MousePoint;
            Point b = new Point(pt.X, MousePoint.Y);
            Point c = pt;
            Point d = new Point(MousePoint.X, pt.Y);

            b = Rotate(b, a, 45);
            c = Rotate(c, a, 45);
            d = Rotate(d, a, 45);
            mapView.MapRenderer.FakeWalls.Clear();
            WallLine(a, true, b, false);
            WallLine(b, true, c, false);
            WallLine(c, true, d, false);
            WallLine(d, true, a);

        }
        public static void WallLine(Point pt, bool proxy = false, Point proxyDest = new Point(), bool dumb = true)
        {
            Point MouseKeep = MapView.GetNearestWallPoint(mapView.mouseKeep, true);
            Point MouseKeepOff = MapView.GetNearestWallPoint(mapView.mouseKeepOff, true);
            
            bool fake = true;
            Point mousePos;
            Point mouseDest = new Point();
            mousePos = pt;
            //mousePos = MapView.GetCenterPoint(pt);
            if (!MouseKeep.IsEmpty)
            {
                fake = true;
                //mouseDest = proxyDest.IsEmpty ? MouseKeep : MapView.GetNearestWallPoint(proxyDest, true);
                mouseDest = proxyDest.IsEmpty ? MouseKeep : proxyDest;
            }
            else
            {
                if (MouseKeepOff.IsEmpty)
                    return;

                //if (!proxy)
                // mapView.MapRenderer.FakeWalls.Clear();

                fake = false;
                //mouseDest = proxyDest.IsEmpty ? MouseKeepOff : MapView.GetNearestWallPoint(proxyDest, true);
                mouseDest = proxyDest.IsEmpty ? MouseKeepOff : proxyDest;
            }
            //mouseDest = MapView.GetNearestWallPoint(mouseDest, true);
            double dX = mouseDest.X - mousePos.X;
            double dY = mouseDest.Y - mousePos.Y;

            double multi = dX * dX + dY * dY;

            double distance = Math.Round(Math.Sqrt(multi) / 23);

            double rotationDirection = 180 - ((Math.Atan2(mousePos.X - mouseDest.X, mousePos.Y - mouseDest.Y)) * 180 / Math.PI);
            double vectorAngle = ((rotationDirection - 90) * Math.PI / 180);

            double xStep = (Math.Cos(vectorAngle)) * 23;
            double yStep = (Math.Sin(vectorAngle)) * 23;

            int xStepT = Convert.ToInt32(Math.Round(xStep));
            int yStepT = Convert.ToInt32(Math.Round(yStep));

            Point wallStep = new Point(mouseDest.X, mouseDest.Y);


            if (!proxy)
            {
                distance++;
                mapView.MapRenderer.FakeWalls.Clear();

            }
            for (int i = 0; i < distance; i++)
            {

                if (i > 0)
                {
                    wallStep.X += xStepT;
                    wallStep.Y += yStepT;
                }

                Point ptAligned1 = MapView.GetNearestWallPoint(wallStep);

                Map.Wall fakePiece = WallAutoBrush(ptAligned1, true, fake);

                if (fakePiece == null)
                    continue;
                /*
                 if (!mapView.MapRenderer.FakeWalls.ContainsKey(fakePiece.Location))
                 {
                     ObjectPlace(mapView.cboObjCreate.Text, ptAligned1.X * MapView.squareSize, ptAligned1.Y * MapView.squareSize);

                 }
                 * */
                if (!mapView.MapRenderer.FakeWalls.ContainsKey(fakePiece.Location) && fake)
                {
                    mapView.MapRenderer.FakeWalls.Add(fakePiece.Location, fakePiece);

                }
            }

            if (dumb)
            {
                mapView.mouseKeepOff = new Point();
                if (!fake && OpUpdatedWalls)
                    mapView.Store(CurrentMode, MapEditor.MapView.TimeEvent.POST);
            }

        }
        public static Map.Wall GetWallInList(Point pt)
        {
            foreach (var wall in mapView.MapRenderer.FakeWalls)
            {
                if (wall.Value.Location == pt)
                {
                    return wall.Value;
                }
            }
            return null;
        }

        public static bool GetLastWalls(Map.Wall wall)
        {
            foreach (Map.Wall thatwall in mapView.LastWalls)
            {
                if (thatwall == null) continue;

                if (thatwall.Equals(wall))
                {
                    return true;
                }
            }
            return false;
        }



        public static Map.Wall WallAutoBrush(Point pt, bool recur, bool fake = false, Point fix = new Point(), Point fxOrigin = new Point())
        {
            int maxWallList = 3;
            maxWallList = mapView.WallMakeNewCtrl.RecWall.Checked ? 300 : 3;


            if (mapView.WallMakeNewCtrl.smartDraw.Checked)
            {

                maxWallList = mapView.WallMakeNewCtrl.LineWall.Checked ? 6 : mapView.WallMakeNewCtrl.RecWall.Checked ? 300 : 3;
            }

            Map.Wall wallc;
            Map.Wall wall;
            Map.Wall OldWall = null;
            string OldWall2 = null;
            wall = WallGet(pt);

            if (TheMap.Walls.ContainsKey(pt))
            {
                OldWall = TheMap.Walls[pt];


                OldWall2 = OldWall.Facing.ToString();
            }
            //Map.Wall OldWall = null;
            
           // if(recur && fix.IsEmpty)
                //OldWall = WallGet(pt);

            
            ArrayList OldWall3 = new ArrayList();

            if (wall != null)
            {
                OldWall3.Add((byte)WallGet(pt).matId);
                OldWall3.Add((byte)WallGet(pt).Variation);
                OldWall3.Add((Map.Wall.WallFacing)WallGet(pt).Facing);
            }
            
           // if(OldWall != null)
           
               // MessageBox.Show(OldWall.Facing.ToString() + " " + OldWall.ToString());
            if (fake) 
            {
                if (!fix.IsEmpty)
                {
                    Map.Wall wallfix = GetWallInList(pt);

                    var wmmf = GetWallInList(new Point(fxOrigin.X - 1, fxOrigin.Y - 1));
                    var wppf = GetWallInList(new Point(fxOrigin.X + 1, fxOrigin.Y + 1));
                    var wpmf = GetWallInList(new Point(fxOrigin.X + 1, fxOrigin.Y - 1));
                    var wmpf = GetWallInList(new Point(fxOrigin.X - 1, fxOrigin.Y + 1));

                    //&& wallfix.Facing != Map.Wall.WallFacing.SE_CORNER && wallfix.Facing != Map.Wall.WallFacing.SW_CORNER && wallfix.Facing != Map.Wall.WallFacing.NE_CORNER && wallfix.Facing != Map.Wall.WallFacing.NW_CORNER
                    if (wallfix != null && wmmf == null && wppf == null && wpmf == null && wmpf == null)
                    {
                        wall = mapView.WallMakeNewCtrl.NewWall(fix, true);
                        if (wall != null && !mapView.MapRenderer.FakeWalls.ContainsKey(wall.Location))
                            mapView.MapRenderer.FakeWalls.Add(wall.Location, wall);
                        pt = fix;
                        wall = wallfix;
                    }
                }
                else if (recur)
                {
                    wall = mapView.WallMakeNewCtrl.NewWall(pt, true);
                    if (wall != null && !mapView.MapRenderer.FakeWalls.ContainsKey(wall.Location))
                        mapView.MapRenderer.FakeWalls.Add(wall.Location, wall);

                }
                wall = GetWallInList(pt);
            }
            else
            {
                //  if (!mapView.WallMakeNewCtrl.smartDraw.Checked)
                //MessageBox.Show("hah");

                if (!fix.IsEmpty)
                {
                    Map.Wall wallfix = WallGet(pt);

                    var wmmf = WallGet(new Point(fxOrigin.X - 1, fxOrigin.Y - 1));
                    var wppf = WallGet(new Point(fxOrigin.X + 1, fxOrigin.Y + 1));
                    var wpmf = WallGet(new Point(fxOrigin.X + 1, fxOrigin.Y - 1));
                    var wmpf = WallGet(new Point(fxOrigin.X - 1, fxOrigin.Y + 1));

                    //&& wallfix.Facing != Map.Wall.WallFacing.SE_CORNER && wallfix.Facing != Map.Wall.WallFacing.SW_CORNER && wallfix.Facing != Map.Wall.WallFacing.NE_CORNER && wallfix.Facing != Map.Wall.WallFacing.NW_CORNER
                    if (wallfix != null && wmmf == null && wppf == null && wpmf == null && wmpf == null)
                    {
                        if (WallPlace(fix))
                        {
                            pt = fix;
                            wall = wallfix;
                            wallc = WallGet(fix);
                            if (!mapView.LastWalls.Contains(wallc))
                                mapView.LastWalls.Add(wallc);//mapView.LastWalls.Insert(0, wallc);
                            if (mapView.LastWalls.Count > maxWallList + 5) mapView.LastWalls.RemoveAt(0);
                        }
                    }
                }
                else if (recur)
                {

                    wallc = WallGet(pt);

                    if (WallPlace(pt, true))
                    {
                        wallc = WallGet(pt);

                    }
                    if (wallc != null)
                    {
                        if (!mapView.LastWalls.Contains(wallc))
                            mapView.LastWalls.Add(wallc);
                        if (mapView.LastWalls.Count > maxWallList) mapView.LastWalls.RemoveAt(0);
                    }
                }
                wall = WallGet(pt);
            }
            Map.Wall wmm;
            Map.Wall wpp;
            Map.Wall wpm;
            Map.Wall wmp;

            if (wall == null || (!GetLastWalls(wall) && mapView.WallMakeNewCtrl.smartDraw.Checked && !fake))
            {

                return wall;

            }
            if (!fake)
            {

                wmm = WallGet(new Point(pt.X - 1, pt.Y - 1));
                wpp = WallGet(new Point(pt.X + 1, pt.Y + 1));
                wpm = WallGet(new Point(pt.X + 1, pt.Y - 1));
                wmp = WallGet(new Point(pt.X - 1, pt.Y + 1));
                if (mapView.WallMakeNewCtrl.smartDraw.Checked)
                {

                    if (wmm != null)
                    {
                        if (!GetLastWalls(wmm)) wmm = null;
                    }
                    if (wpp != null)
                    {
                        if (!GetLastWalls(wpp)) wpp = null;
                    }
                    if (wpm != null)
                    {
                        if (!GetLastWalls(wpm)) wpm = null;
                    }

                    if (wmp != null)
                    {
                        if (!GetLastWalls(wmp)) wmp = null;
                    }
                }
            }
            else
            {
                Map.Wall wallin;
                Point point = new Point(pt.X - 1, pt.Y - 1);
                wmm = mapView.MapRenderer.FakeWalls.TryGetValue(point, out wallin) ? wallin : null;

                point = new Point(pt.X + 1, pt.Y + 1);
                wpp = mapView.MapRenderer.FakeWalls.TryGetValue(point, out wallin) ? wallin : null;

                point = new Point(pt.X + 1, pt.Y - 1);
                wpm = mapView.MapRenderer.FakeWalls.TryGetValue(point, out wallin) ? wallin : null;

                point = new Point(pt.X - 1, pt.Y + 1);
                wmp = mapView.MapRenderer.FakeWalls.TryGetValue(point, out wallin) ? wallin : null;
            }

            if (recur)
            {
                if (fix.IsEmpty)
                {
                    WallAutoBrush(new Point(pt.X + 0, pt.Y + 2), true, fake, new Point(pt.X - 1, pt.Y + 1), pt);
                    WallAutoBrush(new Point(pt.X + 0, pt.Y - 2), true, fake, new Point(pt.X + 1, pt.Y - 1), pt);
                    WallAutoBrush(new Point(pt.X - 2, pt.Y + 0), true, fake, new Point(pt.X - 1, pt.Y + 1), pt);
                    WallAutoBrush(new Point(pt.X + 2, pt.Y + 0), true, fake, new Point(pt.X + 1, pt.Y - 1), pt);
                }
                WallAutoBrush(new Point(pt.X - 1, pt.Y - 1), false, fake);
                WallAutoBrush(new Point(pt.X + 1, pt.Y + 1), false, fake);
                WallAutoBrush(new Point(pt.X + 1, pt.Y - 1), false, fake);
                WallAutoBrush(new Point(pt.X - 1, pt.Y + 1), false, fake);
            }
            
           int seed = Environment.TickCount & Int32.MaxValue;
           Random rnd = new Random((wall.Location.X + wall.Location.Y + (int)DateTime.Now.Ticks + seed));
           int wall_Variation = (byte)rnd.Next((int)mapView.WallMakeNewCtrl.numWallVari.Value, (int)mapView.WallMakeNewCtrl.numWallVariMax.Value + 1);

            if (wmm != null && wpm != null && wmp != null && wpp != null)
            {
                wall.Facing = Map.Wall.WallFacing.CROSS;
                wall.Variation = 0;

                if (OldWall == null)
                {
                    OpUpdatedWalls = true;
                    return wall;//false
                }
                else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
                return wall;//false
            }
            else if (wmm != null && wpm != null && wmp == null)
            {
                if (wpp == null)
                {
                    wall.Facing = Map.Wall.WallFacing.SW_CORNER;
                    wall.Variation = 0;
                }
                else
                {
                    wall.Facing = Map.Wall.WallFacing.EAST_T;
                    
                    wall.Variation = 0;
                }
                if (OldWall == null)
                {
                    OpUpdatedWalls = true;
                    return wall;//false
                }
                else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
                return wall;//false
            }
            else if (wpp != null && wpm != null && wmm == null)
            {
                if (wmp == null)
                {
                    wall.Facing = Map.Wall.WallFacing.NW_CORNER;
                    wall.Variation = 0;
                }
                else
                {
                    wall.Facing = Map.Wall.WallFacing.NORTH_T;
                    wall.Variation = 0;
                }
                if (OldWall == null)
                {
                    OpUpdatedWalls = true;
                    return wall;//false
                }
                else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
                return wall;//false
            }
            else if (wpp != null && wmp != null)
            {
                if (wmm == null)
                {
                    wall.Facing = Map.Wall.WallFacing.NE_CORNER;
                    wall.Variation = 0;
                }
                else
                {
                    wall.Facing = Map.Wall.WallFacing.WEST_T;
                    wall.Variation = 0;
                }
                if (OldWall == null)
                {
                    OpUpdatedWalls = true;
                    return wall;//false
                }
                else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
                return wall;//false
            }
            else if (wmm != null && wmp != null)
            {
                if (wpm == null)
                {
                    wall.Facing = Map.Wall.WallFacing.SE_CORNER;
                    wall.Variation = 0;
                }
                else
                {
                    wall.Facing = Map.Wall.WallFacing.SOUTH_T;
                    wall.Variation = 0;
                }
                if (OldWall == null)
                {
                    OpUpdatedWalls = true;
                    return wall;//false
                }
                else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
                return wall;//false
            }

            // Normal
           // if (OldWall != null)
               // MessageBox.Show("PO RECUR "+OldWall.Facing.ToString() + " " + OldWall.ToString());

            
            if (wmp != null || wmp != null)
            {

                if (wall != null && mapView.WallMakeNewCtrl.smartDraw.Checked || (!recur && OldWall != null))
                {
                    if (wall.Facing == Map.Wall.WallFacing.NE_CORNER ||
                        wall.Facing == Map.Wall.WallFacing.NW_CORNER ||
                        wall.Facing == Map.Wall.WallFacing.SW_CORNER ||
                        wall.Facing == Map.Wall.WallFacing.SE_CORNER)
                    {
                        if (OldWall == null)
                        {
                            OpUpdatedWalls = true;
                            return wall;//false
                        }
                        else if (wall.Facing != OldWall.Facing || wall.Material != OldWall.Material) OpUpdatedWalls = true;
                        return wall;//false
                    }
                }

                //if (OldWall != null)
                   // MessageBox.Show("PO RECUR1 " + OldWall.Facing.ToString() + " " + OldWall.ToString());


                wall.Facing = Map.Wall.WallFacing.NORTH;

               // if (OldWall != null)
                  //  MessageBox.Show("PO RECUR2 " + OldWall.Facing.ToString() + " " + OldWall.ToString());

               // if (OldWall2 != null)
                   // MessageBox.Show("PO RECUR :WALL2:: " + OldWall2);

              //  if (OldWall != null)
                    //MessageBox.Show("PO RECUR :WALL3333:: " + OldWall3[3].ToString());

                if (mapView.WallMakeNewCtrl.wallFacing > 1 && wall.Variation < 1 && !wall.Window && mapView.WallMakeNewCtrl.autovari.Checked && !mapView.WallMakeNewCtrl.started)
                    wall.Variation = (byte)wall_Variation;

               // if (OldWall != null)
                   // MessageBox.Show(OldWall.Facing.ToString() + " " + OldWall.ToString() + " " + wall.Facing.ToString());
                
                
                
                if (OldWall == null)
                {
                    OpUpdatedWalls = true;
                    return wall;//false
                }
                else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
                return wall;//false
            }
            if (wmm != null || wpp != null)
            {
                if (wall != null && mapView.WallMakeNewCtrl.smartDraw.Checked || (!recur && OldWall != null))
                {
                    if (wall.Facing == Map.Wall.WallFacing.NE_CORNER ||
                        wall.Facing == Map.Wall.WallFacing.NW_CORNER ||
                        wall.Facing == Map.Wall.WallFacing.SW_CORNER ||
                        wall.Facing == Map.Wall.WallFacing.SE_CORNER)
                    {

                        if (OldWall == null)
                        {
                            OpUpdatedWalls = true;
                            return wall;//false
                        }
                        else if (wall.Facing != OldWall.Facing || wall.Material != OldWall.Material) OpUpdatedWalls = true;
                        return wall;//false
                    }
                }
                wall.Facing = Map.Wall.WallFacing.WEST;
                if (mapView.WallMakeNewCtrl.wallFacing > 1 && wall.Variation < 1 && !wall.Window && mapView.WallMakeNewCtrl.autovari.Checked && !mapView.WallMakeNewCtrl.started)
                    wall.Variation = (byte)wall_Variation;
                
                
               // if (OldWall != null)
                   // MessageBox.Show(OldWall.Facing.ToString() + " " + OldWall.ToString() + " " + wall.Facing.ToString());
                if (OldWall == null)
                {
                    OpUpdatedWalls = true;
                    return wall;//false
                }
                else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
                return wall;//false
            }
            if (OldWall == null)
            {
                OpUpdatedWalls = true;
                return wall;//false
            }
            else if (wall.Facing != (Map.Wall.WallFacing)OldWall3[2] || wall.matId != (byte)OldWall3[0] || (byte)OldWall3[1] != wall.Variation) OpUpdatedWalls = true;
            return wall;//false
        }


        #endregion

        #region Floor operations

        public static bool FloorPlace(int x, int y)
        {
            return FloorPlace(new Point(x, y));
        }

        public static bool FloorPlace(Point pt)
        {
            bool added = false;
            if (pt.X < 0 || pt.Y < 0 || pt.X > 252 || pt.Y > 252) return false;
            if (mapView.TileMakeNewCtrl.BrushSize.Value >= 2)
            {
                Point tilePt = new Point();
                tilePt.X = pt.X;
                tilePt.Y = pt.Y;
                Point pat = new Point();
                int i = 0;

                int cols = (int)mapView.TileMakeNewCtrl.BrushSize.Value;
                int rows = cols;//(int)mapView.TileMakeNewCtrl.BrushSize.Value;
                for (pat = tilePt; i < rows; i++, pat.X--, pat.Y++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Point pat2 = new Point();
                        pat2 = pat;
                        pat2.X += j * 1;
                        pat2.Y += j * 1;
                        pat2.Y -= ((cols - 1) + ((cols % 2 == 0) ? 1 : 0));
                        //pat2.Y -= 2;
                        //(re)place tile + auto edge + auto vari

                        if (pat2.X < 1 || pat2.Y < 1 || pat2.X > 251 || pat2.Y > 251) continue;
                        Map.Tile newTile = mapView.TileMakeNewCtrl.GetTile(pat2);
                        if (TheMap.Tiles.ContainsKey(pat2))
                        {
                            if (!AllowPlaceOverride) continue;
                            if (TheMap.Tiles[pat2].Variation == newTile.Variation && TheMap.Tiles[pat2].graphicId == newTile.graphicId && TheMap.Tiles[pat2].EdgeTiles.Count == 0) continue;
                            TheMap.Tiles.Remove(pat2);
                        }
                        TheMap.Tiles.Add(pat2, newTile);
                        added = true;
                    }
                }
                if (added)
                {
                    OpUpdatedTiles = true;
                    return true;
                }
            }
            else
            {
                Map.Tile newTile = mapView.TileMakeNewCtrl.GetTile(pt);
                if (TheMap.Tiles.ContainsKey(pt))
                {
                    if (!AllowPlaceOverride) return false;
                    if (TheMap.Tiles[pt].Variation == newTile.Variation && TheMap.Tiles[pt].graphicId == newTile.graphicId && TheMap.Tiles[pt].EdgeTiles.Count == 0) return false;
                    TheMap.Tiles.Remove(pt);
                }

                TheMap.Tiles.Add(pt, mapView.TileMakeNewCtrl.GetTile(pt));
                OpUpdatedTiles = true;
                return true;
            }

            return false;
        }

        public static bool FloorRemove(int x, int y)
        {
            return FloorRemove(new Point(x, y));
        }

        public static bool FloorRemove(Point pt)
        {
            bool removed = false;
            if (CurrentMode == EditMode.FLOOR_BRUSH || CurrentMode == EditMode.FLOOR_PLACE)
            {
                // Remove multiple tiles if Auto Brush is enabled
                if (mapView.TileMakeNewCtrl.BrushSize.Value >= 2)
                {
                    Point pat = new Point();
                    int i = 0;

                    int cols = (int)mapView.TileMakeNewCtrl.BrushSize.Value;
                    int rows = (int)mapView.TileMakeNewCtrl.BrushSize.Value;
                    for (pat = pt; i < rows; i++, pat.X--, pat.Y++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            Point pat2 = new Point();
                            pat2 = pat;
                            pat2.X += j * 1;
                            pat2.Y += j * 1;
                            pat2.Y -= ((cols - 1) + ((cols % 2 == 0) ? 1 : 0));
                            //pat2.Y -= 2;

                            if (TheMap.Tiles.ContainsKey(pat2))
                            {
                                TheMap.Tiles[pat2].EdgeTiles.Clear();
                                TheMap.Tiles.Remove(pat2);
                                removed = true;
                            }
                        }
                    }
                    if (removed)
                    {
                        OpUpdatedTiles = true;
                        return true;
                    }
                }
            }
            // Remove singular tile
            if (TheMap.Tiles.ContainsKey(pt))
            {
                TheMap.Tiles[pt].EdgeTiles.Clear();
                TheMap.Tiles.Remove(pt);
                OpUpdatedTiles = true;
                return true;
            }
            return false;
        }

        public static bool FloorAutoBrush(int x, int y)
        {
            var edge = mapView.EdgeMakeNewCtrl.GetEdge();
            // TODO: move stuff from MapHelper to this class
            _instance._mapHelper.SetTileMaterial(ThingDb.FloorTileNames[edge.Graphic]);
            _instance._mapHelper.SetEdgeMaterial(ThingDb.EdgeTileNames[edge.Edge]);

            if (mapView.TileMakeNewCtrl.BrushSize.Value >= 2)
            {
                Point tilePt = new Point();
                tilePt.X = x;
                tilePt.Y = y;
                Point pat = new Point();
                int i = 0;

                int cols = (int)mapView.TileMakeNewCtrl.BrushSize.Value;
                int rows = (int)mapView.TileMakeNewCtrl.BrushSize.Value;
                for (pat = tilePt; i < rows; i++, pat.X--, pat.Y++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Point pat2 = new Point();
                        pat2 = pat;
                        pat2.X += j * 1;
                        pat2.Y += j * 1;
                        pat2.Y -= ((cols - 1) + ((cols % 2 == 0) ? 1 : 0));
                        // pat2.Y -= 2;
                        //(re)place tile + auto edge + auto vari
                        if (!(pat2.X < 0 || pat2.Y < 0 || pat2.X > 252 || pat2.Y > 252))
                        {
                            // _instance._mapHelper.RemoveTile(pat2.X, pat2.Y);
                            _instance._mapHelper.PlaceTile(pat2.X, pat2.Y);
                            _instance._mapHelper.BrushAutoBlend(pat2.X, pat2.Y);
                        }


                    }
                }
            }
            else
            {
                //_instance._mapHelper.RemoveTile(x, y);
                _instance._mapHelper.PlaceTile(x, y);
                _instance._mapHelper.BrushAutoBlend(x, y);
            }
            //  OpUpdatedTiles = true;
            return true;
        }

        public static bool FloorAutoBrush(Point pt)
        {
            return FloorAutoBrush(pt.X, pt.Y);
        }
        #endregion

        #region Edge operations
        public static bool EdgePlace(int x, int y)
        {
            return EdgePlace(new Point(x, y));
        }

        public static bool EdgePlace(Point pt)
        {
            if (TheMap.Tiles.ContainsKey(pt))
            {
                Map.Tile.EdgeTile edge = mapView.EdgeMakeNewCtrl.GetEdge();
                if (MainWindow.Instance.mapView.EdgeMakeNewCtrl.AutoEgeBox.Checked)
                {
                    _instance._mapHelper.SetTileMaterial(ThingDb.FloorTileNames[edge.Graphic]);
                    _instance._mapHelper.SetEdgeMaterial(ThingDb.EdgeTileNames[edge.Edge]);
                    _instance._mapHelper.AutoEdge(pt);
                }
                else
                {
                    Map.Tile tile = TheMap.Tiles[pt];


                    foreach (Map.Tile.EdgeTile ex in tile.EdgeTiles)
                    {
                        // don't create edges with equal EdgeType, Direction and CoverTile
                        if (ex.Edge == edge.Edge && ex.Dir == edge.Dir && ex.Graphic == edge.Graphic) return false;
                    }

                    tile.EdgeTiles.Add(edge);
                }

                OpUpdatedTiles = true;
                return true;
            }
            return false;
        }
        public static bool EdgeRemove(int x, int y)
        {
            return EdgeRemove(new Point(x, y));
        }

        public static bool EdgeRemove(Point pt)
        {
            if (TheMap.Tiles.ContainsKey(pt))
            {

                Map.Tile tile = TheMap.Tiles[pt];
                if (tile.EdgeTiles.Count <= 0) return false;
                byte edgeTypeID = mapView.EdgeMakeNewCtrl.GetEdge().Edge;
                // filter edges with specific type (selected)
                ArrayList newlist = new ArrayList();
                if (!KeyHelper.ShiftKey)
                {

                    int i = tile.EdgeTiles.Count - 1;
                    foreach (Map.Tile.EdgeTile edge in tile.EdgeTiles)
                    {
                        if (i > 0) newlist.Add(edge);
                        i--;
                    }
                    tile.EdgeTiles = newlist;
                }
                else
                {
                    foreach (Map.Tile.EdgeTile edge in tile.EdgeTiles)
                    {
                        if (edge.Edge != edgeTypeID) newlist.Add(edge);
                    }
                    tile.EdgeTiles = newlist;
                }

                OpUpdatedTiles = true;
                return true;
            }
            return false;
        }
        #endregion

        private MapObjectCollection _SelectedObjects = new MapObjectCollection();

        public static MapObjectCollection SelectedObjects
        {
            get
            {
                return _instance._SelectedObjects;
            }
        }

        #region Object operations
        public static int FindUnusedExtent()
        {
            int result = 3; // 2 = host player
            while (result != int.MaxValue)
            {
                bool found = false;

                // check if there are no objects with this extent
                foreach (Map.Object obj in TheMap.Objects)
                {
                    if (obj.Extent == result)
                    {
                        found = true; break;
                    }
                }

                if (found) result++;
                else break; // found unused
            }
            return result;
        }

        public static Map.Object ObjectPlace(string type, float x, float y)
        {
            OpUpdatedObjects = true;
            return ObjectPlace(type, new PointF(x, y));
        }

        public static Map.Object ObjectPlace(string type, PointF loc)
        {
            if (!ThingDb.Things.ContainsKey(type)) return null;

            Map.Object result = new Map.Object();
            result.Name = type;
            result.Location = loc;
            result.Extent = FindUnusedExtent();

            // смотрим нету ли редактора, устанавливаем стандартные значения
            XferEditor editor = XferEditors.GetEditorForXfer(ThingDb.Things[type].Xfer);
            if (editor != null) editor.SetDefaultData(result);
            else result.NewDefaultExtraData();


            if (ThingDb.Things[type].Xfer == "DoorXfer")
            {
                int dorDir = (int)mapView.delta;
                NoxShared.ObjDataXfer.DoorXfer door = result.GetExtraData<NoxShared.ObjDataXfer.DoorXfer>();
                door.Direction = (NoxShared.ObjDataXfer.DoorXfer.DOORS_DIR)dorDir;
            }
            else if (ThingDb.Things[type].Xfer == "MonsterXfer")
            {
                int dir = (int)mapView.delta;
                NoxShared.ObjDataXfer.MonsterXfer m = result.GetExtraData<NoxShared.ObjDataXfer.MonsterXfer>();
                m.DirectionId = (byte)dir;
            }
            else if (ThingDb.Things[type].Xfer == "NPCXfer")
            {
                int dir = (int)mapView.delta;
                NoxShared.ObjDataXfer.NPCXfer npc = result.GetExtraData<NoxShared.ObjDataXfer.NPCXfer>();
                npc.DirectionId = (byte)dir;
            }
            else if (ThingDb.Things[type].Xfer == "SentryXfer")
            {
                float dir = mapView.delta;
                NoxShared.ObjDataXfer.SentryXfer s = result.GetExtraData<NoxShared.ObjDataXfer.SentryXfer>();
                s.BasePosRadian = (float)dir;
            }


            TheMap.Objects.Add(result);
            return result;
        }

        public static bool ObjectRemove(Map.Object obj)
        {
            if (TheMap.Objects.Contains(obj))
            {
                TheMap.Objects.Remove(obj);
                OpUpdatedObjects = true;
                return true;
            }
            return false;
        }
        public static void ObjectSelect45Rectangle(Point pt)
        {

            if (SelectedObjects.Origin != null)
               return;

            Point MousePoint = mapView.mouseKeep;
            if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT && !mapView.picking && !MousePoint.IsEmpty)
            {
                if (mapView.select45Box.Checked)
                    pt = Rotate(pt, MousePoint, -45);

                Point a = MousePoint;
                Point b = new Point(pt.X, MousePoint.Y);
                Point c = pt;
                Point d = new Point(MousePoint.X, pt.Y);

                if (mapView.select45Box.Checked)
                {
                    b = Rotate(b, a, 45);
                    c = Rotate(c, a, 45);
                    d = Rotate(d, a, 45);
                }
                selected45Area[0] = a;
                selected45Area[1] = b;
                selected45Area[2] = c;
                selected45Area[3] = d;
                //int size = Math.Abs(MapInterface.selected45Area[0].X - MapInterface.selected45Area[2].X);
                if (mapView.Get45RecSize() >= 5)
                    ObjectSelect(pt);
            }
        }
        /*
        public static void ObjectSelectRectangle(Point pt)
        {
            Point MouseKeep = mapView.mouseKeep;
            if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT && !mapView.picking && !MouseKeep.IsEmpty)
            {
                if (selectedArea.IsEmpty && SelectedObjects.Items.Count > 0 && !KeyHelper.ShiftKey)
                    return;

                selectedArea = new Rectangle((int)(Math.Min(MouseKeep.X, pt.X)),
                (int)(Math.Min(MouseKeep.Y, pt.Y)),
                (int)(Math.Abs(MouseKeep.X - pt.X)),
                (int)(Math.Abs(MouseKeep.Y - pt.Y)));

                if (selectedArea.Height >= 3 || selectedArea.Width >= 3)
                    ObjectSelect(pt, selectedArea);
            }


        }
        */
        public bool ContainsTile(Point tilePoint)
        {
            int panelVisibleH = mapView.scrollPanel.Height;
            int panelVisibleW = mapView.scrollPanel.Width;
            Rectangle visibleArea = new Rectangle(-mapView.mapPanel.Location.X - 48, -mapView.mapPanel.Location.Y - 48, panelVisibleW + 48, panelVisibleH + 48);
            foreach (Point tila in TheMap.Tiles.Keys)
            {
                if (!visibleArea.Contains(tila.X, tila.Y)) continue;


                if (tilePoint.Equals(tila))
                    return true;

            }
            return false;
        }

        public static Map.Object ObjectSelect(Point pt)
        {
            double closestDistance = Double.MaxValue;
            Map.Object closest = null;
            int panelVisibleH = mapView.scrollPanel.Height;
            int panelVisibleW = mapView.scrollPanel.Width;
            Rectangle visibleArea = new Rectangle(-mapView.mapPanel.Location.X - 48, -mapView.mapPanel.Location.Y - 48, panelVisibleW + 48, panelVisibleH + 48);

            foreach (Map.Object obj in TheMap.Objects)
            {

                int x = (int)obj.Location.X;
                int y = (int)obj.Location.Y;

                if (!visibleArea.Contains(x, y)) continue;

                double distance = Math.Pow(pt.X - obj.Location.X, 2) + Math.Pow(pt.Y - obj.Location.Y, 2);
                ThingDb.Thing tt = ThingDb.Things[obj.Name];
                PointF center = obj.Location;
                int radius = 0;
                bool hitTest = false;
                int Zsize = tt.ZSizeY;
                int ExtentX = tt.ExtentX;
                int ExtentY = tt.ExtentY;
                if (!EditorSettings.Default.Edit_PreviewMode && tt.DrawType != "TriggerDraw" && tt.DrawType != "PressurePlateDraw")
                {
                    Point topLeft = new Point((int)center.X - 8, (int)center.Y - 8);
                    Rectangle smallRec = new Rectangle(topLeft, new Size(2 * 8, 2 * 8));//55
                    

                    if (mapView.Get45RecSize() >= 5)
                    {

                        if (PointInPolygon(new Point((int)center.X, (int)center.Y), selected45Area) && !RecSelected.Contains(obj))
                            RecSelected.Add(obj);
                        else
                        {
                            for (int i = 0; i <= 2; i++)
                            {
                                if (LineIntersectsRect(selected45Area[i], selected45Area[i + 1], smallRec))
                                {
                                    if (!RecSelected.Contains(obj))
                                    {
                                        RecSelected.Add(obj);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        hitTest = smallRec.Contains(pt);
                    }
                }
                else
                {
                    if (tt.ExtentType == "CIRCLE")
                    {
                        PointF t = new PointF(center.X - ExtentX, center.Y - tt.ExtentX);
                        PointF p = new PointF((center.X) - ExtentX, (center.Y - Zsize) - ExtentX);
                        radius = tt.DrawType == "DoorDraw" ? (int)selectRadius * 3 : (ExtentX * ExtentX);
                        Rectangle r1 = new Rectangle((int)t.X, (int)t.Y - (Zsize * 1), ExtentX * 2, Zsize + ExtentX);
                        //hitTest = tt.ExtentX <= 12 ? r1.Contains(pt) : false;


                        if (mapView.Get45RecSize() >= 5)
                        {
                            if (PointInPolygon(new Point((int)center.X, (int)center.Y), selected45Area) && !RecSelected.Contains(obj))
                                RecSelected.Add(obj);
                            else
                            {
                                for (int i = 0; i <= 2; i++)
                                {
                                    if (LineIntersectsRect(selected45Area[i], selected45Area[i + 1], r1))
                                    {
                                        if (!RecSelected.Contains(obj))
                                        {
                                            RecSelected.Add(obj);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            hitTest = tt.ExtentX <= 15 ? r1.Contains(pt) : tt.ZSizeY >= 25 ? r1.Contains(pt) : false;

                            if (tt.ExtentX <= 15)
                                hitTest = r1.Contains(pt);
                            else if (tt.ZSizeY >= 20 && tt.ExtentX <= 30)
                                hitTest = r1.Contains(pt);
                            else
                                hitTest = false;

                        }
                    }
                    else
                    {
                        /////////////////////////////////////////////////////////////úúú
                        Point t = new Point((int)(center.X - (tt.ExtentX / 2)), (int)(center.Y - (tt.ExtentY / 2)));
                        Point p = new Point((int)((center.X - (Zsize / 2)) - (tt.ExtentX / 2)), (int)((center.Y - (Zsize / 2)) - (tt.ExtentY / 2)));
                        if (tt.DrawType == "TriggerDraw" || tt.DrawType == "PressurePlateDraw")
                        {
                            if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.TRIGGER))
                            {
                                
                                NoxShared.ObjDataXfer.TriggerXfer trigger = obj.GetExtraData<NoxShared.ObjDataXfer.TriggerXfer>();

                                t = new Point((int)(center.X - (trigger.SizeX / 2)), (int)(center.Y - (trigger.SizeY / 2)));
                                p = new Point((int)((center.X - (Zsize / 2)) - (trigger.SizeX / 2)), (int)((center.Y - (Zsize / 2)) - (trigger.SizeY / 2)));
                                ExtentY = trigger.SizeY;
                                ExtentX = trigger.SizeX;
                            }
                        }
                        
                        Point[] pointss = new Point[6];
                        Point point1 = new Point(t.X, t.Y);
                        Point point2 = new Point(p.X, p.Y);
                        pointss[0] = Rotate(point2, center, 45);
                        point1 = new Point(t.X, t.Y);
                        point2 = new Point(p.X, p.Y);
                        point1.Y += ExtentY;
                        point2.Y += ExtentY;

                        pointss[1] = Rotate(point2, center, 45);
                        pointss[2] = Rotate(point1, center, 45);

                        point1 = new Point(t.X, t.Y);
                        point2 = new Point(p.X, p.Y);
                        point1.X += ExtentX;
                        point2.X += ExtentX;

                        pointss[4] = Rotate(point1, center, 45);
                        pointss[5] = Rotate(point2, center, 45);

                        point1 = new Point(t.X, t.Y);
                        point2 = new Point(p.X, p.Y);
                        point1.X += ExtentX;
                        point2.X += ExtentX;

                        point1.Y += ExtentY;
                        point2.Y += ExtentY;

                        pointss[3] = Rotate(point1, center, 45);

                        if (mapView.Get45RecSize() >= 5)
                        {

                            if (PointInPolygon(new Point((int)center.X, (int)center.Y), selected45Area) && !RecSelected.Contains(obj))
                                RecSelected.Add(obj);
                            else
                            {
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (LineIntersectsPolygon(pointss[i], pointss[i + 1], selected45Area) && !RecSelected.Contains(obj))
                                    {
                                        RecSelected.Add(obj);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                            hitTest = PointInPolygon(pt, pointss);
                    }
                }
                /////////////////////////////////////////////////////////////////

                if ((distance < selectRadius || (distance < radius) || hitTest) && distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = obj;
                }
            }

            return closest;
        }

        public static bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }
        public static bool LineIntersectsPolygon(Point p1, Point p2, Point[] polygon)
        {
            for (int i = 0; i <= polygon.Length - 2; i++)
            {
                if (LineIntersectsLine(polygon[i], polygon[i + 1], p1, p2))
                    return true;

            }

            return false;

        }
        private static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }

        public static Point Rotate(Point point, PointF pivot, double angleSet)
        {
            double angle = angleSet * Math.PI / 180;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            float dx = point.X - pivot.X;
            float dy = point.Y - pivot.Y;
            double x = cos * dx - sin * dy + pivot.X;
            double y = sin * dx + cos * dy + pivot.Y;

            Point result = new Point((int)Math.Round(x), (int)Math.Round(y));
            return result;
        }
        /*
        public static Boolean checkPointInDoor(int rx, int ry, int rw, int rh, int px, int py)
        {

            double rotRad = (Math.PI * 45) / 180;
            int dx = px - rx;
            int dy = py - ry;
            double h1 = Math.Sqrt(dx * dx + dy * dy);

            double currA = Math.Atan2(dy, dx);
            double newA = currA - rotRad;
            double x2 = Math.Cos(newA) * h1;
            double y2 = Math.Sin(newA) * h1;

            if (x2 > -0.5 * rw && x2 < 0.5 * rw && y2 > -0.5 * rh && y2 < 0.5 * rh)
                return true;

            return false;

        }
        */
        public static bool PointInPolygon(Point p, Point[] poly)
        {
            Point p1, p2;
            bool inside = false;
            if (poly.Length < 3)
            {
                return inside;
            }
            Point oldPoint = new Point(
            poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                Point newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                && ((long)p.Y - (long)p1.Y) * (long)(p2.X - p1.X)
                 < ((long)p2.Y - (long)p1.Y) * (long)(p.X - p1.X))
                {
                    inside = !inside;
                }
                oldPoint = newPoint;
            }

            return inside;
        }

        #endregion

        private Map.Waypoint _SelectedWaypoint = null;

        public static Map.Waypoint SelectedWaypoint
        {
            get
            {
                return _instance._SelectedWaypoint;
            }
            set
            {
                _instance._SelectedWaypoint = value;
            }
        }

        #region Waypoint operations
        const byte WaypointFlag = 128;
        const double selectRadius = MapView.objectSelectionRadius * MapView.objectSelectionRadius;

        public static Map.Waypoint WaypointSelect(Point pt)
        {
            double closestDistance = Double.MaxValue;
            Map.Waypoint closest = null;

            foreach (Map.Waypoint wp in TheMap.Waypoints)
            {
                double distance = Math.Pow(pt.X - wp.Point.X, 2) + Math.Pow(pt.Y - wp.Point.Y, 2);

                if (distance < selectRadius && distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = wp;
                }
            }

            return closest;
        }

        public static void WaypointEnable()
        {
            //int count = TheMap.Waypoints.Count - 1;
            //int inx = 0;

            foreach (Map.Waypoint wp in TheMap.Waypoints)
            {

                wp.connections.Clear();

                /*
                if (count == inx)
                {
                    WaypointRemove(wp);
                    return;
                }
                inx++;
                */
                // wp.Flags = 1;
            }
            TheMap.Waypoints.num_wp.Clear();
            TheMap.Waypoints.Clear();
        }



        public static bool WaypointRemove(Map.Waypoint wp)
        {
            if (TheMap.Waypoints.Contains(wp))
            {
                if (wp == SelectedWaypoint) SelectedWaypoint = null;
                TheMap.Waypoints.Remove(wp);
                TheMap.Waypoints.num_wp.Remove(wp.Number);
                OpUpdatedWaypoints = true;
                return true;
            }
            return false;
        }

        public static Map.Waypoint WaypointPlace(string name, PointF loc, bool enabled)
        {
            int i;
            for (i = 1; TheMap.Waypoints.num_wp.ContainsKey(i); i++) ;
            Map.Waypoint wp = new Map.Waypoint("", loc, i);
            wp.Flags = enabled ? 1 : 0;
            TheMap.Waypoints.Add(wp);
            TheMap.Waypoints.num_wp.Add(wp.Number, wp);
            OpUpdatedWaypoints = true;
            return wp;
        }

        public static bool WaypointConnect(Map.Waypoint wp, Map.Waypoint proxyWP = null)
        {

            Map.Waypoint destWaypoint = proxyWP == null ? SelectedWaypoint : proxyWP;

            if (wp != null && destWaypoint != null && !wp.Equals(destWaypoint))
            {
                bool ok = true;

                foreach (Map.Waypoint.WaypointConnection wpc in wp.connections)
                {

                    foreach (Map.Waypoint.WaypointConnection wpcs in destWaypoint.connections)//Checks if the waypoint connection is connecting to wp
                    {
                        if (wpcs.wp.Equals(wp))
                        {
                            ok = false;
                            break;
                        }
                    }
                }

                if (ok)
                {
                    mapView.ApplyStore();
                    destWaypoint.AddConnByNum(wp, WaypointFlag);
                    OpUpdatedWaypoints = true;
                   // MessageBox.Show("sdsd");

                }
                if (mapView.doubleWp.Checked && proxyWP == null)
                {
                    WaypointConnect(SelectedWaypoint, wp);
                }
                if (ok) return true;

            }
            return false;
        }

        public static bool WaypointUnconnect(Map.Waypoint wp)
        {


            if (wp != null && SelectedWaypoint != null && !wp.Equals(SelectedWaypoint))
            {
                bool ok = false;
                foreach (Map.Waypoint.WaypointConnection wpc in wp.connections)
                {

                    if (wpc.wp.Equals(SelectedWaypoint))
                    {
                        ok = true;
                        break;
                    }

                }

                if (ok)
                {
                    mapView.ApplyStore();
                    wp.RemoveConnByNum(SelectedWaypoint);
                    OpUpdatedWaypoints = true;
                    return true;
                }

            }
            return false;
        }
        #endregion

        public static int PolyPointSelect(Point pt)
        {
            double closestDistance = Double.MaxValue;
            int i = 0;
            int page = MainWindow.Instance.tabControl1.SelectedIndex;

            double selRadius = (page == 1 ? 1500 : selectRadius);
            //MessageBox.Show(selRadius.ToString());
            foreach (Map.Polygon poly in TheMap.Polygons)
            {

                if (poly == MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon)
                {

                    foreach (PointF points in poly.Points)
                    {
                        i++;
                        double distance = Math.Pow(pt.X - points.X, 2) + Math.Pow(pt.Y - points.Y, 2);

                        if (distance < selRadius && distance < closestDistance)
                        {

                            closestDistance = distance;
                            SelectedPolyPoint = points;
                            return i - 1;
                        }
                    }
                }
            }
            SelectedPolyPoint = new PointF();
            return 0;
        }
        public static PointF PolyPointSnap(Point pt)
        {
            double closestDistance = Double.MaxValue;
            int page = MainWindow.Instance.tabControl1.SelectedIndex;
            Map.Polygon SelectedPolygon = MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon;
            double selRadius = (page == 1 ? 100 : 200);
            List<PointF> pointsMini = new List<PointF>();
            foreach (Map.Polygon poly in TheMap.Polygons)
            {
                
                if (poly != SelectedPolygon)
                {

                    foreach (PointF points in poly.Points)
                    {
                        PointF center2 = new Point();
                        PointF center = points;
                        if (page == 1)
                        {
                            int mapZoom = MainWindow.Instance.mapZoom;
                            int squareSize = MapView.squareSize;
                            float pointX = (points.X / squareSize) * mapZoom;
                            float pointY = (points.Y / squareSize) * mapZoom;
                            PointF SelectedPolyPointMini = new PointF((SelectedPolyPoint.X / squareSize) * mapZoom, (SelectedPolyPoint.Y / squareSize) * mapZoom);
                            center = new PointF(pointX, pointY);
                            pointsMini.Add(center);
                            if (center == SelectedPolyPointMini && Array.IndexOf(pointsMini.ToArray(), center) == -1) continue;
                            center2 = new PointF((center.X * 2) * squareSize / (mapZoom * 2), (center.Y * 2) * squareSize / (mapZoom * 2));
                        }
                        else if (points == SelectedPolyPoint && Array.IndexOf(SelectedPolygon.Points.ToArray(), points) == -1) continue;

                        double distance = Math.Pow(pt.X - center.X, 2) + Math.Pow(pt.Y - center.Y, 2);

                        if (distance < selRadius && distance < closestDistance)
                        {
                            closestDistance = distance;

                            return page == 1 ? center2 : center;
                        }
                    }
                }
            }

            return new Point();
        }

        /// <summary>
        /// Marks that latest operation has modified some tiles on the map
        /// </summary>
        public static bool OpUpdatedTiles = false;
        public static bool OpUpdatedWalls = false;
        public static bool OpUpdatedWaypoints = false;
        public static bool OpUpdatedPolygons = false;
        /// <summary>
        /// Marks that latest operation has modified some objects on the map
        /// </summary>
        public static bool OpUpdatedObjects = false;

        public static void ResetUpdateTracker()
        {
            OpUpdatedObjects = false;
            OpUpdatedTiles = false;
            OpUpdatedWalls = false;
            OpUpdatedWaypoints = false;
            OpUpdatedPolygons = false;
        }

        public static void HandleLMouseClick(Point pt)
        {
            Point wallPt = MapView.GetNearestWallPoint(pt);
            Point tilePt = MapView.GetNearestTilePoint(pt);
            Point pt2 = pt;
            pt2.Y += ((MainWindow.Instance.mapZoom * 2));
            pt2.X += 2;
            Point wallPt2 = MapView.GetNearestWallPoint(pt2);
            // Perform an action depending on current editing mode.
            switch (CurrentMode)
            {
                case EditMode.WALL_PLACE:
                    WallPlace(wallPt);
                    break;
                case EditMode.WALL_CHANGE:
                    WallChange(wallPt);
                    break;
                case EditMode.WALL_BRUSH:

                    if (!mapView.WallMakeNewCtrl.LineWall.Checked && !mapView.WallMakeNewCtrl.RecWall.Checked)
                        WallAutoBrush(wallPt, true);
                    break;
                case EditMode.FLOOR_PLACE:
                    FloorPlace(tilePt);
                    break;
                case EditMode.FLOOR_BRUSH:
                    FloorAutoBrush(tilePt);
                    break;
                case EditMode.EDGE_PLACE:
                    EdgePlace(tilePt);
                    break;
                case EditMode.OBJECT_PLACE:
                    ObjectPlace(mapView.cboObjCreate.Text, pt.X, pt.Y);
                    break;
                case EditMode.OBJECT_SELECT:
                    var obj = ObjectSelect(pt);
                    if (obj != null)
                    {
                        if (!SelectedObjects.Items.Contains(obj))
                        {
                            // clear selection if not multiselecting
                            if (!KeyHelper.ShiftKey) SelectedObjects.Items.Clear();
                            // put into selection
                            SelectedObjects.Items.Add(obj);
                        }
                        else if (KeyHelper.ShiftKey)
                            mapView.DeletefromSelected(obj);
                        SelectedObjects.Origin = obj;
                    }
                    else
                    {
                        if (!KeyHelper.ShiftKey)
                            SelectedObjects.Items.Clear();
                        SelectedObjects.Origin = null;
                    }
                    break;
                case EditMode.WAYPOINT_PLACE:
                    mapView.waypointName.Text = "";
                    WaypointPlace(mapView.waypointName.Text, new PointF(pt.X, pt.Y), mapView.waypointEnabled.Checked);
                    
                    break;
                case EditMode.WAYPOINT_CONNECT:
                case EditMode.WAYPOINT_SELECT:
                    // Connect previously selected waypoint and one that is under cursor currently
                    if (CurrentMode == EditMode.WAYPOINT_CONNECT)
                    {

                        // if (SelectedWaypoint == null)
                        //SelectedWaypoint = WaypointSelect(pt);
                        if (KeyHelper.ShiftKey) // Shift unconnects (reverse)
                            WaypointUnconnect(WaypointSelect(pt));
                        else
                            WaypointConnect(WaypointSelect(pt));
                    }
                    // Mark waypoint under cursor as selected, or reset
                    SelectedWaypoint = WaypointSelect(pt);
                    if (SelectedWaypoint != null)
                    {
                        // update info box
                        mapView.waypointName.Text = SelectedWaypoint.Name;
                        mapView.waypointEnabled.Checked = SelectedWaypoint.Flags > 0;
                    }
                    break;
            }
        }

        public static void HandleRMouseClick(Point pt)
        {
            Point wallPt = MapView.GetNearestWallPoint(pt);
            Point tilePt = MapView.GetNearestTilePoint(pt);

            switch (CurrentMode)
            {

                case EditMode.WALL_CHANGE:
                    WallChange(wallPt, true);
                    break;
                case EditMode.FLOOR_BRUSH:
                    FloorRemove(tilePt);
                    break;
                case EditMode.WALL_BRUSH:
                    if (!mapView.WallMakeNewCtrl.LineWall.Checked && !mapView.WallMakeNewCtrl.RecWall.Checked)
                        WallRemove(wallPt);
                    break;
                case EditMode.FLOOR_PLACE:
                    FloorRemove(tilePt);
                    break;
                case EditMode.WALL_PLACE:
                    WallRemove(wallPt);
                    break;
                case EditMode.OBJECT_PLACE:
                    var obj = ObjectSelect(pt);
                    if (obj != null) ObjectRemove(obj);
                    break;
                case EditMode.WAYPOINT_PLACE:
                    var way = WaypointSelect(pt);
                    if (way != null) WaypointRemove(way);
                    break;
                case EditMode.EDGE_PLACE:
                    EdgeRemove(tilePt);
                    break;

            }
        }
        /*
        public void PerformOperation(Operation o)
        {
             add to history
            if (history.Count >= HISTORY_LIMIT) RemoveLast(history);
            history.Enqueue(o);
            // execute implementation
            o.Perform(this);
        }
		
        private void RemoveLast(Queue<Operation> q)
        {
            var first = q.Peek();
            Operation current = null;
            while (true) {
                current = q.Dequeue();
                if (q.Peek() == first) {
                    break;
                }
                q.Enqueue(current);
            }
        }
        */
    }

    public interface Operation
    {
        bool Perform(MapInterface i);
        void Undo(MapInterface i);
    }
}