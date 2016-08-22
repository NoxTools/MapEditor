using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared;
using TileEdgeDirection = NoxShared.Map.Tile.EdgeTile.Direction;
using Mode = MapEditor.MapInt.EditMode;
using MapEditor.render;
using MapEditor.newgui;
using MapEditor.objgroups;
using MapEditor.MapInt;

using NoxShared.ObjDataXfer;
using System.Collections.Generic;
using System.Collections;
namespace MapEditor
{
    public class MapView : UserControl
    {
        public WallProperties secprops = new WallProperties();
        public bool TileTabLoaded = false;
        bool renderingOk = true;
        int WidthMod = 0;
        int winX = 0;
        int winY = 0;
        public Point highlightUndoRedo;
        List<timetile> TimeEdges = new List<timetile>();
        List<timeobject> TimeObjects = new List<timeobject>();
        List<timewaypoint> TimeWPs = new List<timewaypoint>();
        List<timewall> TimeWalls = new List<timewall>();
        List<timepolygon> TimePolygons = new List<timepolygon>();
        public List<TimeContent> TimeManager = new List<TimeContent>();
        public List<Map.Wall> LastWalls = new List<Map.Wall>();
        public int currentStep = 0;
        public Point mouseKeep;
        public Point mouseKeepOff;
        public bool picking = false;
        public int arrowPoly = 0;
        float relXX = 0;
        float relYY = 0;
        public bool StopUndo;
        public bool StopRedo;
        public int higlightRad = 150;
        RadioButton[] buttons = new RadioButton[5];
        StatusbarHelper statusHelper = new StatusbarHelper();
        public const int squareSize = 23;
        public const int objectSelectionRadius = 8;
        protected Button currentButton;
        public MapObjectCollection SelectedObjects { get { return MapInterface.SelectedObjects; } }
        public MapViewRenderer MapRenderer;
        public PolygonEditor PolygonEditDlg = null;
        public ScriptFunctionDialog strSd = new ScriptFunctionDialog();
        public Point mouseLocation;
        public Point destLinePoint;
        public float delta;
        private int tilecount;
        private bool added;
        private int wallcount;
        public bool BlockTime;
        public Point copyPoint;
       public List<PointF> PolyPointOffset = new List<PointF>();
        private Map Map
        {
            get
            {
                return MapInterface.TheMap;
            }
        }

        /* Controls */
        private MenuItem contextMenuDelete;
        private MenuItem contextMenuProperties;
        private MenuItem menuItem3;
        private StatusBarPanel statusMapItem;
        private StatusBar statusBar;
        private StatusBarPanel statusLocation;
        private StatusBarPanel statusMode;
        public Panel scrollPanel;//WARNING: the form designer is not happy with this
        public MapView.FlickerFreePanel mapPanel;
        private MenuItem contextMenuCopy;
        private MenuItem contextMenuPaste;
        private ContextMenu contextMenu;
        private Timer tmrInvalidate;
        private System.ComponentModel.IContainer components;
        private GroupBox groupAdv;
        private TabControl tabMapTools;
        private TabPage tabObjectWps;
        private GroupBox objectGroup;
        public ComboBox cboObjCreate;
        private PictureBox objectPreview;
        private RadioButton radFullSnap;
        private RadioButton radCenterSnap;
        private RadioButton radNoSnap;
        private GroupBox extentsGroup;
        private RadioButton radioExtentShowColl;
        private RadioButton radioExtentsShowAll;
        private RadioButton customRadio;
        public NumericUpDown customSnapValue;
        private Button button1;
        private RadioButton SelectObjectBtn;
        private RadioButton PlaceObjectBtn;
        private RadioButton pathWPBtn;
        private RadioButton placeWPBtn;
        private RadioButton selectWPBtn;
        private Button button2;
        private ToolTip toolTip1;
        public CheckBox Picker;
        public Button undo;
        public Button redo;
        private Button button5;
        private RadioButton radioExtentsHide;
        public CheckBox prwSwitch;
        private Timer UndoTimer;
        private ListBox listBox1;
        private Timer RedoTimer;
        public CheckBox doubleWp;
        public CheckBox select45Box;
        private MenuItem contextcopyContent;
        private bool moved = false;
        public Label label2;
        public bool done;
        public bool contextMenuOpen;
        private ContextMenuStrip contextMenuStrip;

        public enum TimeEvent
        {
            PRE, POST
        };



        public class timetile
        {
            public Point Location;
            public Map.Tile.EdgeTile edge;
            public ArrayList EdgeTiles;
            public Map.Tile Tile;

        }
        public class timeobject
        {
            public PointF Location;
            public Map.Object Object;

        }
        public class timewaypoint
        {
            public ArrayList connections;
            public string Name;
            public PointF Location;
            public Map.Waypoint wp;
        }
        public class timewall
        {
            public Map.Wall Wall;
            public Map.Wall.WallFacing Facing;
            public Map.Wall.SecretScanFlags Sflags;
            public bool Destructable;
            public int Secret_OpenWaitSeconds;
            public byte Minimap;
            public byte Secret_WallState;
        }
        public class timepolygon
        {
            public Map.Polygon Polygon;
            public List<PointF> Points;
        }
        public class TimeContent
        {
            public EditMode Mode;
            public TimeEvent Event;
            public PointF Location;
            public List<timewall> StoredWalls = new List<timewall>();
            public List<timetile> StoredTiles = new List<timetile>();
            public List<timewaypoint> StoredWPs = new List<timewaypoint>();
            public List<timeobject> StoredObjects = new List<timeobject>();
            public List<timepolygon> Storedpolygons = new List<timepolygon>();

        }
        public static int[] directions = new int[8] { 0, 6, 2, 7, 1, 4, 5, 3 };
        //16bit windows needs the deprectated version of DoubleBufferring to work. Otherwise it crashes :\
        public class FlickerFreePanel : Panel
        {
            public FlickerFreePanel()
                : base()
            { SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true); }
            // set styles to reduce flicker and painting over twice
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapView));
            this.contextMenuDelete = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.contextMenuProperties = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusMode = new System.Windows.Forms.StatusBarPanel();
            this.statusLocation = new System.Windows.Forms.StatusBarPanel();
            this.statusMapItem = new System.Windows.Forms.StatusBarPanel();
            this.statusPolygon = new System.Windows.Forms.StatusBarPanel();
            this.groupAdv = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.prwSwitch = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.redo = new System.Windows.Forms.Button();
            this.tabMapTools = new System.Windows.Forms.TabControl();
            this.tabWalls = new System.Windows.Forms.TabPage();
            this.WallMakeNewCtrl = new MapEditor.newgui.WallMakeTab();
            this.tabTiles = new System.Windows.Forms.TabPage();
            this.TileMakeNewCtrl = new MapEditor.newgui.TileMakeTab();
            this.tabEdges = new System.Windows.Forms.TabPage();
            this.EdgeMakeNewCtrl = new MapEditor.newgui.EdgeMakeTab();
            this.tabObjectWps = new System.Windows.Forms.TabPage();
            this.Picker = new System.Windows.Forms.CheckBox();
            this.select45Box = new System.Windows.Forms.CheckBox();
            this.groupGridSnap = new System.Windows.Forms.GroupBox();
            this.customRadio = new System.Windows.Forms.RadioButton();
            this.customSnapValue = new System.Windows.Forms.NumericUpDown();
            this.radFullSnap = new System.Windows.Forms.RadioButton();
            this.radNoSnap = new System.Windows.Forms.RadioButton();
            this.radCenterSnap = new System.Windows.Forms.RadioButton();
            this.waypointGroup = new System.Windows.Forms.GroupBox();
            this.doubleWp = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonObjectMode = new SwitchModeButton();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonWaypointMode = new SwitchModeButton();
            this.pathWPBtn = new System.Windows.Forms.RadioButton();
            this.placeWPBtn = new System.Windows.Forms.RadioButton();
            this.selectWPBtn = new System.Windows.Forms.RadioButton();
            this.waypointName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.waypointEnabled = new System.Windows.Forms.CheckBox();
            this.extentsGroup = new System.Windows.Forms.GroupBox();
            this.radioExtentsShowAll = new System.Windows.Forms.RadioButton();
            this.radioExtentsHide = new System.Windows.Forms.RadioButton();
            this.radioExtentShowColl = new System.Windows.Forms.RadioButton();
            this.objectGroup = new System.Windows.Forms.GroupBox();
            this.SelectObjectBtn = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.PlaceObjectBtn = new System.Windows.Forms.RadioButton();
            this.objectCategoriesBox = new System.Windows.Forms.ComboBox();
            this.cboObjCreate = new System.Windows.Forms.ComboBox();
            this.objectPreview = new System.Windows.Forms.PictureBox();
            this.undo = new System.Windows.Forms.Button();
            this.scrollPanel = new System.Windows.Forms.Panel();
            this.mapPanel = new MapEditor.MapView.FlickerFreePanel();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.contextMenuCopy = new System.Windows.Forms.MenuItem();
            this.contextMenuPaste = new System.Windows.Forms.MenuItem();
            this.contextcopyContent = new System.Windows.Forms.MenuItem();
            this.tmrInvalidate = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.UndoTimer = new System.Windows.Forms.Timer(this.components);
            this.RedoTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.statusMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusMapItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusPolygon)).BeginInit();
            this.groupAdv.SuspendLayout();
            this.tabMapTools.SuspendLayout();
            this.tabWalls.SuspendLayout();
            this.tabTiles.SuspendLayout();
            this.tabEdges.SuspendLayout();
            this.tabObjectWps.SuspendLayout();
            this.groupGridSnap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customSnapValue)).BeginInit();
            this.waypointGroup.SuspendLayout();
            this.extentsGroup.SuspendLayout();
            this.objectGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectPreview)).BeginInit();
            this.scrollPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuDelete
            // 
            this.contextMenuDelete.Index = 2;
            this.contextMenuDelete.Text = "Delete";
            this.contextMenuDelete.Click += new System.EventHandler(this.contextMenuDelete_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "-";
            // 
            // contextMenuProperties
            // 
            this.contextMenuProperties.Index = 4;
            this.contextMenuProperties.Text = "Properties";
            this.contextMenuProperties.Click += new System.EventHandler(this.contextMenuProperties_Click);
            // 
            // statusBar
            // 
            this.statusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.statusBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statusBar.Location = new System.Drawing.Point(0, 682);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusMode,
            this.statusLocation,
            this.statusMapItem,
            this.statusPolygon});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(859, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 1;
            this.statusBar.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.statusBar_PanelClick);
            // 
            // statusMode
            // 
            this.statusMode.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.statusMode.MinWidth = 0;
            this.statusMode.Name = "statusMode";
            this.statusMode.ToolTipText = "Currently active editor mode";
            this.statusMode.Width = 10;
            // 
            // statusLocation
            // 
            this.statusLocation.MinWidth = 0;
            this.statusLocation.Name = "statusLocation";
            this.statusLocation.ToolTipText = "Location (depends on mode)";
            // 
            // statusMapItem
            // 
            this.statusMapItem.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.statusMapItem.MinWidth = 0;
            this.statusMapItem.Name = "statusMapItem";
            this.statusMapItem.ToolTipText = "Info about the map element";
            this.statusMapItem.Width = 10;
            // 
            // statusPolygon
            // 
            this.statusPolygon.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.statusPolygon.MinWidth = 0;
            this.statusPolygon.Name = "statusPolygon";
            this.statusPolygon.ToolTipText = "Name of the polygon";
            this.statusPolygon.Width = 10;
            // 
            // groupAdv
            // 
            this.groupAdv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupAdv.Controls.Add(this.listBox1);
            this.groupAdv.Controls.Add(this.label2);
            this.groupAdv.Controls.Add(this.prwSwitch);
            this.groupAdv.Controls.Add(this.button5);
            this.groupAdv.Controls.Add(this.redo);
            this.groupAdv.Controls.Add(this.tabMapTools);
            this.groupAdv.Controls.Add(this.undo);
            this.groupAdv.Location = new System.Drawing.Point(-1, 0);
            this.groupAdv.Name = "groupAdv";
            this.groupAdv.Size = new System.Drawing.Size(250, 680);
            this.groupAdv.TabIndex = 28;
            this.groupAdv.TabStop = false;
            this.groupAdv.Enter += new System.EventHandler(this.groupAdv_Enter);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(173, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1, 4);
            this.listBox1.TabIndex = 0;
            this.listBox1.Visible = false;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 34;
            this.label2.Visible = false;
            // 
            // prwSwitch
            // 
            this.prwSwitch.Appearance = System.Windows.Forms.Appearance.Button;
            this.prwSwitch.Location = new System.Drawing.Point(97, 8);
            this.prwSwitch.Name = "prwSwitch";
            this.prwSwitch.Size = new System.Drawing.Size(23, 23);
            this.prwSwitch.TabIndex = 32;
            this.prwSwitch.Text = "F";
            this.prwSwitch.UseVisualStyleBackColor = true;
            this.prwSwitch.CheckedChanged += new System.EventHandler(this.prwSwitch_CheckedChanged);
            this.prwSwitch.Click += new System.EventHandler(this.prwSwitch_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(11, 8);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(23, 23);
            this.button5.TabIndex = 31;
            this.button5.Text = "S";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // redo
            // 
            this.redo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.redo.Enabled = false;
            this.redo.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.redo.Location = new System.Drawing.Point(66, 8);
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(23, 23);
            this.redo.TabIndex = 30;
            this.redo.Text = "8&z";
            this.redo.UseVisualStyleBackColor = true;
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // tabMapTools
            // 
            this.tabMapTools.Controls.Add(this.tabWalls);
            this.tabMapTools.Controls.Add(this.tabTiles);
            this.tabMapTools.Controls.Add(this.tabEdges);
            this.tabMapTools.Controls.Add(this.tabObjectWps);
            this.tabMapTools.Location = new System.Drawing.Point(7, 33);
            this.tabMapTools.Name = "tabMapTools";
            this.tabMapTools.SelectedIndex = 0;
            this.tabMapTools.Size = new System.Drawing.Size(236, 641);
            this.tabMapTools.TabIndex = 29;
            this.tabMapTools.SelectedIndexChanged += new System.EventHandler(this.TabMapToolsSelectedIndexChanged);
            // 
            // tabWalls
            // 
            this.tabWalls.BackColor = System.Drawing.Color.LightGray;
            this.tabWalls.Controls.Add(this.WallMakeNewCtrl);
            this.tabWalls.Location = new System.Drawing.Point(4, 22);
            this.tabWalls.Name = "tabWalls";
            this.tabWalls.Padding = new System.Windows.Forms.Padding(3);
            this.tabWalls.Size = new System.Drawing.Size(228, 615);
            this.tabWalls.TabIndex = 4;
            this.tabWalls.Text = "Walls";
            this.tabWalls.Click += new System.EventHandler(this.tabWalls_Click);
            // 
            // WallMakeNewCtrl
            // 
            this.WallMakeNewCtrl.Location = new System.Drawing.Point(6, 0);
            this.WallMakeNewCtrl.Name = "WallMakeNewCtrl";
            this.WallMakeNewCtrl.SelectedWallFacing = 0;
            this.WallMakeNewCtrl.Size = new System.Drawing.Size(216, 614);
            this.WallMakeNewCtrl.TabIndex = 0;
            this.WallMakeNewCtrl.Tag = "d";
            this.WallMakeNewCtrl.Load += new System.EventHandler(this.WallMakeNewCtrl_Load_1);
            // 
            // tabTiles
            // 
            this.tabTiles.BackColor = System.Drawing.Color.LightGray;
            this.tabTiles.Controls.Add(this.TileMakeNewCtrl);
            this.tabTiles.Location = new System.Drawing.Point(4, 22);
            this.tabTiles.Name = "tabTiles";
            this.tabTiles.Size = new System.Drawing.Size(192, 74);
            this.tabTiles.TabIndex = 5;
            this.tabTiles.Text = "Tiles";
            this.tabTiles.Click += new System.EventHandler(this.tabTiles_Click);
            // 
            // TileMakeNewCtrl
            // 
            this.TileMakeNewCtrl.Location = new System.Drawing.Point(7, 0);
            this.TileMakeNewCtrl.Name = "TileMakeNewCtrl";
            this.TileMakeNewCtrl.Size = new System.Drawing.Size(216, 612);
            this.TileMakeNewCtrl.TabIndex = 0;
            this.toolTip1.SetToolTip(this.TileMakeNewCtrl, "f");
            // 
            // tabEdges
            // 
            this.tabEdges.BackColor = System.Drawing.Color.LightGray;
            this.tabEdges.Controls.Add(this.EdgeMakeNewCtrl);
            this.tabEdges.Location = new System.Drawing.Point(4, 22);
            this.tabEdges.Name = "tabEdges";
            this.tabEdges.Size = new System.Drawing.Size(192, 74);
            this.tabEdges.TabIndex = 6;
            this.tabEdges.Text = "Edges";
            this.tabEdges.Click += new System.EventHandler(this.tabEdges_Click);
            this.tabEdges.Enter += new System.EventHandler(this.tabEdges_Enter);
            // 
            // EdgeMakeNewCtrl
            // 
            this.EdgeMakeNewCtrl.BackColor = System.Drawing.Color.Transparent;
            this.EdgeMakeNewCtrl.Location = new System.Drawing.Point(5, 8);
            this.EdgeMakeNewCtrl.Name = "EdgeMakeNewCtrl";
            this.EdgeMakeNewCtrl.Size = new System.Drawing.Size(216, 536);
            this.EdgeMakeNewCtrl.TabIndex = 1;
            this.EdgeMakeNewCtrl.TabStop = false;
            // 
            // tabObjectWps
            // 
            this.tabObjectWps.BackColor = System.Drawing.Color.LightGray;
            this.tabObjectWps.Controls.Add(this.Picker);
            this.tabObjectWps.Controls.Add(this.select45Box);
            this.tabObjectWps.Controls.Add(this.groupGridSnap);
            this.tabObjectWps.Controls.Add(this.waypointGroup);
            this.tabObjectWps.Controls.Add(this.extentsGroup);
            this.tabObjectWps.Controls.Add(this.objectGroup);
            this.tabObjectWps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabObjectWps.Location = new System.Drawing.Point(4, 22);
            this.tabObjectWps.Name = "tabObjectWps";
            this.tabObjectWps.Size = new System.Drawing.Size(192, 74);
            this.tabObjectWps.TabIndex = 2;
            this.tabObjectWps.Text = "Objects/WayPoints";
            this.tabObjectWps.Click += new System.EventHandler(this.tabObjectWps_Click);
            // 
            // Picker
            // 
            this.Picker.Appearance = System.Windows.Forms.Appearance.Button;
            this.Picker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Picker.BackgroundImage")));
            this.Picker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Picker.Location = new System.Drawing.Point(77, 367);
            this.Picker.Name = "Picker";
            this.Picker.Size = new System.Drawing.Size(30, 30);
            this.Picker.TabIndex = 36;
            this.toolTip1.SetToolTip(this.Picker, "Item Picker (Ctrl+A)");
            this.Picker.UseVisualStyleBackColor = true;
            this.Picker.CheckedChanged += new System.EventHandler(this.Picker_CheckedChanged);
            // 
            // select45Box
            // 
            this.select45Box.AutoSize = true;
            this.select45Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.select45Box.Location = new System.Drawing.Point(10, 369);
            this.select45Box.Name = "select45Box";
            this.select45Box.Size = new System.Drawing.Size(64, 16);
            this.select45Box.TabIndex = 31;
            this.select45Box.Text = "45 degree";
            this.toolTip1.SetToolTip(this.select45Box, "Rotates selection area 45 degrees (Ctrl+D)");
            this.select45Box.UseVisualStyleBackColor = true;
            // 
            // groupGridSnap
            // 
            this.groupGridSnap.Controls.Add(this.customRadio);
            this.groupGridSnap.Controls.Add(this.customSnapValue);
            this.groupGridSnap.Controls.Add(this.radFullSnap);
            this.groupGridSnap.Controls.Add(this.radNoSnap);
            this.groupGridSnap.Controls.Add(this.radCenterSnap);
            this.groupGridSnap.Location = new System.Drawing.Point(112, 274);
            this.groupGridSnap.Name = "groupGridSnap";
            this.groupGridSnap.Size = new System.Drawing.Size(110, 121);
            this.groupGridSnap.TabIndex = 32;
            this.groupGridSnap.TabStop = false;
            this.groupGridSnap.Text = "Grid alignment";
            // 
            // customRadio
            // 
            this.customRadio.AutoSize = true;
            this.customRadio.Location = new System.Drawing.Point(7, 89);
            this.customRadio.Name = "customRadio";
            this.customRadio.Size = new System.Drawing.Size(59, 17);
            this.customRadio.TabIndex = 30;
            this.customRadio.TabStop = true;
            this.customRadio.Text = "custom";
            this.customRadio.UseVisualStyleBackColor = true;
            this.customRadio.CheckedChanged += new System.EventHandler(this.customRadio_CheckedChanged);
            // 
            // customSnapValue
            // 
            this.customSnapValue.Enabled = false;
            this.customSnapValue.Location = new System.Drawing.Point(66, 88);
            this.customSnapValue.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.customSnapValue.Name = "customSnapValue";
            this.customSnapValue.Size = new System.Drawing.Size(39, 20);
            this.customSnapValue.TabIndex = 29;
            this.customSnapValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // radFullSnap
            // 
            this.radFullSnap.AutoSize = true;
            this.radFullSnap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radFullSnap.Location = new System.Drawing.Point(6, 65);
            this.radFullSnap.Name = "radFullSnap";
            this.radFullSnap.Size = new System.Drawing.Size(69, 17);
            this.radFullSnap.TabIndex = 28;
            this.radFullSnap.Text = "Full Snap";
            this.radFullSnap.UseVisualStyleBackColor = true;
            this.radFullSnap.CheckedChanged += new System.EventHandler(this.radFullSnap_CheckedChanged);
            // 
            // radNoSnap
            // 
            this.radNoSnap.AutoSize = true;
            this.radNoSnap.Checked = true;
            this.radNoSnap.Location = new System.Drawing.Point(6, 20);
            this.radNoSnap.Name = "radNoSnap";
            this.radNoSnap.Size = new System.Drawing.Size(67, 17);
            this.radNoSnap.TabIndex = 26;
            this.radNoSnap.TabStop = true;
            this.radNoSnap.Text = "No Snap";
            this.radNoSnap.UseVisualStyleBackColor = true;
            this.radNoSnap.CheckedChanged += new System.EventHandler(this.radNoSnap_CheckedChanged);
            // 
            // radCenterSnap
            // 
            this.radCenterSnap.BackColor = System.Drawing.Color.Transparent;
            this.radCenterSnap.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radCenterSnap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radCenterSnap.Location = new System.Drawing.Point(6, 42);
            this.radCenterSnap.Name = "radCenterSnap";
            this.radCenterSnap.Size = new System.Drawing.Size(97, 17);
            this.radCenterSnap.TabIndex = 27;
            this.radCenterSnap.Text = "Center/Door Snap";
            this.radCenterSnap.UseVisualStyleBackColor = false;
            this.radCenterSnap.CheckedChanged += new System.EventHandler(this.radCenterSnap_CheckedChanged);
            // 
            // waypointGroup
            // 
            this.waypointGroup.Controls.Add(this.doubleWp);
            this.waypointGroup.Controls.Add(this.button1);
            this.waypointGroup.Controls.Add(this.buttonObjectMode);
            this.waypointGroup.Controls.Add(this.button2);
            this.waypointGroup.Controls.Add(this.buttonWaypointMode);
            this.waypointGroup.Controls.Add(this.pathWPBtn);
            this.waypointGroup.Controls.Add(this.placeWPBtn);
            this.waypointGroup.Controls.Add(this.selectWPBtn);
            this.waypointGroup.Controls.Add(this.waypointName);
            this.waypointGroup.Controls.Add(this.label1);
            this.waypointGroup.Controls.Add(this.waypointEnabled);
            this.waypointGroup.Location = new System.Drawing.Point(14, 409);
            this.waypointGroup.Name = "waypointGroup";
            this.waypointGroup.Size = new System.Drawing.Size(200, 180);
            this.waypointGroup.TabIndex = 30;
            this.waypointGroup.TabStop = false;
            this.waypointGroup.Text = "Waypoint properties";
            // 
            // doubleWp
            // 
            this.doubleWp.AutoSize = true;
            this.doubleWp.Location = new System.Drawing.Point(54, 157);
            this.doubleWp.Name = "doubleWp";
            this.doubleWp.Size = new System.Drawing.Size(85, 17);
            this.doubleWp.TabIndex = 25;
            this.doubleWp.Text = "Double Way";
            this.doubleWp.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 21);
            this.button1.TabIndex = 21;
            this.button1.Text = "EnableAll";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.button1, "Enables all waypoints in the current map.");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonObjectMode
            // 
            this.buttonObjectMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonObjectMode.Location = new System.Drawing.Point(10, 153);
            this.buttonObjectMode.Name = "buttonObjectMode";
            this.buttonObjectMode.Size = new System.Drawing.Size(18, 23);
            this.buttonObjectMode.TabIndex = 32;
            this.buttonObjectMode.Text = " ";
            this.buttonObjectMode.UseVisualStyleBackColor = true;
            this.buttonObjectMode.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 23);
            this.button2.TabIndex = 33;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonWaypointMode
            // 
            this.buttonWaypointMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonWaypointMode.Location = new System.Drawing.Point(27, 151);
            this.buttonWaypointMode.Name = "buttonWaypointMode";
            this.buttonWaypointMode.Size = new System.Drawing.Size(14, 23);
            this.buttonWaypointMode.TabIndex = 20;
            this.buttonWaypointMode.Text = " ";
            this.buttonWaypointMode.UseVisualStyleBackColor = true;
            this.buttonWaypointMode.Visible = false;
            this.buttonWaypointMode.Click += new System.EventHandler(this.buttonWaypointMode_Click);
            // 
            // pathWPBtn
            // 
            this.pathWPBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.pathWPBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pathWPBtn.Location = new System.Drawing.Point(54, 129);
            this.pathWPBtn.Name = "pathWPBtn";
            this.pathWPBtn.Size = new System.Drawing.Size(96, 23);
            this.pathWPBtn.TabIndex = 24;
            this.pathWPBtn.TabStop = true;
            this.pathWPBtn.Text = "Make Path";
            this.pathWPBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.pathWPBtn, "Creating connections between selected waypoints .");
            this.pathWPBtn.UseVisualStyleBackColor = true;
            this.pathWPBtn.CheckedChanged += new System.EventHandler(this.ObjectModesChanged);
            // 
            // placeWPBtn
            // 
            this.placeWPBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.placeWPBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.placeWPBtn.Location = new System.Drawing.Point(54, 78);
            this.placeWPBtn.Name = "placeWPBtn";
            this.placeWPBtn.Size = new System.Drawing.Size(96, 23);
            this.placeWPBtn.TabIndex = 23;
            this.placeWPBtn.TabStop = true;
            this.placeWPBtn.Text = "Place/Remove";
            this.placeWPBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.placeWPBtn, "Creating new waypoints. (Switch: ~)");
            this.placeWPBtn.UseVisualStyleBackColor = true;
            this.placeWPBtn.CheckedChanged += new System.EventHandler(this.ObjectModesChanged);
            // 
            // selectWPBtn
            // 
            this.selectWPBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.selectWPBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectWPBtn.Location = new System.Drawing.Point(54, 103);
            this.selectWPBtn.Name = "selectWPBtn";
            this.selectWPBtn.Size = new System.Drawing.Size(96, 23);
            this.selectWPBtn.TabIndex = 22;
            this.selectWPBtn.TabStop = true;
            this.selectWPBtn.Text = "Select";
            this.selectWPBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.selectWPBtn, "Selecting Waypoints (Switch: ~)");
            this.selectWPBtn.UseVisualStyleBackColor = true;
            this.selectWPBtn.CheckedChanged += new System.EventHandler(this.ObjectModesChanged);
            // 
            // waypointName
            // 
            this.waypointName.Location = new System.Drawing.Point(52, 22);
            this.waypointName.Name = "waypointName";
            this.waypointName.Size = new System.Drawing.Size(120, 20);
            this.waypointName.TabIndex = 0;
            this.waypointName.TextChanged += new System.EventHandler(this.WaypointNameTextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // waypointEnabled
            // 
            this.waypointEnabled.AutoSize = true;
            this.waypointEnabled.Checked = true;
            this.waypointEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.waypointEnabled.Location = new System.Drawing.Point(119, 49);
            this.waypointEnabled.Name = "waypointEnabled";
            this.waypointEnabled.Size = new System.Drawing.Size(65, 17);
            this.waypointEnabled.TabIndex = 2;
            this.waypointEnabled.Text = "Enabled";
            this.waypointEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.waypointEnabled.UseVisualStyleBackColor = true;
            this.waypointEnabled.CheckedChanged += new System.EventHandler(this.WaypointEnabledCheckedChanged);
            // 
            // extentsGroup
            // 
            this.extentsGroup.Controls.Add(this.radioExtentsShowAll);
            this.extentsGroup.Controls.Add(this.radioExtentsHide);
            this.extentsGroup.Controls.Add(this.radioExtentShowColl);
            this.extentsGroup.Location = new System.Drawing.Point(7, 274);
            this.extentsGroup.Name = "extentsGroup";
            this.extentsGroup.Size = new System.Drawing.Size(99, 88);
            this.extentsGroup.TabIndex = 29;
            this.extentsGroup.TabStop = false;
            this.extentsGroup.Text = "Extents";
            // 
            // radioExtentsShowAll
            // 
            this.radioExtentsShowAll.AutoSize = true;
            this.radioExtentsShowAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioExtentsShowAll.Location = new System.Drawing.Point(6, 65);
            this.radioExtentsShowAll.Name = "radioExtentsShowAll";
            this.radioExtentsShowAll.Size = new System.Drawing.Size(66, 17);
            this.radioExtentsShowAll.TabIndex = 1;
            this.radioExtentsShowAll.Text = "Show All";
            this.radioExtentsShowAll.UseVisualStyleBackColor = true;
            this.radioExtentsShowAll.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioExtentsHide
            // 
            this.radioExtentsHide.AutoSize = true;
            this.radioExtentsHide.Checked = true;
            this.radioExtentsHide.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioExtentsHide.Location = new System.Drawing.Point(6, 19);
            this.radioExtentsHide.Name = "radioExtentsHide";
            this.radioExtentsHide.Size = new System.Drawing.Size(47, 17);
            this.radioExtentsHide.TabIndex = 0;
            this.radioExtentsHide.TabStop = true;
            this.radioExtentsHide.Text = "Hide";
            this.radioExtentsHide.UseVisualStyleBackColor = true;
            this.radioExtentsHide.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioExtentShowColl
            // 
            this.radioExtentShowColl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioExtentShowColl.Location = new System.Drawing.Point(6, 42);
            this.radioExtentShowColl.Name = "radioExtentShowColl";
            this.radioExtentShowColl.Size = new System.Drawing.Size(87, 17);
            this.radioExtentShowColl.TabIndex = 0;
            this.radioExtentShowColl.Text = "Show Colliding";
            this.radioExtentShowColl.UseVisualStyleBackColor = true;
            this.radioExtentShowColl.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // objectGroup
            // 
            this.objectGroup.Controls.Add(this.SelectObjectBtn);
            this.objectGroup.Controls.Add(this.label6);
            this.objectGroup.Controls.Add(this.PlaceObjectBtn);
            this.objectGroup.Controls.Add(this.objectCategoriesBox);
            this.objectGroup.Controls.Add(this.cboObjCreate);
            this.objectGroup.Controls.Add(this.objectPreview);
            this.objectGroup.Location = new System.Drawing.Point(24, 7);
            this.objectGroup.Name = "objectGroup";
            this.objectGroup.Size = new System.Drawing.Size(182, 257);
            this.objectGroup.TabIndex = 24;
            this.objectGroup.TabStop = false;
            this.objectGroup.Text = "Objects list";
            this.objectGroup.Enter += new System.EventHandler(this.objectGroup_Enter);
            // 
            // SelectObjectBtn
            // 
            this.SelectObjectBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.SelectObjectBtn.BackColor = System.Drawing.Color.LightGray;
            this.SelectObjectBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SelectObjectBtn.Location = new System.Drawing.Point(93, 224);
            this.SelectObjectBtn.Name = "SelectObjectBtn";
            this.SelectObjectBtn.Size = new System.Drawing.Size(79, 24);
            this.SelectObjectBtn.TabIndex = 33;
            this.SelectObjectBtn.TabStop = true;
            this.SelectObjectBtn.Text = "Select";
            this.SelectObjectBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.SelectObjectBtn, "Selecting Objects (Switch: ~)");
            this.SelectObjectBtn.UseVisualStyleBackColor = false;
            this.SelectObjectBtn.CheckedChanged += new System.EventHandler(this.ObjectModesChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 23);
            this.label6.TabIndex = 31;
            this.label6.Text = "Category";
            // 
            // PlaceObjectBtn
            // 
            this.PlaceObjectBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.PlaceObjectBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PlaceObjectBtn.Location = new System.Drawing.Point(8, 224);
            this.PlaceObjectBtn.Name = "PlaceObjectBtn";
            this.PlaceObjectBtn.Size = new System.Drawing.Size(79, 24);
            this.PlaceObjectBtn.TabIndex = 32;
            this.PlaceObjectBtn.TabStop = true;
            this.PlaceObjectBtn.Text = "Create";
            this.PlaceObjectBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.PlaceObjectBtn, "Placing Objects (Switch: ~)");
            this.PlaceObjectBtn.UseVisualStyleBackColor = true;
            this.PlaceObjectBtn.CheckedChanged += new System.EventHandler(this.ObjectModesChanged);
            // 
            // objectCategoriesBox
            // 
            this.objectCategoriesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectCategoriesBox.FormattingEnabled = true;
            this.objectCategoriesBox.Location = new System.Drawing.Point(68, 192);
            this.objectCategoriesBox.Name = "objectCategoriesBox";
            this.objectCategoriesBox.Size = new System.Drawing.Size(104, 21);
            this.objectCategoriesBox.TabIndex = 30;
            this.objectCategoriesBox.SelectedIndexChanged += new System.EventHandler(this.ObjectCategoriesBoxSelectedIndexChanged);
            // 
            // cboObjCreate
            // 
            this.cboObjCreate.FormattingEnabled = true;
            this.cboObjCreate.Location = new System.Drawing.Point(13, 165);
            this.cboObjCreate.MaxDropDownItems = 15;
            this.cboObjCreate.Name = "cboObjCreate";
            this.cboObjCreate.Size = new System.Drawing.Size(160, 21);
            this.cboObjCreate.TabIndex = 23;
            this.cboObjCreate.SelectedIndexChanged += new System.EventHandler(this.CboObjCreateSelectedIndexChanged);
            // 
            // objectPreview
            // 
            this.objectPreview.BackColor = System.Drawing.Color.Black;
            this.objectPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.objectPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.objectPreview.Location = new System.Drawing.Point(24, 22);
            this.objectPreview.Name = "objectPreview";
            this.objectPreview.Size = new System.Drawing.Size(133, 133);
            this.objectPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.objectPreview.TabIndex = 25;
            this.objectPreview.TabStop = false;
            this.objectPreview.Click += new System.EventHandler(this.objectPreview_Click);
            // 
            // undo
            // 
            this.undo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.undo.Enabled = false;
            this.undo.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.undo.Location = new System.Drawing.Point(42, 8);
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(23, 23);
            this.undo.TabIndex = 29;
            this.undo.Text = "7&y";
            this.undo.UseVisualStyleBackColor = true;
            this.undo.Click += new System.EventHandler(this.undo_Click);
            // 
            // scrollPanel
            // 
            this.scrollPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.mapPanel);
            this.scrollPanel.Location = new System.Drawing.Point(249, 0);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(608, 678);
            this.scrollPanel.TabIndex = 6;
            this.scrollPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollPanel_Scroll);
            // 
            // mapPanel
            // 
            this.mapPanel.Location = new System.Drawing.Point(-8, 5);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(5888, 5888);
            this.mapPanel.TabIndex = 0;
            this.mapPanel.AutoSizeChanged += new System.EventHandler(this.mapPanel_AutoSizeChanged);
            this.mapPanel.ClientSizeChanged += new System.EventHandler(this.mapPanel_ClientSizeChanged);
            this.mapPanel.RegionChanged += new System.EventHandler(this.mapPanel_RegionChanged);
            this.mapPanel.SizeChanged += new System.EventHandler(this.mapPanel_SizeChanged);
            this.mapPanel.VisibleChanged += new System.EventHandler(this.mapPanel_VisibleChanged);
            this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
            this.mapPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseDoubleClick);
            this.mapPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseDown);
            this.mapPanel.MouseLeave += new System.EventHandler(this.mapPanel_MouseLeave);
            this.mapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseMove);
            this.mapPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseUp);
            this.mapPanel.ParentChanged += new System.EventHandler(this.mapPanel_ParentChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.contextMenuCopy,
            this.contextMenuPaste,
            this.contextMenuDelete,
            this.menuItem3,
            this.contextMenuProperties,
            this.contextcopyContent});
            this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
            // 
            // contextMenuCopy
            // 
            this.contextMenuCopy.Index = 0;
            this.contextMenuCopy.Text = "Copy";
            this.contextMenuCopy.Click += new System.EventHandler(this.contextMenuCopy_Click);
            // 
            // contextMenuPaste
            // 
            this.contextMenuPaste.Index = 1;
            this.contextMenuPaste.Text = "Paste";
            this.contextMenuPaste.Click += new System.EventHandler(this.contextMenuPaste_Click);
            // 
            // contextcopyContent
            // 
            this.contextcopyContent.Index = 5;
            this.contextcopyContent.Text = "Copy Extents";
            this.contextcopyContent.Visible = false;
            this.contextcopyContent.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // tmrInvalidate
            // 
            this.tmrInvalidate.Enabled = true;
            this.tmrInvalidate.Tick += new System.EventHandler(this.tmrInvalidate_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // UndoTimer
            // 
            this.UndoTimer.Interval = 120;
            this.UndoTimer.Tick += new System.EventHandler(this.UndoTimer_Tick);
            // 
            // RedoTimer
            // 
            this.RedoTimer.Interval = 120;
            this.RedoTimer.Tick += new System.EventHandler(this.RedoTimer_Tick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip.Opened += new System.EventHandler(this.contextMenu_Popup);
            this.contextMenuStrip.Click += new System.EventHandler(this.contextMenuStrip_Open);
            // 
            // MapView
            // 
            this.Controls.Add(this.groupAdv);
            this.Controls.Add(this.scrollPanel);
            this.Controls.Add(this.statusBar);
            this.Name = "MapView";
            this.Size = new System.Drawing.Size(859, 704);
            ((System.ComponentModel.ISupportInitialize)(this.statusMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusMapItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusPolygon)).EndInit();
            this.groupAdv.ResumeLayout(false);
            this.groupAdv.PerformLayout();
            this.tabMapTools.ResumeLayout(false);
            this.tabWalls.ResumeLayout(false);
            this.tabTiles.ResumeLayout(false);
            this.tabEdges.ResumeLayout(false);
            this.tabObjectWps.ResumeLayout(false);
            this.tabObjectWps.PerformLayout();
            this.groupGridSnap.ResumeLayout(false);
            this.groupGridSnap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customSnapValue)).EndInit();
            this.waypointGroup.ResumeLayout(false);
            this.waypointGroup.PerformLayout();
            this.extentsGroup.ResumeLayout(false);
            this.extentsGroup.PerformLayout();
            this.objectGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectPreview)).EndInit();
            this.scrollPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.GroupBox groupGridSnap;
        public System.Windows.Forms.CheckBox waypointEnabled;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox waypointName;
        private System.Windows.Forms.GroupBox waypointGroup;
        public MapEditor.newgui.EdgeMakeTab EdgeMakeNewCtrl;
        private System.Windows.Forms.TabPage tabEdges;
        public TileMakeTab TileMakeNewCtrl;
        private System.Windows.Forms.TabPage tabTiles;
        public WallMakeTab WallMakeNewCtrl;
        private System.Windows.Forms.TabPage tabWalls;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox objectCategoriesBox;
        private System.Windows.Forms.StatusBarPanel statusPolygon;
        private ContextMenuStrip contexMenu = new ContextMenuStrip();
        #endregion

        public MapView()
        {
            InitializeComponent();

            WidthMod = groupAdv.Width;
            MapRenderer = new MapViewRenderer(this);

            // setup window handlers
            if (MainWindow.Instance != null)
            {
                MainWindow.Instance.KeyDown += new KeyEventHandler(MapView_DeletePressed);
                MainWindow.Instance.KeyDown += new KeyEventHandler(StartShiftSelecting);
                MainWindow.Instance.KeyUp += new KeyEventHandler(StopShiftSelecting);
                MainWindow.Instance.KeyDown += new KeyEventHandler(TabsShortcuts);
                MainWindow.Instance.MouseWheel += new MouseEventHandler(MouseWheelEventHandler);
                cboObjCreate.MouseWheel += new MouseEventHandler(cboObjMouseWheel);
                objectCategoriesBox.MouseWheel += new MouseEventHandler(cboObjMouseWheel);
                contexMenu.Items.Add("Copy");
                contexMenu.Items.Add("Paste");
                contexMenu.Items.Add("Delete");
                contexMenu.Items.Add(new ToolStripSeparator());
                contexMenu.Items.Add("Properties");
                contexMenu.Items.Add("Copy Coords");
                contexMenu.Items.Add("Copy Extents");
                contexMenu.Items[6].Visible = false;
                contexMenu.ItemClicked += new ToolStripItemClickedEventHandler(contexMenu_ItemClicked);
                contexMenu.Opened += new EventHandler(contextMenu_Popup);




            }

            // initialize tabs


            buttons[0] = SelectObjectBtn;
            buttons[1] = PlaceObjectBtn;
            buttons[2] = selectWPBtn;
            buttons[3] = placeWPBtn;
            buttons[4] = pathWPBtn;

            SelectObjectBtn.Tag = EditMode.OBJECT_SELECT;
            PlaceObjectBtn.Tag = EditMode.OBJECT_PLACE;
            selectWPBtn.Tag = EditMode.WAYPOINT_SELECT;
            placeWPBtn.Tag = EditMode.WAYPOINT_PLACE;
            pathWPBtn.Tag = EditMode.WAYPOINT_CONNECT;


            WallMakeNewCtrl.AutoWalltBtn.Tag = EditMode.WALL_BRUSH;
            WallMakeNewCtrl.PlaceWalltBtn.Tag = EditMode.WALL_PLACE;
            TileMakeNewCtrl.AutoTileBtn.Tag = EditMode.FLOOR_BRUSH;
            TileMakeNewCtrl.PlaceTileBtn.Tag = EditMode.FLOOR_PLACE;
            WallMakeNewCtrl.SetMapView(this);
            TileMakeNewCtrl.SetMapView(this);
            EdgeMakeNewCtrl.SetMapView(this);
            PolygonEditDlg = new PolygonEditor();
            //strSd = new ScriptFunctionDialog();
            // initialize buttons
            prwSwitch.Checked = !EditorSettings.Default.Edit_PreviewMode;
            buttonObjectMode.SetStates(new EditMode[] { EditMode.OBJECT_SELECT, EditMode.OBJECT_PLACE });
            buttonWaypointMode.SetStates(new EditMode[] { EditMode.WAYPOINT_PLACE, EditMode.WAYPOINT_SELECT, EditMode.WAYPOINT_CONNECT });
            // alter initial mode
            tabMapTools.SelectedTab = tabObjectWps;
            MapInterface.CurrentMode = EditMode.OBJECT_SELECT;

            SelectObjectBtn.Checked = true;
        }
        public void openScripts()
        {
            strSd = new ScriptFunctionDialog();
            strSd.Scripts = Map.Scripts;
            strSd.ShowDialog(this);
            Map.Scripts = strSd.Scripts;
        }

       public void setRadioDraw()
        {
            radioExtentsShowAll.Checked = EditorSettings.Default.Draw_AllExtents;
            radioExtentsHide.Checked = !radioExtentsShowAll.Checked;
        }

        void MapView_DeletePressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedObjects();
                mapPanel.Invalidate();
            }
            // I won't make this feature accessible from menus, cuz it's kinda unusable at the moment 

            if (e.KeyCode == Keys.F7)
            {
                new MapGeneratorDlg(this).ShowDialog();
            }
        }

        public string removeSpace(string spaceChar)
        {
            string temp = spaceChar.Substring(0, 1);

            if (temp.IndexOf("*") != -1)
            {
                return spaceChar.Substring(1, spaceChar.Length - 1);
            }
            else
                return spaceChar;
        }
        public bool findObjectInList(string data, bool sec = false)
        {

            for (int i = 0; i < cboObjCreate.Items.Count; i++)
            {
                if (removeSpace(cboObjCreate.Items[i].ToString()) == data)
                {
                    cboObjCreate.SelectedIndex = i;

                    return true;
                }

            }
            if (!sec)
            {
                objectCategoriesBox.SelectedIndex = 0;
                if (findObjectInList(data, true)) return true;
            }
            return false;
        }
        void cboObjMouseWheel(object sender, MouseEventArgs e)
        {



            ComboBox combo = sender as ComboBox;
            Point mousePt = new Point(e.X, e.Y);
            mousePt = combo.PointToScreen(mousePt);
            mousePt = MainWindow.Instance.PointToClient(mousePt);

            if (groupAdv.ClientRectangle.Contains(mousePt)) return;
            object thingName = cboObjCreate.SelectedItem;
            if (ThingDb.Things[(string)thingName].Xfer == "DoorXfer" ||
                ThingDb.Things[(string)thingName].Xfer == "NPCXfer" ||
                ThingDb.Things[(string)thingName].Xfer == "MonsterXfer" ||
                ThingDb.Things[(string)thingName].Xfer == "SentryXfer")
            {
                ((HandledMouseEventArgs)e).Handled = true;
                MouseWheelEventHandler(sender, e);
            }

        }
        public int Get45RecSize()
        {
            int x1 = MapInterface.selected45Area[0].X;
            int x2 = MapInterface.selected45Area[2].X;
            int y1 = MapInterface.selected45Area[0].Y;
            int y2 = MapInterface.selected45Area[2].Y;
            return ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public void Switch45Area()
        {
            MapInterface.RecSelected.Clear();
            MapInterface.selected45Area = new Point[4];
            select45Box.Checked = !select45Box.Checked;
            MapInterface.ObjectSelect45Rectangle(mouseLocation);

            mapPanel.Invalidate();
        }


        void MouseWheelEventHandler(object sender, MouseEventArgs e)
        {

            if (MapInterface.CurrentMode == Mode.OBJECT_PLACE || MapInterface.CurrentMode == Mode.OBJECT_SELECT)
            {

                object thingName = cboObjCreate.SelectedItem;
                // Update object image
                if (thingName != null)
                {

                    if (ThingDb.Things[(string)thingName].Xfer == "DoorXfer")
                    {
                        sbyte facing = (sbyte)delta;
                        if (e.Delta >= 90) facing += 8;
                        if (e.Delta <= 90) facing -= 8;

                        if (facing > 24) facing = 0;
                        if (facing < 0) facing = 24;
                        delta = facing;
                    }
                    else if (ThingDb.Things[(string)thingName].Xfer == "MonsterXfer" || ThingDb.Things[(string)thingName].Xfer == "NPCXfer")
                    {
                        int facing = Array.IndexOf(directions, (int)delta);

                        if (e.Delta >= 90) facing += 1;
                        if (e.Delta <= 90) facing -= 1;

                        if (facing > 7) facing = 0;
                        if (facing < 0) facing = 7;
                        delta = (byte)directions[facing];
                    }
                    else if (ThingDb.Things[(string)thingName].Xfer == "SentryXfer")
                    {
                        int rotatDegrees = (int)(delta * 180 / Math.PI);
                        rotatDegrees += e.Delta / 30;
                        if (rotatDegrees > 360) rotatDegrees -= 360;
                        if (e.Delta >= 90)
                            rotatDegrees = 5 * (int)(decimal.Ceiling((decimal)(rotatDegrees - 0.1) / 5));

                        if (e.Delta <= 90)
                            rotatDegrees = -5 * (int)(decimal.Ceiling((decimal)(rotatDegrees + 0.1) / -5));

                        // deg2rad

                        float kagor = (float)(rotatDegrees * Math.PI / 180);
                        delta = kagor;
                        //MessageBox.Show(delta.ToString());
                    }


                }


                // rotate some objects
                if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT && !SelectedObjects.IsEmpty)
                {
                    foreach (Map.Object obj in SelectedObjects)
                    {
                        if (obj.CanBeRotated())
                        {
                            // doors
                            if (ThingDb.Things[obj.Name].Xfer == "DoorXfer")
                            {
                                DoorXfer door = obj.GetExtraData<DoorXfer>();
                                sbyte facing = (sbyte)door.Direction;
                                if (e.Delta >= 90) facing += 8;
                                if (e.Delta <= 90) facing -= 8;

                                if (facing > 24) facing = 0;
                                if (facing < 0) facing = 24;
                                door.Direction = (DoorXfer.DOORS_DIR)facing;

                            }
                            // sentry beams
                            else if (ThingDb.Things[obj.Name].Xfer == "SentryXfer")
                            {
                                SentryXfer xfer = obj.GetExtraData<SentryXfer>();
                                // rad2deg
                                int rotatDegrees = (int)(xfer.BasePosRadian * 180 / Math.PI);
                                // rotate(lel)
                                rotatDegrees += e.Delta / 30;
                                if (rotatDegrees > 360) rotatDegrees -= 360;
                                if (e.Delta >= 90)
                                    rotatDegrees = 5 * (int)(decimal.Ceiling((decimal)(rotatDegrees - 0.1) / 5));

                                if (e.Delta <= 90)
                                    rotatDegrees = -5 * (int)(decimal.Ceiling((decimal)(rotatDegrees + 0.1) / -5));

                                // deg2rad

                                float kagor = (float)(rotatDegrees * Math.PI / 180);
                                xfer.BasePosRadian = kagor;
                            }

                            //monsters, NPC

                            else if (ThingDb.Things[obj.Name].Xfer == "MonsterXfer")
                            {
                                MonsterXfer monster = obj.GetExtraData<MonsterXfer>();
                                int facing = Array.IndexOf(directions, monster.DirectionId);

                                if (e.Delta >= 90) facing += 1;
                                if (e.Delta <= 90) facing -= 1;

                                if (facing > 7) facing = 0;
                                if (facing < 0) facing = 7;
                                monster.DirectionId = (byte)directions[facing];

                            }
                            else if (ThingDb.Things[obj.Name].Xfer == "NPCXfer")
                            {
                                NPCXfer npc = obj.GetExtraData<NPCXfer>();
                                int facing = Array.IndexOf(directions, npc.DirectionId);

                                if (e.Delta >= 90) facing += 1;
                                if (e.Delta <= 90) facing -= 1;

                                if (facing > 7) facing = 0;
                                if (facing < 0) facing = 7;
                                npc.DirectionId = (byte)directions[facing];

                            }
                        }
                    }
                }
            }
            else if (MapInterface.CurrentMode == Mode.FLOOR_BRUSH || MapInterface.CurrentMode == Mode.FLOOR_PLACE)
            {
                if (e.Delta >= 90 && TileMakeNewCtrl.BrushSize.Value < 6) TileMakeNewCtrl.BrushSize.Value += 1;
                if (e.Delta <= 90 && TileMakeNewCtrl.BrushSize.Value > 1) TileMakeNewCtrl.BrushSize.Value -= 1;

            }
        }

        void StopShiftSelecting(object sender, KeyEventArgs e)
        {
            if (!e.Shift)
            {
                if (MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                    WallMakeNewCtrl.smartDraw.Checked = false;
                MapInterface.KeyHelper.ShiftKey = false;
            }
        }

        void TabsShortcuts(object sender, KeyEventArgs e)
        {

            int page = MainWindow.Instance.tabControl1.SelectedIndex;
            int mode = tabMapTools.SelectedIndex;


            var activeControl = WallMakeNewCtrl.ActiveControl;
            if (mode == 1)
                activeControl = TileMakeNewCtrl.ActiveControl;
            else if (mode == 2)
                activeControl = EdgeMakeNewCtrl.ActiveControl;
            else if (mode == 3)
                activeControl = this.ActiveControl;


            if (activeControl is TextBox || activeControl is NumericUpDown)
                return;

            if (page != 0)
                return;

            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                tabMapTools.SelectedTab = tabWalls;
                TabMapToolsSelectedIndexChanged(sender, e);
                return;
            }
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {

                tabMapTools.SelectedTab = tabTiles;
                TabMapToolsSelectedIndexChanged(sender, e);
                return;
            }
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {

                tabMapTools.SelectedTab = tabEdges;
                TabMapToolsSelectedIndexChanged(sender, e);
                return;
            }
            else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                tabMapTools.SelectedTab = tabObjectWps;
                TabMapToolsSelectedIndexChanged(sender, e);
                return;
            }
            else if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.OemSemicolon || e.KeyCode == Keys.NumPad0)
            {
                ModeSwitcher();
                return;
            }



        }
        void ctrlPicking(object sender, KeyEventArgs e)
        {
            /*
            if (e.Control && e.KeyCode == Keys.S) Picker.Checked = Picker.Checked ? false : true;
            if (e.Control && e.KeyCode == Keys.Z && undo.Enabled) undo_Click(sender, e);
            if (e.Control && e.KeyCode == Keys.Y && redo.Enabled) redo_Click(sender, e);
             * */
        }


        void StartShiftSelecting(object sender, KeyEventArgs e)
        {
            if (e.Shift)
            {
                if (MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                    WallMakeNewCtrl.smartDraw.Checked = true;
                MapInterface.KeyHelper.ShiftKey = true;
            }
        }


        public bool ApplyStore()
        {
            int steps = (TimeManager.Count - 1) - currentStep;
            if (TimeManager.Count == 0 || ((TimeManager.Count - 1) - currentStep) < 1 && currentStep > 0)
            {
                Store(MapInterface.CurrentMode, TimeEvent.PRE);
                return true;

            }
            else if (TimeManager.Count > 1)
            {
                if (getBaseMode(TimeManager[steps - 1].Mode) != getBaseMode(MapInterface.CurrentMode))
                {
                    Store(MapInterface.CurrentMode, TimeEvent.PRE);
                    return true;
                }
            }
            return false;
        }


        private void mapPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (!done) return;
            BlockTime = true;
            if (picking)
            {
                var page = tabMapTools.SelectedTab;
                var ptFlt = new Point(e.X, e.Y);


                Map.Object obj = MapInterface.ObjectSelect(ptFlt);

                // Alter current mode depending on the tab testudo

                if (page == tabTiles || page == tabEdges)
                {
                    Map.Tile tile = Map.Tiles.ContainsKey(GetNearestTilePoint(ptFlt)) ? Map.Tiles[MapView.GetNearestTilePoint(ptFlt)] : null;
                    if (tile == null) return;
                    TileMakeNewCtrl.findTileInList(tile.Graphic);
                    if (page == tabEdges) tabEdges_Enter(sender, e);


                }
                else if (page == tabWalls)
                {


                    Map.Wall wall = Map.Walls.ContainsKey(GetNearestWallPoint(ptFlt)) ? Map.Walls[MapView.GetNearestWallPoint(ptFlt)] : null;
                    if (wall == null) return;
                    if (MapInterface.KeyHelper.ShiftKey)
                    {

                        Button o = WallMakeNewCtrl.WallSelectButtons[(int)wall.Facing + (wall.Window ? 11 : 0)];
                        o.PerformClick();
                        o.Focus();
                    }
                    WallMakeNewCtrl.findWallInList(wall.Material);
                    if (MapInterface.KeyHelper.ShiftKey)
                        WallMakeNewCtrl.numWallVari.Value = wall.Variation;
                }
                else if (page == tabEdges)
                {
                    return;
                }
                else
                {
                    if (MapInterface.CurrentMode > EditMode.OBJECT_SELECT) return;
                    Map.Object obj0 = MapInterface.ObjectSelect(ptFlt);
                    if (obj0 == null) return;
                    findObjectInList(obj0.Name);
                }
                return;
            }

            Point pt = new Point(e.X, e.Y);

            //if (WallMakeNewCtrl.RecWall.Checked)
            //  MapInterface.WallRectangle(pt);


            Point ptAligned = pt;
            if (e.Button.Equals(MouseButtons.Middle))
            {
                // re-center camera
                CenterAtPoint(pt);
                mapPanel.Invalidate();
            }
            // Open properties if shift is hold, show context menu otherwise
            if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT && e.Button.Equals(MouseButtons.Right))
            {
                // TODO: ShowObjectProperties(MapInterface.ObjectSelect(pt));
                contexMenu.Show(mapPanel, pt);

            }


            if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT || MapInterface.CurrentMode == EditMode.OBJECT_PLACE || MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE || MapInterface.CurrentMode == EditMode.WAYPOINT_PLACE)
            {
                string objName = cboObjCreate.Text;
                if (ThingDb.Things.ContainsKey(objName))
                {
                    // Snap to grid
                    if (EditorSettings.Default.Edit_SnapGrid || ThingDb.Things[objName].Xfer == "DoorXfer")
                        ptAligned = new Point((int)Math.Round((decimal)(pt.X / squareSize)) * squareSize, (int)Math.Round((decimal)(pt.Y / squareSize)) * squareSize);
                    if (EditorSettings.Default.Edit_SnapHalfGrid)
                        ptAligned = new Point((int)Math.Round((decimal)((pt.X / (squareSize)) * squareSize) + squareSize / 2), (int)Math.Round((decimal)((pt.Y / (squareSize)) * squareSize) + squareSize / 2));
                    if (EditorSettings.Default.Edit_SnapCustom)
                    {
                        int snap = (int)customSnapValue.Value;
                        ptAligned = new Point((int)Math.Round((decimal)(pt.X / snap)) * snap, (int)Math.Round((decimal)(pt.Y / snap)) * snap);
                    }
                }
            }
            wallcount = Map.Walls.Count;
            tilecount = Map.Tiles.Count;
            added = false;
            if ((MapInterface.CurrentMode <= EditMode.OBJECT_PLACE || MapInterface.CurrentMode == EditMode.WAYPOINT_PLACE || MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE))
            {
                if (MapInterface.CurrentMode == EditMode.WALL_CHANGE && MapInterface.KeyHelper.ShiftKey)
                    goto done;

                if (e.Button.Equals(MouseButtons.Left))
                {
                    if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && MapInterface.KeyHelper.ShiftKey)
                        added = ApplyStore();
                    else if(MapInterface.CurrentMode != EditMode.POLYGON_RESHAPE)
                        added = ApplyStore();

                }
                else if (e.Button.Equals(MouseButtons.Right))
                {
                    //vyradit zdi z podminky stejne jako tiles
                    if (MapInterface.CurrentMode != EditMode.FLOOR_PLACE && MapInterface.CurrentMode != EditMode.FLOOR_BRUSH && MapInterface.CurrentMode != EditMode.WALL_BRUSH && !GetEdgeUnderCursor() && GetWallUnderCursor() == null && GetObjectUnderCursor() == null && GetWPUnderCursor() == null)
                        goto done;

                    added = ApplyStore();
                }
            }

        done:

            // reshape polygon if special mode is active
            if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
            {
                if (PolygonEditDlg.SelectedPolygon != null && e.Button.Equals(MouseButtons.Left))
                {

                    if (MapInterface.KeyHelper.ShiftKey == true)
                    {
                        PolygonEditDlg.SelectedPolygon.Points.Insert(arrowPoly, new PointF(ptAligned.X, ptAligned.Y));
                        // PolygonEditDlg.SelectedPolygon.Points.Add(new PointF(e.X, e.Y));
                        //arrowPoly = 0;
                        if (PolygonEditDlg.SelectedPolygon.Points.Count > 2) MapInterface.OpUpdatedPolygons = true;

                    }
                    else
                        arrowPoly = MapInterface.PolyPointSelect(pt);
                }
            }

            // pass into mapinterface handlers (operations)


            if (e.Button.Equals(MouseButtons.Left))
            {
                if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && PolygonEditDlg.SelectedPolygon != null && !MapInterface.KeyHelper.ShiftKey)
                {

                    if (MapInterface.SelectedPolyPoint.IsEmpty && PolygonEditDlg.SelectedPolygon.Points.Count > 2)
                    {
                        if (PolygonEditDlg.SelectedPolygon == PolygonEditDlg.SuperPolygon && PolygonEditDlg.SelectedPolygon != null && !PolygonEditDlg.LockedBox.Checked && !PolygonEditDlg.SelectedPolygon.IsPointInside(pt))
                        {
                            PolygonEditDlg.SuperPolygon = null;
                            //PolygonEditDlg.SelectedPolygon = null;

                        }
                        else if (PolygonEditDlg.Visible && PolygonEditDlg.SelectedPolygon != null && PolygonEditDlg.SelectedPolygon.IsPointInside(pt))
                            PolygonEditDlg.SuperPolygon = PolygonEditDlg.SelectedPolygon;
                        else if (PolygonEditDlg.SuperPolygon != PolygonEditDlg.SelectedPolygon)
                            PolygonEditDlg.SelectedPolygon = null;
                    }
                }
                mouseKeep = new Point(e.X, e.Y);
                MapInterface.HandleLMouseClick(MapInterface.CurrentMode == EditMode.OBJECT_PLACE ? ptAligned : pt);

            }
            else if (e.Button.Equals(MouseButtons.Right))
                MapInterface.HandleRMouseClick(pt);
            
            //TODO: presunout do mouseUp
            
            MapRenderer.UpdateCanvas(MapInterface.OpUpdatedObjects, MapInterface.OpUpdatedTiles);

            moved = false;
        }
        private void mapPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!done) return;
            mouseLocation = new Point(e.X, e.Y);
            debugTime();

            label2.Text = currentStep.ToString();

            if (PolygonEditDlg.Visible)
            {
                Point mousePt = PolygonEditDlg.PointToScreen(mouseLocation);
                mousePt = MainWindow.Instance.PointToClient(mousePt);

                if (PolygonEditDlg.ClientRectangle.Contains(mousePt)) return;
                MainWindow.Instance.Focus();
            }




            if (!e.Button.Equals(MouseButtons.Left))
            {
                statusHelper.Update(mouseLocation);
                statusLocation.Text = statusHelper.StatusLocation;
                if (statusHelper.ValuesChanged())
                {

                    statusMapItem.Text = statusHelper.StatusMapItem;
                    statusPolygon.Text = statusHelper.StatusPolygon;
                }
                if (!mouseKeep.IsEmpty)
                {
                    mouseKeepOff = mouseKeep;
                    mouseKeep = new Point();
                }
            }
            if (MapInterface.CurrentMode == EditMode.WALL_BRUSH)
            {
                if (e.Button.Equals(MouseButtons.Left))
                {
                    if (!WallMakeNewCtrl.WallSelectButtons[0].Focused && !WallMakeNewCtrl.WallSelectButtons[11].Focused && !WallMakeNewCtrl.WallSelectButtons[12].Focused)
                    {
                        Button o = WallMakeNewCtrl.WallSelectButtons[0];

                    }
                }
                if (WallMakeNewCtrl.RecWall.Checked)
                    MapInterface.WallRectangle(mouseLocation);
                else if (WallMakeNewCtrl.LineWall.Checked)
                    MapInterface.WallLine(GetNearestWallPoint(mouseLocation, true));

            }

            if (MapInterface.CurrentMode == EditMode.OBJECT_PLACE)
                tmrInvalidate.Interval = 25;
            else
                tmrInvalidate.Interval = 100;

            if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT && !picking && !mouseKeep.IsEmpty)
            {
                MapInterface.RecSelected.Clear();

                MapInterface.ObjectSelect45Rectangle(mouseLocation);

                tmrInvalidate.Interval = 2;
                // mapPanel.Invalidate();
            }

            if (e.Button.Equals(MouseButtons.Left) && !picking)
            {
                
                if (contextMenuOpen || moved) goto nah;
                
                if (!mouseKeep.IsEmpty && !SelectedObjects.IsEmpty && (MapInterface.CurrentMode == EditMode.OBJECT_SELECT))
                {
                    ApplyStore();
                    moved = true;
                }
                else if (!mouseKeep.IsEmpty && MapInterface.SelectedWaypoint != null && (MapInterface.CurrentMode == EditMode.WAYPOINT_SELECT))
                {
                    ApplyStore();
                    moved = true;
                }
                else if ( !mouseKeep.IsEmpty && (!MapInterface.SelectedPolyPoint.IsEmpty || PolygonEditDlg.SuperPolygon != null) && (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE))
                {
                    if (PolygonEditDlg.SuperPolygon != null)
                    {

                        if (PolygonEditDlg.SuperPolygon.IsPointInside(mouseLocation))
                        {
                            ApplyStore();
                            moved = true;
                        }
                        else if (!MapInterface.SelectedPolyPoint.IsEmpty)
                        {
                            ApplyStore();
                            moved = true;
                        }
                    }
                    else if (!MapInterface.SelectedPolyPoint.IsEmpty)
                    {
                        ApplyStore();
                        moved = true;

                    }
                }
            nah:
                if (!contexMenu.Visible)
                    contextMenuOpen = false;

                if (Get45RecSize() >= 5) moved = false;
                Point pt = mouseLocation;
                Point ptAligned = pt;

                // call handlers for some mouse operations
                if (MapInterface.CurrentMode == EditMode.FLOOR_BRUSH || MapInterface.CurrentMode == EditMode.FLOOR_PLACE || MapInterface.CurrentMode == EditMode.WALL_BRUSH || (EdgeMakeNewCtrl.AutoEgeBox.Checked && MapInterface.CurrentMode == EditMode.EDGE_PLACE))
                    MapInterface.HandleLMouseClick(pt);

                // Snap to grid
                if (EditorSettings.Default.Edit_SnapGrid)
                {

                    ptAligned = new Point((int)Math.Round((decimal)(pt.X / squareSize)) * squareSize, (int)Math.Round((decimal)(pt.Y / squareSize)) * squareSize);
                }
                if (EditorSettings.Default.Edit_SnapHalfGrid)
                    ptAligned = new Point((int)Math.Round((decimal)((pt.X / (squareSize)) * squareSize) + squareSize / 2), (int)Math.Round((decimal)((pt.Y / (squareSize)) * squareSize) + squareSize / 2));
                if (EditorSettings.Default.Edit_SnapCustom)
                {
                    int snap = (int)customSnapValue.Value;
                    ptAligned = new Point((int)Math.Round((decimal)(pt.X / squareSize)) * squareSize, (int)Math.Round((decimal)(pt.Y / squareSize)) * squareSize);
                    //ptAligned = new Point((int)Math.Round((decimal)(pt.X / snap)) * snap, (int)Math.Round((decimal)(pt.Y / snap)) * snap);
                }

                // moving waypoints
                if (MapInterface.CurrentMode == EditMode.WAYPOINT_SELECT)
                {
                    if (MapInterface.SelectedWaypoint != null)
                    {
                        MapInterface.SelectedWaypoint.Point.X = ptAligned.X; // Move the waypoint
                        MapInterface.SelectedWaypoint.Point.Y = ptAligned.Y;

                        mapPanel.Invalidate(); // Repaint the screen
                    }
                }
                // moving polypoints tudo
                if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                {
                    if (!MapInterface.SelectedPolyPoint.IsEmpty && !MapInterface.KeyHelper.ShiftKey && e.Y < 5870 && e.X < 5885 && e.X > 10 && e.Y > 10)
                    {
                        PointF AlignedPt = ptAligned;
                        if (PolygonEditDlg.snapPoly.Checked)
                            AlignedPt = MapInterface.PolyPointSnap(mouseLocation).IsEmpty ? ptAligned : MapInterface.PolyPointSnap(mouseLocation);

                        Map.Polygon poly = PolygonEditDlg.SelectedPolygon;
                        //Array.IndexOf(poly.Points.ToArray(), AlignedPt);
                        poly.Points[arrowPoly] = AlignedPt;
                        MapInterface.SelectedPolyPoint = AlignedPt;
                        mapPanel.Invalidate();

                    }
                    if (PolygonEditDlg.SuperPolygon != null && MapInterface.SelectedPolyPoint.IsEmpty)
                    {
                        if (PolygonEditDlg.SuperPolygon.IsPointInside(mouseLocation))
                        {
                            for (int i = 0; i < PolygonEditDlg.SuperPolygon.Points.Count; i++)
                            {
                                PointF pts = PolygonEditDlg.SuperPolygon.Points[i];
                                if (PolyPointOffset.Count <= PolygonEditDlg.SuperPolygon.Points.Count)
                                {
                                    float polyrelX = (pts.X - mouseLocation.X) * -1;
                                    float polyrelY = (pts.Y - mouseLocation.Y) * -1;
                                    PolyPointOffset.Add(new PointF(polyrelX, polyrelY));
                                }
                                PolygonEditDlg.SuperPolygon.Points[i] = new PointF(mouseLocation.X - PolyPointOffset[i].X, mouseLocation.Y - PolyPointOffset[i].Y);
                            }
                           mapPanel.Cursor = Cursors.SizeAll;
                            mapPanel.Invalidate();
                        }
                       
                        
                    }
                   
                }
                // moving objects
                bool aligned = false;

                if (!SelectedObjects.IsEmpty && Get45RecSize() < 5 && e.Y < 5870 && e.X < 5885 && e.X > 10 && e.Y > 10)
                {
                    if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT)
                    {
                        if (SelectedObjects.Origin != null)
                        {

                            float closestX = SelectedObjects.Origin.Location.X;
                            float closestY = SelectedObjects.Origin.Location.Y;
                            // update position of all objects relative


                            if (relXX == 0 && relYY == 0)
                            {
                                relXX = closestX - pt.X;
                                relYY = closestY - pt.Y;
                            }

                            if (EditorSettings.Default.Edit_SnapGrid)
                            {
                                aligned = true;
                                ptAligned = new Point((int)Math.Round((decimal)((pt.X + (int)relXX) / squareSize)) * squareSize, (int)Math.Round((decimal)((pt.Y + (int)relYY) / squareSize)) * squareSize);
                            }
                            if (EditorSettings.Default.Edit_SnapHalfGrid)
                            {
                                aligned = true;
                                ptAligned = new Point((int)Math.Round((decimal)(((pt.X + (int)relXX) / squareSize) * squareSize) + squareSize / 2), (int)Math.Round((decimal)(((pt.Y + (int)relYY) / squareSize) * squareSize) + squareSize / 2));
                            }
                            if (EditorSettings.Default.Edit_SnapCustom)
                            {
                                aligned = true;
                                int snap = (int)customSnapValue.Value;
                                ptAligned = new Point((int)Math.Round((decimal)((pt.X + relXX) / snap)) * snap, (int)Math.Round((decimal)((pt.Y + relYY) / snap)) * snap);
                            }
                            foreach (Map.Object co in SelectedObjects)
                            {
                                float relX = (closestX - co.Location.X) - (!aligned ? (float)relXX : 0);
                                float relY = (closestY - co.Location.Y) - (!aligned ? (float)relYY : 0);
                                PointF ResultLoc = new PointF(ptAligned.X - relX, ptAligned.Y - relY);
                                if (!(ResultLoc.Y < 5870 && ResultLoc.X < 5885 && ResultLoc.X > 10 && ResultLoc.Y > 10)) continue;
                                co.Location = ResultLoc;
                            }
                            tmrInvalidate.Interval = 2;
                            //mapPanel.Invalidate(true); // Repaint the screen
                        }
                    }
                }
            }
            else
            {
                if (mapPanel.Cursor == Cursors.SizeAll) mapPanel.Cursor = Cursors.Default;
               if(PolyPointOffset.Count>0) PolyPointOffset.Clear();
                relXX = 0;
                relYY = 0;
            }
            if (e.Button.Equals(MouseButtons.Right) && !picking)
            {
                if (MapInterface.CurrentMode == EditMode.FLOOR_BRUSH || MapInterface.CurrentMode == EditMode.FLOOR_PLACE || MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                    MapInterface.HandleRMouseClick(mouseLocation);
            }




            // update the visible map part
            MapRenderer.UpdateCanvas(MapInterface.OpUpdatedObjects, MapInterface.OpUpdatedTiles);
            if (WallMakeNewCtrl.RecWall.Checked || WallMakeNewCtrl.LineWall.Checked && mouseKeep.IsEmpty && MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                MapInterface.ResetUpdateTracker();
        }

        /// <summary>
        /// Удаляет предметы, содержащие? ?SelectedObjects, ?карт?
        /// </summary>
        public void DeleteSelectedObjects()
        {
            if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && !MapInterface.SelectedPolyPoint.IsEmpty)
            {
                ApplyStore();
                Map.Polygon poly = PolygonEditDlg.SelectedPolygon;
                poly.Points.RemoveAt(arrowPoly);
                MapInterface.SelectedPolyPoint = new PointF();
                Store(MapInterface.CurrentMode, TimeEvent.POST);
                mapPanel.Invalidate();
            }

            if (!SelectedObjects.IsEmpty && MapInterface.CurrentMode == EditMode.OBJECT_SELECT)
            {
                ApplyStore();
                // Store(MapInterface.CurrentMode, TimeEvent.PRE);
                foreach (Map.Object o in SelectedObjects) MapInterface.ObjectRemove(o);
                SelectedObjects.Items.Clear();
                Store(MapInterface.CurrentMode, TimeEvent.POST);
            }
            if (MapInterface.CurrentMode == EditMode.WAYPOINT_SELECT && MapInterface.SelectedWaypoint != null)
            {
                ApplyStore();
                MapInterface.WaypointRemove(MapInterface.SelectedWaypoint);
                Store(MapInterface.CurrentMode, TimeEvent.POST);
            }

            MapRenderer.UpdateCanvas(true, false);
        }


        void contexMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            if (item.Text == "Paste")
                contextMenuPaste_Click(sender, e);
            else if (item.Text == "Copy")
                contextMenuCopy_Click(sender, e);
            else if (item.Text == "Delete")
                contextMenuDelete_Click(sender, e);
            else if (item.Text == "Properties")
                contextMenuProperties_Click(sender, e);
            else if (item.Text == "Copy Coords")
                Copy_Coords(sender, e);
            else if (item.Text == "Copy Extents")
                menuItem1_Click(sender, e);
            
        }
        
        private void contextMenuDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedObjects();
            mapPanel.Invalidate();
        }
        private void Copy_Coords(object sender, EventArgs e)
        {
            copyPoint = mouseLocation;

            string content = "X:" + copyPoint.X.ToString() + " Y:" + copyPoint.Y.ToString();
           
            Clipboard.SetDataObject(content, true);

        }
        private SwitchModeButton buttonWaypointMode;
        private SwitchModeButton buttonObjectMode;

        private void contextMenuProperties_Click(object sender, EventArgs e)
        {
            if (!SelectedObjects.IsEmpty)
            {
                ShowObjectProperties(SelectedObjects.Items[0]);
            }
        }
        public void ShowObjectProperties(Map.Object obj)
        {
            if (obj == null) return;
          
            // working on the object clone (we should be able to rollback changes)
            int ndx = Map.Objects.IndexOf(obj);
            //MessageBox.Show(ndx.ToString());
            var propDlg = new ObjectPropertiesDialog();
            propDlg.Object = (Map.Object)Map.Objects[ndx];

            if (propDlg.ShowDialog() == DialogResult.OK)
            {
                // update object reference to updated version
                Map.Objects[ndx] = propDlg.Object;
                if(SelectedObjects.Items.Count>0) SelectedObjects.Items[0] = propDlg.Object;
                
                // hint renderer
                MapRenderer.UpdateCanvas(true, false);
                mapPanel.Invalidate();
            }
        }
        public void DeletefromSelected(Map.Object item)
        {
            int indx = Array.IndexOf(SelectedObjects.Items.ToArray(), item);
            if(indx >= 0)
            SelectedObjects.Items.RemoveAt(indx);

        }

        private void mapPanel_MouseUp(object sender, MouseEventArgs e)
        {
           
            foreach (Map.Object objct in MapInterface.RecSelected)
            {
                if (SelectedObjects.Items.Contains(objct) && MapInterface.KeyHelper.ShiftKey)
                    DeletefromSelected(objct);
                else if(!SelectedObjects.Items.Contains(objct))
                    SelectedObjects.Items.Add(objct);
            }

            MapInterface.RecSelected.Clear();
            //MapInterface.selectedArea = new Rectangle();
            MapInterface.selected45Area = new Point[4];

            //////////////delete rnpty time///////////////////////ъъ

            if (!MapInterface.OpUpdatedTiles && !MapInterface.OpUpdatedWalls && !MapInterface.OpUpdatedObjects && !MapInterface.OpUpdatedPolygons && !MapInterface.OpUpdatedWaypoints && !moved && added)
            {
                if (MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                {
                    if (WallMakeNewCtrl.LineWall.Checked || WallMakeNewCtrl.RecWall.Checked) goto noPre;
                }
                while (TimeManager.Count > 0 && TimeManager[(TimeManager.Count - 1) - currentStep].Event == TimeEvent.PRE)
                {
                    if (TimeManager[(TimeManager.Count - 1) - currentStep].Event == TimeEvent.PRE && TimeManager.Count > 0)
                    {
                        TimeManager.RemoveAt((TimeManager.Count - 1) - currentStep);
                    }
                }
                if (TimeManager.Count <= 1)
                {
                    StopUndo = true;
                    MainWindow.Instance.undo.Enabled = false;
                    undo.Enabled = false;
                }
            }
            BlockTime = false;
        noPre:





            //////////////////////////////////////////////////







            if (WallMakeNewCtrl.LineWall.Checked || WallMakeNewCtrl.RecWall.Checked)
                LastWalls.Clear();

            if (picking) goto hop;

            //  MessageBox.Show(MapInterface.OpUpdatedObjects.ToString() + " menu:" + contextMenuOpen.ToString());
            if (!MapInterface.OpUpdatedTiles && !MapInterface.OpUpdatedWalls && !MapInterface.OpUpdatedObjects && !MapInterface.OpUpdatedPolygons && !MapInterface.OpUpdatedWaypoints && (!moved))
            {


                goto hop;
            }

            if (MapInterface.CurrentMode <= EditMode.OBJECT_SELECT || MapInterface.CurrentMode == EditMode.WAYPOINT_CONNECT || MapInterface.CurrentMode == EditMode.WAYPOINT_PLACE || MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE || MapInterface.CurrentMode == EditMode.WAYPOINT_SELECT)
            {
                if ((WallMakeNewCtrl.LineWall.Checked || WallMakeNewCtrl.RecWall.Checked) && MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                    goto hop;



                if (MapInterface.CurrentMode >= EditMode.WALL_PLACE && MapInterface.CurrentMode < EditMode.OBJECT_PLACE)
                {

                    if (MapInterface.OpUpdatedTiles || MapInterface.OpUpdatedWalls || MapInterface.OpUpdatedObjects)
                    {
                        Store(MapInterface.CurrentMode, TimeEvent.POST);
                    }
                    goto hop;
                }

                if (moved)
                {
                    if (!SelectedObjects.IsEmpty && MapInterface.CurrentMode == EditMode.OBJECT_SELECT)
                        Store(MapInterface.CurrentMode, TimeEvent.POST);
                    else if (MapInterface.SelectedWaypoint != null && MapInterface.CurrentMode == EditMode.WAYPOINT_SELECT)
                        Store(MapInterface.CurrentMode, TimeEvent.POST);
                    else if ((!MapInterface.SelectedPolyPoint.IsEmpty || PolygonEditDlg.SuperPolygon != null) && !MapInterface.KeyHelper.ShiftKey && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                        Store(MapInterface.CurrentMode, TimeEvent.POST);
                }
                if (MapInterface.KeyHelper.ShiftKey && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && MapInterface.OpUpdatedPolygons)
                    Store(MapInterface.CurrentMode, TimeEvent.POST);
                if (MapInterface.CurrentMode == EditMode.OBJECT_PLACE && MapInterface.OpUpdatedObjects)
                    Store(MapInterface.CurrentMode, TimeEvent.POST);
                else if (MapInterface.CurrentMode == EditMode.WAYPOINT_PLACE && MapInterface.OpUpdatedWaypoints)
                    Store(MapInterface.CurrentMode, TimeEvent.POST);
                else if (MapInterface.SelectedWaypoint != null && MapInterface.CurrentMode == EditMode.WAYPOINT_CONNECT)
                    Store(MapInterface.CurrentMode, TimeEvent.POST);

            }
        hop:
            moved = false;
            MapRenderer.FakeWalls.Clear();
            mapPanel.Invalidate();

            if (!mouseKeep.IsEmpty)
            {
                mouseKeepOff = mouseKeep;
                mouseKeep = new Point();
            }
            if (MapInterface.CurrentMode != EditMode.WALL_PLACE && MapInterface.CurrentMode != EditMode.WALL_BRUSH)
                mouseKeepOff = new Point();

            if (picking)
            {
                Picker.Checked = false;
                //picking = false;
            }

            MapInterface.ResetUpdateTracker();
            //contextMenuOpen = false;
        }

        private void mapPanel_Paint(object sender, PaintEventArgs e)
        {
            // Something goes wrong / there is no map
            if (Map == null || !renderingOk) return;

            try
            {
                // Render map
                MapRenderer.RenderTo(e.Graphics);
            }
            catch (Exception ex)
            {
                renderingOk = false;
                new ExceptionDialog(ex, "Exception in rendering routine").ShowDialog();
                Environment.Exit(-1);
            }
        }

        public static Point GetNearestTilePoint(Point pt)
        {
            pt.Offset(0, -squareSize);
            return GetNearestWallPoint(pt);
        }

        public static Point GetCenterPoint(Point pt, bool wallPt = false)
        {
            Point pti = GetNearestTilePoint(pt);
            int x = (pti.X * squareSize);
            int y = (pti.Y * squareSize) + squareSize / 2;

            if (!wallPt)
                return new Point(x + squareSize / 2, y + (3 / 2) * squareSize);
            else
                return new Point((x + squareSize / 2) / squareSize, (y + (3 / 2) * squareSize) / squareSize);
        }

        public static Point GetNearestWallPoint(Point pt, bool cart = false)
        {
            int sqSize = squareSize;
            if (cart) sqSize = 1;

            Point tl = new Point((pt.X / squareSize) * squareSize, (pt.Y / squareSize) * squareSize);
            if (tl.X / squareSize % 2 == tl.Y / squareSize % 2)
                return new Point(tl.X / sqSize, tl.Y / sqSize);
            else
            {
                Point left = new Point(tl.X, tl.Y + squareSize / 2);
                Point right = new Point(tl.X + squareSize, tl.Y + squareSize / 2);
                Point top = new Point(tl.X + squareSize / 2, tl.Y);
                Point bottom = new Point(tl.X + squareSize / 2, tl.Y + squareSize);
                Point closest = left;
                foreach (Point point in new Point[] { left, right, top, bottom })
                    if (Distance(point, pt) < Distance(closest, pt))
                        closest = point;

                if (closest == left)
                    return new Point(tl.X / sqSize - 1, tl.Y / sqSize);
                else if (closest == right)
                    return new Point(tl.X / sqSize + 1, tl.Y / sqSize);
                else if (closest == top)
                    return new Point(tl.X / sqSize, tl.Y / sqSize - 1);
                else
                    return new Point(tl.X / sqSize, tl.Y / sqSize + 1);
            }
        }

        private static int Distance(Point a, Point b)
        {
            return (int)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private void scrollPanel_Scroll(object sender, ScrollEventArgs e)
        {
            // update visible area
            MapRenderer.UpdateCanvas(true, true);
            mapPanel.Invalidate(new Rectangle(scrollPanel.HorizontalScroll.Value, scrollPanel.VerticalScroll.Value, scrollPanel.Width, scrollPanel.Height));
        }

        public void CenterAtPoint(Point centerAt)
        {
            if (mapPanel.ClientRectangle.Contains(centerAt))
            {
                int Y = centerAt.Y - scrollPanel.Height / 2;
                int X = centerAt.X - scrollPanel.Width / 2;
                if (Y < 0)
                    Y = 0;
                if (X < 0)
                    X = 0;
                scrollPanel.VerticalScroll.Value = Y;
                scrollPanel.HorizontalScroll.Value = X;
                winX = centerAt.X - WidthMod;
                winY = centerAt.Y;
                scrollPanel.PerformLayout();
                MapRenderer.UpdateCanvas(true, true);
                mapPanel.Invalidate();
            }
        }

        private void contextMenu_Popup(object sender, EventArgs e)
        {
            contextMenuOpen = true;
            bool enable = true;
            /*
            if (SelectedObjects.IsEmpty)
                enable = false;

            // These are inaccessible if there are no selected objects
            contextMenuCopy.Enabled = enable;
            contextMenuProperties.Enabled = enable;
            contextMenuDelete.Enabled = enable;
            if (SelectedObjects.Items.Count > 1)
                contextcopyContent.Visible = true;
            else
                contextcopyContent.Visible = false;

            */

            if (SelectedObjects.IsEmpty)
                enable = false;

            // These are inaccessible if there are no selected objects
            contexMenu.Items[0].Enabled = enable;
            contexMenu.Items[2].Enabled = enable;
            contexMenu.Items[3].Enabled = enable;


            if (SelectedObjects.Items.Count > 1)
                contexMenu.Items[6].Visible = true;
            else
                contexMenu.Items[6].Visible = false;
        }

        private void contextMenuCopy_Click(object sender, EventArgs e)
        {
            if (!SelectedObjects.IsEmpty)
                Clipboard.SetDataObject(SelectedObjects.Clone(), false);
        }

        void contextMenuStrip_Open(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
                MessageBox.Show(clickedItem.ToString());
        }


        private void contextMenuPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(typeof(MapObjectCollection)))
            {
                MapObjectCollection collection = (MapObjectCollection)Clipboard.GetDataObject().GetData(typeof(MapObjectCollection));
                // find closest object
                double dist = double.MaxValue;
                float closestX = 0, closestY = 0;
                //Store(MapInterface.CurrentMode, TimeEvent.PRE);
                ApplyStore();
                foreach (Map.Object ot in collection)
                {
                    float dx = ot.Location.X - mouseLocation.X;
                    float dy = ot.Location.Y - mouseLocation.Y;
                    double ndist = Math.Sqrt(dx * dx + dy * dy);
                    if (ndist < dist)
                    {
                        dist = ndist;
                        closestX = ot.Location.X;
                        closestY = ot.Location.Y;
                    }
                }
                // clear (old) selection, duplicate objects
                SelectedObjects.Items.Clear();
                SelectedObjects.Origin = null;
                Map.Object clone;
                foreach (Map.Object o in collection)
                {
                    clone = (Map.Object)o.Clone();
                    // calc relative coordinates
                    float relX = closestX - o.Location.X;
                    float relY = closestY - o.Location.Y;
                    clone.Location = new PointF(mouseLocation.X - relX, mouseLocation.Y - relY);
                    clone.Extent = MapInterface.FindUnusedExtent();
                    Map.Objects.Add(clone);
                    // add into selection
                    SelectedObjects.Items.Add(clone);
                }
                // update canvas
                Store(MapInterface.CurrentMode, TimeEvent.POST);
                MapRenderer.UpdateCanvas(true, false);
                mapPanel.Invalidate();
            }
        }

        /// <summary>
        /// Renders entire map to a new bitmap, using current settings
        /// </summary>
        public Bitmap MapToImage()
        {
            if (Map == null)
                return null;

            Bitmap bitmap = new Bitmap(5880, 5880);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                MapRenderer.RenderTo(g, true);
            }
            return bitmap;
        }

        private void tmrInvalidate_Tick(object sender, EventArgs e)
        {
            done = true;
            // label2.Text = tmrInvalidate.Interval.ToString();
            if (MapInterface.ModeIsUpdated)
            {
                // Update mode text.
                statusMode.Text = String.Format("Mode: {0}", MapInterface.CurrentMode);
                MapInterface.ModeIsUpdated = false;
            }
           
            //mapPanel.Invalidate( ,);
             mapPanel.Invalidate((new Rectangle(scrollPanel.HorizontalScroll.Value, scrollPanel.VerticalScroll.Value, scrollPanel.Width, scrollPanel.Height)));


        }

        private void mapPanel_MouseLeave(object sender, EventArgs e)
        {


        }

        public Map.Tile GetCurrentTileVar(Point tilePt)
        {
            return TileMakeNewCtrl.GetTile(tilePt);
        }

        private void customRadio_CheckedChanged(object sender, EventArgs e)
        {
            customSnapValue.Enabled = true;
            EditorSettings.Default.Edit_SnapCustom = customRadio.Checked;
            EditorSettings.Default.Edit_SnapGrid = false;
            EditorSettings.Default.Edit_SnapHalfGrid = false;
        }

        private void radFullSnap_CheckedChanged(object sender, EventArgs e)
        {
            customSnapValue.Enabled = false;
            EditorSettings.Default.Edit_SnapCustom = false;
            EditorSettings.Default.Edit_SnapGrid = false;
            EditorSettings.Default.Edit_SnapHalfGrid = radFullSnap.Checked;
        }

        private void radCenterSnap_CheckedChanged(object sender, EventArgs e)
        {
            customSnapValue.Enabled = false;
            EditorSettings.Default.Edit_SnapCustom = false;
            EditorSettings.Default.Edit_SnapGrid = radCenterSnap.Checked;
            EditorSettings.Default.Edit_SnapHalfGrid = false;
        }

        private void radNoSnap_CheckedChanged(object sender, EventArgs e)
        {
            customSnapValue.Enabled = false;
            EditorSettings.Default.Edit_SnapCustom = false;
            EditorSettings.Default.Edit_SnapGrid = false;
            EditorSettings.Default.Edit_SnapHalfGrid = false;
        }

        // Radio button handlers - switch extent view
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            EditorSettings.Default.Draw_Extents = false;
            EditorSettings.Default.Draw_AllExtents = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            EditorSettings.Default.Draw_Extents = true;
            EditorSettings.Default.Draw_AllExtents = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            EditorSettings.Default.Draw_Extents = true;
            EditorSettings.Default.Draw_AllExtents = true;
        }

        void CboObjCreateSelectedIndexChanged(object sender, EventArgs e)
        {
            object thingName = cboObjCreate.SelectedItem;
            // Update object image
            delta = 0;
            if (thingName != null)
            {
                PlaceObjectBtn.Checked = true;
                //MapInterface.CurrentMode = EditMode.OBJECT_PLACE;
                ThingDb.Thing tt = ThingDb.Things[(string)thingName];
                Bitmap icon = null;
                if (tt.SpritePrettyImage > 0 && (tt.Class & ThingDb.Thing.ClassFlags.MONSTER) == 0)
                {
                    icon = MapRenderer.VideoBag.GetBitmap(tt.SpritePrettyImage);
                   
                }
                else if (tt.SpriteMenuIcon > 0)
                {
                    icon = MapRenderer.VideoBag.GetBitmap(tt.SpriteMenuIcon);

                    if (tt.Xfer != "InvisibleLightXfer" && !tt.Name.StartsWith("Amb"))
                        if (tt.SpriteAnimFrames.Count > 0) icon = MapRenderer.VideoBag.GetBitmap(tt.SpriteAnimFrames[tt.SpriteAnimFrames.Count-1]);
                   // MessageBox.Show(tt.SpriteAnimFrames.Count.ToString());

                }
                objectPreview.BackgroundImage = icon;

            }
        }

        internal void LoadObjectCategories()
        {
            try
            {
                // Load object list file
                ObjectCategory[] categories = XMLCategoryListReader.ReadCategories("categories.xml");
                objectCategoriesBox.Items.AddRange(categories);
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to load object category listfile: " + ex.Message);
            }
            // If object list file is either empty or failed to load, create "All Objects"
            if (objectCategoriesBox.Items.Count < 1)
            {
                ObjectCategory catDefault = new ObjectCategory("All Objects");
                catDefault.Rules.Add(new IncludeRule("", IncludeRule.IncludeRuleType.ANYTHING));
                objectCategoriesBox.Items.Add(catDefault);
            }
            objectCategoriesBox.SelectedIndex = 0;
        }

        void ObjectCategoriesBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            object selItem = objectCategoriesBox.SelectedItem;
            delta = 0;
            if (selItem != null)
            {
                object prewItem = cboObjCreate.SelectedItem;
                cboObjCreate.Items.Clear();
                ObjectCategory category = (ObjectCategory)selItem;
                // update object list contents
                cboObjCreate.Items.AddRange(category.GetThings());
                // update selection
                if (prewItem != null)
                {
                    if (!findObjectInList(prewItem.ToString(), true))
                        cboObjCreate.SelectedIndex = 0;
                }


            }
            if (cboObjCreate.SelectedItem == null) cboObjCreate.SelectedIndex = 0;
        }

        public Map.Object GetObjectUnderCursor()
        {
            if (MapInterface.CurrentMode != EditMode.OBJECT_SELECT && MapInterface.CurrentMode != EditMode.OBJECT_PLACE) return null;
            return MapInterface.ObjectSelect(mouseLocation);
        }
        public Map.Waypoint GetWPUnderCursor()
        {
            if (MapInterface.CurrentMode != EditMode.WAYPOINT_PLACE && MapInterface.CurrentMode != EditMode.WAYPOINT_PLACE) return null;
            return MapInterface.WaypointSelect(mouseLocation);
        }
        public Map.Wall GetWallUnderCursor(Point proxyPt = new Point())
        {
            Point pt;
            if (proxyPt.IsEmpty)
                pt = mouseLocation;
            else
                pt = proxyPt;

            if (picking && MapInterface.CurrentMode == EditMode.WALL_BRUSH) goto nocheck;

            if (MapInterface.CurrentMode != EditMode.WALL_PLACE && MapInterface.CurrentMode != EditMode.WALL_CHANGE && MapInterface.CurrentMode != EditMode.WALL_BRUSH) return null;
        nocheck:
            Point wallPt = GetNearestWallPoint(pt);
            if (!Map.Walls.ContainsKey(wallPt)) return null;
            return Map.Walls[wallPt];
        }
        public bool GetTileUnderCursor()
        {
            if (MapInterface.CurrentMode != EditMode.FLOOR_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_PLACE && MapInterface.CurrentMode != EditMode.EDGE_PLACE) return false;
            Point tilePt = GetNearestTilePoint(mouseLocation);
            if (!Map.Tiles.ContainsKey(tilePt)) return false;
            return true;
        }
        public bool GetEdgeUnderCursor()
        {
            if (MapInterface.CurrentMode != EditMode.EDGE_PLACE) return false;
            Point tilePt = GetNearestTilePoint(mouseLocation);
            if (!Map.Tiles.ContainsKey(tilePt)) return false;
            if (Map.Tiles[tilePt].EdgeTiles.Count <= 0) return false;

            return true;
        }


        public Point GetPosTileUnderCursor2()
        {
            Point tilePt = GetNearestTilePoint(mouseLocation);
            if (!Map.Tiles.ContainsKey(tilePt)) return Map.Tiles[tilePt].Location;
            return new Point();
        }

        void WaypointNameTextChanged(object sender, EventArgs e)
        {
            if (MapInterface.SelectedWaypoint != null)
            {
                MapInterface.SelectedWaypoint.Name = waypointName.Text;
            }
        }

        void WaypointEnabledCheckedChanged(object sender, EventArgs e)
        {
            if (MapInterface.SelectedWaypoint != null)
            {
                MapInterface.SelectedWaypoint.Flags = waypointEnabled.Checked ? 1 : 0;
            }
        }

        public void ModeSwitcher()
        {
            var page = tabMapTools.SelectedTab;
            if (page == tabTiles)
            {
                TileMakeNewCtrl.PlaceTileBtn.Checked = !TileMakeNewCtrl.PlaceTileBtn.Checked;
                TileMakeNewCtrl.AutoTileBtn.Checked = !TileMakeNewCtrl.PlaceTileBtn.Checked;
            }
            else if (page == tabWalls)
            {
                WallMakeNewCtrl.PlaceWalltBtn.Checked = !WallMakeNewCtrl.PlaceWalltBtn.Checked;
                WallMakeNewCtrl.AutoWalltBtn.Checked = !WallMakeNewCtrl.PlaceWalltBtn.Checked;
            }
            else if (page != tabEdges)
            {

                if (PlaceObjectBtn.Checked || SelectObjectBtn.Checked)
                {
                    PlaceObjectBtn.Checked = !PlaceObjectBtn.Checked;
                    SelectObjectBtn.Checked = !PlaceObjectBtn.Checked;
                }
                else
                {
                    placeWPBtn.Checked = !placeWPBtn.Checked;
                    selectWPBtn.Checked = !placeWPBtn.Checked;
                }
            }
        }


        public void TabMapToolsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PolygonEditDlg.Visible)
                return;

            var page = tabMapTools.SelectedTab;


            // Alter current mode depending on the tab testudo

            if (page == tabTiles)
                MapInterface.CurrentMode = (EditMode)getSelectedMode(TileMakeNewCtrl.buttons).Tag;
            else if (page == tabWalls)
            {
                if (WallMakeNewCtrl.WallProp.Visible)
                {
                    MapInterface.CurrentMode = EditMode.WALL_CHANGE;
                    return;
                }

                MapInterface.CurrentMode = (EditMode)getSelectedMode(WallMakeNewCtrl.buttons).Tag;
            }
            else if (page == tabEdges)
                MapInterface.CurrentMode = EditMode.EDGE_PLACE;
            else
                MapInterface.CurrentMode = (EditMode)getSelectedMode(buttons).Tag;




        }

        //objectGroupBtn
        private RadioButton getSelectedMode(Array group)
        {

            foreach (RadioButton rb in group)
            {

                if (rb.Checked)
                    return rb;

            }
            return null;

        }






        /*
            void TabMapToolsSelectedIndexChanged(object sender, EventArgs e)
            {
                // Alter current mode depending on the tab testudo
                var page = tabMapTools.SelectedTab;
                if (page == tabTiles)
                    MapInterface.CurrentMode = TileMakeNewCtrl.buttonMode.SelectedMode;
                else if (page == tabWalls)
                    MapInterface.CurrentMode = WallMakeNewCtrl.buttonMode.SelectedMode;
                else if (page == tabEdges)
                    MapInterface.CurrentMode = EditMode.EDGE_PLACE;
                else
                    MapInterface.CurrentMode = buttonObjectMode.SelectedMode;
            }
            */



        private void tabObjectWps_Click(object sender, EventArgs e)
        {

        }

        private void WallMakeNewCtrl_Load(object sender, EventArgs e)
        {

        }

        private void statusBar_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MapInterface.WaypointEnable();
        }

        private void tabEdges_Click(object sender, EventArgs e)
        {

        }

        private void tabEdges_Enter(object sender, EventArgs e)
        {
            int selectedIndex = MainWindow.Instance.mapView.TileMakeNewCtrl.comboTileType.SelectedIndex;
            string tileName = MainWindow.Instance.mapView.TileMakeNewCtrl.comboTileType.Items[selectedIndex].ToString();
            EdgeMakeNewCtrl.ignoreAllBox.Text = "Ignore all but " + tileName;
            EdgeMakeNewCtrl.preserveBox.Text = "Preserve " + tileName;
            EdgeMakeNewCtrl.UpdateListView(sender, e);


        }

        private void TileMakeNewCtrl_Load(object sender, EventArgs e)
        {

        }

        private void mapPanel_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void mapPanel_SizeChanged(object sender, EventArgs e)
        {

        }

        private void mapPanel_RegionChanged(object sender, EventArgs e)
        {

        }

        private void mapPanel_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void mapPanel_ParentChanged(object sender, EventArgs e)
        {

        }

        private void mapPanel_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void ObjectModesChanged_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void ObjectModesChanged(object sender, EventArgs e)
        {

            RadioButton radioButton = sender as RadioButton;
            radioButton.Font = new Font(radioButton.Font.Name, radioButton.Font.Size, FontStyle.Regular);
            if (!radioButton.Checked) return;
            radioButton.Font = new Font(radioButton.Font.Name, radioButton.Font.Size, FontStyle.Bold);

            Picker.Checked = false;
            MapInterface.CurrentMode = (EditMode)radioButton.Tag;



            if (radioButton.Name == "selectWPBtn" || radioButton.Name == "placeWPBtn" || radioButton.Name == "pathWPBtn")
            {
                PlaceObjectBtn.Checked = false;
                SelectObjectBtn.Checked = false;
            }
            else
            {
                selectWPBtn.Checked = false;
                placeWPBtn.Checked = false;
                pathWPBtn.Checked = false;

            }

        }

        private void objectGroupBtn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void objectGroup_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            foreach (Map.Object obj in Map.Objects)
            {
                if (ThingDb.Things[obj.Name].Xfer == "InvisibleLightXfer")
                {

                    InvisibleLightXfer xfer = obj.GetExtraData<InvisibleLightXfer>();

                    if (xfer.ChangeIntensity != 21 && xfer.ChangeIntensity != 22 && xfer.ChangeIntensity != 0)
                    {
                        MessageBox.Show("FOUND");
                        ShowObjectProperties(obj);
                        break;
                    }
                }
            }
        }

        private void tabWalls_Click(object sender, EventArgs e)
        {

        }



        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Picker_CheckedChanged(object sender, EventArgs e)
        {
            if (Picker.Checked)
            {
                TileMakeNewCtrl.Picker.Checked = true;
                WallMakeNewCtrl.Picker.Checked = true;
                EdgeMakeNewCtrl.Picker.Checked = true;
                picking = true;
                Cursor myCursor = Cursors.Cross;
                if (System.IO.File.Exists("picker.cur"))
                    myCursor = new Cursor("picker.cur");

                mapPanel.Cursor = myCursor;
            }
            else
            {
                picking = false;
                TileMakeNewCtrl.Picker.Checked = false;
                EdgeMakeNewCtrl.Picker.Checked = false;
                WallMakeNewCtrl.Picker.Checked = false;
                mapPanel.Cursor = Cursors.Default;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MapRenderer.FakeWalls.Clear();
            MapRenderer.UpdateCanvas(false, true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.menuItemSave.PerformClick();

        }

        private void Release(int step)
        {
            EditMode mode = TimeManager[step].Mode;

            if (mode == EditMode.POLYGON_RESHAPE)
            {
                foreach (Map.Polygon polygon in Map.Polygons)
                {
                    polygon.Points.Clear();
                }
                Map.Polygons.Clear();
                Map.Polygons = new Map.PolygonList();
                foreach (timepolygon polygon in TimeManager[step].Storedpolygons)
                {
                    Map.Polygons.Add(polygon.Polygon);
                    Map.Polygon itema = (Map.Polygon)Map.Polygons[Map.Polygons.Count - 1];

                    foreach (PointF polypoint in polygon.Points)
                        itema.Points.Add(polypoint);
                }
            }

            if (mode == EditMode.WAYPOINT_CONNECT || mode == EditMode.WAYPOINT_SELECT || mode == EditMode.WAYPOINT_PLACE)
            {

                foreach (Map.Waypoint wp in Map.Waypoints)
                {
                    wp.connections.Clear();
                }

                Map.Waypoints.num_wp.Clear();
                Map.Waypoints.Clear();
                //Map.Waypoints = new Map.WaypointList();

                foreach (timewaypoint wps in TimeManager[step].StoredWPs)
                {

                    Map.Waypoints.Add(wps.wp);
                    Map.Waypoint itema = (Map.Waypoint)Map.Waypoints[Map.Waypoints.Count - 1];
                    itema.Name = wps.Name;
                    itema.Point = wps.Location;
                    Map.Waypoints.num_wp.Add(wps.wp.Number, wps.wp);

                    foreach (Map.Waypoint.WaypointConnection wpcon in wps.connections)
                        itema.connections.Add(wpcon);
                }
            }
            if (mode == EditMode.OBJECT_SELECT || mode == EditMode.OBJECT_PLACE)
            {
                //CenterAtPoint(new Point((int)TimeManager[step+1].Location.X, (int)TimeManager[step+1].Location.Y));

                Map.Objects.Clear();
                Map.Objects = new Map.ObjectTable();

                foreach (timeobject item in TimeManager[step].StoredObjects)
                {
                    Map.Objects.Add(item.Object);
                    Map.Object itema = (Map.Object)Map.Objects[Map.Objects.Count - 1];
                    itema.Location = item.Location;
                }
                MapRenderer.UpdateCanvas(true, false);
                mapPanel.Invalidate();

            }

            if (mode == EditMode.WALL_PLACE || mode == EditMode.WALL_CHANGE || mode == EditMode.WALL_BRUSH)
            {

                Map.Walls.Clear();
                //Map.Walls = new Map.WallMap();
                foreach (timewall wall in TimeManager[step].StoredWalls)
                {
                    Map.Walls.Add(wall.Wall.Location, wall.Wall);
                    Map.Walls[wall.Wall.Location].Facing = wall.Facing;
                    Map.Walls[wall.Wall.Location].Secret_ScanFlags = (byte)wall.Sflags;
                    Map.Walls[wall.Wall.Location].Destructable = wall.Destructable;
                    Map.Walls[wall.Wall.Location].Minimap = wall.Minimap;
                    Map.Walls[wall.Wall.Location].Secret_WallState = wall.Secret_WallState;
                    Map.Walls[wall.Wall.Location].Secret_OpenWaitSeconds = wall.Secret_OpenWaitSeconds;
                }
            }
            if (mode == EditMode.FLOOR_BRUSH || mode == EditMode.EDGE_PLACE || mode == EditMode.FLOOR_PLACE)
            {
                foreach (Map.Tile tila in Map.Tiles.Values)
                {
                    tila.EdgeTiles.Clear();
                }

                Map.Tiles.Clear();
                Map.Tiles = new Map.FloorMap();

                foreach (timetile item in TimeManager[step].StoredTiles)
                {
                    Map.Tiles.Add(item.Tile.Location, item.Tile);


                    foreach (Map.Tile.EdgeTile edga in item.EdgeTiles)
                        Map.Tiles[item.Tile.Location].EdgeTiles.Add(edga);
                }
            }
        }


        private string getBaseMode(EditMode mode)
        {
            string modeString = mode.ToString();

            modeString = modeString.Substring(0, modeString.IndexOf("_")).Trim();

            return modeString;
        }

        public void Store(EditMode mode, TimeEvent Event)
        {
            int steps = TimeManager.Count - 1;


            if (TimeManager.Count > 1 && currentStep > 0)
            {
                TimeManager.RemoveRange((steps - currentStep) + 1, TimeManager.Count - ((steps - currentStep) + 1));
                currentStep = 0;
                redo.Enabled = false;
                StopRedo = true;
                MainWindow.Instance.redo.Enabled = false;
            }

            if (TimeManager.Count > 15)
                TimeManager.RemoveAt(0);

            /*
            if (TimeManager.Count <= 1)
            {
                StopUndo = true;
                MainWindow.Instance.undo.Enabled = false;
                undo.Enabled = false;

            }
            */
            if (steps - currentStep >= 0)
            {
                StopUndo = false;
                undo.Enabled = true;
                MainWindow.Instance.undo.Enabled = true;
            }

            if (steps < TimeManager.Count - 1)
            {
                StopRedo = false;
                redo.Enabled = true;
                MainWindow.Instance.redo.Enabled = true;

            }
            /////////////polygons///////////////
            if (mode == EditMode.POLYGON_RESHAPE)
            {
                TimePolygons.Clear();
                TimePolygons = new List<timepolygon>();

                TimeContent content = new TimeContent();
                content.Mode = MapInterface.CurrentMode;
                content.Event = Event;


                foreach (Map.Polygon polygon in Map.Polygons)
                {
                    timepolygon subject = new timepolygon();
                    subject.Points = new List<PointF>();

                    foreach (PointF point in polygon.Points)
                    {
                        subject.Points.Add(point);
                    }
                    subject.Polygon = polygon;

                    TimePolygons.Add(subject);

                    content.Storedpolygons.Add(subject);
                }
                int page = MainWindow.Instance.tabControl1.SelectedIndex;
                if (page == 1)
                {
                    Point MouseLocInMini = MainWindow.Instance.mouseLocation;
                    content.Location = new Point(MouseLocInMini.X / MainWindow.Instance.mapZoom * squareSize, MouseLocInMini.Y / MainWindow.Instance.mapZoom * squareSize);

                }
                else
                    content.Location = new Point(mouseLocation.X, mouseLocation.Y);
                TimeManager.Add(content);

                return;
            }
            ///////////Waypoints/////////////
            if (mode == EditMode.WAYPOINT_CONNECT || mode == EditMode.WAYPOINT_SELECT || mode == EditMode.WAYPOINT_PLACE)
            {
                TimeWPs.Clear();
                TimeWPs = new List<timewaypoint>();

                TimeContent content = new TimeContent();
                content.Mode = MapInterface.CurrentMode;
                content.Event = Event;
                foreach (Map.Waypoint wp in Map.Waypoints)
                {
                    timewaypoint subject = new timewaypoint();
                    subject.connections = new ArrayList();

                    foreach (Map.Waypoint.WaypointConnection wpc in wp.connections)
                    {
                        subject.connections.Add(wpc);
                    }
                    subject.Name = wp.Name;
                    subject.wp = wp;
                    subject.Location = wp.Point;

                    // TimeWPs.Add(subject);

                    content.StoredWPs.Add(subject);

                }
                content.Location = new Point(mouseLocation.X, mouseLocation.Y);
                TimeManager.Add(content);

                return;
            }

            //////////Objects/////////////
            if (mode == EditMode.OBJECT_SELECT || mode == EditMode.OBJECT_PLACE)
            {
                TimeContent content = new TimeContent();
                content.Mode = MapInterface.CurrentMode;
                content.Event = Event;
                TimeObjects.Clear();
                TimeObjects = new List<timeobject>();
                foreach (Map.Object item in Map.Objects)
                {
                    timeobject subject = new timeobject();
                    subject.Location = item.Location;
                    subject.Object = item;
                    TimeObjects.Add(subject);
                    content.StoredObjects.Add(subject);

                }
                content.Location = new Point(mouseLocation.X, mouseLocation.Y);
                TimeManager.Add(content);

                return;
            }
            ////////////Tiles//////////////
            if (mode == EditMode.FLOOR_BRUSH || mode == EditMode.EDGE_PLACE || mode == EditMode.FLOOR_PLACE)
            {
                TimeContent content = new TimeContent();
                content.Mode = MapInterface.CurrentMode;
                content.Event = Event;
                foreach (Map.Tile tila in Map.Tiles.Values)
                {

                    timetile subject = new timetile();
                    subject.EdgeTiles = new ArrayList();

                    foreach (Map.Tile.EdgeTile edga in tila.EdgeTiles)
                    {
                        subject.EdgeTiles.Add(edga);
                    }

                    subject.Tile = tila;

                    content.StoredTiles.Add(subject);


                }
                int page = MainWindow.Instance.tabControl1.SelectedIndex;
                if (page == 1)
                {
                    Point MouseLocInMini = MainWindow.Instance.mouseLocation;
                    content.Location = new Point(MouseLocInMini.X / MainWindow.Instance.mapZoom * squareSize, MouseLocInMini.Y / MainWindow.Instance.mapZoom * squareSize);

                }
                else
                    content.Location = new Point(mouseLocation.X, mouseLocation.Y);
                TimeManager.Add(content);

                return;
            }

            ///////////Walls/////////////
            if (mode == EditMode.WALL_PLACE || mode == EditMode.WALL_CHANGE || mode == EditMode.WALL_BRUSH)
            {
                TimeContent content = new TimeContent();
                content.Mode = MapInterface.CurrentMode;
                content.Event = Event;
                TimeWalls.Clear();
                TimeWalls = new List<timewall>();
                foreach (Map.Wall wall in Map.Walls.Values)
                {
                    timewall subject = new timewall();

                    subject.Wall = wall;
                    subject.Facing = wall.Facing;
                    subject.Sflags = (Map.Wall.SecretScanFlags)wall.Secret_ScanFlags;
                    subject.Minimap = wall.Minimap;
                    subject.Destructable = wall.Destructable;
                    subject.Secret_WallState = wall.Secret_WallState;
                    subject.Secret_OpenWaitSeconds = wall.Secret_OpenWaitSeconds;
                    TimeWalls.Add(subject);
                    content.StoredWalls.Add(subject);
                }

                int page = MainWindow.Instance.tabControl1.SelectedIndex;
                if (page == 1)
                {
                    Point MouseLocInMini = MainWindow.Instance.mouseLocation;
                    content.Location = new Point(MouseLocInMini.X / MainWindow.Instance.mapZoom * squareSize, MouseLocInMini.Y / MainWindow.Instance.mapZoom * squareSize);

                }
                else
                    content.Location = new Point(mouseLocation.X, mouseLocation.Y);
                TimeManager.Add(content);

                return;
            }

        }


        private void storePolygons()
        {
            TimePolygons.Clear();
            TimePolygons = new List<timepolygon>();

            TimeContent content = new TimeContent();
            content.Mode = MapInterface.CurrentMode;

            foreach (Map.Polygon polygon in Map.Polygons)
            {
                timepolygon subject = new timepolygon();
                subject.Points = new List<PointF>();

                foreach (PointF point in polygon.Points)
                {
                    subject.Points.Add(point);
                }

                subject.Polygon = polygon;

                TimePolygons.Add(subject);

                content.Storedpolygons.Add(subject);
            }
            content.Location = new Point(mouseLocation.X, mouseLocation.Y);
            TimeManager.Add(content);

        }
        private void releasePolygons()
        {
            foreach (Map.Polygon polygon in Map.Polygons)
            {
                polygon.Points.Clear();
            }

            Map.Polygons.Clear();
            Map.Polygons = new Map.PolygonList();

            foreach (timepolygon polygon in TimePolygons)
            {

                Map.Polygons.Add(polygon.Polygon);
                Map.Polygon itema = (Map.Polygon)Map.Polygons[Map.Polygons.Count - 1];

                foreach (PointF polypoint in polygon.Points)
                    itema.Points.Add(polypoint);

            }

        }

        private void storeWalls()
        {
            TimeWalls.Clear();
            TimeWalls = new List<timewall>();
            foreach (Map.Wall wall in Map.Walls.Values)
            {
                timewall subject = new timewall();

                subject.Wall = wall;
                subject.Facing = wall.Facing;
                TimeWalls.Add(subject);
            }

        }

        private void releaseWalls()
        {
            Map.Walls.Clear();
            Map.Walls = new Map.WallMap();
            foreach (timewall wall in TimeWalls)
            {
                Map.Walls.Add(wall.Wall.Location, wall.Wall);
                Map.Walls[wall.Wall.Location].Facing = wall.Facing;

            }


        }

        private void storeWaypoits()
        {
            TimeWPs.Clear();
            TimeWPs = new List<timewaypoint>();

            TimeContent content = new TimeContent();
            content.Mode = MapInterface.CurrentMode;
            foreach (Map.Waypoint wp in Map.Waypoints)
            {
                timewaypoint subject = new timewaypoint();
                subject.connections = new ArrayList();



                foreach (Map.Waypoint.WaypointConnection wpc in wp.connections)
                {
                    subject.connections.Add(wpc);
                }

                subject.Name = wp.Name;
                subject.wp = wp;
                subject.Location = wp.Point;

                // TimeWPs.Add(subject);

                content.StoredWPs.Add(subject);

            }
            TimeManager.Add(content);
        }
        private void releaseWaypoits()
        {
            foreach (Map.Waypoint wp in Map.Waypoints)
            {
                wp.connections.Clear();
            }

            Map.Waypoints.Clear();
            Map.Waypoints = new Map.WaypointList();

            foreach (timewaypoint wps in TimeManager[0].StoredWPs)
            //foreach (timewaypoint wps in TimeWPs)
            {

                Map.Waypoints.Add(wps.wp);
                Map.Waypoint itema = (Map.Waypoint)Map.Waypoints[Map.Waypoints.Count - 1];
                itema.Name = wps.Name;
                itema.Point = wps.Location;


                foreach (Map.Waypoint.WaypointConnection wpcon in wps.connections)
                    itema.connections.Add(wpcon);

            }

        }
        private void storeObjects()
        {
            TimeObjects.Clear();
            TimeObjects = new List<timeobject>();
            foreach (Map.Object item in Map.Objects)
            {
                timeobject subject = new timeobject();
                subject.Location = item.Location;
                subject.Object = item;
                TimeObjects.Add(subject);

            }

        }

        private void releaseObjects()
        {

            Map.Objects.Clear();
            Map.Objects = new Map.ObjectTable();
            foreach (timeobject item in TimeObjects)
            {
                Map.Objects.Add(item.Object);
                Map.Object itema = (Map.Object)Map.Objects[Map.Objects.Count - 1];

                itema.Location = item.Location;
            }

            MapRenderer.UpdateCanvas(true, false);
            mapPanel.Invalidate();

        }

        private void storeTiles()
        {

            TimeContent content = new TimeContent();
            content.Mode = MapInterface.CurrentMode;

            foreach (Map.Tile tila in Map.Tiles.Values)
            {

                timetile subject = new timetile();
                subject.EdgeTiles = new ArrayList();

                foreach (Map.Tile.EdgeTile edga in tila.EdgeTiles)
                {
                    subject.EdgeTiles.Add(edga);
                }

                subject.Tile = tila;

                content.StoredTiles.Add(subject);

            }
            TimeManager.Add(content);

        }
        private void releaseTiles()
        {
            foreach (Map.Tile tila in Map.Tiles.Values)
            {
                tila.EdgeTiles.Clear();
            }

            Map.Tiles.Clear();
            Map.Tiles = new Map.FloorMap();



            foreach (timetile item in TimeManager[0].StoredTiles)
            {
                Map.Tiles.Add(item.Tile.Location, item.Tile);


                foreach (Map.Tile.EdgeTile edga in item.EdgeTiles)
                    Map.Tiles[item.Tile.Location].EdgeTiles.Add(edga);
            }

            MapRenderer.UpdateCanvas(false, true);
            mapPanel.Invalidate();
        }



        /// /////////////////////////////////////
        /// 
        /*
         private void releaseTiles()
         {
             foreach (Map.Tile tila in Map.Tiles.Values)
             {
                 tila.EdgeTiles.Clear();
             }
             Map.Tiles.Clear();
             Map.Tiles = new Map.FloorMap();
             foreach (Map.Tile item in Time.Values)
             {
                 Map.Tiles.Add(item.Location, item);
             }
             foreach (timetile tilasaa in TimeEdges)
                 Map.Tiles[tilasaa.Location].EdgeTiles.Add(tilasaa.edge);

             MapRenderer.UpdateCanvas(false, true);
             mapPanel.Invalidate();
         }
        */


        public void redo_Click(object sender, EventArgs e)
        {
            if (StopRedo || BlockTime) return;
            Redo();

        }

        public void undo_Click(object sender, EventArgs e)
        {
            if (StopUndo || BlockTime) return;
            Undo();

        }
        public void Undo(bool timed = true)
        {


            if (timed && !UndoTimer.Enabled)
            {
                int stepsT = TimeManager.Count - 1;
                int currentStepT = currentStep + 1;
                stepsT -= currentStepT;
                int panelVisibleH = scrollPanel.Height;
                int panelVisibleW = scrollPanel.Width;
                Rectangle visibleArea = new Rectangle(-mapPanel.Location.X, -mapPanel.Location.Y + 5, panelVisibleW - 5, panelVisibleH - 5);
                int x = (int)TimeManager[stepsT + 1].Location.X;
                int y = (int)TimeManager[stepsT + 1].Location.Y;

                // MessageBox.Show(visibleArea.Width.ToString());
                if (stepsT < TimeManager.Count - 1)
                {
                    StopRedo = false;
                    redo.Enabled = true;
                    MainWindow.Instance.redo.Enabled = true;
                }

                if (stepsT < 1)
                {
                    StopUndo = true;
                    undo.Enabled = false;
                    MainWindow.Instance.undo.Enabled = false;
                }




                if (!visibleArea.Contains(x, y))
                {
                    highlightUndoRedo = new Point(x, y);
                    CenterAtPoint(new Point(x, y));
                    UndoTimer.Enabled = true;
                    return;
                }
            }
            highlightUndoRedo = new Point();
            UndoTimer.Enabled = false;
            int steps = TimeManager.Count - 1;
            currentStep++;

            steps -= currentStep;

            Release(steps);

            MapRenderer.UpdateCanvas(true, true);
            mapPanel.Invalidate();

            debugTime();

            if (steps < TimeManager.Count - 1)
            {
                StopRedo = false;
                redo.Enabled = true;
                MainWindow.Instance.redo.Enabled = true;
            }

            if (steps < 1)
            {
                StopUndo = true;
                undo.Enabled = false;
                MainWindow.Instance.undo.Enabled = false;
                // TimeManager.Clear();
                //currentStep = 0;
                return;
            }



            if (TimeManager[steps].Event == TimeEvent.PRE)
                Undo(false);


        }

        public void Redo(bool timed = true)
        {


            if (timed && !RedoTimer.Enabled)
            {
                int stepsT = TimeManager.Count - 1;
                int currentStepT = currentStep - 1;
                stepsT -= currentStepT;
                int panelVisibleH = scrollPanel.Height;
                int panelVisibleW = scrollPanel.Width;
                Rectangle visibleArea = new Rectangle(-mapPanel.Location.X, -mapPanel.Location.Y + 5, panelVisibleW - 5, panelVisibleH - 5);
                int x = (int)TimeManager[stepsT].Location.X;
                int y = (int)TimeManager[stepsT].Location.Y;


                if (currentStepT >= 0)
                {
                    StopUndo = false;
                    undo.Enabled = true;
                    MainWindow.Instance.undo.Enabled = true;
                }

                if (stepsT >= TimeManager.Count - 1)
                {
                    StopRedo = true;
                    redo.Enabled = false;
                    MainWindow.Instance.redo.Enabled = false;
                }


                if (!visibleArea.Contains(x, y))
                {
                    highlightUndoRedo = new Point(x, y);
                    CenterAtPoint(new Point(x, y));

                    RedoTimer.Enabled = true;
                    StopRedo = false;
                    return;
                }
            }

            RedoTimer.Enabled = false;
            highlightUndoRedo = new Point();
            int steps = TimeManager.Count - 1;
            currentStep--;

            steps -= currentStep;

            Release(steps);

            MapRenderer.UpdateCanvas(true, true);
            mapPanel.Invalidate();

            debugTime();

            if (currentStep >= 0)
            {
                StopUndo = false;
                undo.Enabled = true;
                MainWindow.Instance.undo.Enabled = true;
            }

            if (steps >= TimeManager.Count - 1)
            {
                StopRedo = true;
                redo.Enabled = false;
                MainWindow.Instance.redo.Enabled = false;
                return;
            }



            if (TimeManager[steps].Event == TimeEvent.PRE)
                Redo(false);
        }



        private void groupAdv_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick_1(object sender, MouseEventArgs e)
        {

            MessageBox.Show(getBaseMode(MapInterface.CurrentMode));


        }

        private void prwSwitch_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabTiles_Click(object sender, EventArgs e)
        {

        }

        private void WallMakeNewCtrl_Load_1(object sender, EventArgs e)
        {

        }

        private void RedoUndoTimer_Tick(object sender, EventArgs e)
        {


        }

        private void UndoTimer_Tick(object sender, EventArgs e)
        {

            higlightRad -= 30;

            if (higlightRad > 40) return;

            Undo(false);
            UndoTimer.Enabled = false;
            highlightUndoRedo = new Point();
            higlightRad = 150;
        }

        private void RedoTimer_Tick(object sender, EventArgs e)
        {
            higlightRad -= 30;

            if (higlightRad > 40) return;

            Redo(false);
            RedoTimer.Enabled = false;
            highlightUndoRedo = new Point();
            higlightRad = 150;
        }
        private void debugTime()
        {
            if (listBox1.Visible != true)
                return;

            listBox1.Items.Clear();
            int i = 0;
            foreach (TimeContent tm in TimeManager)
            {
                string info = tm.Mode.ToString() + " e:" + tm.Event;
                listBox1.Items.Add(info);
                i++;
            }
            listBox1.SelectedIndex = (TimeManager.Count - 1) - currentStep;
        }
        private void prwSwitch_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.menuUseNewRenderer.PerformClick();
        }

        private void listBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void mapPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PolygonEditDlg.Visible && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && PolygonEditDlg.SelectedPolygon != null && PolygonEditDlg.SelectedPolygon.Points.Count > 2)
            {
                if (PolygonEditDlg.SelectedPolygon.IsPointInside(new Point(e.X, e.Y)))
                    PolygonEditDlg.ButtonModifyClick(sender, e);
            }

            if (MapInterface.CurrentMode == EditMode.OBJECT_SELECT)
                ShowObjectProperties(MapInterface.ObjectSelect(new Point(e.X, e.Y)));
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

            string content = "";
            foreach (Map.Object obj in SelectedObjects.Items)
            {
                content = content + obj.Extent.ToString() + Environment.NewLine;

            }
            Clipboard.SetDataObject(content, false);

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mapPanel.Cursor = Cursors.Default;
        }

        private void buttonWaypointMode_Click(object sender, EventArgs e)
        {

        }

        private void objectPreview_Click(object sender, EventArgs e)
        {

        }

    }
}