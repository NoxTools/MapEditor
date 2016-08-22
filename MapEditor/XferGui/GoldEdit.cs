/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 11.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of GoldEdit.
	/// </summary>
	public partial class GoldEdit : XferEditor
	{
		public GoldEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			goldAmount.Maximum = int.MaxValue;
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			obj.GetExtraData<GoldXfer>().Amount = (int) goldAmount.Value;
			Close();
		}
		
		public override void SetObject(NoxShared.Map.Object obj)
		{
			this.obj = obj;
			GoldXfer gold = obj.GetExtraData<GoldXfer>();
			if (gold.Amount < 0) gold.Amount = 0;
			goldAmount.Value = gold.Amount;
		}

        private void GoldEdit_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
	}
}
