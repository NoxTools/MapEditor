/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 03.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of QuestRewardEdit.
	/// </summary>
	public partial class QuestRewardEdit : XferEditor
	{
		// т.к. мы некоторые свойства не выводим пользователю
		private RewardMarkerXfer xfer;
		private static uint[] flagsArray = { 1, 2, 4, 8, 0x10, 0x20, 0x40, 0x80 };
		
		public QuestRewardEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			xfer = obj.GetExtraData<RewardMarkerXfer>();
			spawnChance.SelectedIndex = xfer.ActivateChance;
			checkRare.Checked = xfer.RareOrSpecial;
			for (int i = 0; i < 8; i++)
			{
				rewardTypes.SetItemChecked(i, (((uint)xfer.RewardType & flagsArray[i]) == flagsArray[i]));
			}
		}
		
		public override Map.Object GetObject()
		{
			uint flags = 0;
			for (int i = 0; i < 8; i++)
			{
				if (rewardTypes.GetItemChecked(i))
					flags |= flagsArray[i];
			}
			xfer.RewardType = (RewardMarkerXfer.RewardFlags) flags;
			xfer.RareOrSpecial = checkRare.Checked;
			xfer.ActivateChance = spawnChance.SelectedIndex;
			return obj;
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
