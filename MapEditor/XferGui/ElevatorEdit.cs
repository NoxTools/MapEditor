/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 27.10.2014
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MapEditor.MapInt;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of ElevatorEdit.
	/// </summary>
	public partial class ElevatorEdit : XferEditor
	{
		private readonly List<Map.Object> listElevators;
		private readonly List<Map.Object> listElevShafts;
		private bool isShaft;
		private ElevatorXfer xfer;
		
		public ElevatorEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			// добавляем подьемники в список
			Map.ObjectTable objects = MapInterface.TheMap.Objects;
			listElevators = new List<Map.Object>();
			listElevShafts = new List<Map.Object>();
			ThingDb.Thing tt;
			foreach (Map.Object obj in objects)
			{
				tt = ThingDb.Things[obj.Name];
				if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.ELEVATOR_SHAFT))
					listElevShafts.Add(obj);
				else if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.ELEVATOR))
					listElevators.Add(obj);
			}
			checkIsLinked.Checked = false;
			elevatorList.Enabled = false;
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			ThingDb.Thing tt = ThingDb.Things[obj.Name];
			// читаем Xfer
			xfer = obj.GetExtraData<ElevatorXfer>();
			List<Map.Object> objects = new List<Map.Object>();
			// убираем этот элеватор из списков
			listElevators.Remove(obj);
			listElevShafts.Remove(obj);
			// для шахт выводим список подьемников
			if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.ELEVATOR_SHAFT))
			{
				objects = listElevators;
				isShaft = true;
			}
			// для подьемников список шахт
			else if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.ELEVATOR))
			{
				objects = listElevShafts;
				isShaft = false;
			}
			
			elevatorList.Items.Clear();
			string name;
			foreach (Map.Object e in objects)
			{
				name = e.ToString();
				if (e.Scr_Name.Length > 0)
					name = '"' + e.ScrNameShort + '"';
				
				elevatorList.Items.Add(name);
			}
			// показываем куда подключен
			if (xfer.ExtentLink > 0)
			{
				checkIsLinked.Checked = true;
				int index = 0;
				foreach (Map.Object o in objects)
				{
					if (o.Extent == xfer.ExtentLink)
					{
						elevatorList.SelectedIndex = index;
						break;
					}
						
					index++;
				}
			}
		}
		
		void CheckIsLinkedCheckedChanged(object sender, EventArgs e)
		{
			elevatorList.Enabled = checkIsLinked.Checked;
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			xfer.ExtentLink = 0;
			if (checkIsLinked.Checked && elevatorList.SelectedIndex >= 0)
			{
				if (isShaft)
					xfer.ExtentLink = listElevators[elevatorList.SelectedIndex].Extent;
				else
					xfer.ExtentLink = listElevShafts[elevatorList.SelectedIndex].Extent;
			}
			
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
