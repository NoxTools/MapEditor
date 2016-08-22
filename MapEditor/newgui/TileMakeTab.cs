/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 15.01.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MapEditor.MapInt;
using MapEditor.videobag;
using NoxShared;

namespace MapEditor.newgui
{
    /// <summary>
    /// Tile creation GUI
    /// </summary>
    public partial class TileMakeTab : UserControl
    {
        public RadioButton[] buttons = new RadioButton[2];
        public bool TabLoaded = false;
        private List<string> sortedEdgeNames;
        private List<string> sortedTileNames;
        private MapView mapView;
        private VideoBagCachedProvider videoBag = null;
        private int tileVariation;
        private int tileTypeID;
        public bool AutoVari
        {
            get
            {
                return checkAutoVari.Checked;
            }
        }

        public TileMakeTab()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            // setup listview handlers
            listTileImages.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listTileImages_RetrieveVirtualItem);
        	// setup modes
			buttonMode.SetStates(new EditMode[] { EditMode.FLOOR_PLACE, EditMode.FLOOR_BRUSH });
            BrushSize.MouseWheel += new MouseEventHandler(BrushSizeMouseWheel);
            buttons[0] = PlaceTileBtn;
            buttons[1] = AutoTileBtn;

        }

        public void SetMapView(MapView view)
        {
            mapView = view;
            // provides access to images
            videoBag = mapView.MapRenderer.VideoBag;
            // sort tile names and add them into combobox
            sortedTileNames = new List<string>(ThingDb.FloorTileNames.ToArray());
            sortedTileNames.Sort();
            comboTileType.Items.AddRange(sortedTileNames.ToArray());
            comboIgnoreTile.Items.Add("NONE");
            comboIgnoreTile.Items.AddRange(sortedTileNames.ToArray());
            comboTileType.SelectedIndex = 0;
            comboIgnoreTile.SelectedIndex = 0;

            sortedEdgeNames = new List<string>(ThingDb.EdgeTileNames.ToArray());
            sortedEdgeNames.Sort();
            edgeBox.Items.AddRange(sortedEdgeNames.ToArray());
            edgeBox.SelectedIndex = 0;




        }

        void listTileImages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem item = new ListViewItem("", e.ItemIndex);
            item.BackColor = Color.White;
            e.Item = item;
        }

        private int GetSelTileTypeIndex()
        {
            int selectedIndex = comboTileType.SelectedIndex;
            string tileName = comboTileType.Items[selectedIndex].ToString();
            int index = ThingDb.FloorTileNames.IndexOf(tileName);

            if (index > 0) return index;
            return 0;
        }

        private List<uint> GetVariationsForType(int ttype)
        {
            return ThingDb.FloorTiles[ttype].Variations;
        }

        void UpdateListView(object sender, EventArgs e)
        {
          
            // принуждаем обновить данные
          
            tileVariation = 0;
            tileTypeID = GetSelTileTypeIndex();
            listTileImages.VirtualListSize = 0;
            listTileImages.VirtualListSize = GetVariationsForType(tileTypeID).Count;
            // если не создан
            if (listTileImages.LargeImageList == null)
                listTileImages.LargeImageList = new ImageList();
            // обновляем ImageList
            ImageList imglist = listTileImages.LargeImageList;
            imglist.Images.Clear();
            imglist.ImageSize = new Size(46, 46);
            List<uint> variations = GetVariationsForType(tileTypeID);
            // грузим только первые 90 картинок
            int varns = variations.Count;
            if (varns > 90) varns = 90;
            for (int varn = 0; varn < varns; varn++)
            {
                imglist.Images.Add(videoBag.GetBitmap((int)variations[varn]));
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
        public void findTileInList(string data)
        {
            for (int i = 0; i <= comboTileType.Items.Count; i++)
            {
                if (removeSpace(comboTileType.Items[i].ToString()) == data)
                {
                    comboTileType.SelectedIndex = i;
                    break;
                }
            }
        }

        public Map.Tile GetTile(Point loc, bool fake = false)
        {
            
            // проверяем координаты
            if (loc.X < 0 || loc.Y < 0 || loc.X > 252 || loc.Y > 252) return null;

            ushort vari = (ushort)tileVariation;
            if (checkAutoVari.Checked)
            {
                int x = loc.X;
                int y = loc.Y;
                int cols = ThingDb.FloorTiles[tileTypeID].numCols;
                int rows = ThingDb.FloorTiles[tileTypeID].numRows;

                vari = (ushort)(((x + y) / 2 % cols) + (((y % rows) + 1 + cols - ((x + y) / 2) % cols) % rows) * cols);
            }

            return new Map.Tile(loc, (byte)tileTypeID, vari);
        }

        void ChangeTileType(object sender, EventArgs e)
        {
            if (listTileImages.SelectedIndices.Count > 0)
            {
                tileVariation = listTileImages.SelectedIndices[0];
                // update mapinterface
                //mapView.GetMapInt().FloorSetData((byte)tileTypeID, tileVariation);
            }
        }
        private void BrushSizeMouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            if (e.Delta >= 90 && BrushSize.Value < 6) BrushSize.Value += 1;
            if (e.Delta <= 90 && BrushSize.Value > 1) BrushSize.Value -= 1;
        }
        private void TileMakeTab_Load(object sender, EventArgs e)
        {

        }

        private void buttonMode_Click(object sender, EventArgs e)
        {

        }

        private void PlaceTileBtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            radioButton.Font = new Font(radioButton.Font.Name, radioButton.Font.Size, FontStyle.Regular);
            if (!radioButton.Checked) return;
            radioButton.Font = new Font(radioButton.Font.Name, radioButton.Font.Size, FontStyle.Bold);
            Picker.Checked = false;
            MapInterface.CurrentMode = (EditMode)radioButton.Tag;


            if (PlaceTileBtn.Checked)
            {
                miniEdges.Enabled = false;
                groupBox1.Enabled = false;
            }
            else
            {
                miniEdges.Enabled = true;
                groupBox1.Enabled = true;

            }
        }

        private void Picker_Click(object sender, EventArgs e)
        {
            mapView.picking = true;
            Cursor myCursor = new Cursor("picker.cur");
            //this.Cursor = myCursor;
            //MainWindow.Instance.Cursor = myCursor;
            mapView.mapPanel.Cursor = myCursor;

        }

        private void labelSep1_Click(object sender, EventArgs e)
        {

        }

        private void Picker_CheckedChanged(object sender, EventArgs e)
        {
            if (Picker.Checked)
                mapView.Picker.Checked = true;
                else
            {
                mapView.Picker.Checked = false;
                mapView.picking = false;


            }
        }

        private int GetSelEdgeTypeIndex()
        {
            return ThingDb.EdgeTileNames.IndexOf(sortedEdgeNames[edgeBox.SelectedIndex]);
        }

        private void BrushSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void edgeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (mapView.EdgeMakeNewCtrl.comboEdgeType.Items.Count > 0)
            mapView.EdgeMakeNewCtrl.comboEdgeType.SelectedIndex = edgeBox.SelectedIndex;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


      
    }
}
