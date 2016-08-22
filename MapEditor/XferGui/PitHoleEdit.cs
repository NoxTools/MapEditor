/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 05.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of PitHoleEdit.
	/// </summary>
	public partial class PitHoleEdit : XferEditor
	{
		public PitHoleEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			HoleXfer xfer = obj.GetExtraData<HoleXfer>();
			exitX.Text = xfer.FallX.ToString();
			exitY.Text = xfer.FallY.ToString();
			scriptFn.Text = xfer.UnknownScriptHandler;
			scriptTime.Text = xfer.ScriptTimeout.ToString();
            
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			HoleXfer xfer = obj.GetExtraData<HoleXfer>();
			xfer.FallX = int.Parse(exitX.Text);
			xfer.FallY = int.Parse(exitY.Text);
			xfer.UnknownScriptHandler = scriptFn.Text;
			xfer.ScriptTimeout = short.Parse(scriptTime.Text);
			DialogResult = DialogResult.OK;
			Close();
		}
		
		void ButtonCenterClick(object sender, EventArgs e)
		{
			Point point = new Point(int.Parse(exitX.Text), int.Parse(exitY.Text));
			MainWindow.Instance.mapView.CenterAtPoint(point);
		}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void PitHoleEdit_Load(object sender, EventArgs e)
        {
            Point CopyPoint = MainWindow.Instance.mapView.copyPoint;

            if (CopyPoint.IsEmpty)
                pasteButton.Enabled = false;
            else
                pasteButton.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point CopyPoint = MainWindow.Instance.mapView.copyPoint;
            exitX.Text = CopyPoint.X.ToString();
            exitY.Text = CopyPoint.Y.ToString();
        }
	}
}
