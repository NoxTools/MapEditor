/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 06.10.2014
 */
namespace MapEditor.XferGui
{
	partial class NPCEdit
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.directionSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.wpFlagBox = new System.Windows.Forms.TextBox();
            this.statusFlagsList = new MapEditor.XferGui.StatusCheckList();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.healthMultiplier = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.aggressiveness = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.retreatHpC = new System.Windows.Forms.TextBox();
            this.resumeHpC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sightRange = new System.Windows.Forms.TextBox();
            this.defaultAction = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.spellsetRemoveSpell = new System.Windows.Forms.Button();
            this.spellsetAddSpell = new System.Windows.Forms.Button();
            this.spellPower = new System.Windows.Forms.NumericUpDown();
            this.aimAccuracy = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.customSpellSet = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.delayEscapeMax = new System.Windows.Forms.NumericUpDown();
            this.delayEscapeMin = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.delayOffensiveMax = new System.Windows.Forms.NumericUpDown();
            this.delayOffensiveMin = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.delayOffensive2Max = new System.Windows.Forms.NumericUpDown();
            this.delayOffensive2Min = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.delayBuffMax = new System.Windows.Forms.NumericUpDown();
            this.delayBuffMin = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.delayReactionMax = new System.Windows.Forms.NumericUpDown();
            this.delayReactionMin = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.npcStrength = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.immortal = new System.Windows.Forms.CheckBox();
            this.health = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.expReward = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.npcUnknown = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.poisonPower = new System.Windows.Forms.NumericUpDown();
            this.pathLockDist = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.npcGroup = new System.Windows.Forms.GroupBox();
            this.SoundSet = new System.Windows.Forms.ComboBox();
            this.npcColor6 = new System.Windows.Forms.Button();
            this.npcColor5 = new System.Windows.Forms.Button();
            this.npcColor4 = new System.Windows.Forms.Button();
            this.npcColor3 = new System.Windows.Forms.Button();
            this.npcColor2 = new System.Windows.Forms.Button();
            this.npcColor1 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.callScriptEditor = new System.Windows.Forms.Button();
            this.callEffectEditor = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonSaveChanges = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spellPower)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayEscapeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayEscapeMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensiveMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensiveMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensive2Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensive2Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayBuffMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayBuffMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayReactionMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayReactionMin)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcStrength)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.poisonPower)).BeginInit();
            this.npcGroup.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // directionSelector
            // 
            this.directionSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.directionSelector.FormattingEnabled = true;
            this.directionSelector.Location = new System.Drawing.Point(103, 10);
            this.directionSelector.Name = "directionSelector";
            this.directionSelector.Size = new System.Drawing.Size(112, 21);
            this.directionSelector.TabIndex = 0;
            this.directionSelector.SelectedIndexChanged += new System.EventHandler(this.directionSelector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Look direction";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Waypoint flag";
            // 
            // wpFlagBox
            // 
            this.wpFlagBox.Location = new System.Drawing.Point(113, 13);
            this.wpFlagBox.Name = "wpFlagBox";
            this.wpFlagBox.Size = new System.Drawing.Size(45, 20);
            this.wpFlagBox.TabIndex = 3;
            // 
            // statusFlagsList
            // 
            this.statusFlagsList.Location = new System.Drawing.Point(12, 62);
            this.statusFlagsList.Name = "statusFlagsList";
            this.statusFlagsList.Size = new System.Drawing.Size(172, 79);
            this.statusFlagsList.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Status flags";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Health multiplier";
            // 
            // healthMultiplier
            // 
            this.healthMultiplier.Location = new System.Drawing.Point(122, 65);
            this.healthMultiplier.Name = "healthMultiplier";
            this.healthMultiplier.Size = new System.Drawing.Size(81, 20);
            this.healthMultiplier.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Aggressiveness";
            // 
            // aggressiveness
            // 
            this.aggressiveness.Location = new System.Drawing.Point(122, 39);
            this.aggressiveness.Name = "aggressiveness";
            this.aggressiveness.Size = new System.Drawing.Size(81, 20);
            this.aggressiveness.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Retreat HP %";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 23);
            this.label7.TabIndex = 11;
            this.label7.Text = "Resume HP %";
            // 
            // retreatHpC
            // 
            this.retreatHpC.Location = new System.Drawing.Point(122, 129);
            this.retreatHpC.Name = "retreatHpC";
            this.retreatHpC.Size = new System.Drawing.Size(81, 20);
            this.retreatHpC.TabIndex = 12;
            // 
            // resumeHpC
            // 
            this.resumeHpC.Location = new System.Drawing.Point(122, 152);
            this.resumeHpC.Name = "resumeHpC";
            this.resumeHpC.Size = new System.Drawing.Size(81, 20);
            this.resumeHpC.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(12, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 23);
            this.label8.TabIndex = 15;
            this.label8.Text = "Sight range";
            // 
            // sightRange
            // 
            this.sightRange.Location = new System.Drawing.Point(103, 152);
            this.sightRange.Name = "sightRange";
            this.sightRange.Size = new System.Drawing.Size(81, 20);
            this.sightRange.TabIndex = 16;
            // 
            // defaultAction
            // 
            this.defaultAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultAction.FormattingEnabled = true;
            this.defaultAction.Location = new System.Drawing.Point(103, 178);
            this.defaultAction.Name = "defaultAction";
            this.defaultAction.Size = new System.Drawing.Size(112, 21);
            this.defaultAction.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 23);
            this.label9.TabIndex = 18;
            this.label9.Text = "Startup Action";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.spellsetRemoveSpell);
            this.groupBox1.Controls.Add(this.spellsetAddSpell);
            this.groupBox1.Controls.Add(this.spellPower);
            this.groupBox1.Controls.Add(this.aimAccuracy);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.customSpellSet);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(230, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 338);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spell casting";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // spellsetRemoveSpell
            // 
            this.spellsetRemoveSpell.Location = new System.Drawing.Point(153, 228);
            this.spellsetRemoveSpell.Name = "spellsetRemoveSpell";
            this.spellsetRemoveSpell.Size = new System.Drawing.Size(90, 23);
            this.spellsetRemoveSpell.TabIndex = 12;
            this.spellsetRemoveSpell.Text = "Remove Spell";
            this.spellsetRemoveSpell.UseVisualStyleBackColor = true;
            this.spellsetRemoveSpell.Click += new System.EventHandler(this.SpellsetRemoveSpellClick);
            // 
            // spellsetAddSpell
            // 
            this.spellsetAddSpell.Location = new System.Drawing.Point(153, 202);
            this.spellsetAddSpell.Name = "spellsetAddSpell";
            this.spellsetAddSpell.Size = new System.Drawing.Size(90, 23);
            this.spellsetAddSpell.TabIndex = 11;
            this.spellsetAddSpell.Text = "Add Spell";
            this.spellsetAddSpell.UseVisualStyleBackColor = true;
            this.spellsetAddSpell.Click += new System.EventHandler(this.SpellsetAddSpellClick);
            // 
            // spellPower
            // 
            this.spellPower.Location = new System.Drawing.Point(187, 281);
            this.spellPower.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spellPower.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spellPower.Name = "spellPower";
            this.spellPower.Size = new System.Drawing.Size(40, 20);
            this.spellPower.TabIndex = 10;
            this.spellPower.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // aimAccuracy
            // 
            this.aimAccuracy.Location = new System.Drawing.Point(164, 306);
            this.aimAccuracy.Name = "aimAccuracy";
            this.aimAccuracy.Size = new System.Drawing.Size(63, 20);
            this.aimAccuracy.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(12, 306);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(146, 23);
            this.label17.TabIndex = 8;
            this.label17.Text = "Aim accuracy (0.0 - 1.0)";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(12, 283);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(169, 23);
            this.label16.TabIndex = 6;
            this.label16.Text = "Spell Power (always 3 for Arena)";
            // 
            // customSpellSet
            // 
            this.customSpellSet.FormattingEnabled = true;
            this.customSpellSet.Location = new System.Drawing.Point(12, 204);
            this.customSpellSet.Name = "customSpellSet";
            this.customSpellSet.Size = new System.Drawing.Size(135, 69);
            this.customSpellSet.TabIndex = 3;
            this.customSpellSet.DoubleClick += new System.EventHandler(this.CustomSpellSetDoubleClick);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(47, 178);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 2;
            this.label14.Text = "Custom Spellset";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.delayEscapeMax);
            this.groupBox2.Controls.Add(this.delayEscapeMin);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.delayOffensiveMax);
            this.groupBox2.Controls.Add(this.delayOffensiveMin);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.delayOffensive2Max);
            this.groupBox2.Controls.Add(this.delayOffensive2Min);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.delayBuffMax);
            this.groupBox2.Controls.Add(this.delayBuffMin);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.delayReactionMax);
            this.groupBox2.Controls.Add(this.delayReactionMin);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(6, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 145);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Casting delays, min-max (frames)";
            // 
            // delayEscapeMax
            // 
            this.delayEscapeMax.Location = new System.Drawing.Point(172, 113);
            this.delayEscapeMax.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayEscapeMax.Name = "delayEscapeMax";
            this.delayEscapeMax.Size = new System.Drawing.Size(50, 20);
            this.delayEscapeMax.TabIndex = 14;
            // 
            // delayEscapeMin
            // 
            this.delayEscapeMin.Location = new System.Drawing.Point(112, 112);
            this.delayEscapeMin.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayEscapeMin.Name = "delayEscapeMin";
            this.delayEscapeMin.Size = new System.Drawing.Size(50, 20);
            this.delayEscapeMin.TabIndex = 13;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(6, 115);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(100, 23);
            this.label22.TabIndex = 12;
            this.label22.Text = "Escape spells:";
            // 
            // delayOffensiveMax
            // 
            this.delayOffensiveMax.Location = new System.Drawing.Point(172, 90);
            this.delayOffensiveMax.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayOffensiveMax.Name = "delayOffensiveMax";
            this.delayOffensiveMax.Size = new System.Drawing.Size(50, 20);
            this.delayOffensiveMax.TabIndex = 11;
            // 
            // delayOffensiveMin
            // 
            this.delayOffensiveMin.Location = new System.Drawing.Point(112, 89);
            this.delayOffensiveMin.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayOffensiveMin.Name = "delayOffensiveMin";
            this.delayOffensiveMin.Size = new System.Drawing.Size(50, 20);
            this.delayOffensiveMin.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 9;
            this.label13.Text = "Offensive spells:";
            // 
            // delayOffensive2Max
            // 
            this.delayOffensive2Max.Location = new System.Drawing.Point(172, 67);
            this.delayOffensive2Max.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayOffensive2Max.Name = "delayOffensive2Max";
            this.delayOffensive2Max.Size = new System.Drawing.Size(50, 20);
            this.delayOffensive2Max.TabIndex = 8;
            // 
            // delayOffensive2Min
            // 
            this.delayOffensive2Min.Location = new System.Drawing.Point(112, 66);
            this.delayOffensive2Min.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayOffensive2Min.Name = "delayOffensive2Min";
            this.delayOffensive2Min.Size = new System.Drawing.Size(50, 20);
            this.delayOffensive2Min.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 6;
            this.label12.Text = "Disabling spells:";
            // 
            // delayBuffMax
            // 
            this.delayBuffMax.Location = new System.Drawing.Point(172, 44);
            this.delayBuffMax.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayBuffMax.Name = "delayBuffMax";
            this.delayBuffMax.Size = new System.Drawing.Size(50, 20);
            this.delayBuffMax.TabIndex = 5;
            // 
            // delayBuffMin
            // 
            this.delayBuffMin.Location = new System.Drawing.Point(112, 43);
            this.delayBuffMin.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayBuffMin.Name = "delayBuffMin";
            this.delayBuffMin.Size = new System.Drawing.Size(50, 20);
            this.delayBuffMin.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 3;
            this.label11.Text = "Defensive spells:";
            // 
            // delayReactionMax
            // 
            this.delayReactionMax.Location = new System.Drawing.Point(172, 21);
            this.delayReactionMax.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayReactionMax.Name = "delayReactionMax";
            this.delayReactionMax.Size = new System.Drawing.Size(50, 20);
            this.delayReactionMax.TabIndex = 2;
            // 
            // delayReactionMin
            // 
            this.delayReactionMin.Location = new System.Drawing.Point(112, 20);
            this.delayReactionMin.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.delayReactionMin.Name = "delayReactionMin";
            this.delayReactionMin.Size = new System.Drawing.Size(50, 20);
            this.delayReactionMin.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 0;
            this.label10.Text = "Reaction spells:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.npcStrength);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.immortal);
            this.groupBox3.Controls.Add(this.health);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.aggressiveness);
            this.groupBox3.Controls.Add(this.healthMultiplier);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.retreatHpC);
            this.groupBox3.Controls.Add(this.resumeHpC);
            this.groupBox3.Location = new System.Drawing.Point(12, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 216);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Combat";
            // 
            // npcStrength
            // 
            this.npcStrength.Location = new System.Drawing.Point(122, 178);
            this.npcStrength.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.npcStrength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.npcStrength.Name = "npcStrength";
            this.npcStrength.Size = new System.Drawing.Size(81, 20);
            this.npcStrength.TabIndex = 24;
            this.npcStrength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(6, 180);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 23);
            this.label23.TabIndex = 23;
            this.label23.Text = "Strength";
            // 
            // immortal
            // 
            this.immortal.Location = new System.Drawing.Point(16, 94);
            this.immortal.Name = "immortal";
            this.immortal.Size = new System.Drawing.Size(75, 24);
            this.immortal.TabIndex = 6;
            this.immortal.Text = "Immortal";
            this.immortal.UseVisualStyleBackColor = true;
            this.immortal.CheckedChanged += new System.EventHandler(this.ImmortalCheckedChanged);
            // 
            // health
            // 
            this.health.Location = new System.Drawing.Point(122, 13);
            this.health.Name = "health";
            this.health.Size = new System.Drawing.Size(81, 20);
            this.health.TabIndex = 22;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 23);
            this.label18.TabIndex = 21;
            this.label18.Text = "Max Health";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.expReward);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.npcUnknown);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.poisonPower);
            this.groupBox4.Controls.Add(this.pathLockDist);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.wpFlagBox);
            this.groupBox4.Location = new System.Drawing.Point(485, 273);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(193, 151);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Misc";
            // 
            // expReward
            // 
            this.expReward.Location = new System.Drawing.Point(113, 116);
            this.expReward.Name = "expReward";
            this.expReward.Size = new System.Drawing.Size(68, 20);
            this.expReward.TabIndex = 12;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(6, 119);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(106, 23);
            this.label24.TabIndex = 11;
            this.label24.Text = "Experience reward";
            // 
            // npcUnknown
            // 
            this.npcUnknown.Location = new System.Drawing.Point(118, 90);
            this.npcUnknown.Name = "npcUnknown";
            this.npcUnknown.Size = new System.Drawing.Size(63, 20);
            this.npcUnknown.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 93);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 23);
            this.label15.TabIndex = 9;
            this.label15.Text = "Speed multiplier";
            // 
            // poisonPower
            // 
            this.poisonPower.Location = new System.Drawing.Point(113, 39);
            this.poisonPower.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.poisonPower.Name = "poisonPower";
            this.poisonPower.Size = new System.Drawing.Size(45, 20);
            this.poisonPower.TabIndex = 8;
            // 
            // pathLockDist
            // 
            this.pathLockDist.Location = new System.Drawing.Point(120, 64);
            this.pathLockDist.Name = "pathLockDist";
            this.pathLockDist.Size = new System.Drawing.Size(61, 20);
            this.pathLockDist.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(6, 67);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(106, 23);
            this.label20.TabIndex = 6;
            this.label20.Text = "Path lock distance?";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(6, 41);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 23);
            this.label19.TabIndex = 4;
            this.label19.Text = "Poison level";
            // 
            // npcGroup
            // 
            this.npcGroup.Controls.Add(this.SoundSet);
            this.npcGroup.Controls.Add(this.npcColor6);
            this.npcGroup.Controls.Add(this.npcColor5);
            this.npcGroup.Controls.Add(this.npcColor4);
            this.npcGroup.Controls.Add(this.npcColor3);
            this.npcGroup.Controls.Add(this.npcColor2);
            this.npcGroup.Controls.Add(this.npcColor1);
            this.npcGroup.Controls.Add(this.label21);
            this.npcGroup.Location = new System.Drawing.Point(485, 17);
            this.npcGroup.Name = "npcGroup";
            this.npcGroup.Size = new System.Drawing.Size(193, 155);
            this.npcGroup.TabIndex = 23;
            this.npcGroup.TabStop = false;
            this.npcGroup.Text = "NPC Visual";
            this.npcGroup.Enter += new System.EventHandler(this.npcGroup_Enter);
            // 
            // SoundSet
            // 
            this.SoundSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SoundSet.FormattingEnabled = true;
            this.SoundSet.Items.AddRange(new object[] {
            "Necromancer",
            "_FireKnight1",
            "_FireKnight2",
            "NPC",
            "_MaleNPC1",
            "_MaleNPC2",
            "Maiden",
            "_Wizard1",
            "_Wizard2"});
            this.SoundSet.Location = new System.Drawing.Point(66, 24);
            this.SoundSet.Name = "SoundSet";
            this.SoundSet.Size = new System.Drawing.Size(104, 21);
            this.SoundSet.TabIndex = 9;
            this.SoundSet.SelectedIndexChanged += new System.EventHandler(this.SoundSet_SelectedIndexChanged);
            // 
            // npcColor6
            // 
            this.npcColor6.Location = new System.Drawing.Point(95, 112);
            this.npcColor6.Name = "npcColor6";
            this.npcColor6.Size = new System.Drawing.Size(75, 23);
            this.npcColor6.TabIndex = 8;
            this.npcColor6.Text = "Skin 2";
            this.npcColor6.UseVisualStyleBackColor = true;
            // 
            // npcColor5
            // 
            this.npcColor5.Location = new System.Drawing.Point(14, 112);
            this.npcColor5.Name = "npcColor5";
            this.npcColor5.Size = new System.Drawing.Size(75, 23);
            this.npcColor5.TabIndex = 7;
            this.npcColor5.Text = "Skin";
            this.npcColor5.UseVisualStyleBackColor = true;
            // 
            // npcColor4
            // 
            this.npcColor4.Location = new System.Drawing.Point(95, 83);
            this.npcColor4.Name = "npcColor4";
            this.npcColor4.Size = new System.Drawing.Size(75, 23);
            this.npcColor4.TabIndex = 6;
            this.npcColor4.Text = "Beard";
            this.npcColor4.UseVisualStyleBackColor = true;
            // 
            // npcColor3
            // 
            this.npcColor3.Location = new System.Drawing.Point(14, 83);
            this.npcColor3.Name = "npcColor3";
            this.npcColor3.Size = new System.Drawing.Size(75, 23);
            this.npcColor3.TabIndex = 5;
            this.npcColor3.Text = "Side Burns";
            this.npcColor3.UseVisualStyleBackColor = true;
            // 
            // npcColor2
            // 
            this.npcColor2.Location = new System.Drawing.Point(95, 54);
            this.npcColor2.Name = "npcColor2";
            this.npcColor2.Size = new System.Drawing.Size(75, 23);
            this.npcColor2.TabIndex = 4;
            this.npcColor2.Text = "Hair ?";
            this.npcColor2.UseVisualStyleBackColor = true;
            // 
            // npcColor1
            // 
            this.npcColor1.Location = new System.Drawing.Point(14, 54);
            this.npcColor1.Name = "npcColor1";
            this.npcColor1.Size = new System.Drawing.Size(75, 23);
            this.npcColor1.TabIndex = 3;
            this.npcColor1.Text = "Hair";
            this.npcColor1.UseVisualStyleBackColor = true;
            this.npcColor1.Click += new System.EventHandler(this.npcColor1_Click);
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(6, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 23);
            this.label21.TabIndex = 0;
            this.label21.Text = "Voice Set";
            // 
            // callScriptEditor
            // 
            this.callScriptEditor.Location = new System.Drawing.Point(35, 19);
            this.callScriptEditor.Name = "callScriptEditor";
            this.callScriptEditor.Size = new System.Drawing.Size(125, 23);
            this.callScriptEditor.TabIndex = 24;
            this.callScriptEditor.Text = "Edit Script Handlers";
            this.callScriptEditor.UseVisualStyleBackColor = true;
            this.callScriptEditor.Click += new System.EventHandler(this.CallScriptEditorClick);
            // 
            // callEffectEditor
            // 
            this.callEffectEditor.Location = new System.Drawing.Point(35, 48);
            this.callEffectEditor.Name = "callEffectEditor";
            this.callEffectEditor.Size = new System.Drawing.Size(125, 23);
            this.callEffectEditor.TabIndex = 25;
            this.callEffectEditor.Text = "Edit Effects List";
            this.callEffectEditor.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.callScriptEditor);
            this.groupBox5.Controls.Add(this.callEffectEditor);
            this.groupBox5.Location = new System.Drawing.Point(485, 181);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(193, 86);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Other";
            // 
            // buttonSaveChanges
            // 
            this.buttonSaveChanges.Location = new System.Drawing.Point(251, 366);
            this.buttonSaveChanges.Name = "buttonSaveChanges";
            this.buttonSaveChanges.Size = new System.Drawing.Size(100, 23);
            this.buttonSaveChanges.TabIndex = 28;
            this.buttonSaveChanges.Text = "Save changes";
            this.buttonSaveChanges.UseVisualStyleBackColor = true;
            this.buttonSaveChanges.Click += new System.EventHandler(this.ButtonSaveChangesClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(361, 366);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 23);
            this.buttonCancel.TabIndex = 29;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // NPCEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 435);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSaveChanges);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.npcGroup);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.defaultAction);
            this.Controls.Add(this.sightRange);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusFlagsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.directionSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NPCEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NPC Property Editor";
            this.Load += new System.EventHandler(this.NPCEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spellPower)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.delayEscapeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayEscapeMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensiveMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensiveMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensive2Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayOffensive2Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayBuffMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayBuffMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayReactionMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayReactionMin)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npcStrength)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.poisonPower)).EndInit();
            this.npcGroup.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox expReward;
		private System.Windows.Forms.TextBox npcUnknown;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.NumericUpDown npcStrength;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button spellsetRemoveSpell;
		private System.Windows.Forms.Button spellsetAddSpell;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.NumericUpDown delayEscapeMin;
		private System.Windows.Forms.NumericUpDown delayEscapeMax;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSaveChanges;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button callEffectEditor;
		private System.Windows.Forms.Button callScriptEditor;
		private System.Windows.Forms.Button npcColor3;
		private System.Windows.Forms.Button npcColor4;
		private System.Windows.Forms.Button npcColor5;
		private System.Windows.Forms.Button npcColor6;
		private System.Windows.Forms.Button npcColor1;
		private System.Windows.Forms.Button npcColor2;
        private System.Windows.Forms.Label label21;
		private System.Windows.Forms.GroupBox npcGroup;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox pathLockDist;
		private System.Windows.Forms.CheckBox immortal;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.NumericUpDown poisonPower;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox health;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox aimAccuracy;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown spellPower;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ListBox customSpellSet;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown delayOffensiveMin;
		private System.Windows.Forms.NumericUpDown delayOffensiveMax;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown delayOffensive2Min;
		private System.Windows.Forms.NumericUpDown delayOffensive2Max;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown delayBuffMin;
		private System.Windows.Forms.NumericUpDown delayBuffMax;
		private System.Windows.Forms.NumericUpDown delayReactionMax;
		private System.Windows.Forms.NumericUpDown delayReactionMin;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox defaultAction;
		private System.Windows.Forms.TextBox sightRange;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox resumeHpC;
		private System.Windows.Forms.TextBox retreatHpC;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox aggressiveness;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox healthMultiplier;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private MapEditor.XferGui.StatusCheckList statusFlagsList;
		private System.Windows.Forms.TextBox wpFlagBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox directionSelector;
        private System.Windows.Forms.ComboBox SoundSet;
	}
}
