/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 15.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of MoverEdit.
	/// </summary>
	public partial class MoverEdit : XferEditor
	{
		private static NumberFormatInfo floatFormat = NumberFormatInfo.InvariantInfo;
		
		public MoverEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			movingSpeed.Maximum = int.MaxValue;
			waypointID.Maximum = int.MaxValue;
			movedObjExtent.Maximum = int.MaxValue;
			loopWaypointA.Maximum = int.MaxValue;
			loopWaypointB.Maximum = int.MaxValue;
			moverStatusBox.SelectedIndex = 3;
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			MoverXfer xfer = obj.GetExtraData<MoverXfer>();
			movingSpeed.Value = xfer.MovingSpeed;
			waypointID.Value = xfer.WaypointID;
			movedObjExtent.Value = xfer.MovedObjExtent;
			moverStatusBox.SelectedIndex = xfer.MoveType;
			loopWaypointA.Value = xfer.WaypointStartID;
			loopWaypointB.Value = xfer.WaypointEndID;
			moverAccel.Text = xfer.MoverAcceleration.ToString(floatFormat);
			moverSpeed.Text = xfer.MoverSpeed.ToString(floatFormat);
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
            
			MoverXfer xfer = obj.GetExtraData<MoverXfer>();
          
			xfer.MovingSpeed = (int) movingSpeed.Value;
			xfer.WaypointID = (int) waypointID.Value;
			xfer.MovedObjExtent = (int) movedObjExtent.Value;
			xfer.MoveType = (byte) moverStatusBox.SelectedIndex;
			xfer.WaypointStartID = (int) loopWaypointA.Value;
			xfer.WaypointEndID = (int) loopWaypointB.Value;
			xfer.MoverAcceleration = float.Parse(moverAccel.Text, floatFormat);
			xfer.MoverSpeed = float.Parse(moverSpeed.Text, floatFormat);
            
			Close();
		}

        private void MoverEdit_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
	}
}
