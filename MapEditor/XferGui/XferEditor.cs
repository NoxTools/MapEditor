/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 23.10.2014
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NoxShared;

namespace MapEditor.XferGui
{
	/// <summary>
	/// класс представляющий визуальный редактор дополнительных свойств обьекта на карте.
	/// </summary>
	public partial class XferEditor : Form
	{
		protected Map.Object obj;
		
		public virtual void SetObject(Map.Object obj) { this.obj = obj; }
		
		public virtual Map.Object GetObject() { return obj; }
		
		public virtual void SetDefaultData(Map.Object obj) { obj.NewDefaultExtraData(); }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XferEditor
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "XferEditor";
            this.Load += new System.EventHandler(this.XferEditor_Load);
            this.ResumeLayout(false);

        }

        private void XferEditor_Load(object sender, EventArgs e)
        {

        }
	}
	
	public static class XferEditors
	{
		private static Dictionary<string, Type> Editors;
		
		public static XferEditor GetEditorForXfer(string xferName)
		{
			if (xferName != null)
			{
				if (Editors.ContainsKey(xferName))
				{
					return (XferEditor) Activator.CreateInstance(Editors[xferName]);
				}
			}
			return null;
		}
		
		static XferEditors()
		{
			Editors = new Dictionary<string, Type>();
			/* TODO: Implement editors for commented out Xfer types */
			// SpellPagePedestalXfer
			Editors.Add("SpellRewardXfer", typeof(KnowledgeRewardEdit));
			Editors.Add("AbilityRewardXfer", typeof(KnowledgeRewardEdit));
			Editors.Add("FieldGuideXfer", typeof(KnowledgeRewardEdit));
			Editors.Add("ReadableXfer", typeof(ReadableEdit));
			Editors.Add("ExitXfer", typeof(ExitXferEdit));
			Editors.Add("DoorXfer", typeof(DoorProperties));
			Editors.Add("TriggerXfer", typeof(TriggerEdit));
			Editors.Add("MonsterXfer", typeof(MonsterEdit));
			Editors.Add("HoleXfer", typeof(PitHoleEdit));
			Editors.Add("TransporterXfer", typeof(TeleportEdit));
			Editors.Add("ElevatorXfer", typeof(ElevatorEdit));
			Editors.Add("ElevatorShaftXfer", typeof(ElevatorEdit));
			Editors.Add("MoverXfer", typeof(MoverEdit));
			Editors.Add("GlyphXfer", typeof(GlyphEdit));
			Editors.Add("InvisibleLightXfer", typeof(ColorLightEdit));
			Editors.Add("SentryXfer", typeof(SentryGlobeEdit));
			Editors.Add("WeaponXfer", typeof(EquipmentEdit));
			Editors.Add("ArmorXfer", typeof(EquipmentEdit));
			Editors.Add("TeamXfer", typeof(EquipmentEdit));
			Editors.Add("GoldXfer", typeof(GoldEdit));
			Editors.Add("AmmoXfer", typeof(EquipmentEdit));
			Editors.Add("NPCXfer", typeof(NPCEdit));
			Editors.Add("ObeliskXfer", typeof(ObeliskEdit));
			// ToxicCloudXfer
			Editors.Add("MonsterGeneratorXfer", typeof(MonsterGenEdit));
			Editors.Add("RewardMarkerXfer", typeof(QuestRewardEdit));
		}
	}
}
