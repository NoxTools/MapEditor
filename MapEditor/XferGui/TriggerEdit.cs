/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 09.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of TriggerEdit.
	/// </summary>
	public partial class TriggerEdit : XferEditor
	{
		private TriggerXfer xfer;
		
		public TriggerEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.

            

          



			//
			InitializeComponent();
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			// читаем Xfer
			xfer = obj.GetExtraData<TriggerXfer>();
			scriptActivated.Text = xfer.ScriptOnPressed;
			scriptReleased.Text = xfer.ScriptOnReleased;
			scriptCollided.Text = xfer.ScriptOnCollided;
			// единственный способ отличить PressurePlate/Trigger от Button/Lever
			if (ThingDb.Things[obj.Name].ExtentType != "BOX")
			{
				groupBoxArea.Enabled = false;
			}
			else
			{
				sizeX.Value = xfer.SizeX;
				sizeY.Value = xfer.SizeY;
				plateEdgeColor.BackColor = xfer.EdgeColor;
			}

           
            flagsBox.SetItemChecked(0, (xfer.AllowedObjClass & 0x2) == 0x2); // NO_UPDATE
            flagsBox.SetItemChecked(1, (xfer.AllowedObjClass & 0x4) == 0x4); // DESTROYED
            flagsBox.SetItemChecked(2, (xfer.AllowedObjClass & 0x1) == 0x1); //MISSILE
            flagsBox.SetItemChecked(3, (xfer.AllowedObjClass & 0x8) == 0x8); // NO_COLLIDE
            flagsBox.SetItemChecked(4, (xfer.AllowedObjClass & 0x80000000) == 0x80000000);// NPC
            flagsBox.SetItemChecked(5, (xfer.AllowedObjClass & 0x10) == 0x10); // EQUIPPED
            flagsBox.SetItemChecked(6, (xfer.AllowedObjClass & 0x1000000) == 0x1000000); // ENABLED
            flagsBox.SetItemChecked(7, (xfer.AllowedObjClass & 0x2000000) == 0x2000000); // NO_AUTO_DROP
            flagsBox.SetItemChecked(8, (xfer.AllowedObjClass & 0x8000000) == 0x8000000); // NO_PUSH_CHARACTERS
            flagsBox.SetItemChecked(9, (xfer.AllowedObjClass & 0x1000) == 0x1000); // wand
            numericUpDown1.Value = xfer.AllowedTeamID;
           




		}
		
		public override Map.Object GetObject()
		{
            // записываем все изменения   MISSILE = 0x1
			xfer.ScriptOnPressed = scriptActivated.Text;
			xfer.ScriptOnReleased = scriptReleased.Text;
			xfer.ScriptOnCollided = scriptCollided.Text;
			xfer.SizeX = (int) sizeX.Value;
			xfer.SizeY = (int) sizeY.Value;
			xfer.EdgeColor = plateEdgeColor.BackColor;
            xfer.AllowedTeamID = (byte)numericUpDown1.Value;

            uint[] flags = { 0x2, 0x4, 0x1, 0x8, 0x80000000, 0x10, 0x1000000, 0x2000000, 0x8000000, 0x1000 };


            uint CreatedFlags = 0;
            foreach (int i in flagsBox.CheckedIndices)
            {
                CreatedFlags |= flags[i];
            }

            xfer.AllowedObjClass = (int)CreatedFlags;

			return obj;
		}
		
		public override void SetDefaultData(Map.Object obj)
		{
			obj.NewDefaultExtraData();
			xfer = obj.GetExtraData<TriggerXfer>();
			if (ThingDb.Things[obj.Name].ExtentType != "BOX")
			{
				xfer.BackColor = Color.Black;
				xfer.EdgeColor = Color.Black;
			}
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		
		void PlateEdgeColorClick(object sender, EventArgs e)
		{
			ColorDialog colorDlg = new ColorDialog();
			if (colorDlg.ShowDialog() == DialogResult.OK)
			{
				plateEdgeColor.BackColor = colorDlg.Color;
			}
		}

        private void groupBoxArea_Enter(object sender, EventArgs e)
        {

        }

        private void TriggerEdit_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void flagsBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
