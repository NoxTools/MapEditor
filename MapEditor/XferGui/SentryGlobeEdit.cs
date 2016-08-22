/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 14.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of SentryGlobeEdit.
	/// </summary>
	public partial class SentryGlobeEdit : XferEditor
	{
		private static NumberFormatInfo floatFormat = NumberFormatInfo.InvariantInfo;
		
		public SentryGlobeEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public override void SetObject(NoxShared.Map.Object obj)
		{
			this.obj = obj;
			SentryXfer xfer = obj.GetExtraData<SentryXfer>();
			sentryAngle.Text = xfer.BasePosRadian.ToString(floatFormat);
			sentrySpeed.Text = xfer.RotateSpeed.ToString(floatFormat);
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
            SentryXfer xfer = obj.GetExtraData<SentryXfer>();
			xfer.BasePosRadian = float.Parse(sentryAngle.Text, floatFormat);
			xfer.RotateSpeed = float.Parse(sentrySpeed.Text, floatFormat);
			Close();
		}

        private void SentryGlobeEdit_Load(object sender, EventArgs e)
        {

        }
	}
}
