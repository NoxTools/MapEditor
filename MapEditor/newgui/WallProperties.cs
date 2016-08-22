/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 24.01.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared;

namespace MapEditor.newgui
{
	/// <summary>
	/// GUI editor for special wall properties
	/// </summary>
	public partial class WallProperties : Form
	{
		private Map.Wall wall;
        public byte flags;
		public WallProperties()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//comboWallState.SelectedIndex = 0;
		}
		
		public void SetWall(Map.Wall wall, bool read = false)
		{

			this.wall = wall;

			
			// Flags

            if (read)
            {
                polygonGroup.Value = 100;
                checkListFlags.SetItemChecked(0, false);
                checkListFlags.SetItemChecked(1, false);
                checkListFlags.SetItemChecked(2, false);
                checkListFlags.SetItemChecked(3, false);
                comboWallState.SelectedIndex = 0;
                checkDestructable.Checked = false;
                numericCloseDelay.Value = 3;
                  

                if (wall.Secret_WallState > 0) comboWallState.SelectedIndex = wall.Secret_WallState - 1;
                openWallBox.Checked = wall.Secret_WallState == 4 ? true : false;
                checkDestructable.Checked = wall.Destructable;
                polygonGroup.Value = wall.Minimap;
                numericCloseDelay.Value = wall.Secret_OpenWaitSeconds;
                if ((wall.Secret_ScanFlags & 1) == 1) checkListFlags.SetItemChecked(0, true);
                if ((wall.Secret_ScanFlags & 2) == 2) checkListFlags.SetItemChecked(1, true);
                if ((wall.Secret_ScanFlags & 4) == 4) checkListFlags.SetItemChecked(2, true);
                if ((wall.Secret_ScanFlags & 8) == 8) checkListFlags.SetItemChecked(3, true);

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
		
		void ButtonDoneClick(object sender, EventArgs e)
		{
            MainWindow.Instance.mapView.TabMapToolsSelectedIndexChanged(sender, e);
            /*
            flags = 0;
            //wall.Secret_ScanFlags = 0;
            if (checkListFlags.GetItemChecked(0)) flags |= 1;
            if (checkListFlags.GetItemChecked(1)) flags |= 2;
            if (checkListFlags.GetItemChecked(2)) flags |= 4;
            if (checkListFlags.GetItemChecked(3)) flags |= 8;
            //MessageBox.Show(flags.ToString());
            wall.Secret_ScanFlags = flags;
            */
			DialogResult = DialogResult.OK;
            polygonGroup.Value = 100;
            checkListFlags.SetItemChecked(0, false);
            checkListFlags.SetItemChecked(1, false);
            checkListFlags.SetItemChecked(2, false);
            checkListFlags.SetItemChecked(3, false);
            comboWallState.SelectedIndex = 0;
            checkDestructable.Checked = false;
            numericCloseDelay.Value = 3;
			this.Visible = false;

		}
		
		void CheckDestructableCheckedChanged(object sender, EventArgs e)
		{
			//wall.Destructable = checkDestructable.Checked;
		}
		
		void ComboWallStateSelectedIndexChanged(object sender, EventArgs e)
		{
			//if (wall == null) return;
			//wall.Secret_WallState = (byte) (comboWallState.SelectedIndex + 1);
		}
		
		void NumericCloseDelayValueChanged(object sender, EventArgs e)
		{
			//wall.Secret_OpenWaitSeconds = (int) numericCloseDelay.Value;
		}
		
		void PolygonGroupValueChanged(object sender, EventArgs e)
		{
			//wall.Minimap = (byte) polygonGroup.Value;
		}
		
		void CheckListFlagsItemCheck(object sender, ItemCheckEventArgs e)
		{
            
            /*
            flags = wall.Secret_ScanFlags;
			if (e.Index == 0 && e.NewValue == CheckState.Checked) flags |= 1;
			if (e.Index == 1 && e.NewValue == CheckState.Checked) flags |= 2;
			if (e.Index == 2 && e.NewValue == CheckState.Checked) flags |= 4;
			if (e.Index == 3 && e.NewValue == CheckState.Checked) flags |= 8;
			wall.Secret_ScanFlags = flags;
            */
           // if (e.Index == 0 && e.NewValue == CheckState.Checked) 
             //   


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

        private void WallProperties_Load(object sender, EventArgs e)
        {
             
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void WallProperties_Leave(object sender, EventArgs e)
        {
            MainWindow.Instance.Focus();
        }

        private void buttonDone_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void WallProperties_MouseLeave(object sender, EventArgs e)
        {
            MainWindow.Instance.Focus();
        }

        private void openWallBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        
	}
}
