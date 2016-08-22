/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 06.11.2014
 */
using System;
using MapEditor.MapInt;
using System.Windows.Forms;
using System.Collections.Generic;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of TeleportEdit.
	/// </summary>
	public partial class TeleportEdit : XferEditor
	{
		private readonly List<Map.Object> listTeleporters;
		
		public TeleportEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// добавляем телепортаторы в список
			Map.ObjectTable objects = MapInterface.TheMap.Objects;
			listTeleporters = new List<Map.Object>();
			ThingDb.Thing tt;
			foreach (Map.Object obj in objects)
			{
				tt = ThingDb.Things[obj.Name];
				if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.TRANSPORTER))
					listTeleporters.Add(obj);
			}
			
			checkIsLinked.Checked = false;
		}
		
		void CheckIsLinkedCheckedChanged(object sender, EventArgs e)
		{
			transpSelect.Enabled = checkIsLinked.Checked;
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			ThingDb.Thing tt = ThingDb.Things[obj.Name];
			
			TransporterXfer xfer = obj.GetExtraData<TransporterXfer>();
			transpSelect.Items.Clear();
			// из списка добавляем в gui
			string name;
			foreach (Map.Object e in listTeleporters)
			{
				name = e.ToString();
				if (e.Scr_Name.Length > 0)
					name = '"' + e.ScrNameShort + '"';
				
				transpSelect.Items.Add(name);
			}
			// с каким телепортером связан
			if (xfer.ExtentLink > 0)
			{
				checkIsLinked.Checked = true;
				int index = 0;
				foreach (Map.Object o in listTeleporters)
				{
					if (o.Extent == xfer.ExtentLink)
					{
						transpSelect.SelectedIndex = index;
						break;
					}
					index++;
				}
			}
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			TransporterXfer xfer = obj.GetExtraData<TransporterXfer>();
            xfer.ExtentLink = 0;
			// куда телепортер подключен
			if (checkIsLinked.Checked && transpSelect.SelectedIndex >= 0)
				xfer.ExtentLink = listTeleporters[transpSelect.SelectedIndex].Extent;
			

			DialogResult = DialogResult.OK;
			Close();
		}

        private void TeleportEdit_Load(object sender, EventArgs e)
        {

        }
	}
}
