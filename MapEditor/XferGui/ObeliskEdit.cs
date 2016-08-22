/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 12.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of ObeliskEdit.
	/// </summary>
	public partial class ObeliskEdit : XferEditor
	{
		public ObeliskEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			manaStored.Value = 50;
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			obj.GetExtraData<ObeliskXfer>().ManaPool = (int) manaStored.Value;
           // obj.GetExtraData<ObeliskXfer>().Unused = (byte)numericUpDown1.Value;
		}
		
		public override void SetObject(NoxShared.Map.Object obj)
		{
			this.obj = obj;
			manaStored.Value = obj.GetExtraData<ObeliskXfer>().ManaPool;
            //numericUpDown1.Value = obj.GetExtraData<ObeliskXfer>().Unused;
		}

        private void ObeliskEdit_Load(object sender, EventArgs e)
        {

        }
	}
}
