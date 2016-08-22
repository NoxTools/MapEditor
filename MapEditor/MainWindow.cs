using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NoxShared;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using MapEditor.render;
//using MapEditor.noxscript2;
using MapEditor.newgui;
using MapEditor.MapInt;


namespace MapEditor
{
    public class MainWindow : Form
    {
        private TabPage largeMap;
        private MainMenu mainMenu1;
        private MenuItem menuItemOpen;
        private MenuItem menuSeparator1;
        private MenuItem menuItemExit;
        public TabControl tabControl1;
        private TabPage mapInfoTab;
        private Label labelTitle;
        private GroupBox groupBox1;
        private TextBox mapSummary;
        private Label labelDescription;
        private Label labelVersion;
        private TextBox mapDescription;
        private Label labelAuthor;
        private Label labelAuthor2;
        private Label labelEmail;
        private Label labelEmail2;
        private TextBox mapAuthor;
        private TextBox mapEmail;
        private TextBox mapEmail2;
        private TextBox mapAuthor2;
        private Label labelDate;
        private TextBox mapDate;
        private TextBox mapVersion;
        private Label labelCopyright;
        public MenuItem menuItemSave;
        private Label minRecLbl;
        private Label maxRecLbl;
        private Label recommendedLbl;
        private Label mapTypeLbl;
        private TextBox mapMinRec;
        private TextBox mapMaxRec;
        private ComboBox mapType;
        private MenuItem menuItemAbout;
        private MenuItem menuItemNew;
        private MenuItem menuItemSaveAs;
        private MenuItem menuGroupMap;
        private MenuItem viewObjects;
        private TextBox mapCopyright;
        private MenuItem menuGroupOptions;
        public int mapZoom = 2, mapDimension = 256;
        public Point mouseLocation;
        Bitmap bitmap2;
        Rectangle redraw;
        public bool RightDown = false;
        protected Map map
        {
            get
            {
                return MapInterface.TheMap;
            }
        }

        public MapView mapView;
        private MenuItem menuGroupFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox questTitle;
        private System.Windows.Forms.TextBox questGraphic;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.IContainer components;
        private MenuItem menuItemGroups;
        private TabPage minimapTab;
        //private Panel miniViewPanel;
        public MainWindow.FlickerFreePanel miniViewPanel;
        private Panel MinimapPanel;
        private GroupBox groupBox2;
        private Button buttonCenter;
        private MenuItem menuItem12;
        private CheckBox chkDevide;
        private CheckBox chkDevide2;
        protected IList cultures;
        private MenuItem menuGroupView;
        private MenuItem menuShowGrid;
        public MenuItem menuUseNewRenderer;
        private Label label4;
        private Panel ambientColorPanel;
        public static MainWindow Instance;
        private MenuItem menuItemSettings;
        private MenuItem menuItem1;
        private MenuItem menuItemPolygons;
        private MenuItem menuItemExportImg;
        private MenuItem menuScripts;
        private Panel panel1;
        private NumericUpDown numericUpDown1;
        private Label label5;
        private CheckBox miniEdit;
        private Panel MiniEditPanel;
        private Label labelSep2;
        private RadioButton miniTileBrush;
        private RadioButton miniTilePLace;
        private RadioButton miniWallBrush;
        private NumericUpDown numericUpDown2;
        private Label label8;
        private Label label7;
        private Label label6;
        public CheckBox MiniLineWall;
        public Button redo;
        public Button undo;
        private Label label9;
        private MenuItem menuItem2;
        private MenuItem undoc;
        private MenuItem redoc;
        private MenuItem fastc;
        private MenuItem pickerc;
        private MenuItem recWallc;
        private MenuItem LineWallc;
        private bool moved = false;
        private Button button1;
        private Label label10;
        private MenuItem menuItem3;
        private MenuItem WallDraw;
        private MenuItem mapInstall;
        private MenuItem ObjectDraw;
        private bool added;
        private MenuItem fullExtentsDraw;
        private MenuItem GridDraw;
        private MenuItem menuItem4;
        private MenuItem menuItemImportScript;
        private MenuItem menuItemExportScript;
        
        private const string TITLE_FORMAT = "NoxEdit2014: {0} ( {1} )";

        public class FlickerFreePanel : Panel
        {
            public FlickerFreePanel()
                : base()
            { SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true); }
            // set styles to reduce flicker and painting over twice
        }


        public MainWindow(string[] args)
        {
            Instance = this;
            // Show the loading splash screen
            Splash spl = new Splash();
            spl.Show();
            spl.Refresh();
            // Setup locales
            string m_ExePath = Process.GetCurrentProcess().MainModule.FileName;
            Environment.CurrentDirectory = Path.GetDirectoryName(m_ExePath);
            cultures = GetSupportedCultures();
            InitializeComponent();

            // Set up map type selector (arena by default)
            mapType.Items.AddRange(new ArrayList(Map.MapInfo.MapTypeNames.Values).ToArray());
            mapType.SelectedIndex = 3;
            // load categories xml
            mapView.LoadObjectCategories();

            // Keep up shortcut menus with current settings
            menuShowGrid.Checked = EditorSettings.Default.Draw_Grid;
            menuUseNewRenderer.Checked = EditorSettings.Default.Edit_PreviewMode;

            LoadNewMap();
            if (args.Length > 0)
            {
                if (File.Exists(args[0])) MapInterface.SwitchMap(args[0]);
            }
            spl.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>

        /// Required method for Designer support - do not modify

        /// the contents of this method with the code editor.

        /// </summary>

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuGroupFile = new System.Windows.Forms.MenuItem();
            this.menuItemNew = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.mapInstall = new System.Windows.Forms.MenuItem();
            this.menuItemSave = new System.Windows.Forms.MenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemImportScript = new System.Windows.Forms.MenuItem();
            this.menuItemExportScript = new System.Windows.Forms.MenuItem();
            this.menuSeparator1 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuGroupMap = new System.Windows.Forms.MenuItem();
            this.viewObjects = new System.Windows.Forms.MenuItem();
            this.menuScripts = new System.Windows.Forms.MenuItem();
            this.menuItemGroups = new System.Windows.Forms.MenuItem();
            this.menuItemPolygons = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuGroupOptions = new System.Windows.Forms.MenuItem();
            this.menuItemSettings = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuGroupView = new System.Windows.Forms.MenuItem();
            this.menuShowGrid = new System.Windows.Forms.MenuItem();
            this.menuUseNewRenderer = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemExportImg = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.undoc = new System.Windows.Forms.MenuItem();
            this.redoc = new System.Windows.Forms.MenuItem();
            this.fastc = new System.Windows.Forms.MenuItem();
            this.pickerc = new System.Windows.Forms.MenuItem();
            this.recWallc = new System.Windows.Forms.MenuItem();
            this.LineWallc = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.GridDraw = new System.Windows.Forms.MenuItem();
            this.WallDraw = new System.Windows.Forms.MenuItem();
            this.ObjectDraw = new System.Windows.Forms.MenuItem();
            this.fullExtentsDraw = new System.Windows.Forms.MenuItem();
            this.mapInfoTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkServPlayerLimit = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ambientColorPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.questGraphic = new System.Windows.Forms.TextBox();
            this.questTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mapType = new System.Windows.Forms.ComboBox();
            this.mapTypeLbl = new System.Windows.Forms.Label();
            this.recommendedLbl = new System.Windows.Forms.Label();
            this.maxRecLbl = new System.Windows.Forms.Label();
            this.minRecLbl = new System.Windows.Forms.Label();
            this.mapMaxRec = new System.Windows.Forms.TextBox();
            this.mapMinRec = new System.Windows.Forms.TextBox();
            this.mapCopyright = new System.Windows.Forms.TextBox();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.mapVersion = new System.Windows.Forms.TextBox();
            this.mapDate = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.mapAuthor2 = new System.Windows.Forms.TextBox();
            this.mapEmail2 = new System.Windows.Forms.TextBox();
            this.mapEmail = new System.Windows.Forms.TextBox();
            this.mapAuthor = new System.Windows.Forms.TextBox();
            this.labelEmail2 = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelAuthor2 = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.mapDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.mapSummary = new System.Windows.Forms.TextBox();
            this.minimapTab = new System.Windows.Forms.TabPage();
            this.MinimapPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.miniViewPanel = new MapEditor.MainWindow.FlickerFreePanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MiniEditPanel = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.redo = new System.Windows.Forms.Button();
            this.undo = new System.Windows.Forms.Button();
            this.MiniLineWall = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.miniTileBrush = new System.Windows.Forms.RadioButton();
            this.miniTilePLace = new System.Windows.Forms.RadioButton();
            this.miniWallBrush = new System.Windows.Forms.RadioButton();
            this.labelSep2 = new System.Windows.Forms.Label();
            this.miniEdit = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.chkDevide2 = new System.Windows.Forms.CheckBox();
            this.chkDevide = new System.Windows.Forms.CheckBox();
            this.buttonCenter = new System.Windows.Forms.Button();
            this.largeMap = new System.Windows.Forms.TabPage();
            this.mapView = new MapEditor.MapView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mapInfoTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.minimapTab.SuspendLayout();
            this.MinimapPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.MiniEditPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.largeMap.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuGroupFile
            // 
            this.menuGroupFile.Index = 0;
            this.menuGroupFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.mapInstall,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.menuItem4,
            this.menuItemImportScript,
            this.menuItemExportScript,
            this.menuSeparator1,
            this.menuItemExit});
            this.menuGroupFile.Text = "File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Index = 0;
            this.menuItemNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItemNew.Text = "New";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 1;
            this.menuItemOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemOpen.Text = "Open";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // mapInstall
            // 
            this.mapInstall.Index = 2;
            this.mapInstall.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.mapInstall.Text = "Install Map";
            this.mapInstall.Click += new System.EventHandler(this.mapInstall_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Index = 3;
            this.menuItemSave.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.menuItemSave.Text = "&Save";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Index = 4;
            this.menuItemSaveAs.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuItemSaveAs.Text = "Save As...";
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 5;
            this.menuItem4.Text = "-";
            // 
            // menuItemImportScript
            // 
            this.menuItemImportScript.Index = 6;
            this.menuItemImportScript.Text = "Import Script";
            this.menuItemImportScript.Click += new System.EventHandler(this.menuItemImportScript_Click);
            // 
            // menuItemExportScript
            // 
            this.menuItemExportScript.Index = 7;
            this.menuItemExportScript.Text = "Export Script";
            this.menuItemExportScript.Click += new System.EventHandler(this.menuItemExportScript_Click);
            // 
            // menuSeparator1
            // 
            this.menuSeparator1.Index = 8;
            this.menuSeparator1.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 9;
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuGroupMap
            // 
            this.menuGroupMap.Index = 1;
            this.menuGroupMap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.viewObjects,
            this.menuScripts,
            this.menuItemGroups,
            this.menuItemPolygons,
            this.menuItem12});
            this.menuGroupMap.Text = "Map";
            // 
            // viewObjects
            // 
            this.viewObjects.Index = 0;
            this.viewObjects.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftL;
            this.viewObjects.Text = "List Objects";
            this.viewObjects.Click += new System.EventHandler(this.viewObjects_Click);
            // 
            // menuScripts
            // 
            this.menuScripts.Index = 1;
            this.menuScripts.Text = "Scripts";
            this.menuScripts.Click += new System.EventHandler(this.menuScripts_Click_1);
            // 
            // menuItemGroups
            // 
            this.menuItemGroups.Index = 2;
            this.menuItemGroups.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftG;
            this.menuItemGroups.Text = "Groups";
            this.menuItemGroups.Click += new System.EventHandler(this.menuItemGroups_Click);
            // 
            // menuItemPolygons
            // 
            this.menuItemPolygons.Index = 3;
            this.menuItemPolygons.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
            this.menuItemPolygons.Text = "&Polygons";
            this.menuItemPolygons.Click += new System.EventHandler(this.menuItemPolygons_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 4;
            this.menuItem12.Text = "Reorder Extents";
            this.menuItem12.Click += new System.EventHandler(this.menuItemReorderExt_Click);
            // 
            // menuGroupOptions
            // 
            this.menuGroupOptions.Index = 3;
            this.menuGroupOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSettings});
            this.menuGroupOptions.Text = "&Options";
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Index = 0;
            this.menuItemSettings.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.menuItemSettings.Text = "Settings";
            this.menuItemSettings.Click += new System.EventHandler(this.SettingsItemClick);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 4;
            this.menuItemAbout.Text = "About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuGroupFile,
            this.menuGroupMap,
            this.menuGroupView,
            this.menuGroupOptions,
            this.menuItemAbout,
            this.menuItem2});
            // 
            // menuGroupView
            // 
            this.menuGroupView.Index = 2;
            this.menuGroupView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuShowGrid,
            this.menuUseNewRenderer,
            this.menuItem1,
            this.menuItemExportImg});
            this.menuGroupView.Text = "View";
            // 
            // menuShowGrid
            // 
            this.menuShowGrid.Checked = true;
            this.menuShowGrid.Index = 0;
            this.menuShowGrid.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.menuShowGrid.Text = "&Show Grid";
            this.menuShowGrid.Click += new System.EventHandler(this.menuShowGrid_Click);
            // 
            // menuUseNewRenderer
            // 
            this.menuUseNewRenderer.Index = 1;
            this.menuUseNewRenderer.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuUseNewRenderer.Text = "Visual Preview Mode";
            this.menuUseNewRenderer.Click += new System.EventHandler(this.menuUseNewRenderer_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "Invert Colors";
            this.menuItem1.Click += new System.EventHandler(this.menuItemInvertColors_Click);
            // 
            // menuItemExportImg
            // 
            this.menuItemExportImg.Index = 3;
            this.menuItemExportImg.Text = "Export Image";
            this.menuItemExportImg.Click += new System.EventHandler(this.exportImageMenu_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.undoc,
            this.redoc,
            this.fastc,
            this.pickerc,
            this.recWallc,
            this.LineWallc,
            this.menuItem3,
            this.GridDraw,
            this.WallDraw,
            this.ObjectDraw,
            this.fullExtentsDraw});
            this.menuItem2.Text = "Commands";
            this.menuItem2.Visible = false;
            // 
            // undoc
            // 
            this.undoc.Index = 0;
            this.undoc.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.undoc.Text = "Undo";
            this.undoc.Click += new System.EventHandler(this.undoc_Click);
            // 
            // redoc
            // 
            this.redoc.Index = 1;
            this.redoc.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.redoc.Text = "Redo";
            this.redoc.Click += new System.EventHandler(this.redoc_Click);
            // 
            // fastc
            // 
            this.fastc.Index = 2;
            this.fastc.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.fastc.Text = "Fast Preview";
            this.fastc.Click += new System.EventHandler(this.fastc_Click);
            // 
            // pickerc
            // 
            this.pickerc.Index = 3;
            this.pickerc.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.pickerc.Text = "Picker";
            this.pickerc.Click += new System.EventHandler(this.pickerc_Click);
            // 
            // recWallc
            // 
            this.recWallc.Index = 4;
            this.recWallc.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.recWallc.Text = "Rectangle Draw";
            this.recWallc.Click += new System.EventHandler(this.recWallc_Click);
            // 
            // LineWallc
            // 
            this.LineWallc.Index = 5;
            this.LineWallc.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.LineWallc.Text = "Line Draw";
            this.LineWallc.Click += new System.EventHandler(this.LineWallc_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 6;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.menuItem3.Text = "45 Degree Selection";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click_1);
            // 
            // GridDraw
            // 
            this.GridDraw.Index = 7;
            this.GridDraw.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.GridDraw.Text = "Draw Grid";
            this.GridDraw.Click += new System.EventHandler(this.GridDraw_Click);
            // 
            // WallDraw
            // 
            this.WallDraw.Index = 8;
            this.WallDraw.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.WallDraw.Text = "Draw Walls";
            this.WallDraw.Click += new System.EventHandler(this.WallDraw_Click);
            // 
            // ObjectDraw
            // 
            this.ObjectDraw.Index = 9;
            this.ObjectDraw.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.ObjectDraw.Text = "Draw Objects";
            this.ObjectDraw.Click += new System.EventHandler(this.ObjectDraw_Click);
            // 
            // fullExtentsDraw
            // 
            this.fullExtentsDraw.Index = 10;
            this.fullExtentsDraw.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.fullExtentsDraw.Text = "3d Extents Draw";
            this.fullExtentsDraw.Click += new System.EventHandler(this.fullExtentsDraw_Click);
            // 
            // mapInfoTab
            // 
            this.mapInfoTab.Controls.Add(this.groupBox1);
            this.mapInfoTab.Location = new System.Drawing.Point(4, 22);
            this.mapInfoTab.Name = "mapInfoTab";
            this.mapInfoTab.Size = new System.Drawing.Size(921, 681);
            this.mapInfoTab.TabIndex = 0;
            this.mapInfoTab.Text = "Map Info";
            this.mapInfoTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkServPlayerLimit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ambientColorPanel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.questGraphic);
            this.groupBox1.Controls.Add(this.questTitle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mapType);
            this.groupBox1.Controls.Add(this.mapTypeLbl);
            this.groupBox1.Controls.Add(this.recommendedLbl);
            this.groupBox1.Controls.Add(this.maxRecLbl);
            this.groupBox1.Controls.Add(this.minRecLbl);
            this.groupBox1.Controls.Add(this.mapMaxRec);
            this.groupBox1.Controls.Add(this.mapMinRec);
            this.groupBox1.Controls.Add(this.mapCopyright);
            this.groupBox1.Controls.Add(this.labelCopyright);
            this.groupBox1.Controls.Add(this.mapVersion);
            this.groupBox1.Controls.Add(this.mapDate);
            this.groupBox1.Controls.Add(this.labelDate);
            this.groupBox1.Controls.Add(this.mapAuthor2);
            this.groupBox1.Controls.Add(this.mapEmail2);
            this.groupBox1.Controls.Add(this.mapEmail);
            this.groupBox1.Controls.Add(this.mapAuthor);
            this.groupBox1.Controls.Add(this.labelEmail2);
            this.groupBox1.Controls.Add(this.labelEmail);
            this.groupBox1.Controls.Add(this.labelAuthor2);
            this.groupBox1.Controls.Add(this.labelAuthor);
            this.groupBox1.Controls.Add(this.labelVersion);
            this.groupBox1.Controls.Add(this.mapDescription);
            this.groupBox1.Controls.Add(this.labelDescription);
            this.groupBox1.Controls.Add(this.labelTitle);
            this.groupBox1.Controls.Add(this.mapSummary);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(921, 560);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // checkServPlayerLimit
            // 
            this.checkServPlayerLimit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkServPlayerLimit.Location = new System.Drawing.Point(288, 315);
            this.checkServPlayerLimit.Name = "checkServPlayerLimit";
            this.checkServPlayerLimit.Size = new System.Drawing.Size(104, 24);
            this.checkServPlayerLimit.TabIndex = 34;
            this.checkServPlayerLimit.Text = "Server Settings";
            this.checkServPlayerLimit.UseVisualStyleBackColor = true;
            this.checkServPlayerLimit.CheckedChanged += new System.EventHandler(this.CheckServPlayerLimitCheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(256, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Ambient Color";
            // 
            // ambientColorPanel
            // 
            this.ambientColorPanel.Location = new System.Drawing.Point(259, 384);
            this.ambientColorPanel.Name = "ambientColorPanel";
            this.ambientColorPanel.Size = new System.Drawing.Size(69, 36);
            this.ambientColorPanel.TabIndex = 32;
            this.ambientColorPanel.Click += new System.EventHandler(this.ambientColorPanel_Click);
            // 
            // label3
            // 
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(72, 352);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 24);
            this.label3.TabIndex = 31;
            this.label3.Text = "Quest Intro";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // questGraphic
            // 
            this.questGraphic.Location = new System.Drawing.Point(88, 416);
            this.questGraphic.Name = "questGraphic";
            this.questGraphic.Size = new System.Drawing.Size(128, 20);
            this.questGraphic.TabIndex = 30;
            // 
            // questTitle
            // 
            this.questTitle.Location = new System.Drawing.Point(88, 384);
            this.questTitle.Name = "questTitle";
            this.questTitle.Size = new System.Drawing.Size(128, 20);
            this.questTitle.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(32, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 24);
            this.label2.TabIndex = 28;
            this.label2.Text = "Graphic";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(32, 384);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 27;
            this.label1.Text = "Title";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mapType
            // 
            this.mapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapType.FormattingEnabled = true;
            this.mapType.ItemHeight = 13;
            this.mapType.Location = new System.Drawing.Point(88, 24);
            this.mapType.Name = "mapType";
            this.mapType.Size = new System.Drawing.Size(88, 21);
            this.mapType.TabIndex = 26;
            // 
            // mapTypeLbl
            // 
            this.mapTypeLbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mapTypeLbl.Location = new System.Drawing.Point(24, 24);
            this.mapTypeLbl.Name = "mapTypeLbl";
            this.mapTypeLbl.Size = new System.Drawing.Size(64, 24);
            this.mapTypeLbl.TabIndex = 25;
            this.mapTypeLbl.Text = "Map Type";
            this.mapTypeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // recommendedLbl
            // 
            this.recommendedLbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.recommendedLbl.Location = new System.Drawing.Point(256, 264);
            this.recommendedLbl.Name = "recommendedLbl";
            this.recommendedLbl.Size = new System.Drawing.Size(184, 24);
            this.recommendedLbl.TabIndex = 24;
            this.recommendedLbl.Text = "Recommended Number of Players";
            this.recommendedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // maxRecLbl
            // 
            this.maxRecLbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.maxRecLbl.Location = new System.Drawing.Point(328, 288);
            this.maxRecLbl.Name = "maxRecLbl";
            this.maxRecLbl.Size = new System.Drawing.Size(32, 24);
            this.maxRecLbl.TabIndex = 23;
            this.maxRecLbl.Text = "Max";
            this.maxRecLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // minRecLbl
            // 
            this.minRecLbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.minRecLbl.Location = new System.Drawing.Point(256, 288);
            this.minRecLbl.Name = "minRecLbl";
            this.minRecLbl.Size = new System.Drawing.Size(32, 24);
            this.minRecLbl.TabIndex = 22;
            this.minRecLbl.Text = "Min";
            this.minRecLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mapMaxRec
            // 
            this.mapMaxRec.Location = new System.Drawing.Point(360, 288);
            this.mapMaxRec.Name = "mapMaxRec";
            this.mapMaxRec.Size = new System.Drawing.Size(32, 20);
            this.mapMaxRec.TabIndex = 21;
            // 
            // mapMinRec
            // 
            this.mapMinRec.Location = new System.Drawing.Point(288, 288);
            this.mapMinRec.Name = "mapMinRec";
            this.mapMinRec.Size = new System.Drawing.Size(32, 20);
            this.mapMinRec.TabIndex = 20;
            // 
            // mapCopyright
            // 
            this.mapCopyright.Location = new System.Drawing.Point(88, 264);
            this.mapCopyright.Name = "mapCopyright";
            this.mapCopyright.Size = new System.Drawing.Size(128, 20);
            this.mapCopyright.TabIndex = 17;
            // 
            // labelCopyright
            // 
            this.labelCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCopyright.Location = new System.Drawing.Point(8, 264);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(72, 24);
            this.labelCopyright.TabIndex = 16;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mapVersion
            // 
            this.mapVersion.Location = new System.Drawing.Point(88, 288);
            this.mapVersion.Name = "mapVersion";
            this.mapVersion.Size = new System.Drawing.Size(128, 20);
            this.mapVersion.TabIndex = 15;
            // 
            // mapDate
            // 
            this.mapDate.Location = new System.Drawing.Point(88, 312);
            this.mapDate.Name = "mapDate";
            this.mapDate.Size = new System.Drawing.Size(128, 20);
            this.mapDate.TabIndex = 14;
            // 
            // labelDate
            // 
            this.labelDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDate.Location = new System.Drawing.Point(8, 312);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(64, 24);
            this.labelDate.TabIndex = 13;
            this.labelDate.Text = "Date";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mapAuthor2
            // 
            this.mapAuthor2.Location = new System.Drawing.Point(88, 224);
            this.mapAuthor2.Name = "mapAuthor2";
            this.mapAuthor2.Size = new System.Drawing.Size(128, 20);
            this.mapAuthor2.TabIndex = 12;
            // 
            // mapEmail2
            // 
            this.mapEmail2.Location = new System.Drawing.Point(288, 224);
            this.mapEmail2.Name = "mapEmail2";
            this.mapEmail2.Size = new System.Drawing.Size(160, 20);
            this.mapEmail2.TabIndex = 11;
            // 
            // mapEmail
            // 
            this.mapEmail.Location = new System.Drawing.Point(288, 192);
            this.mapEmail.Name = "mapEmail";
            this.mapEmail.Size = new System.Drawing.Size(160, 20);
            this.mapEmail.TabIndex = 10;
            // 
            // mapAuthor
            // 
            this.mapAuthor.Location = new System.Drawing.Point(88, 192);
            this.mapAuthor.Name = "mapAuthor";
            this.mapAuthor.Size = new System.Drawing.Size(128, 20);
            this.mapAuthor.TabIndex = 9;
            // 
            // labelEmail2
            // 
            this.labelEmail2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelEmail2.Location = new System.Drawing.Point(248, 224);
            this.labelEmail2.Name = "labelEmail2";
            this.labelEmail2.Size = new System.Drawing.Size(40, 24);
            this.labelEmail2.TabIndex = 8;
            this.labelEmail2.Text = "Email";
            this.labelEmail2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEmail
            // 
            this.labelEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelEmail.Location = new System.Drawing.Point(248, 192);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(40, 24);
            this.labelEmail.TabIndex = 7;
            this.labelEmail.Text = "Email";
            this.labelEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAuthor2
            // 
            this.labelAuthor2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelAuthor2.Location = new System.Drawing.Point(8, 224);
            this.labelAuthor2.Name = "labelAuthor2";
            this.labelAuthor2.Size = new System.Drawing.Size(72, 24);
            this.labelAuthor2.TabIndex = 6;
            this.labelAuthor2.Text = "Secondary Author";
            this.labelAuthor2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAuthor
            // 
            this.labelAuthor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelAuthor.Location = new System.Drawing.Point(8, 192);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(72, 24);
            this.labelAuthor.TabIndex = 5;
            this.labelAuthor.Text = "Author";
            this.labelAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelVersion
            // 
            this.labelVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelVersion.Location = new System.Drawing.Point(8, 288);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(72, 24);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mapDescription
            // 
            this.mapDescription.Location = new System.Drawing.Point(88, 88);
            this.mapDescription.Multiline = true;
            this.mapDescription.Name = "mapDescription";
            this.mapDescription.Size = new System.Drawing.Size(360, 88);
            this.mapDescription.TabIndex = 3;
            // 
            // labelDescription
            // 
            this.labelDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDescription.Location = new System.Drawing.Point(8, 88);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(80, 24);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Description";
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTitle
            // 
            this.labelTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTitle.Location = new System.Drawing.Point(8, 56);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(80, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title/Summary";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mapSummary
            // 
            this.mapSummary.Location = new System.Drawing.Point(88, 56);
            this.mapSummary.Name = "mapSummary";
            this.mapSummary.Size = new System.Drawing.Size(360, 20);
            this.mapSummary.TabIndex = 1;
            // 
            // minimapTab
            // 
            this.minimapTab.Controls.Add(this.MinimapPanel);
            this.minimapTab.Location = new System.Drawing.Point(4, 22);
            this.minimapTab.Name = "minimapTab";
            this.minimapTab.Size = new System.Drawing.Size(921, 681);
            this.minimapTab.TabIndex = 0;
            this.minimapTab.Text = "Mini Map";
            this.minimapTab.UseVisualStyleBackColor = true;
            // 
            // MinimapPanel
            // 
            this.MinimapPanel.Controls.Add(this.panel1);
            this.MinimapPanel.Controls.Add(this.groupBox2);
            this.MinimapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinimapPanel.Location = new System.Drawing.Point(0, 0);
            this.MinimapPanel.Name = "MinimapPanel";
            this.MinimapPanel.Size = new System.Drawing.Size(921, 681);
            this.MinimapPanel.TabIndex = 0;
            this.MinimapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MinimapPanel_Paint);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.miniViewPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(120, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 681);
            this.panel1.TabIndex = 3;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown_1);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // miniViewPanel
            // 
            this.miniViewPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.miniViewPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.miniViewPanel.Location = new System.Drawing.Point(3, 3);
            this.miniViewPanel.Name = "miniViewPanel";
            this.miniViewPanel.Size = new System.Drawing.Size(561, 534);
            this.miniViewPanel.TabIndex = 1;
            this.miniViewPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.miniViewPanel_Scroll);
            this.miniViewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.minimap_Paint);
            this.miniViewPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.miniViewPanel_MouseDoubleClick);
            this.miniViewPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.miniViewPanel_MouseDown);
            this.miniViewPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniViewPanel_MouseMove);
            this.miniViewPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.miniViewPanel_MouseUp);
            this.miniViewPanel.Resize += new System.EventHandler(this.miniViewPanel_Resize);
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox2.Controls.Add(this.MiniEditPanel);
            this.groupBox2.Controls.Add(this.miniEdit);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.chkDevide2);
            this.groupBox2.Controls.Add(this.chkDevide);
            this.groupBox2.Controls.Add(this.buttonCenter);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 681);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // MiniEditPanel
            // 
            this.MiniEditPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MiniEditPanel.Controls.Add(this.label10);
            this.MiniEditPanel.Controls.Add(this.button1);
            this.MiniEditPanel.Controls.Add(this.label9);
            this.MiniEditPanel.Controls.Add(this.redo);
            this.MiniEditPanel.Controls.Add(this.undo);
            this.MiniEditPanel.Controls.Add(this.MiniLineWall);
            this.MiniEditPanel.Controls.Add(this.label8);
            this.MiniEditPanel.Controls.Add(this.label7);
            this.MiniEditPanel.Controls.Add(this.label6);
            this.MiniEditPanel.Controls.Add(this.numericUpDown2);
            this.MiniEditPanel.Controls.Add(this.miniTileBrush);
            this.MiniEditPanel.Controls.Add(this.miniTilePLace);
            this.MiniEditPanel.Controls.Add(this.miniWallBrush);
            this.MiniEditPanel.Controls.Add(this.labelSep2);
            this.MiniEditPanel.Enabled = false;
            this.MiniEditPanel.Location = new System.Drawing.Point(8, 78);
            this.MiniEditPanel.Name = "MiniEditPanel";
            this.MiniEditPanel.Size = new System.Drawing.Size(102, 287);
            this.MiniEditPanel.TabIndex = 12;
            this.MiniEditPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(0, 241);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 2);
            this.label10.TabIndex = 35;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "Polygons";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.menuItemPolygons_Click);
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(0, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 2);
            this.label9.TabIndex = 33;
            // 
            // redo
            // 
            this.redo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.redo.Enabled = false;
            this.redo.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.redo.Location = new System.Drawing.Point(49, 3);
            this.redo.Name = "redo";
            this.redo.Size = new System.Drawing.Size(23, 23);
            this.redo.TabIndex = 32;
            this.redo.Text = "8";
            this.redo.UseVisualStyleBackColor = true;
            this.redo.Click += new System.EventHandler(this.redo_Click);
            // 
            // undo
            // 
            this.undo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.undo.Enabled = false;
            this.undo.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.undo.Location = new System.Drawing.Point(25, 3);
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(23, 23);
            this.undo.TabIndex = 31;
            this.undo.Text = "7";
            this.undo.UseVisualStyleBackColor = true;
            this.undo.Click += new System.EventHandler(this.undo_Click);
            // 
            // MiniLineWall
            // 
            this.MiniLineWall.Appearance = System.Windows.Forms.Appearance.Button;
            this.MiniLineWall.BackgroundImage = global::MapEditor.Properties.Resources.LineWall;
            this.MiniLineWall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.MiniLineWall.Location = new System.Drawing.Point(10, 79);
            this.MiniLineWall.Name = "MiniLineWall";
            this.MiniLineWall.Size = new System.Drawing.Size(25, 25);
            this.MiniLineWall.TabIndex = 27;
            this.MiniLineWall.UseVisualStyleBackColor = true;
            this.MiniLineWall.Visible = false;
            this.MiniLineWall.CheckedChanged += new System.EventHandler(this.MiniLineWall_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Brush size:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Tile editing";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Wall editing";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(28, 213);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown2.TabIndex = 23;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // miniTileBrush
            // 
            this.miniTileBrush.Appearance = System.Windows.Forms.Appearance.Button;
            this.miniTileBrush.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.miniTileBrush.Location = new System.Drawing.Point(10, 162);
            this.miniTileBrush.Name = "miniTileBrush";
            this.miniTileBrush.Size = new System.Drawing.Size(80, 25);
            this.miniTileBrush.TabIndex = 22;
            this.miniTileBrush.TabStop = true;
            this.miniTileBrush.Text = "Tile Brush";
            this.miniTileBrush.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.miniTileBrush.UseVisualStyleBackColor = true;
            this.miniTileBrush.CheckedChanged += new System.EventHandler(this.miniTileBrush_CheckedChanged);
            // 
            // miniTilePLace
            // 
            this.miniTilePLace.Appearance = System.Windows.Forms.Appearance.Button;
            this.miniTilePLace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.miniTilePLace.Location = new System.Drawing.Point(10, 135);
            this.miniTilePLace.Name = "miniTilePLace";
            this.miniTilePLace.Size = new System.Drawing.Size(80, 25);
            this.miniTilePLace.TabIndex = 21;
            this.miniTilePLace.TabStop = true;
            this.miniTilePLace.Text = "Tile Place";
            this.miniTilePLace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.miniTilePLace.UseVisualStyleBackColor = true;
            this.miniTilePLace.CheckedChanged += new System.EventHandler(this.miniTilePLace_CheckedChanged);
            // 
            // miniWallBrush
            // 
            this.miniWallBrush.Appearance = System.Windows.Forms.Appearance.Button;
            this.miniWallBrush.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.miniWallBrush.Location = new System.Drawing.Point(10, 52);
            this.miniWallBrush.Name = "miniWallBrush";
            this.miniWallBrush.Size = new System.Drawing.Size(80, 25);
            this.miniWallBrush.TabIndex = 20;
            this.miniWallBrush.TabStop = true;
            this.miniWallBrush.Text = "Wall Brush";
            this.miniWallBrush.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.miniWallBrush.UseVisualStyleBackColor = true;
            this.miniWallBrush.CheckedChanged += new System.EventHandler(this.miniWallBrush_CheckedChanged);
            // 
            // labelSep2
            // 
            this.labelSep2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSep2.Location = new System.Drawing.Point(0, 111);
            this.labelSep2.Name = "labelSep2";
            this.labelSep2.Size = new System.Drawing.Size(99, 2);
            this.labelSep2.TabIndex = 18;
            // 
            // miniEdit
            // 
            this.miniEdit.AutoSize = true;
            this.miniEdit.Location = new System.Drawing.Point(12, 55);
            this.miniEdit.Name = "miniEdit";
            this.miniEdit.Size = new System.Drawing.Size(87, 17);
            this.miniEdit.TabIndex = 10;
            this.miniEdit.Text = "Editing mode";
            this.miniEdit.UseVisualStyleBackColor = true;
            this.miniEdit.CheckedChanged += new System.EventHandler(this.miniEdit_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Zoom:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(52, 23);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // chkDevide2
            // 
            this.chkDevide2.AutoSize = true;
            this.chkDevide2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDevide2.Location = new System.Drawing.Point(14, 403);
            this.chkDevide2.Name = "chkDevide2";
            this.chkDevide2.Size = new System.Drawing.Size(62, 17);
            this.chkDevide2.TabIndex = 7;
            this.chkDevide2.Text = "Divide2";
            this.chkDevide2.UseVisualStyleBackColor = true;
            this.chkDevide2.CheckedChanged += new System.EventHandler(this.chkDevide2_CheckedChanged);
            // 
            // chkDevide
            // 
            this.chkDevide.AutoSize = true;
            this.chkDevide.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDevide.Location = new System.Drawing.Point(14, 380);
            this.chkDevide.Name = "chkDevide";
            this.chkDevide.Size = new System.Drawing.Size(62, 17);
            this.chkDevide.TabIndex = 6;
            this.chkDevide.Text = "Divide1";
            this.chkDevide.UseVisualStyleBackColor = true;
            this.chkDevide.CheckedChanged += new System.EventHandler(this.chkDevide_CheckedChanged);
            // 
            // buttonCenter
            // 
            this.buttonCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCenter.Location = new System.Drawing.Point(8, 426);
            this.buttonCenter.Name = "buttonCenter";
            this.buttonCenter.Size = new System.Drawing.Size(84, 23);
            this.buttonCenter.TabIndex = 2;
            this.buttonCenter.Text = "Go to Center";
            this.buttonCenter.UseVisualStyleBackColor = true;
            this.buttonCenter.Click += new System.EventHandler(this.buttonCenter_Click);
            // 
            // largeMap
            // 
            this.largeMap.Controls.Add(this.mapView);
            this.largeMap.Location = new System.Drawing.Point(4, 22);
            this.largeMap.Name = "largeMap";
            this.largeMap.Size = new System.Drawing.Size(921, 681);
            this.largeMap.TabIndex = 0;
            this.largeMap.Text = "Large Map";
            this.largeMap.UseVisualStyleBackColor = true;
            // 
            // mapView
            // 
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.Name = "mapView";
            this.mapView.Size = new System.Drawing.Size(921, 681);
            this.mapView.TabIndex = 0;
            this.mapView.Load += new System.EventHandler(this.mapView_Load);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.largeMap);
            this.tabControl1.Controls.Add(this.minimapTab);
            this.tabControl1.Controls.Add(this.mapInfoTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(63, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(929, 707);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Enter += new System.EventHandler(this.tabControl1_Enter);
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // MainWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(929, 707);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(840, 680);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizedBoundsChanged += new System.EventHandler(this.MainWindow_MaximizedBoundsChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindowFormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.SizeChanged += new System.EventHandler(this.miniViewPanel_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.Move += new System.EventHandler(this.miniViewPanel_Resize);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.mapInfoTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.minimapTab.ResumeLayout(false);
            this.MinimapPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.MiniEditPanel.ResumeLayout(false);
            this.MiniEditPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.largeMap.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.CheckBox checkServPlayerLimit;
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            Logger.Init();

#if !DEBUG
            try
            {
#endif
            Application.Run(new MainWindow(args));
#if !DEBUG
			}
			catch (Exception ex)
			{
				new ExceptionDialog(ex, "Exception in main loop").ShowDialog();
				Environment.Exit(-1);
			}
#endif
        }

        public void UpdateMapInfo()
        {
            mapView.SelectedObjects.Items.Clear();

            mapType.SelectedIndex = Map.MapInfo.MapTypeNames.IndexOfKey(map.Info.Type);
            mapSummary.Text = map.Info.Summary;
            mapDescription.Text = map.Info.Description;

            mapAuthor.Text = map.Info.Author;
            mapEmail.Text = map.Info.Email;
            mapAuthor2.Text = map.Info.Author2;
            mapEmail2.Text = map.Info.Email2;

            mapVersion.Text = map.Info.Version;
            mapCopyright.Text = map.Info.Copyright;
            mapDate.Text = map.Info.Date;

            mapMinRec.Text = String.Format("{0}", map.Info.RecommendedMin);
            mapMaxRec.Text = String.Format("{0}", map.Info.RecommendedMax);
            checkServPlayerLimit.Checked = false;
            // если ст??дв?ну? значит нужн?использовать настройк?сервер?
            // игра пр?загрузке ставит 2 - 16
            if (map.Info.RecommendedMin == 0 && map.Info.RecommendedMax == 0)
                checkServPlayerLimit.Checked = true;

            questTitle.Text = map.Info.QIntroTitle;
            questGraphic.Text = map.Info.QIntroGraphic;

            ambientColorPanel.BackColor = map.Ambient.AmbientColor;
            // ме?ем заголово?окна
            Text = string.Format(TITLE_FORMAT, map.FileName, map.Info.Summary);

            // читаем скрипт?
            // try
            //{
            //aScriptUserControl.UpdateForMap(map);
            //}
            //catch (Exception ex)
            // {
            //	MessageBox.Show(ex.Message, "Failed to load scripts!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            mapView.MapRenderer.UpdateCanvas(true, true);
            Invalidate(true);
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "Nox Map Files (*.map)|*.map|Compressed Map Files (*.nxz)|*.nxz";

            if (fd.ShowDialog() == DialogResult.OK && File.Exists(fd.FileName))
            {
                // Load map
                MapInterface.SwitchMap(fd.FileName);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            Logger.Close();
            Environment.Exit(0);
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {


            if (map == null) return;

            else if (map.FileName == "" || map.FileName == null)
            {
                // Ask user to choose filename
                menuItemSaveAs.PerformClick();
                return;
            }


            save();




        }

        private void save()
        {
            bool playerStart = false;

            this.Cursor = Cursors.WaitCursor;
            // Invalidate();
            //TODO: check lengths for each to make sure they aren't too long
            map.Info.Type = (Map.MapInfo.MapType)Map.MapInfo.MapTypeNames.GetKey(mapType.SelectedIndex);//FIXME: default to something if unspecified
            map.Info.Summary = mapSummary.Text;
            map.Info.Description = mapDescription.Text;

            map.Info.Author = mapAuthor.Text;
            map.Info.Email = mapEmail.Text;
            map.Info.Author2 = mapAuthor2.Text;
            map.Info.Email2 = mapEmail2.Text;

            map.Info.Version = mapVersion.Text;
            map.Info.Copyright = mapCopyright.Text;
            map.Info.Date = mapDate.Text;

            if (checkServPlayerLimit.Checked)
            {
                map.Info.RecommendedMax = 16;
                map.Info.RecommendedMin = 2;
            }
            else
            {
                map.Info.RecommendedMin = mapMinRec.Text.Length == 0 ? (byte)0 : Convert.ToByte(mapMinRec.Text);
                map.Info.RecommendedMax = mapMaxRec.Text.Length == 0 ? (byte)0 : Convert.ToByte(mapMaxRec.Text);
            }
            map.Info.QIntroTitle = questTitle.Text;
            map.Info.QIntroGraphic = questGraphic.Text;

            map.WriteMap();


            if (EditorSettings.Default.Save_ExportNXZ)
            {
                try
                {
                    map.WriteNxz();
                }
                catch (Exception ex)
                {
                    Logger.Log("Failed to write .nxz file! \n" + ex.Message);
                    MessageBox.Show("Couldn't write the compressed map. Map compression is still buggy. Try changing your map in any way and saving again.");
                }
            }

            //saving.Hide();
            foreach (Map.Object obj in map.Objects)
            {

                if (obj.Name == "PlayerStart")
                    playerStart = true;


            }
            //saving.timer1.Enabled = false;
            //saving.Hide();
            this.Cursor = Cursors.Default;
            this.Text = map.FileName;
            //Invalidate();
            if (!playerStart)
                MessageBox.Show("Warning: There is no PlayerStart object in this map. Every multiplayer map needs at least one this object to work properly.", "Missing PlayerStart", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }





        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            AboutDialog dlg = new AboutDialog();
            dlg.ShowDialog();
        }

        void LoadNewMap()
        {
            MapInterface.SwitchMap(null);
            UpdateMapInfo();
        }

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            LoadNewMap();
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Nox Map Files (*.map)|*.map";

            if (fd.ShowDialog() == DialogResult.OK)//&& fd.FileName)
            {
                map.FileName = fd.FileName;
                menuItemSave.PerformClick();
            }

        }

        private void viewObjects_Click(object sender, EventArgs e)
        {
            ObjectListDialog objLd = new ObjectListDialog();
            objLd.objTable = map.Objects;
            objLd.objTable2 = map.Objects;
            objLd.Map = this.mapView;
            objLd.ShowDialog();
            objLd.Owner = this;
        }

        public static IList GetSupportedCultures()
        {
            ArrayList list = new ArrayList();
            list.Add(CultureInfo.InvariantCulture);
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    Assembly.GetExecutingAssembly().GetSatelliteAssembly(culture);
                    list.Add(culture);//won't get added if not found (exception will be thrown)
                }
                catch (Exception) { }
            }
            return list;
        }

        private void menuItemGroups_Click(object sender, EventArgs e)
        {
            GroupDialog gd = new GroupDialog();
            gd.GroupD = map.Groups;
            gd.Show();
            map.Groups = gd.GroupD;
        }

        private void exportImageMenu_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Windows Bitmap|*.bmp|JPEG Image|*.jpg|PNG Image|*.png";
            sfd.AddExtension = true;
            sfd.ValidateNames = true;
            sfd.OverwritePrompt = true;
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                Bitmap mapBitmap = mapView.MapToImage();
                if (mapBitmap != null)
                {
                    System.Drawing.Imaging.ImageFormat imageFormat;
                    switch (sfd.FilterIndex)
                    {
                        case 1:
                            imageFormat = System.Drawing.Imaging.ImageFormat.Bmp; // Does this export?
                            break;
                        case 2:
                            imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg; // Only PNG right?
                            break;
                        case 3:
                            imageFormat = System.Drawing.Imaging.ImageFormat.Png; // Why have the other options?
                            break;
                        default:
                            return;
                    }
                    mapBitmap.Save(sfd.FileName, imageFormat);
                }
            }
        }

        private void minimap_Paint(object sender, PaintEventArgs e)
        {
            if (map == null) return;

            Pen pen;
            redraw = new Rectangle(new Point(((mouseLocation.X - 34)), ((mouseLocation.Y - 34))), new Size(68, 68));
            Graphics graphics = e.Graphics;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.CompositingMode = CompositingMode.SourceCopy;

            //graphics.Clear(Color.Black);

            // обновляем размер миникарт?
            miniViewPanel.Width = mapDimension * mapZoom;
            miniViewPanel.Height = mapDimension * mapZoom;
            // отрисовк?
            Bitmap bitmap;
            if (bitmap2 != null)
                bitmap = bitmap2;
            else
            {

                redraw = new Rectangle(new Point(0, 0), new Size(miniViewPanel.Width, miniViewPanel.Height));
                bitmap = new Bitmap(miniViewPanel.Width, miniViewPanel.Height);

            }
            MinimapRenderer minimap = new MinimapRenderer(bitmap, map, mapView.MapRenderer.FakeWalls);
            minimap.LockBitmap();

            // MessageBox.Show(miniViewPanel.Top.ToString());
            minimap.DrawMinimap(mapZoom, redraw);
            bitmap = minimap.UnlockBitmap();

            //
            //bitmap = bitmap2;


            // if (bitmap2 == null)
            if (MapInterface.CurrentMode != EditMode.POLYGON_RESHAPE)
                bitmap2 = bitmap;
            graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

            // лини?деления
            if (chkDevide.Checked)
                graphics.DrawLine(new Pen(Color.Aqua, 1), new Point(0, 0), new Point(512, 512));
            if (chkDevide2.Checked)
                graphics.DrawLine(new Pen(Color.Aqua, 1), new Point(512, 0), new Point(0, 512));


            // graphics.DrawRectangle(new Pen(Color.Aqua, 1), redraw);
            //Invalidate(true);
            // Graphics g = Graphics.FromImage(bitmap);
            //  g.DrawImage(bitmap2, 0, 0, bitmap.Width, bitmap.Height);
            //bitmap2.Dispose();




            if (EditorSettings.Default.Draw_Polygons)
            {

                foreach (Map.Polygon poly in map.Polygons)
                {

                    pen = Pens.PaleGreen;
                    // Highlight the polygon being edited
                    //  if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                    // {

                    List<PointF> points = new List<PointF>();

                    pen = Pens.PaleGreen;

                    foreach (PointF pt in poly.Points)
                    {
                        float pointX = (pt.X / MapView.squareSize) * mapZoom;
                        float pointY = (pt.Y / MapView.squareSize) * mapZoom;
                        PointF center = new PointF(pointX, pointY);

                        Pen pen2 = MapInterface.SelectedPolyPoint == pt ? Pens.DodgerBlue : Pens.DeepPink;
                        if (mapView.PolygonEditDlg.SelectedPolygon == poly && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                        {
                            PointF centered = new PointF(center.X - 4, center.Y - 4);
                            pen = Pens.PaleVioletRed;
                            graphics.DrawEllipse(pen2, new RectangleF(centered, new Size(2 * 4, 2 * 4)));

                        }
                        points.Add(center);
                    }
                    if (poly.Points.Count > 2)
                    {

                        if (mapView.PolygonEditDlg.SuperPolygon == poly && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                            pen = new Pen(Color.PaleVioletRed, 2);


                        graphics.DrawLines(pen, points.ToArray());
                        graphics.DrawLine(pen, points[points.Count - 1], points[0]);
                    }
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void buttonCenter_Click(object sender, EventArgs e)
        {
            mapView.CenterAtPoint(new Point((mapDimension / 2) * MapView.squareSize, (mapDimension / 2) * MapView.squareSize));
            tabControl1.SelectTab("largeMap");
        }

        private void menuItemReorderExt_Click(object sender, EventArgs e)
        {
            int val = 20;
            foreach (Map.Object obj in map.Objects)
            {
                obj.Extent = val++;
            }
        }

        private void menuItemInvertColors_Click(object sender, EventArgs e)
        {
            menuItem1.Checked = !menuItem1.Checked;
            if (menuItem1.Checked)
                mapView.MapRenderer.ColorLayout.InvertColors();
            else
                mapView.MapRenderer.ColorLayout.ResetColors();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                mapView.DeleteSelectedObjects();
                Reload();

            }


            /* if (e.KeyCode == Keys.F1)
             {
                 HelpBrowser hlp = new HelpBrowser();
                 hlp.Show();
             }*/
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void chkDevide_CheckedChanged(object sender, EventArgs e)
        {
            miniViewPanel.Refresh();
        }

        private void chkDevide2_CheckedChanged(object sender, EventArgs e)
        {
            miniViewPanel.Refresh();
        }

        private void menuShowGrid_Click(object sender, EventArgs e)
        {
            bool check = !menuShowGrid.Checked;
            menuShowGrid.Checked = check;
            // Update settings
            EditorSettings.Default.Draw_Grid = check;
            EditorSettings.Default.Save();
        }

        private void menuUseNewRenderer_Click(object sender, EventArgs e)
        {
            bool check = !menuUseNewRenderer.Checked;
            menuUseNewRenderer.Checked = check;

            // Update settings
            EditorSettings.Default.Edit_PreviewMode = check;
            EditorSettings.Default.Save();
            mapView.MapRenderer.UpdateCanvas(true, true);
            mapView.prwSwitch.Checked = !check;
            Invalidate(true);

        }

        private void ambientColorPanel_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.Color = ambientColorPanel.BackColor;
            if (color.ShowDialog(this) == DialogResult.OK)
            {
                ambientColorPanel.BackColor = color.Color;
                map.Ambient.AmbientColor = color.Color;
            }
        }

        void MainWindowFormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to close?", "CLOSING EDITOR!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                e.Cancel = true;
        }

        void CheckServPlayerLimitCheckedChanged(object sender, EventArgs e)
        {
            mapMaxRec.Enabled = !checkServPlayerLimit.Checked;
            mapMinRec.Enabled = !checkServPlayerLimit.Checked;
        }

        void SettingsItemClick(object sender, EventArgs e)
        {
            SettingsDialog settings = new SettingsDialog();
            settings.ShowDialog(this);
        }

        private void menuItemPolygons_Click(object sender, EventArgs e)
        {
            PolygonEditor editor = mapView.PolygonEditDlg;
            //  MapInterface.CurrentMode = MapInt.EditMode.POLYGON_RESHAPE;
            editor.Show();

            if (tabControl1.SelectedIndex == 1)
            {
                Point po = new Point(miniViewPanel.Width, 0);
                po = miniViewPanel.PointToScreen(po);
                if (!IsOnScreen(new Point(po.X + mapView.PolygonEditDlg.Width, po.Y))) return;
                editor.Location = po;
                miniEdit.Checked = true;
            }

        }

        private void menuItemScripts_Click(object sender, EventArgs e)
        {
            PolygonEditor editor = mapView.PolygonEditDlg;
            editor.Show(this);

        }

        private void mapView_Load(object sender, EventArgs e)
        {

        }

        private void aScriptUserControl_Load(object sender, EventArgs e)
        {

        }

        private void MainWindow_MaximizedBoundsChanged(object sender, EventArgs e)
        {

        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            mapView.MapRenderer.UpdateCanvas(true, true);
        }

        private void menuScripts_Click_1(object sender, EventArgs e)
        {
            if (map.Scripts.SctStr.Count > 0 && map.Scripts.SctStr[0].StartsWith("NOXSCRIPT3.0"))
            {
                MessageBox.Show("You can't use this editor for NoxScript 3.0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mapView.openScripts();
        }

        private void MinimapPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void miniViewPanel_MouseMove(object sender, MouseEventArgs e)
        {

            if (!mapView.done) return;

            mouseLocation = new Point(e.X, e.Y);


            if (mapView.PolygonEditDlg.Visible)
            {
                Point mousePt = mapView.PolygonEditDlg.PointToScreen(mouseLocation);
                mousePt = MainWindow.Instance.PointToClient(mousePt);

                if (mapView.PolygonEditDlg.ClientRectangle.Contains(mousePt)) return;
                MainWindow.Instance.Focus();
            }


            if (MapInterface.CurrentMode != EditMode.WALL_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_PLACE && MapInterface.CurrentMode != EditMode.POLYGON_RESHAPE)
                return;



            Point MouseKeep = mapView.mouseKeep;

            if (!miniEdit.Checked) return;


            Rectangle minimapBounds = new Rectangle(new Point(0, 0), new Size(mapDimension * mapZoom, mapDimension * mapZoom));
            Rectangle redraw2 = new Rectangle(new Point(e.X + 100 + miniViewPanel.Left, e.Y - 8 + miniViewPanel.Top), new Size(60, 60));
            if (minimapBounds.Contains(e.X, e.Y))
            {

                if (!e.Button.Equals(MouseButtons.Left))
                {

                    if (!MouseKeep.IsEmpty)
                    {
                        mapView.mouseKeepOff = mapView.mouseKeep;
                        mapView.mouseKeep = new Point();
                    }

                    if (MainWindow.Instance.mapView.PolygonEditDlg.Visible && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && !mapView.PolygonEditDlg.LockedBox.Checked && (MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon == null || mapView.PolygonEditDlg.SelectedPolygon != mapView.PolygonEditDlg.SuperPolygon))
                    {
                        int i = -1;
                        foreach (Map.Polygon poly in map.Polygons)
                        {
                            i++;
                            List<Point> points = new List<Point>();
                            foreach (PointF polyPt in poly.Points)
                            {
                                float pointX = (polyPt.X / MapView.squareSize) * mapZoom;
                                float pointY = (polyPt.Y / MapView.squareSize) * mapZoom;
                                Point center = new Point((int)pointX, (int)pointY);
                                points.Add(center);
                            }


                            if (MapInterface.PointInPolygon(mouseLocation, points.ToArray()))
                            {
                                MainWindow.Instance.mapView.PolygonEditDlg.listBoxPolygons.SelectedIndex = i;
                                MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon = poly;
                                //bitmap2 = null;

                                break;
                            }
                        }
                        // Invalidate(redraw2, true);
                    }

                }

                /*
                 if (mapView.WallMakeNewCtrl.RecWall.Checked)
                 {
                     Point pt = new Point((e.X * 2) * MapView.squareSize / (mapZoom * 2), (e.Y * 2) * MapView.squareSize / (mapZoom * 2));
                     MapInterface.WallRectangle(pt);
                 }
                 */
                if (MiniLineWall.Checked && MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                {
                    Point pt = new Point((e.X * 2) * MapView.squareSize / (mapZoom * 2), (e.Y * 2) * MapView.squareSize / (mapZoom * 2));
                    MapInterface.WallLine(pt);
                    bitmap2 = null;
                    Invalidate(true);
                }

                //myMap.SetLoc((int)(e.X / mapZoom * MapView.squareSize)-mapView.WidthMod, (int)(e.Y / mapZoom * MapView.squareSize));
                if (e.Button == MouseButtons.Left)
                {


                    if (MapInterface.CurrentMode != EditMode.WALL_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_PLACE && MapInterface.CurrentMode != EditMode.POLYGON_RESHAPE)
                    {
                        MessageBox.Show("MiniMap Mode supports only Wall, Tile, and Polygon operations!");
                        return;

                    }


                    Point pt = new Point((e.X * 2) * MapView.squareSize / (mapZoom * 2), (e.Y * 2) * MapView.squareSize / (mapZoom * 2));
                    /*
                    if (!MouseKeep.IsEmpty && !MapInterface.SelectedPolyPoint.IsEmpty && (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE))
                    {
                        mapView.ApplyStore();
                        moved = true;
                    }
                    */

                    if (!MouseKeep.IsEmpty && (!MapInterface.SelectedPolyPoint.IsEmpty || mapView.PolygonEditDlg.SuperPolygon != null) && (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE))
                    {

                        if (mapView.PolygonEditDlg.SuperPolygon != null)
                        {

                            if (mapView.PolygonEditDlg.SuperPolygon.IsPointInside(pt))
                            {
                                mapView.ApplyStore();
                                moved = true;
                            }
                            else if (!MapInterface.SelectedPolyPoint.IsEmpty)
                            {
                                mapView.ApplyStore();
                                moved = true;
                            }
                        }
                        else if (!MapInterface.SelectedPolyPoint.IsEmpty)
                        {
                            mapView.ApplyStore();
                            moved = true;
                        }
                    }

                    if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                    {

                        Rectangle saferedraw = new Rectangle(new Point(3, 3), new Size(miniViewPanel.Width - 6, miniViewPanel.Height - 6));
                        if (!MapInterface.SelectedPolyPoint.IsEmpty && !MapInterface.KeyHelper.ShiftKey && saferedraw.Contains(e.Location))
                        {
                            PointF AlignedPt = pt;
                            if (mapView.PolygonEditDlg.snapPoly.Checked)
                                AlignedPt = MapInterface.PolyPointSnap(mouseLocation).IsEmpty ? pt : MapInterface.PolyPointSnap(mouseLocation);

                            Map.Polygon poly = mapView.PolygonEditDlg.SelectedPolygon;
                            poly.Points[mapView.arrowPoly] = AlignedPt;
                            MapInterface.SelectedPolyPoint = AlignedPt;
                            //bitmap2 = null;
                            Invalidate(true);
                        }
                        if (mapView.PolygonEditDlg.SuperPolygon != null && MapInterface.SelectedPolyPoint.IsEmpty)
                        {
                            if (mapView.PolygonEditDlg.SuperPolygon.IsPointInside(pt))
                            {
                                for (int i = 0; i < mapView.PolygonEditDlg.SuperPolygon.Points.Count; i++)
                                {
                                    PointF pts = mapView.PolygonEditDlg.SuperPolygon.Points[i];
                                    if (mapView.PolyPointOffset.Count <= mapView.PolygonEditDlg.SuperPolygon.Points.Count)
                                    {
                                        float polyrelX = (pts.X - pt.X) * -1;
                                        float polyrelY = (pts.Y - pt.Y) * -1;
                                        mapView.PolyPointOffset.Add(new PointF(polyrelX, polyrelY));
                                    }
                                    mapView.PolygonEditDlg.SuperPolygon.Points[i] = new PointF(pt.X - mapView.PolyPointOffset[i].X, pt.Y - mapView.PolyPointOffset[i].Y);
                                }
                                miniViewPanel.Cursor = Cursors.SizeAll;

                                Invalidate(true);
                            }
                        }
                    }
                    else
                        MapInterface.HandleLMouseClick(pt);
                    Invalidate(redraw2, true);
                }
                else
                {
                    if (miniViewPanel.Cursor == Cursors.SizeAll) miniViewPanel.Cursor = Cursors.Default;
                    if (mapView.PolyPointOffset.Count > 0) mapView.PolyPointOffset.Clear();

                }

                if (e.Button == MouseButtons.Right)
                {
                    RightDown = true;
                    Point pt = new Point((e.X * 2) * MapView.squareSize / (mapZoom * 2), (e.Y * 2) * MapView.squareSize / (mapZoom * 2));
                    redraw2 = new Rectangle(new Point(e.X + 100 + miniViewPanel.Left, e.Y - 3 + miniViewPanel.Top), new Size(60, 60));
                    MapInterface.HandleRMouseClick(pt);
                    // mapView.MapRenderer.UpdateCanvas(true, true);
                    //bitmap2 = null;
                    Invalidate(redraw2, true);
                    // Invalidate(true);

                }

            }

            if (MiniLineWall.Checked && MouseKeep.IsEmpty && MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                MapInterface.ResetUpdateTracker();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mapZoom = (int)numericUpDown1.Value;
            bitmap2 = null;
            Invalidate(true);
        }

        private void miniEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (miniEdit.Checked)
                MiniEditPanel.Enabled = true;
            else
                MiniEditPanel.Enabled = false;
        }

        private void miniViewPanel_MouseUp(object sender, MouseEventArgs e)
        {

            if (MapInterface.CurrentMode != EditMode.WALL_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_PLACE && MapInterface.CurrentMode != EditMode.POLYGON_RESHAPE)
                return;



            Point MouseKeep = mapView.mouseKeep;
            RightDown = false;
            bitmap2 = null;
            // Invalidate(true);


            if (!MouseKeep.IsEmpty)
            {
                mapView.mouseKeepOff = mapView.mouseKeep;
                mapView.mouseKeep = new Point();
            }

            if (!MapInterface.OpUpdatedTiles && !MapInterface.OpUpdatedWalls && !MapInterface.OpUpdatedPolygons && added && !moved)
            {
                if (MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                {
                    if (MiniLineWall.Checked) goto noPre;
                }

                while (mapView.TimeManager.Count > 0 && mapView.TimeManager[(mapView.TimeManager.Count - 1) - mapView.currentStep].Event == MapView.TimeEvent.PRE)
                {
                    if (mapView.TimeManager[(mapView.TimeManager.Count - 1) - mapView.currentStep].Event == MapView.TimeEvent.PRE && mapView.TimeManager.Count > 0)
                    {
                        mapView.TimeManager.RemoveAt((mapView.TimeManager.Count - 1) - mapView.currentStep);
                    }
                }
                if (mapView.TimeManager.Count <= 1)
                {
                    //mapView.StopUndo = true;
                    MainWindow.Instance.undo.Enabled = false;
                    undo.Enabled = false;

                }

            }
        noPre:




            if (mapView.WallMakeNewCtrl.LineWall.Checked || mapView.WallMakeNewCtrl.RecWall.Checked)
                mapView.LastWalls.Clear();

            
            if (!MapInterface.OpUpdatedTiles && !MapInterface.OpUpdatedWalls && !MapInterface.OpUpdatedPolygons && !moved)
                goto hop;


            if (MapInterface.CurrentMode <= EditMode.OBJECT_SELECT || MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
            {
                if (MiniLineWall.Checked && MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                    goto hop;

                if (MapInterface.CurrentMode >= EditMode.WALL_PLACE && MapInterface.CurrentMode < EditMode.OBJECT_PLACE)
                {
                    if (MapInterface.OpUpdatedTiles || MapInterface.OpUpdatedWalls || MapInterface.OpUpdatedPolygons)
                    {

                        mapView.Store(MapInterface.CurrentMode, MapEditor.MapView.TimeEvent.POST);

                    }

                }

                if ((!MapInterface.SelectedPolyPoint.IsEmpty || mapView.PolygonEditDlg.SuperPolygon != null) && !MapInterface.KeyHelper.ShiftKey && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && (moved || MapInterface.OpUpdatedPolygons))
                {
                    mapView.Store(MapInterface.CurrentMode, MapEditor.MapView.TimeEvent.POST);

                }
                else if (MapInterface.KeyHelper.ShiftKey && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                    mapView.Store(MapInterface.CurrentMode, MapEditor.MapView.TimeEvent.POST);


            }
        hop:
            mapView.BlockTime = false;
            moved = false;
            MapInterface.ResetUpdateTracker();
            Invalidate(true);
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            //  redraw = new Rectangle(new Point(-500, -500), new Size(0,0));
            if (e.Type == ScrollEventType.ThumbPosition)
            {
                //redraw = new Rectangle(new Point(0, 0), new Size(200, 200));
                bitmap2 = null;
                Invalidate(true);
            }

        }

        private void miniViewPanel_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {

        }

        private void miniViewPanel_MouseDown(object sender, MouseEventArgs e)
        {

            if (!miniEdit.Checked)
            {
                mapView.CenterAtPoint(new Point(e.X / mapZoom * MapView.squareSize, e.Y / mapZoom * MapView.squareSize));
                tabControl1.SelectTab("largeMap");
                mapView.TabMapToolsSelectedIndexChanged(sender, e);
                return;
            }
            if (MapInterface.CurrentMode != EditMode.WALL_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_BRUSH && MapInterface.CurrentMode != EditMode.FLOOR_PLACE && MapInterface.CurrentMode != EditMode.POLYGON_RESHAPE)
            {
                MessageBox.Show("MiniMap Mode supports only Wall, Tile, and Polygon operations!");
                return;

            }
            mapView.BlockTime = true;
            if (!mapView.done) return;
            // miniViewPanel_MouseMove(sender, e);

            mouseLocation = new Point(e.X, e.Y);
            if (!miniEdit.Checked) return;
            Rectangle redraw = new Rectangle(new Point(e.X + 103 + miniViewPanel.Left, e.Y - 4 + miniViewPanel.Top), new Size(50, 50));
            Rectangle minimapBounds = new Rectangle(new Point(0, 0), new Size(mapDimension * mapZoom, mapDimension * mapZoom));
            if (minimapBounds.Contains(e.X, e.Y))
            {
                Point pt = new Point((e.X * 2) * MapView.squareSize / (mapZoom * 2), (e.Y * 2) * MapView.squareSize / (mapZoom * 2));
                added = false;


                if (MapInterface.CurrentMode == EditMode.FLOOR_BRUSH || MapInterface.CurrentMode == EditMode.FLOOR_PLACE || MapInterface.CurrentMode == EditMode.WALL_BRUSH || MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                {
                    if (e.Button.Equals(MouseButtons.Left))
                    {
                        mapView.mouseKeep = pt;
                        if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && MapInterface.KeyHelper.ShiftKey)
                            added = mapView.ApplyStore();
                        else if (MapInterface.CurrentMode != EditMode.POLYGON_RESHAPE)
                            added = mapView.ApplyStore();


                    }
                    else if (e.Button.Equals(MouseButtons.Right))
                    {
                        if (MapInterface.CurrentMode == EditMode.FLOOR_BRUSH || MapInterface.CurrentMode == EditMode.FLOOR_PLACE || MapInterface.CurrentMode == EditMode.WALL_BRUSH)
                            added = mapView.ApplyStore();

                    }
                }

                if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE)
                {
                    if (e.Button == MouseButtons.Left)
                    {

                        if (mapView.PolygonEditDlg.SelectedPolygon != null)
                        {
                            if (MapInterface.KeyHelper.ShiftKey == true)
                            {
                                mapView.PolygonEditDlg.SelectedPolygon.Points.Insert(mapView.arrowPoly, pt);
                                if (mapView.PolygonEditDlg.SelectedPolygon.Points.Count > 2) MapInterface.OpUpdatedPolygons = true;
                            }
                            else
                                mapView.arrowPoly = MapInterface.PolyPointSelect(pt);

                            //bitmap2 = null;
                            Invalidate(true);
                        }




                        if (mapView.PolygonEditDlg.SelectedPolygon != null && !MapInterface.KeyHelper.ShiftKey)
                        {
                            if (MapInterface.SelectedPolyPoint.IsEmpty && mapView.PolygonEditDlg.SelectedPolygon.Points.Count > 2)
                            {

                                if (mapView.PolygonEditDlg.SelectedPolygon == mapView.PolygonEditDlg.SuperPolygon && mapView.PolygonEditDlg.SelectedPolygon != null && !mapView.PolygonEditDlg.LockedBox.Checked && !mapView.PolygonEditDlg.SelectedPolygon.IsPointInside(pt))
                                {

                                    mapView.PolygonEditDlg.SuperPolygon = null;


                                }
                                else if (mapView.PolygonEditDlg.Visible && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && mapView.PolygonEditDlg.SelectedPolygon != null && mapView.PolygonEditDlg.SelectedPolygon.IsPointInside(pt))
                                    mapView.PolygonEditDlg.SuperPolygon = mapView.PolygonEditDlg.SelectedPolygon;
                                else if (mapView.PolygonEditDlg.SuperPolygon != mapView.PolygonEditDlg.SelectedPolygon)
                                    mapView.PolygonEditDlg.SelectedPolygon = null;
                            }
                        }
                    }
                }
                else if (e.Button == MouseButtons.Left)
                    MapInterface.HandleLMouseClick(pt);
                else if (e.Button == MouseButtons.Right)
                {
                    MapInterface.HandleRMouseClick(pt);
                    Invalidate(redraw, true);
                }


            }

            moved = false;
            return;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            RightDown = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void miniWallPlace_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void miniWallPLace_CheckedChanged_1(object sender, EventArgs e)
        {
            //if (miniWallPLace.Checked == true)
            //MapInterface.CurrentMode = EditMode.WALL_PLACE;

        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

            //miniWallPLace.Checked = mapView.WallMakeNewCtrl.PlaceWalltBtn.Checked;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void moveRIGHT_Click(object sender, EventArgs e)
        {/*
            foreach (Map.Object obj in map.Objects)
            {
                obj.Location.X += 23;

            }
            foreach (Map.Wall wall in map.Walls.Values)
            {
               // MapInterface.WallRemove(wall.Location);
               // MapInterface.WallPlace(wall.Location.X + 1, wall.Location.Y);

            }
            foreach (Map.Tile tile in map.Tiles.Values)
            {
                tile.Location.X += 1;
            }
            bitmap2 = null;
            mapView.MapRenderer.UpdateCanvas(true, true);
            Invalidate(true);
            */

        }

        private void miniWallBrush_CheckedChanged(object sender, EventArgs e)
        {
            if (miniWallBrush.Checked)
            {
                MapInterface.CurrentMode = EditMode.WALL_BRUSH;
                MiniLineWall.Visible = true;
            }
            else
                MiniLineWall.Visible = false;

            Reload();
        }

        private void miniTileBrush_CheckedChanged(object sender, EventArgs e)
        {
            if (miniTileBrush.Checked)
                MapInterface.CurrentMode = EditMode.FLOOR_BRUSH;

            Reload();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            mapView.TileMakeNewCtrl.BrushSize.Value = numericUpDown2.Value;

        }

        private void miniTilePLace_CheckedChanged(object sender, EventArgs e)
        {
            if (miniTilePLace.Checked)
                MapInterface.CurrentMode = EditMode.FLOOR_PLACE;

            Reload();
        }

        private void undo_Click(object sender, EventArgs e)
        {

            if (mapView.StopRedo || mapView.BlockTime) return;
            mapView.Undo(false);
            bitmap2 = null;
            mapView.MapRenderer.UpdateCanvas(false, true);
            Invalidate(true);
            restrictWallTilesOnly();
        }

        private void redo_Click(object sender, EventArgs e)
        {
            if (mapView.StopRedo || mapView.BlockTime) return;
            mapView.Redo(false);
            bitmap2 = null;
            mapView.MapRenderer.UpdateCanvas(false, true);
            Invalidate(true);
            restrictWallTilesOnly();

        }

        private string getBaseMode(EditMode mode)
        {
            string modeString = mode.ToString();

            modeString = modeString.Substring(0, modeString.IndexOf("_")).Trim();

            return modeString;
        }


        private void restrictWallTilesOnly()
        {
            int steps = (mapView.TimeManager.Count - 1) - mapView.currentStep;
            if (steps > 0)
            {



                if (getBaseMode(mapView.TimeManager[steps - 1].Mode) != "WALL" && getBaseMode(mapView.TimeManager[steps - 1].Mode) != "FLOOR" && getBaseMode(mapView.TimeManager[steps - 1].Mode) != "POLYGON")
                    undo.Enabled = false;

            }


            if (mapView.currentStep > 0)
            {


                if (getBaseMode(mapView.TimeManager[steps + 1].Mode) != "WALL" && getBaseMode(mapView.TimeManager[steps + 1].Mode) != "FLOOR" && getBaseMode(mapView.TimeManager[steps + 1].Mode) != "POLYGON")
                    redo.Enabled = false;

            }
        }


        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {

            var page = tabControl1.SelectedTab;


            if (page == largeMap)
            {
                mapView.MapRenderer.UpdateCanvas(true, true);
                mapView.TabMapToolsSelectedIndexChanged(sender, e);
            }


            if (page == minimapTab)
            {

                undo.Enabled = mapView.undo.Enabled;
                redo.Enabled = mapView.redo.Enabled;

                if (miniWallBrush.Checked)
                {

                    MiniLineWall.Visible = true;
                }
                else
                    MiniLineWall.Visible = false;


                restrictWallTilesOnly();


                if (MapInterface.CurrentMode == EditMode.FLOOR_PLACE || MapInterface.CurrentMode == EditMode.FLOOR_BRUSH)
                {
                    miniTilePLace.Checked = mapView.TileMakeNewCtrl.PlaceTileBtn.Checked;
                    miniTileBrush.Checked = mapView.TileMakeNewCtrl.AutoTileBtn.Checked;
                }

                if (MapInterface.CurrentMode == EditMode.WALL_BRUSH || MapInterface.CurrentMode == EditMode.WALL_PLACE)
                    miniWallBrush.Checked = true;
                bitmap2 = null;
                Invalidate(true);
                numericUpDown2.Value = mapView.TileMakeNewCtrl.BrushSize.Value;
                if (MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE || mapView.PolygonEditDlg.Visible)
                {

                    miniEdit.Checked = true;
                    Point po = new Point(miniViewPanel.Width, 0);
                    po = miniViewPanel.PointToScreen(po);
                    if (!IsOnScreen(new Point(po.X + mapView.PolygonEditDlg.Width, po.Y))) return;
                    mapView.PolygonEditDlg.Location = po;
                    miniEdit.Checked = true;
                    return;


                }
                if (miniTilePLace.Checked)
                    MapInterface.CurrentMode = EditMode.FLOOR_PLACE;
                else if (miniTileBrush.Checked)
                    MapInterface.CurrentMode = EditMode.FLOOR_BRUSH;
                else if (miniWallBrush.Checked)
                    MapInterface.CurrentMode = EditMode.WALL_BRUSH;


            }


        }

        private void MiniLineWall_CheckedChanged(object sender, EventArgs e)
        {
            mapView.WallMakeNewCtrl.LineWall.Checked = MiniLineWall.Checked;
            mapView.WallMakeNewCtrl.RecWall.Checked = mapView.WallMakeNewCtrl.LineWall.Checked ? false : mapView.WallMakeNewCtrl.RecWall.Checked;

        }

        private void menuItem3_Click(object sender, EventArgs e)
        {

        }

        private void pickerc_Click(object sender, EventArgs e)
        {
            mapView.Picker.Checked = !mapView.Picker.Checked;
        }

        private void undoc_Click(object sender, EventArgs e)
        {
            mapView.undo.PerformClick();
        }

        private void redoc_Click(object sender, EventArgs e)
        {
            mapView.redo.PerformClick();
        }

        private void fastc_Click(object sender, EventArgs e)
        {
            /*
            mapView.prwSwitch.Checked = !mapView.prwSwitch.Checked;
            EditorSettings.Default.Edit_PreviewMode = !mapView.prwSwitch.Checked;
          */
        }

        private void modeswitchersc_Click(object sender, EventArgs e)
        {
            mapView.ModeSwitcher();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //if (saving.Visible)
            // saving.Hide();

        }

        private void recWallc_Click(object sender, EventArgs e)
        {
            if (MapInterface.CurrentMode != EditMode.WALL_BRUSH)
                return;
            mapView.WallMakeNewCtrl.RecWall.Checked = !mapView.WallMakeNewCtrl.RecWall.Checked;
            mapView.WallMakeNewCtrl.LineWall.Checked = false;
        }

        private void LineWallc_Click(object sender, EventArgs e)
        {
            if (MapInterface.CurrentMode != EditMode.WALL_BRUSH)
                return;
            mapView.WallMakeNewCtrl.LineWall.Checked = !mapView.WallMakeNewCtrl.LineWall.Checked;
            mapView.WallMakeNewCtrl.RecWall.Checked = false;
        }


        public void Reload()
        {
            bitmap2 = null;
            //mapView.MapRenderer.UpdateCanvas(true, true);
            Invalidate(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {

                PolygonEditor editor = mapView.PolygonEditDlg;
                Point po = new Point(miniViewPanel.Width, 0);
                po = miniViewPanel.PointToScreen(po);
                editor.Show();
                if (!IsOnScreen(new Point(po.X + mapView.PolygonEditDlg.Width, po.Y))) return;
                editor.Location = po;
            }
        }

        private void miniViewPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mapView.PolygonEditDlg.Visible && MapInterface.CurrentMode == EditMode.POLYGON_RESHAPE && mapView.PolygonEditDlg.SelectedPolygon != null && mapView.PolygonEditDlg.SelectedPolygon.Points.Count > 2)
            {
                Point pt = new Point((e.X * 2) * MapView.squareSize / (mapZoom * 2), (e.Y * 2) * MapView.squareSize / (mapZoom * 2));
                if (mapView.PolygonEditDlg.SelectedPolygon.IsPointInside(pt))
                    mapView.PolygonEditDlg.ButtonModifyClick(sender, e);
            }
        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            mapView.Switch45Area();
        }

        private void miniViewPanel_Resize(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (!mapView.PolygonEditDlg.Visible) return;

                Point po = new Point(miniViewPanel.Width, 0);
                po = miniViewPanel.PointToScreen(po);
                if (this.Location.X + this.Width < po.X + mapView.PolygonEditDlg.Width) return;
                if (!IsOnScreen(new Point(po.X + mapView.PolygonEditDlg.Width, po.Y))) return;
                mapView.PolygonEditDlg.Location = po;
            }

        }
        public bool IsOnScreen(Point pt)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                //Point formTopLeft = new Point(form.Left, form.Top);

                if (screen.WorkingArea.Contains(pt))
                {
                    return true;
                }
            }

            return false;
        }
        /*
        private bool GrantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            System.Security.AccessControl.DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule("everyone", System.Security.AccessControl.FileSystemRights.FullControl,
                                                             System.Security.AccessControl.InheritanceFlags.ObjectInherit | System.Security.AccessControl.InheritanceFlags.ContainerInherit,
                                                             System.Security.AccessControl.PropagationFlags.NoPropagateInherit, System.Security.AccessControl.AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
            return true;
        }
        */
        private bool GrantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            System.Security.AccessControl.DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null), System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.InheritanceFlags.ObjectInherit | System.Security.AccessControl.InheritanceFlags.ContainerInherit, System.Security.AccessControl.PropagationFlags.NoPropagateInherit, System.Security.AccessControl.AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
            return true;
        }


        private void mapInstall_Click(object sender, EventArgs e)
        {

            Saving saving = new Saving();
            string mapName = "";
            if (map.FileName != "" && map.FileName != null)
            {
                mapName = Path.GetFileName(map.FileName);
                string sub = mapName.Substring(0, mapName.Length - 4);
                mapName = sub;
            }
            saving.mapName.Text = mapName;

            if (saving.ShowDialog(this) == DialogResult.OK)
            {
                mapName = saving.mapName.Text;
            }
            else
                return;

            saving.Dispose();

            string mapDest = NoxDb.NoxPath + "Maps\\" + mapName;
            string newname = mapDest + "\\" + mapName + ".map";
            // MessageBox.Show(mapDest);
            if (!Directory.Exists(NoxDb.NoxPath + "Maps\\"))
            {
                MessageBox.Show("No permission to Nox map folder or that folder doesn't exist!");
                return;
            }
            else if (!File.Exists(newname))
            {
                try
                {
                    Directory.CreateDirectory(mapDest);
                    GrantAccess(mapDest);
                }
                catch
                {
                    MessageBox.Show("No permission to create folder!");
                    return;
                }
            }
            map.FileName = newname;
            save();


        }

        private void GridDraw_Click(object sender, EventArgs e)
        {
            EditorSettings.Default.Draw_Grid = !EditorSettings.Default.Draw_Grid;
        }

        private void ObjectDraw_Click(object sender, EventArgs e)
        {
            EditorSettings.Default.Draw_Objects = !EditorSettings.Default.Draw_Objects;
        }

        private void WallDraw_Click(object sender, EventArgs e)
        {
            EditorSettings.Default.Draw_Walls = !EditorSettings.Default.Draw_Walls;
        }

        private void fullExtentsDraw_Click(object sender, EventArgs e)
        {
            EditorSettings.Default.Draw_Extents_3D = !EditorSettings.Default.Draw_Extents_3D;
            EditorSettings.Default.Draw_AllExtents = EditorSettings.Default.Draw_Extents_3D;
            mapView.setRadioDraw();
        }

        private void menuItemImportScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Nox Script Objects (*.obj)|*.obj";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                NoxBinaryReader rdr = new NoxBinaryReader(File.Open(fd.FileName, FileMode.Open), CryptApi.NoxCryptFormat.NONE);
                map.ReadScriptObject(rdr);
            }
        }

        private void menuItemExportScript_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Nox Script Source (*.ns)|*.ns";

            if (map.Scripts.SctStr.Count == 0 || !map.Scripts.SctStr[0].StartsWith("NOXSCRIPT3.0"))
            {
                MessageBox.Show("Invalid Nox Script header.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(fd.FileName, map.Scripts.SctStr[0].Substring(12));
            }
        }



    }
}