/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 30.06.2015
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
	/// Data editor for FieldGuideXfer, AbilityRewardXfer, SpellRewardXfer
	/// </summary>
	public partial class KnowledgeRewardEdit : XferEditor
	{
		//private Dictionary<String, Action> typeFillers = new Dictionary<String, Action>();
		
		public KnowledgeRewardEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		// FieldGuideXfer
		private void FillMonsterIds()
		{
			foreach (ThingDb.Thing t in ThingDb.Things.Values)
			{
				if (t.HasClassFlag(ThingDb.Thing.ClassFlags.MONSTER)) 
					typeOfKnowledge.Items.Add(t.Name);
			}
		}
		
		// SpellRewardXfer
		private void FillSpellIds()
		{
			foreach (ThingDb.Spell s in ThingDb.Spells.Values)
				typeOfKnowledge.Items.Add(s.Name);
		}
		
		// AbilityRewardXfer
		private void FillAbilityIds()
		{
			foreach (ThingDb.Ability s in ThingDb.Abilities.Values)
				typeOfKnowledge.Items.Add(s.Name);
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			typeOfKnowledge.Items.Clear();
			
			switch (ThingDb.Things[obj.Name].Xfer)
			{
				case "FieldGuideXfer":
					FillMonsterIds();
					typeOfKnowledge.Text = obj.GetExtraData<FieldGuideXfer>().MonsterThingType;
					break;
				case "SpellRewardXfer":
					FillSpellIds();
					typeOfKnowledge.Text = obj.GetExtraData<SpellRewardXfer>().SpellName;
					break;
				case "AbilityRewardXfer":
					FillAbilityIds();
					typeOfKnowledge.Text = obj.GetExtraData<AbilityRewardXfer>().AbilityName;
					break;
			}
		}
		
		public override Map.Object GetObject()
		{
			switch (ThingDb.Things[obj.Name].Xfer)
			{
				case "FieldGuideXfer":
					obj.GetExtraData<FieldGuideXfer>().MonsterThingType = typeOfKnowledge.Text;
					break;
				case "SpellRewardXfer":
					obj.GetExtraData<SpellRewardXfer>().SpellName = typeOfKnowledge.Text;
					break;
				case "AbilityRewardXfer":
					obj.GetExtraData<AbilityRewardXfer>().AbilityName = typeOfKnowledge.Text;
					break;	
			}
			return obj;
		}
	}
}
