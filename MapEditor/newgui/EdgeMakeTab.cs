/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 23.01.2015
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MapEditor.videobag;
using NoxShared;

namespace MapEditor.newgui
{
	/// <summary>
	/// Edge creation GUI
	/// </summary>
    public partial class EdgeMakeTab : UserControl
    {
        private List<string> sortedEdgeNames;
        private MapView mapView;
        private VideoBagCachedProvider videoBag = null;
        int edgeDirection = 0;
        int edgeTypeID = 0;

        public EdgeMakeTab()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //blackTileSprite = (int) ThingDb.FloorTiles[ThingDb.FloorTileNames.IndexOf("Black")].Variations[0];
            // listview ставим обработчики
            listEdgeImages.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listEdgeImages_RetrieveVirtualItem);
        }

        void listEdgeImages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem item = new ListViewItem("", e.ItemIndex);
            item.BackColor = Color.White;
            e.Item = item;
        }

        private int GetSelTileTypeIndex()
        {
            return ThingDb.EdgeTileNames.IndexOf(sortedEdgeNames[comboEdgeType.SelectedIndex]);
        }

        public void SetMapView(MapView view)
        {
            mapView = view;
            // необходимо чтобы картинки доставать
            videoBag = mapView.MapRenderer.VideoBag;
            // названия плиток сортируем и добавляем в список
            sortedEdgeNames = new List<string>(ThingDb.EdgeTileNames.ToArray());
            sortedEdgeNames.Sort();
            comboEdgeType.Items.AddRange(sortedEdgeNames.ToArray());
            comboEdgeType.SelectedIndex = 0;
        }

        /// <summary>
        /// Возвращает новый экземпляр EdgeTile в соответствии с настройками пользователя
        /// </summary>
        public Map.Tile.EdgeTile GetEdge()
        {
            // как покрытие юзаем тот тайл что выбран во вкладке Tiles
            Map.Tile coverTile = mapView.TileMakeNewCtrl.GetTile(Point.Empty);
            // MessageBox.Show(edgeDirection.ToString());
            return new Map.Tile.EdgeTile(coverTile.graphicId, coverTile.Variation, (Map.Tile.EdgeTile.Direction)edgeDirection, (byte)edgeTypeID);
        }

        public void UpdateListView(object sender, EventArgs e)
        {
            // force update data
            edgeDirection = 0;
            edgeTypeID = GetSelTileTypeIndex();
            listEdgeImages.VirtualListSize = 0;
            List<uint> variations = ThingDb.EdgeTiles[edgeTypeID].Variations;
            listEdgeImages.VirtualListSize = variations.Count;
            // not yet created
            if (listEdgeImages.LargeImageList == null)
                listEdgeImages.LargeImageList = new ImageList();
            // clear ImageList
            ImageList imglist = listEdgeImages.LargeImageList;
            foreach (Image img in imglist.Images)
            	img.Dispose();
            imglist.Images.Clear();
            imglist.ImageSize = new Size(46, 46);
            // update ImageList showing edge type selected by user
            Map.Tile coverTile = mapView.TileMakeNewCtrl.GetTile(Point.Empty, true);
            int coverSprite = (int)coverTile.Variations[coverTile.Variation];
            int varns = variations.Count;
            if (mapView.TileMakeNewCtrl.edgeBox.Items.Count > 0)
            mapView.TileMakeNewCtrl.edgeBox.SelectedIndex = comboEdgeType.SelectedIndex;
         
            for (int varn = 0; varn < varns; varn++)
            {
            	Bitmap edge = new Bitmap(46, 46);
            	videoBag.ApplyEdgeMask(edge, (int)variations[varn], coverSprite);
            	imglist.Images.Add(edge);
            }
        }

        void SelectEdgeDirection(object sender, EventArgs e)
        {
            if (listEdgeImages.SelectedIndices.Count > 0)
            {
                edgeDirection = listEdgeImages.SelectedIndices[0];
               
            }
            
            // update mapinterface
            //mapView.GetMapInt().EdgeSetData((byte) edgeTypeID, (byte) edgeDirection);
        }

       

        private void AutoEgeBox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (AutoEgeBox.Checked)
            {
                AutoEdgeGrp.Enabled = true;
               // ignoreAllBox.Enabled = true;
                //preserveBox.Enabled = true;
            }
            else
            {
                AutoEdgeGrp.Enabled = false;
                //ignoreAllBox.Enabled = false;
                //preserveBox.Enabled = false;
            }
        
        }

        private void listEdgeImages_MouseClick(object sender, MouseEventArgs e)
        {
            AutoEgeBox.Checked = false;
        }

        private void AutoEdgeGrp_Enter(object sender, EventArgs e)
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

        private void ignoreAllBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EdgeMakeTab_Load(object sender, EventArgs e)
        {

        }

    }   
}
