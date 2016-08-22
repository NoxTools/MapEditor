/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 15.01.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using NoxShared;
using MapEditor.MapInt;
using MapEditor.videobag;

namespace MapEditor.newgui
{
    /// <summary>
    /// Wall creation GUI
    /// </summary>
    public partial class WallMakeTab : UserControl
    {
       // private Map.Wall wall;
        public byte flags;
        public RadioButton[] buttons = new RadioButton[2];
        private MapView mapView;
        private VideoBagCachedProvider videoBag = null;
        public Button[] WallSelectButtons;
        public int wallFacing = 0;
        public bool started = false;

        public int SelectedWallFacing
        {
            get
            {
                return wallFacing;
            }
            set
            {
                if (value > 10) wallFacing = 0;
                else if (value < 0) wallFacing = 10;
                else wallFacing = value;
            }
        }


        public int MinimapGroup
        {
            get
            {
                return (int)numMapGroup.Value;
            }
        }
        private bool wallWindowed = false;
        private int blackWallIndex = 0;
        private List<string> sortedWallNames;

        private static Color btnColorBadWall = Color.LightGray;
        private static Color btnColorGoodWall = Color.White;

        public WallMakeTab()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //
            numMapGroup.Value = 100;
            blackWallIndex = ThingDb.WallNames.IndexOf("BlackWall");
            // названия стен сортируем по алфавиту
            sortedWallNames = new List<string>(ThingDb.WallNames.ToArray());
            sortedWallNames.Sort();
            // кнопочки в массив
            WallSelectButtons = new Button[] { wallBtn1, wallBtn2, wallBtn3, wallBtn4, wallBtn5, wallBtn6, wallBtn7, wallBtn8, wallBtn9, wallBtn10, wallBtn11, wallBtn12, wallBtn13 };
            foreach (Button b in this.WallSelectButtons)
            {
                b.BackgroundImageLayout = ImageLayout.Center;
                b.Enabled = false;
                b.Click += new EventHandler(WallBtnClicked);
            }
            // setup modes
            buttonMode.SetStates(new EditMode[] { EditMode.WALL_PLACE, EditMode.WALL_BRUSH, EditMode.WALL_CHANGE });

            buttons[0] = PlaceWalltBtn;
            buttons[1] = AutoWalltBtn;

        }

        public void SetMapView(MapView view)
        {
            
            mapView = view;
            // provide videobag access
            videoBag = mapView.MapRenderer.VideoBag;
            // update wall list
            comboWallSet.Items.AddRange(sortedWallNames.ToArray());

            comboWallSet.SelectedIndex = 0;
        }
        public void SetWall(Map.Wall wall, bool read = false)
        {

            // Flags

            if (read)
            {
                polygonGroup.Value = 100;
                checkListFlags.SetItemChecked(0, false);
                checkListFlags.SetItemChecked(1, false);
                checkListFlags.SetItemChecked(2, false);
                checkListFlags.SetItemChecked(3, false);
                checkDestructable.Checked = false;
                numericCloseDelay.Value = 3;


                //if (wall.Secret_WallState > 0) comboWallState.SelectedIndex = wall.Secret_WallState - 1;
                openWallBox.Checked = wall.Secret_WallState == 4 ? true : false;
                checkDestructable.Checked = wall.Destructable;
                polygonGroup.Value = wall.Minimap;
                numericCloseDelay.Value = wall.Secret_OpenWaitSeconds;
                if ((wall.Secret_ScanFlags & 1) == 1) checkListFlags.SetItemChecked(0, true);
                if ((wall.Secret_ScanFlags & 2) == 2) checkListFlags.SetItemChecked(1, true);
                if ((wall.Secret_ScanFlags & 4) == 4) checkListFlags.SetItemChecked(2, true);
                if ((wall.Secret_ScanFlags & 8) == 8) checkListFlags.SetItemChecked(3, true);
                if (mapView.WallMakeNewCtrl.checkListFlags.GetItemChecked(0))
                {
                    mapView.WallMakeNewCtrl.openWallBox.Enabled = true;
                }
                else
                {
                    mapView.WallMakeNewCtrl.openWallBox.Checked = false;
                    mapView.WallMakeNewCtrl.openWallBox.Enabled = false;
                }

            }
            else
            {
                flags = 0;
                if (checkListFlags.GetItemChecked(0)) flags |= 1;
                if (checkListFlags.GetItemChecked(1)) flags |= 2;
                if (checkListFlags.GetItemChecked(2)) flags |= 4;
                if (checkListFlags.GetItemChecked(3)) flags |= 8;
                wall.Secret_ScanFlags = flags;


                if (wall != null)
                    wall.Secret_WallState = openWallBox.Checked ? (byte)4 : (byte)0;

                //wall.Secret_WallState = (byte)(comboWallState.SelectedIndex + 1);

                wall.Secret_OpenWaitSeconds = (int)numericCloseDelay.Value;

                // Destructable
                wall.Destructable = checkDestructable.Checked;
                // Minimap
                wall.Minimap = (byte)polygonGroup.Value;
            }

        }
        /// <summary>
        /// Button has been pressed, alter walltype
        /// </summary>
        /// 
        
        void WallBtnClicked(object sender, EventArgs e)
        {
            wallFacing = Array.IndexOf(WallSelectButtons, sender);
            wallWindowed = false;
            started = true;
            if (wallFacing > 10)
            {
                // стены с окном
                wallFacing -= 11;
                wallWindowed = true;
            }
            if (wallFacing < 0) wallFacing = 0;
            if (MapInterface.CurrentMode == EditMode.WALL_CHANGE)
            {
                //exitProp();
                WallProp.Visible = false;
                MainWindow.Instance.mapView.TabMapToolsSelectedIndexChanged(sender, e);
            }

            // update mapinterface
            //mapView.GetMapInt().WallSetData(GetSelWallTypeIndex(), (byte)MinimapGroup, (byte)numWallVari.Value, (byte)wallFacing, checkBreakableWall.Checked, wallWindowed);
        }

        public byte GetSelWallTypeIndex()
        {
            int selectedIndex = comboWallSet.SelectedIndex;

            string wallName = removeSpace(comboWallSet.Items[selectedIndex].ToString());
            int index = ThingDb.WallNames.IndexOf(wallName);

            if (index > 0) return (byte)index;
            return 0;
        }


        /// <summary>
        /// Создаем новую стену, в соответствии с тем что указал пользователь
        /// </summary>
        public Map.Wall NewWall(Point location, bool fake = false)
        {
            byte material = GetSelWallTypeIndex();
            Map.Wall.WallFacing facing = (Map.Wall.WallFacing)wallFacing;

            Map.Wall wall = new Map.Wall(location, facing, material, (byte)MinimapGroup, (byte)numWallVari.Value);

            //wall.Destructable = checkBreakableWall.Checked;
            wall.Window = wallWindowed;

            // generate random variation
            if (autovari.Checked && !fake && !started)
            {
                if (wall.Window) return wall;
                if ((int)wall.Facing > 1) return wall;

                Random rnd = new Random(location.Y + location.Y + (int)DateTime.Now.Ticks);
                int randvar = rnd.Next((int)numWallVari.Value, (int)numWallVariMax.Value + 1);
                byte rndvari = Convert.ToByte(randvar);

                wall.Variation = (byte)rnd.Next((int)numWallVari.Value, (int)numWallVariMax.Value + 1);
                if (wall.Window)
                    wall.Variation = 0;

                if ((int)wall.Facing == 2) wall.Variation = 0;
            }
            started = false;

            return wall;
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

        public void findWallInList(string data)
        {
            for (int i = 0; i <= comboWallSet.Items.Count; i++)
            {
                if (removeSpace(comboWallSet.Items[i].ToString()) == data)
                {
                    comboWallSet.SelectedIndex = i;
                    break;
                }
            }
        }

        void UpdateBtnImages(object sender, EventArgs e)
        {
            if (videoBag == null) return;

            if (numWallVariMax.Value < numWallVari.Value)
                numWallVariMax.Value = numWallVari.Value;

            ThingDb.Wall wall = ThingDb.Walls[GetSelWallTypeIndex()];
            // в движке Нокса зачем-то так
            int vari = (int)numWallVari.Value * 2;
            ThingDb.Wall.WallRenderInfo[] ria;
            Bitmap bitmap; int sprite; Button wallButton;

            byte material = GetSelWallTypeIndex();

            ria = wall.RenderNormal[0];
            int hoho = ria.Length;
            hoho = (hoho / 2) - 1;

            numWallVariMax.Maximum = 30;

            if (numWallVariMax.Value < numWallVari.Value)
                numWallVariMax.Value = numWallVari.Value;

            // для каждого направления добавляем картинку
            if (WallSelectButtons != null)
            {
                for (int facing = 0; facing < 13; facing++)
                {
                    //ria = wall.RenderBreakable[facing];
                    ria = wall.RenderNormal[facing];

                    wallButton = WallSelectButtons[facing];
                    if (ria.Length > vari)
                    {
                        sprite = ria[vari].SpriteIndex;
                        // если включена опция Fast preview (помогает понять направления стен) подменяем картинку
                        if (checkBlackWalls.Checked) sprite = ThingDb.Walls[blackWallIndex].RenderNormal[facing][0].SpriteIndex;
                        // достаем картинку и включаем кнопку если такая стена существует
                        bitmap = videoBag.GetBitmap(sprite);
                        wallButton.BackgroundImage = bitmap;
                        wallButton.Enabled = true;
                        wallButton.BackColor = btnColorGoodWall;
                        numWallVari.Maximum = hoho;
                        numWallVariMax.Maximum = hoho;
                        numWallVariMax.Value = (sender.GetType().Name == "ComboBox") ? hoho : numWallVariMax.Value;
                    }
                    else
                    {
                        if (facing == 0)
                        {
                            numWallVari.Value--;
                            numWallVari.Maximum = numWallVari.Value;
                            numWallVariMax.Maximum = numWallVari.Value;
                            return;
                        }

                        // значит что стены с такими парамиетрами не существует
                        wallButton.BackgroundImage = null;
                        wallButton.Enabled = false;
                        wallButton.BackColor = btnColorBadWall;
                    }
                }
            }
        }

        private void WallMakeTab_Load(object sender, EventArgs e)
        {

        }

        private void numWallVariMax_ValueChanged(object sender, EventArgs e)
        {
            if (numWallVariMax.Value < numWallVari.Value)
                numWallVariMax.Value = numWallVari.Value;
        }

        private void WallSetFirst(object sender, MouseEventArgs e)
        {
        }


        private void PlaceWalltBtn_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void PlaceWalltBtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            RecLinePanel.Visible = false;
            radioButton.Font = new Font(radioButton.Font.Name, radioButton.Font.Size, FontStyle.Regular);
            if (!radioButton.Checked) return;

            if (radioButton.Name == "AutoWalltBtn")
            {
                RecLinePanel.Visible = true;

            }


            radioButton.Font = new Font(radioButton.Font.Name, radioButton.Font.Size, FontStyle.Bold);
            MapInterface.CurrentMode = (EditMode)radioButton.Tag;
            

        }



        private void Picker_CheckedChanged(object sender, EventArgs e)
        {
            if (Picker.Checked)
            {
                mapView.Picker.Checked = true;
               // mapView.picking = true;
                //Cursor myCursor = Cursors.Cross;
                //if (System.IO.File.Exists("picker.cur"))
                //myCursor = new Cursor("picker.cur");

               // mapView.mapPanel.Cursor = myCursor;
            }
            else
            {
                mapView.Picker.Checked = false;
                mapView.picking = false;

                //this.Cursor = myCursor;
                mapView.mapPanel.Cursor = Cursors.Default;
            }
        }

        private void LineWall_CheckedChanged(object sender, EventArgs e)
        {
            mapView.mouseKeepOff = new Point();
        }

        private void RecWall_CheckedChanged(object sender, EventArgs e)
        {
            mapView.mouseKeepOff = new Point();
        }

        private void LineWall_Click(object sender, EventArgs e)
        {
 RecWall.Checked = LineWall.Checked ? false : LineWall.Checked;
        }

        private void RecWall_Click(object sender, EventArgs e)
        {
  LineWall.Checked = RecWall.Checked ? false : RecWall.Checked;
        }

        private void LineWall_EnabledChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = sender as CheckBox;
            if (CheckBox.Enabled)
                CheckBox.Visible = true;
            else
                CheckBox.Visible = false;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            mapView.Picker.Checked = false;
            //mapView.secprops.Show();
            WallProp.Visible = true;
            WallProp.BringToFront();
            
            MapInterface.CurrentMode = MapInt.EditMode.WALL_CHANGE;

        }

        private void wallBtn4_Click(object sender, EventArgs e)
        {

        }

        private void wallBtn1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void wallBtnContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void WallProp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            exitProp();
            MainWindow.Instance.mapView.TabMapToolsSelectedIndexChanged(sender, e);
        }

        private void exitProp()
        {
            
            polygonGroup.Value = 100;
            checkListFlags.SetItemChecked(0, false);
            checkListFlags.SetItemChecked(1, false);
            checkListFlags.SetItemChecked(2, false);
            checkListFlags.SetItemChecked(3, false);
            checkDestructable.Checked = false;
            numericCloseDelay.Value = 3;
            openWallBox.Checked = false;
            WallProp.Visible = false;
           
        }

        private void checkListFlags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkListFlags.GetItemChecked(0))
            {
                openWallBox.Enabled = true;
            }
            else
            {
                openWallBox.Checked = false;
                openWallBox.Enabled = false;
            }
        }

        private void checkListFlags_SelectedValueChanged(object sender, EventArgs e)
        {
            if (checkListFlags.GetItemChecked(0))
            {
                openWallBox.Enabled = true;
            }
            else
            {
                openWallBox.Checked = false;
                openWallBox.Enabled = false;
            }
        }

        private void checkListFlags_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkListFlags.GetItemChecked(0))
            {
                openWallBox.Enabled = true;
            }
            else
            {
                openWallBox.Checked = false;
                openWallBox.Enabled = false;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
