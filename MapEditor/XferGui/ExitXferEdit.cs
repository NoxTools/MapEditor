/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 19.02.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of ExitXferEdit.
	/// </summary>
	public partial class ExitXferEdit : XferEditor
	{
		static NumberFormatInfo floatFormatInfo = NumberFormatInfo.InvariantInfo;
		
		public ExitXferEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		public override void SetObject(NoxShared.Map.Object obj)
		{
			base.SetObject(obj);
			ExitXfer xfer = obj.GetExtraData<ExitXfer>();
			textBoxMapName.Text = xfer.MapName;
			textBoxSpawnX.Text = xfer.ExitX.ToString(floatFormatInfo);
			textBoxSpawnY.Text = xfer.ExitY.ToString(floatFormatInfo);
		}
		
		public override NoxShared.Map.Object GetObject()
		{
			ExitXfer xfer = obj.GetExtraData<ExitXfer>();
			xfer.MapName = textBoxMapName.Text;
			xfer.ExitX = float.Parse(textBoxSpawnX.Text, floatFormatInfo);
			xfer.ExitY = float.Parse(textBoxSpawnY.Text, floatFormatInfo);
			
			return base.GetObject();
		}
		
		void ButtonDoneClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

        private void textBoxSpawnX_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
