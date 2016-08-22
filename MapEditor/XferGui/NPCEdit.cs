/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 06.10.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Визуальный редактор для NPCXfer
	/// </summary>
	public partial class NPCEdit : XferEditor
	{
		private NPCXfer Xfer;
		private NumberFormatInfo numberFormat = NumberFormatInfo.InvariantInfo;
		private Button[] npcColorSelectors;
		
		public NPCEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			statusFlagsList.Items.AddRange(Enum.GetNames(typeof(NoxEnums.MonsterStatus)));
			directionSelector.Items.AddRange(MonsterXfer.NOX_DIRECT_NAMES);
			defaultAction.Items.AddRange(NoxEnums.AIActionStrings);
			
			npcColorSelectors = new Button[] { npcColor1, npcColor2, npcColor3, npcColor4, npcColor5, npcColor6 };
			foreach (Button b in npcColorSelectors) b.Click += new EventHandler(ColorSelectorClick);
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			try
			{
				Xfer = obj.GetExtraData<NPCXfer>();
				Text += string.Format(" - editing {0}", obj);
			}
			catch (Exception ex)
			{
				string msg = string.Format("Failed to parse XFer data. {0}", ex.Message);
				MessageBox.Show(msg, "NPCXFer", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
			InputFormData();
		}
		
		private void InputFormData()
		{
			string dirName = MonsterXfer.NOX_DIRECT_NAMES[Xfer.DirectionId];
			foreach (string item in directionSelector.Items)
			{
				if (item.Equals(dirName))
					directionSelector.SelectedItem = item;
			}
			wpFlagBox.Text = Xfer.ActionRoamPathFlag.ToString("X2");
			healthMultiplier.Text = Xfer.HealthMultiplier.ToString(numberFormat);
			statusFlagsList.SetCheckedFlags(Xfer.StatusFlags);
			retreatHpC.Text = Xfer.RetreatRatio.ToString(numberFormat);
			resumeHpC.Text = Xfer.ResumeRatio.ToString(numberFormat);
			sightRange.Text = Xfer.SightRange.ToString(numberFormat);
			aggressiveness.Text = Xfer.Aggressiveness.ToString(numberFormat);
			defaultAction.SelectedIndex = Xfer.DefaultAction;
			// AddRange fails due to casting problem... weird
			foreach (MonsterXfer.SpellEntry se in Xfer.KnownSpells)
				customSpellSet.Items.Add(se);
			delayReactionMin.Value = Xfer.ReactionCastingDelayMin;
			delayReactionMax.Value = Xfer.ReactionCastingDelayMax;
			delayBuffMin.Value = Xfer.BuffCastingDelayMin;
			delayBuffMax.Value = Xfer.BuffCastingDelayMax;
			delayOffensiveMin.Value = Xfer.OffensiveCastingDelayMin;
			delayOffensiveMax.Value = Xfer.OffensiveCastingDelayMax;
			delayOffensive2Min.Value = Xfer.DebuffCastingDelayMin;
			delayOffensive2Max.Value = Xfer.DebuffCastingDelayMax;
			delayEscapeMin.Value = Xfer.BlinkCastingDelayMin;
			delayEscapeMax.Value = Xfer.BlinkCastingDelayMax;
			pathLockDist.Text = Xfer.LockPathDistance.ToString(numberFormat);
			spellPower.Value = Xfer.SpellPowerLevel;
			aimAccuracy.Text = Xfer.AimSkillLevel.ToString(numberFormat);
            immortal.Checked = Xfer.Immortal;
			health.Text = Xfer.MaxHealth.ToString();
			poisonPower.Value = Xfer.PoisonLevel;
			npcUnknown.Text = Xfer.NPCSpeed.ToString(numberFormat);
			npcStrength.Value = Xfer.NPCStrength;
			//npcSoundSet.Text = Xfer.NPCVoiceSet;

            string xferSound = Xfer.NPCVoiceSet.ToUpper();
            foreach (string item in SoundSet.Items)
            {
                string itemSound = item.ToUpper();

                if (itemSound.Equals(xferSound))
                {
                    SoundSet.SelectedItem = item;
                    break;
                }
            }
			expReward.Text = Xfer.Experience.ToString(numberFormat);
			for (int i = 0; i < 6; i++)
				npcColorSelectors[i].BackColor = Xfer.NPCColors[i];
		}
		
		private void OutputFormData()
		{
			Xfer.DirectionId = (byte) Array.IndexOf(MonsterXfer.NOX_DIRECT_NAMES, (string) directionSelector.SelectedItem);
			Xfer.ActionRoamPathFlag = byte.Parse(wpFlagBox.Text, NumberStyles.HexNumber);
			Xfer.HealthMultiplier = float.Parse(healthMultiplier.Text, numberFormat);
			Xfer.StatusFlags = statusFlagsList.GetCheckedFlags();
			Xfer.RetreatRatio = float.Parse(retreatHpC.Text, numberFormat);
			Xfer.ResumeRatio = float.Parse(resumeHpC.Text, numberFormat);
			Xfer.SightRange = float.Parse(sightRange.Text, numberFormat);
			Xfer.Aggressiveness = float.Parse(aggressiveness.Text, numberFormat);
			if (defaultAction.SelectedIndex >= 0)
				Xfer.DefaultAction = defaultAction.SelectedIndex;
			Xfer.KnownSpells.Clear();
			foreach (MonsterXfer.SpellEntry se in customSpellSet.Items)
				Xfer.KnownSpells.Add(se);
			Xfer.ReactionCastingDelayMin = (ushort) delayReactionMin.Value;
			Xfer.ReactionCastingDelayMax = (ushort) delayReactionMax.Value;
			Xfer.BuffCastingDelayMin = (ushort) delayBuffMin.Value;
			Xfer.BuffCastingDelayMax = (ushort) delayBuffMax.Value;
			Xfer.OffensiveCastingDelayMin = (ushort) delayOffensiveMin.Value;
			Xfer.OffensiveCastingDelayMax = (ushort) delayOffensiveMax.Value;
			Xfer.DebuffCastingDelayMin = (ushort) delayOffensive2Min.Value;
			Xfer.DebuffCastingDelayMax = (ushort) delayOffensive2Max.Value;
			Xfer.BlinkCastingDelayMin = (ushort) delayEscapeMin.Value;
			Xfer.BlinkCastingDelayMax = (ushort) delayEscapeMax.Value;
			Xfer.LockPathDistance = float.Parse(pathLockDist.Text, numberFormat);
			Xfer.SpellPowerLevel = (int) spellPower.Value;
			Xfer.AimSkillLevel = float.Parse(aimAccuracy.Text, numberFormat);
			Xfer.Immortal = immortal.Checked;
			Xfer.MaxHealth = short.Parse(health.Text);
			Xfer.Health = Xfer.MaxHealth;
			Xfer.PoisonLevel = (byte) poisonPower.Value;
			Xfer.NPCSpeed = float.Parse(npcUnknown.Text, numberFormat);
			Xfer.NPCStrength = (byte) npcStrength.Value;
            Xfer.NPCVoiceSet = SoundSet.SelectedItem.ToString();
			Xfer.Experience = float.Parse(expReward.Text, numberFormat);
			for (int i = 0; i < 6; i++)
				Xfer.NPCColors[i] = npcColorSelectors[i].BackColor;
		}
		
		private void ButtonSaveChangesClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			OutputFormData();
			Close();
		}
		
		private void ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
		
		private void SpellsetRemoveSpellClick(object sender, EventArgs e)
		{
			object selected = customSpellSet.SelectedItem;
			
			if (selected != null)
				customSpellSet.Items.Remove(selected);
		}
		
		private void ImmortalCheckedChanged(object sender, EventArgs e)
		{
			health.Enabled = !immortal.Checked;
			healthMultiplier.Enabled = !immortal.Checked;
		}
		
		private void ColorSelectorClick(object sender, EventArgs e)
		{
			int index = Array.IndexOf(npcColorSelectors, sender);
			if (index >= 0)
			{
				ColorDialog dialog = new ColorDialog();
				dialog.Color = ((Button) sender).BackColor;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					((Button) sender).BackColor = dialog.Color;
				}
			}
		}
		
		void CustomSpellSetDoubleClick(object sender, EventArgs e)
		{
			int index = customSpellSet.SelectedIndex;
			if (index >= 0)
			{
				MonsterSpellForm form = new MonsterSpellForm();
				form.SetSpell(Xfer.KnownSpells[index]);
				if (form.ShowDialog() == DialogResult.OK)
				{
					Xfer.KnownSpells[index] = form.GetSpell();
					customSpellSet.Items[index] = Xfer.KnownSpells[index];
				}
			}
		}
		
		void SpellsetAddSpellClick(object sender, EventArgs e)
		{
			MonsterSpellForm form = new MonsterSpellForm();
			if (form.ShowDialog() == DialogResult.OK)
			{
				MonsterXfer.SpellEntry entry = form.GetSpell();
				customSpellSet.Items.Add(entry);
				Xfer.KnownSpells.Add(entry);
			}
		}
		
		void CallScriptEditorClick(object sender, EventArgs e)
		{
			MonsterScriptForm form = new MonsterScriptForm();
			form.SetScriptStrings(Xfer.ScriptEvents);
			if (form.ShowDialog() == DialogResult.OK)
			{
				Xfer.ScriptEvents = form.GetScriptStrings();
			}
		}
		
		public override void SetDefaultData(Map.Object obj)
		{
			base.SetDefaultData(obj);
			Xfer = obj.GetExtraData<NPCXfer>();
			Xfer.DirectionId = 0;
			Xfer.ScriptEvents = new string[10];
			for (int i = 0; i < 10; i++) Xfer.ScriptEvents[i] = "";
			Xfer.DetectEventTimeout = 1;
			Xfer.ActionRoamPathFlag = 0xFF;
			Xfer.StatusFlags = (NoxEnums.MonsterStatus) 0;
			Xfer.HealthMultiplier = 1F;
			Xfer.RetreatRatio = 0.05F;
			Xfer.ResumeRatio = 0.5F;
			Xfer.SightRange = 150F;
			Xfer.Aggressiveness = 0.5F;
			Xfer.DefaultAction = 0;
			Xfer.EscortObjName = "";
			Xfer.KnownSpells = new System.Collections.Generic.List<MonsterXfer.SpellEntry>();
			Xfer.ReactionCastingDelayMin = 15;
			Xfer.ReactionCastingDelayMax = 30;
			Xfer.BuffCastingDelayMin = 90;
			Xfer.BuffCastingDelayMax = 120;
			Xfer.DebuffCastingDelayMin = 30;
			Xfer.DebuffCastingDelayMax = 40;
			Xfer.OffensiveCastingDelayMin = 30;
			Xfer.OffensiveCastingDelayMax = 40;
			Xfer.BlinkCastingDelayMin = 90;
			Xfer.BlinkCastingDelayMax = 120;
			Xfer.LockPathDistance = 30F;
			Xfer.SpellPowerLevel = 3;
			Xfer.AimSkillLevel = 0.5F;
			Xfer.Immortal = false;
			Xfer.TrapSpell1 = "SPELL_INVALID";
			Xfer.TrapSpell2 = "SPELL_INVALID";
			Xfer.TrapSpell3 = "SPELL_INVALID";
			Xfer.MagicNumber = 0xDEADFACE;
			Xfer.AddedSubclass = 0;
			Xfer.Health = 100;
			Xfer.MaxHealth = 100;
			Xfer.NPCStrength = 30;
			Xfer.NPCColors = new Color[6];
			for (int i = 0; i < 6; i++) Xfer.NPCColors[i] = Color.FromArgb(0xD2, 0xAE, 0x79);
			Xfer.NPCSpeed = 0.5F;
			Xfer.NPCVoiceSet = "NPC";
			Xfer.BuffList = new MonsterXfer.BuffEntry[0];
			Xfer.PoisonLevel = 0;
		}

        private void directionSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void npcColor1_Click(object sender, EventArgs e)
        {

        }

        private void npcSoundSet_TextChanged(object sender, EventArgs e)
        {

        }

        private void NPCEdit_Load(object sender, EventArgs e)
        {

        }

        private void npcGroup_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SoundSet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
