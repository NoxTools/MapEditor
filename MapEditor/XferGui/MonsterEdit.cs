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
	/// Description of MonsterEdit.
	/// </summary>
	public partial class MonsterEdit : XferEditor
	{
		private MonsterXfer Xfer;
		private NumberFormatInfo numberFormat = NumberFormatInfo.InvariantInfo;
		private Button[] maidenColorSelectors;
		private bool monsterAlwaysPeaceful; // Maiden/Shopkeeper
		
		// TODO list:
		// Сделать редактор магазинов
		// Сделать редактор энчантов
		// Импортировать дефолтные настройки монстров из monster.bin
		public MonsterEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			statusFlagsList.Items.AddRange(Enum.GetNames(typeof(NoxEnums.MonsterStatus)));
			directionSelector.Items.AddRange(MonsterXfer.NOX_DIRECT_NAMES);
			defaultAction.Items.AddRange(NoxEnums.AIActionStrings);
			maidenColorSelectors = new Button[] { maidenColor1, maidenColor2, maidenColor3, maidenColor4, maidenColor5, maidenColor6 };
			foreach (Button b in maidenColorSelectors) b.Click += new EventHandler(MaidenColorSelectorClick);
			
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			try
			{
				Xfer = obj.GetExtraData<MonsterXfer>();
				Text += string.Format(" - editing {0}", obj);
			}
			catch (Exception ex)
			{
				string msg = string.Format("Failed to parse XFer data. {0}", ex.Message);
				MessageBox.Show(msg, "MonsterXFer", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//this.DialogResult = DialogResult.Cancel;
				//this.Close();
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
			customSpellSet.Items.Clear();
			// AddRange fails due to casting problem... weird
			if (Xfer.KnownSpells != null)
			{
				foreach (MonsterXfer.SpellEntry se in Xfer.KnownSpells) customSpellSet.Items.Add(se);
			}
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
			if (Xfer.SpellPowerLevel < 1 || Xfer.SpellPowerLevel > 5) Xfer.SpellPowerLevel = 3;
			spellPower.Value = Xfer.SpellPowerLevel;
			aimAccuracy.Text = Xfer.AimSkillLevel.ToString(numberFormat);
            immortal.Checked = Xfer.Immortal;
            defaultSF.Checked = Xfer.SetDefaultMonsterStatus;
			health.Text = Xfer.Health.ToString();
			defaultResumeR.Checked = Xfer.SetDefaultResumeRatio;
			defaultRetreatR.Checked = Xfer.SetDefaultRetreatRatio;
			learnDefaultSpells.Checked = Xfer.LearnDefaultSpells;
			poisonPower.Value = Xfer.PoisonLevel;
			if (Xfer.MaidenBodyColors != null)
			{
				for (int i = 0; i < 6; i++)
					maidenColorSelectors[i].BackColor = Xfer.MaidenBodyColors[i];
			}
			
			ThingDb.Thing tt = ThingDb.Things[obj.Name];
			
			bool isBomber = tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.BOMBER];
			callBomberEditor.Enabled = isBomber;
			
			bool isShopkeeper = tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.SHOPKEEPER];
			callShopEditor.Enabled = isShopkeeper;
			monsterAlwaysPeaceful = isShopkeeper;
			
			bool checkIsNPC = tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.WOUNDED_NPC] || tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.FEMALE_NPC];
			if (!checkIsNPC)
				npcGroup.Enabled = false;
			else
			{

                string xferSound = "";

				monsterAlwaysPeaceful = true;
                if (Xfer.MaidenVoiceSet != null)
                {
                    npcSoundSet.Text = Xfer.MaidenVoiceSet;
                    xferSound = Xfer.MaidenVoiceSet.ToUpper();
                }
                else if (Xfer.WoundedNPCVoiceSet != null)
                {
                    npcSoundSet.Text = Xfer.WoundedNPCVoiceSet;
                    xferSound = Xfer.WoundedNPCVoiceSet.ToUpper();
                }
               
                foreach (string item in SoundSet.Items)
                {
                    string itemSound = item.ToUpper();

                    if (itemSound.Equals(xferSound))
                    {
                        SoundSet.SelectedItem = item;
                        break;
                    }
                }
            
            
            
            
            
            }
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
			Xfer.Health = short.Parse(health.Text);
			Xfer.SetDefaultResumeRatio = defaultResumeR.Checked;
			Xfer.SetDefaultRetreatRatio = defaultRetreatR.Checked;
            Xfer.SetDefaultMonsterStatus = defaultSF.Checked;
			Xfer.LearnDefaultSpells = learnDefaultSpells.Checked;
			Xfer.PoisonLevel = (byte) poisonPower.Value;
			if (Xfer.MaidenBodyColors != null)
			{
				for (int i = 0; i < 6; i++)
					Xfer.MaidenBodyColors[i] = maidenColorSelectors[i].BackColor;
			}
			ThingDb.Thing tt = ThingDb.Things[obj.Name];
			if (tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.FEMALE_NPC])
				Xfer.MaidenVoiceSet = npcSoundSet.Text;
			if (tt.Subclass[(int) ThingDb.Thing.SubclassBitIndex.WOUNDED_NPC])
				Xfer.WoundedNPCVoiceSet = npcSoundSet.Text;
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
		
		private void DefaultRetreatRCheckedChanged(object sender, EventArgs e)
		{
			retreatHpC.Enabled = !defaultRetreatR.Checked;
		}
		
		private void DefaultResumeRCheckedChanged(object sender, EventArgs e)
		{
			resumeHpC.Enabled = !defaultResumeR.Checked;
		}
		
		private void ImmortalCheckedChanged(object sender, EventArgs e)
		{
			health.Enabled = !immortal.Checked;
			healthMultiplier.Enabled = !immortal.Checked;
		}
		
		private void DefaultSFCheckedChanged(object sender, EventArgs e)
		{
			statusFlagsList.Enabled = !defaultSF.Checked;
		}
		
		private void MaidenColorSelectorClick(object sender, EventArgs e)
		{
			int index = Array.IndexOf(maidenColorSelectors, sender);
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
		
		void CallShopEditorClick(object sender, EventArgs e)
		{
			ShopEditForm form = new ShopEditForm();
			form.SetShopInfo(Xfer.ShopkeeperInfo);
			if (form.ShowDialog() == DialogResult.OK)
			{
				Xfer.ShopkeeperInfo = form.GetShopInfo();
			}
		}
		
		void ButtonDefsClick(object sender, EventArgs e)
		{
			SetDefaultData(obj);
			InputFormData();
		}
		
		public override void SetDefaultData(Map.Object obj)
		{
			base.SetDefaultData(obj);
			Xfer = obj.GetExtraData<MonsterXfer>();
			Xfer.InitForMonsterName(obj.Name);
		}
		
		void CallBomberEditorClick(object sender, EventArgs e)
		{
			BomberSpells bombGui = new BomberSpells(Xfer);
			bombGui.ShowDialog();
		}

        private void npcGroup_Enter(object sender, EventArgs e)
        {

        }
	}
}
