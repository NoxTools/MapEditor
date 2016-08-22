/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 28.10.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of ReadableEdit.
	/// </summary>
	public partial class ReadableEdit : XferEditor
	{
		private StringDb fileCsf;
		
		public ReadableEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			fileCsf = StringDb.Current;
		}
		
		void ReadableTextTextChanged(object sender, EventArgs e)
		{
			string text = readableText.Text;
			labelPreview.Text = "";
			if (text.Contains(":"))
			{
				string preview = fileCsf.GetEntryFirstVal(text);
				if (preview != null)
					labelPreview.Text = '"' + preview + '"';
			}
		}
		
		public override void SetObject(Map.Object obj)
		{
			base.SetObject(obj);
			ReadableXfer xfer = obj.GetExtraData<ReadableXfer>();
			readableText.Text = xfer.Text.Trim('\0');
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			ReadableXfer xfer = obj.GetExtraData<ReadableXfer>();
			xfer.Text = readableText.Text;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}