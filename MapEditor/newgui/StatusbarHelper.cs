/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 29.12.2015
 */
using System;
using System.Drawing;
using NoxShared;
using MapEditor.MapInt;

namespace MapEditor.newgui
{
    /// <summary>
    /// Helper class tracking statusbar values.
    /// </summary>
    public class StatusbarHelper
    {
        Map map
        {
            get
            {
                return MapInterface.TheMap;
            }
        }

        string statusLocation;
        /// <summary>
        /// Workaround to prevent flickering.
        /// </summary>
        bool updateStatusbars = false;
        string statusMapItem;
        string statusPolygon;

        public string StatusLocation
        {
            get
            {
                return statusLocation;
            }
        }

        public string StatusMapItem
        {
            get
            {
                return statusMapItem;
            }
        }

        public string StatusPolygon
        {
            get
            {
                return statusPolygon;
            }
        }

        const int squareSize = MapView.squareSize;
        // Format strings
        const string FORMAT_COORDINATES = "X={0} Y={1}";
        const string FORMAT_WALL_INFO = "{0} #{1}";
        const string FORMAT_TILE_INFO = "{0}-0x{1:x2}";
        const string FORMAT_EDGE_COUNT = " Edges({0}):";
        const string FORMAT_EDGE_INFO = " {0}-0x{1:x2}-{2}-{3}";
        const string FORMAT_OBJECT_INFO = "ThingId: {0} Ext: {1}";

        // Used for tracking changes
        Map.Wall prevWall = null;
        Map.Tile prevTile = null;
        Map.Object prevObj = null;
        Map.Polygon prevPoly = null;
        int prevEdge = -1;
        int edgeCount = 0;
        public void Update(Point mousePt)
        {

            bool iWalls = (MapInterface.CurrentMode >= EditMode.WALL_PLACE && MapInterface.CurrentMode <= EditMode.WALL_CHANGE);
            bool iTiles = (MapInterface.CurrentMode >= EditMode.FLOOR_PLACE && MapInterface.CurrentMode <= EditMode.EDGE_PLACE);
            bool iObjs = (MapInterface.CurrentMode == EditMode.OBJECT_SELECT);

            // Search for walls/tiles/objects under cursor

            statusLocation = String.Format(FORMAT_COORDINATES, mousePt.X, mousePt.Y);
            statusMapItem = "";
            statusPolygon = "";

            // Wall tracking
            if (iWalls)
            {
                var wallPt = MapView.GetNearestWallPoint(mousePt);
                Map.Wall wall = map.Walls.ContainsKey(wallPt) ? map.Walls[wallPt] : null;
                statusLocation = String.Format(FORMAT_COORDINATES, wallPt.X, wallPt.Y);

                if (wall != null)
                    statusMapItem = String.Format(FORMAT_WALL_INFO, wall.Material, wall.Variation);



                if (prevWall != wall)
                {
                    prevWall = wall;
                    updateStatusbars = true;
                }

            }
            else
                prevWall = null;



            // Tile tracking
            if (iTiles)
            {
                var tilePt = MapView.GetNearestTilePoint(mousePt);
                Map.Tile tile = map.Tiles.ContainsKey(tilePt) ? map.Tiles[tilePt] : null;
                statusLocation = String.Format(FORMAT_COORDINATES, tilePt.X, tilePt.Y);

                if (tile != null)
                {
                    statusMapItem = String.Format(FORMAT_TILE_INFO, tile.Graphic, tile.Variation);
                    edgeCount = tile.EdgeTiles.Count;
                    if (tile.EdgeTiles.Count > 0)
                    {

                        statusMapItem += String.Format(FORMAT_EDGE_COUNT, tile.EdgeTiles.Count);

                        foreach (Map.Tile.EdgeTile edge in tile.EdgeTiles)
                        {
                            statusMapItem += String.Format(FORMAT_EDGE_INFO, ThingDb.FloorTileNames[edge.Graphic], edge.Variation, edge.Dir, ThingDb.EdgeTileNames[edge.Edge]);

                        }
                    }
                }

                if (prevTile != tile)
                {
                    prevTile = tile;
                    updateStatusbars = true;
                }
                if (prevEdge != edgeCount && tile != null)
                {
                    prevEdge = tile.EdgeTiles.Count;
                    updateStatusbars = true;
                }



            }
            else
                prevTile = null;






            // Object tracking
            if (iObjs)
            {
                Map.Object obj = MapInterface.ObjectSelect(mousePt);
                if (obj == null) return;

                statusMapItem = String.Format(FORMAT_OBJECT_INFO, obj.Name, obj.Extent);



                if (prevObj != obj)
                {
                    prevObj = obj;
                    updateStatusbars = true;
                }

            }
            else
                prevObj = null;


            // Polygon tracking
            Map.Polygon ins = null;
            var ptFlt = new PointF(mousePt.X, mousePt.Y);
            int i = -1;
            foreach (Map.Polygon poly in map.Polygons)
            {
                i++;
                if (poly.IsPointInside(ptFlt))
                {
                    statusPolygon = poly.Name;
                    ins = poly;

                    if (MainWindow.Instance.mapView.PolygonEditDlg.Visible && !MainWindow.Instance.mapView.PolygonEditDlg.LockedBox.Checked && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && (MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon != MainWindow.Instance.mapView.PolygonEditDlg.SuperPolygon || MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon == null))
                    {
                        MainWindow.Instance.mapView.PolygonEditDlg.listBoxPolygons.SelectedIndex = i;
                        MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon = ins;
                        //System.Windows.Forms.MessageBox.Show("ON:");
                    }
                    break;
                }
               
            }
            /*
            if (ins == null)
            {
                
                MainWindow.Instance.mapView.PolygonEditDlg.listBoxPolygons.ClearSelected();
                MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon = null;
                 // System.Windows.Forms.MessageBox.Show("OFF:");
            }
            */
            if (prevPoly != ins)
            {
                prevPoly = ins;
                updateStatusbars = true;
            }
        }

        public bool ValuesChanged()
        {
            if (updateStatusbars)
            {
                updateStatusbars = false;
                return true;
            }
            return false;
        }
    }
}
