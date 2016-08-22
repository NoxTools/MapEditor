/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 15.01.2015
 */
namespace MapEditor.newgui
{
    partial class WallMakeTab
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the control.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WallMakeTab));
            this.label1 = new System.Windows.Forms.Label();
            this.comboWallSet = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numMapGroup = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numWallVari = new System.Windows.Forms.NumericUpDown();
            this.numWallVariMax = new System.Windows.Forms.NumericUpDown();
            this.autovari = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSep1 = new System.Windows.Forms.Label();
            this.labelSep2 = new System.Windows.Forms.Label();
            this.AutoWalltBtn = new System.Windows.Forms.RadioButton();
            this.PlaceWalltBtn = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBlackWalls = new System.Windows.Forms.CheckBox();
            this.Picker = new System.Windows.Forms.CheckBox();
            this.RecLinePanel = new System.Windows.Forms.Panel();
            this.smartDraw = new System.Windows.Forms.CheckBox();
            this.RecWall = new System.Windows.Forms.CheckBox();
            this.LineWall = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.WallProp = new System.Windows.Forms.Panel();
            this.ok = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openWallBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkListFlags = new System.Windows.Forms.CheckedListBox();
            this.numericCloseDelay = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkDestructable = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.polygonGroup = new System.Windows.Forms.NumericUpDown();
            this.wallBtn1 = new System.Windows.Forms.Button();
            this.wallBtn4 = new System.Windows.Forms.Button();
            this.wallBtn2 = new System.Windows.Forms.Button();
            this.wallBtn5 = new System.Windows.Forms.Button();
            this.wallBtn9 = new System.Windows.Forms.Button();
            this.wallBtn3 = new System.Windows.Forms.Button();
            this.wallBtn6 = new System.Windows.Forms.Button();
            this.wallBtn10 = new System.Windows.Forms.Button();
            this.wallBtn12 = new System.Windows.Forms.Button();
            this.wallBtn7 = new System.Windows.Forms.Button();
            this.wallBtn11 = new System.Windows.Forms.Button();
            this.wallBtn13 = new System.Windows.Forms.Button();
            this.wallBtnContainer = new System.Windows.Forms.Panel();
            this.wallBtn8 = new System.Windows.Forms.Button();
            this.buttonMode = new SwitchModeButton();
            ((System.ComponentModel.ISupportInitialize)(this.numMapGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWallVari)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWallVariMax)).BeginInit();
            this.RecLinePanel.SuspendLayout();
            this.WallProp.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCloseDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonGroup)).BeginInit();
            this.wallBtnContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wall type:";
            // 
            // comboWallSet
            // 
            this.comboWallSet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboWallSet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboWallSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWallSet.FormattingEnabled = true;
            this.comboWallSet.Location = new System.Drawing.Point(89, 4);
            this.comboWallSet.MaxDropDownItems = 16;
            this.comboWallSet.Name = "comboWallSet";
            this.comboWallSet.Size = new System.Drawing.Size(112, 21);
            this.comboWallSet.TabIndex = 1;
            this.comboWallSet.SelectedIndexChanged += new System.EventHandler(this.UpdateBtnImages);
            this.comboWallSet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WallSetFirst);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(71, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Minimap group:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMapGroup
            // 
            this.numMapGroup.Location = new System.Drawing.Point(160, 129);
            this.numMapGroup.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numMapGroup.Name = "numMapGroup";
            this.numMapGroup.Size = new System.Drawing.Size(46, 20);
            this.numMapGroup.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Variation:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numWallVari
            // 
            this.numWallVari.Location = new System.Drawing.Point(61, 160);
            this.numWallVari.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numWallVari.Name = "numWallVari";
            this.numWallVari.Size = new System.Drawing.Size(47, 20);
            this.numWallVari.TabIndex = 7;
            this.numWallVari.ValueChanged += new System.EventHandler(this.UpdateBtnImages);
            // 
            // numWallVariMax
            // 
            this.numWallVariMax.Location = new System.Drawing.Point(159, 160);
            this.numWallVariMax.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numWallVariMax.Name = "numWallVariMax";
            this.numWallVariMax.Size = new System.Drawing.Size(47, 20);
            this.numWallVariMax.TabIndex = 10;
            this.numWallVariMax.ValueChanged += new System.EventHandler(this.numWallVariMax_ValueChanged);
            // 
            // autovari
            // 
            this.autovari.AutoSize = true;
            this.autovari.BackColor = System.Drawing.Color.Transparent;
            this.autovari.Checked = true;
            this.autovari.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autovari.Location = new System.Drawing.Point(11, 101);
            this.autovari.Name = "autovari";
            this.autovari.Size = new System.Drawing.Size(91, 17);
            this.autovari.TabIndex = 11;
            this.autovari.Text = "Auto variation";
            this.autovari.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Max:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelSep1
            // 
            this.labelSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSep1.Location = new System.Drawing.Point(8, 62);
            this.labelSep1.Name = "labelSep1";
            this.labelSep1.Size = new System.Drawing.Size(201, 2);
            this.labelSep1.TabIndex = 16;
            // 
            // labelSep2
            // 
            this.labelSep2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSep2.Location = new System.Drawing.Point(6, 125);
            this.labelSep2.Name = "labelSep2";
            this.labelSep2.Size = new System.Drawing.Size(201, 2);
            this.labelSep2.TabIndex = 17;
            // 
            // AutoWalltBtn
            // 
            this.AutoWalltBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.AutoWalltBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AutoWalltBtn.Location = new System.Drawing.Point(115, 69);
            this.AutoWalltBtn.Name = "AutoWalltBtn";
            this.AutoWalltBtn.Size = new System.Drawing.Size(86, 24);
            this.AutoWalltBtn.TabIndex = 1;
            this.AutoWalltBtn.Text = "Wall Drawing";
            this.AutoWalltBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.AutoWalltBtn, "Drawing Walls (Switch: ~)");
            this.AutoWalltBtn.UseVisualStyleBackColor = true;
            this.AutoWalltBtn.CheckedChanged += new System.EventHandler(this.PlaceWalltBtn_CheckedChanged);
            // 
            // PlaceWalltBtn
            // 
            this.PlaceWalltBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.PlaceWalltBtn.Checked = true;
            this.PlaceWalltBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PlaceWalltBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaceWalltBtn.ForeColor = System.Drawing.Color.Black;
            this.PlaceWalltBtn.Location = new System.Drawing.Point(11, 70);
            this.PlaceWalltBtn.Name = "PlaceWalltBtn";
            this.PlaceWalltBtn.Size = new System.Drawing.Size(84, 24);
            this.PlaceWalltBtn.TabIndex = 0;
            this.PlaceWalltBtn.TabStop = true;
            this.PlaceWalltBtn.Text = "Wall Place";
            this.PlaceWalltBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.PlaceWalltBtn, "Placing Walls (Switch: ~)");
            this.PlaceWalltBtn.UseVisualStyleBackColor = true;
            this.PlaceWalltBtn.CheckedChanged += new System.EventHandler(this.PlaceWalltBtn_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Properties";
            this.toolTip1.SetToolTip(this.button1, "Set a wall properties and then select a wall you wish to affect.");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBlackWalls
            // 
            this.checkBlackWalls.AutoSize = true;
            this.checkBlackWalls.Location = new System.Drawing.Point(115, 101);
            this.checkBlackWalls.Name = "checkBlackWalls";
            this.checkBlackWalls.Size = new System.Drawing.Size(86, 17);
            this.checkBlackWalls.TabIndex = 21;
            this.checkBlackWalls.Text = "Fast prewiev";
            this.checkBlackWalls.UseVisualStyleBackColor = true;
            this.checkBlackWalls.CheckedChanged += new System.EventHandler(this.UpdateBtnImages);
            // 
            // Picker
            // 
            this.Picker.Appearance = System.Windows.Forms.Appearance.Button;
            this.Picker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Picker.BackgroundImage")));
            this.Picker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Picker.Location = new System.Drawing.Point(171, 29);
            this.Picker.Name = "Picker";
            this.Picker.Size = new System.Drawing.Size(30, 30);
            this.Picker.TabIndex = 37;
            this.toolTip1.SetToolTip(this.Picker, "Wall Picker (Ctrl+A)");
            this.Picker.UseVisualStyleBackColor = true;
            this.Picker.CheckedChanged += new System.EventHandler(this.Picker_CheckedChanged);
            // 
            // RecLinePanel
            // 
            this.RecLinePanel.Controls.Add(this.smartDraw);
            this.RecLinePanel.Controls.Add(this.RecWall);
            this.RecLinePanel.Controls.Add(this.LineWall);
            this.RecLinePanel.Location = new System.Drawing.Point(7, 28);
            this.RecLinePanel.Name = "RecLinePanel";
            this.RecLinePanel.Size = new System.Drawing.Size(162, 30);
            this.RecLinePanel.TabIndex = 38;
            this.RecLinePanel.Visible = false;
            // 
            // smartDraw
            // 
            this.smartDraw.AutoSize = true;
            this.smartDraw.Location = new System.Drawing.Point(76, 8);
            this.smartDraw.Name = "smartDraw";
            this.smartDraw.Size = new System.Drawing.Size(81, 17);
            this.smartDraw.TabIndex = 39;
            this.smartDraw.Text = "Smart Draw";
            this.toolTip1.SetToolTip(this.smartDraw, "Shift");
            this.smartDraw.UseVisualStyleBackColor = true;
            this.smartDraw.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // RecWall
            // 
            this.RecWall.Appearance = System.Windows.Forms.Appearance.Button;
            this.RecWall.BackgroundImage = global::MapEditor.Properties.Resources.RecWall;
            this.RecWall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RecWall.Location = new System.Drawing.Point(36, 3);
            this.RecWall.Name = "RecWall";
            this.RecWall.Size = new System.Drawing.Size(25, 25);
            this.RecWall.TabIndex = 1;
            this.toolTip1.SetToolTip(this.RecWall, "Oriented Rectangle - Draws a 45 degree oriented wall rectangle by dragging the mo" +
                    "use. (Ctrl+R)");
            this.RecWall.UseVisualStyleBackColor = true;
            this.RecWall.CheckedChanged += new System.EventHandler(this.RecWall_CheckedChanged);
            this.RecWall.EnabledChanged += new System.EventHandler(this.LineWall_EnabledChanged);
            this.RecWall.Click += new System.EventHandler(this.RecWall_Click);
            // 
            // LineWall
            // 
            this.LineWall.Appearance = System.Windows.Forms.Appearance.Button;
            this.LineWall.BackgroundImage = global::MapEditor.Properties.Resources.LineWall;
            this.LineWall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LineWall.Location = new System.Drawing.Point(5, 3);
            this.LineWall.Name = "LineWall";
            this.LineWall.Size = new System.Drawing.Size(25, 25);
            this.LineWall.TabIndex = 0;
            this.toolTip1.SetToolTip(this.LineWall, "Free Line - Draws a wall line by dragging the mouse. (Ctrl+T)");
            this.LineWall.UseVisualStyleBackColor = true;
            this.LineWall.CheckedChanged += new System.EventHandler(this.LineWall_CheckedChanged);
            this.LineWall.EnabledChanged += new System.EventHandler(this.LineWall_EnabledChanged);
            this.LineWall.Click += new System.EventHandler(this.LineWall_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 9000;
            this.toolTip1.InitialDelay = 600;
            this.toolTip1.ReshowDelay = 100;
            // 
            // WallProp
            // 
            this.WallProp.Controls.Add(this.ok);
            this.WallProp.Controls.Add(this.groupBox2);
            this.WallProp.Controls.Add(this.checkDestructable);
            this.WallProp.Controls.Add(this.label7);
            this.WallProp.Controls.Add(this.polygonGroup);
            this.WallProp.Location = new System.Drawing.Point(4, 1);
            this.WallProp.Name = "WallProp";
            this.WallProp.Size = new System.Drawing.Size(209, 189);
            this.WallProp.TabIndex = 40;
            this.toolTip1.SetToolTip(this.WallProp, "Sets the properties of the selected wall. \r\n(only displays the properties of the " +
                    "selected wall while holding Shift)");
            this.WallProp.Visible = false;
            this.WallProp.Paint += new System.Windows.Forms.PaintEventHandler(this.WallProp_Paint);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(143, 162);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(55, 23);
            this.ok.TabIndex = 5;
            this.ok.Text = "Cancel";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.openWallBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.checkListFlags);
            this.groupBox2.Controls.Add(this.numericCloseDelay);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(10, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 124);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Secret Wall";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // openWallBox
            // 
            this.openWallBox.AutoSize = true;
            this.openWallBox.Enabled = false;
            this.openWallBox.Location = new System.Drawing.Point(19, 27);
            this.openWallBox.Name = "openWallBox";
            this.openWallBox.Size = new System.Drawing.Size(52, 17);
            this.openWallBox.TabIndex = 5;
            this.openWallBox.Text = "Open";
            this.openWallBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(76, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "Wall status && flags";
            // 
            // checkListFlags
            // 
            this.checkListFlags.FormattingEnabled = true;
            this.checkListFlags.Items.AddRange(new object[] {
            "Scripted",
            "Auto-Open",
            "Auto-Close",
            "UnkFlag8"});
            this.checkListFlags.Location = new System.Drawing.Point(77, 26);
            this.checkListFlags.Name = "checkListFlags";
            this.checkListFlags.Size = new System.Drawing.Size(104, 64);
            this.checkListFlags.TabIndex = 2;
            this.toolTip1.SetToolTip(this.checkListFlags, resources.GetString("checkListFlags.ToolTip"));
            this.checkListFlags.SelectedIndexChanged += new System.EventHandler(this.checkListFlags_SelectedIndexChanged);
            this.checkListFlags.SelectedValueChanged += new System.EventHandler(this.checkListFlags_SelectedValueChanged);
            this.checkListFlags.MouseMove += new System.Windows.Forms.MouseEventHandler(this.checkListFlags_MouseMove);
            // 
            // numericCloseDelay
            // 
            this.numericCloseDelay.Location = new System.Drawing.Point(131, 97);
            this.numericCloseDelay.Name = "numericCloseDelay";
            this.numericCloseDelay.Size = new System.Drawing.Size(49, 20);
            this.numericCloseDelay.TabIndex = 1;
            this.numericCloseDelay.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Open/close delay:";
            // 
            // checkDestructable
            // 
            this.checkDestructable.Location = new System.Drawing.Point(14, 161);
            this.checkDestructable.Name = "checkDestructable";
            this.checkDestructable.Size = new System.Drawing.Size(95, 19);
            this.checkDestructable.TabIndex = 4;
            this.checkDestructable.Text = "Destructable";
            this.toolTip1.SetToolTip(this.checkDestructable, "Wall can be destroed");
            this.checkDestructable.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Polygon (Minimap) Group";
            this.toolTip1.SetToolTip(this.label7, "Determines polygon where this wall will be displayed on the minimap.");
            // 
            // polygonGroup
            // 
            this.polygonGroup.Location = new System.Drawing.Point(143, 136);
            this.polygonGroup.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.polygonGroup.Name = "polygonGroup";
            this.polygonGroup.Size = new System.Drawing.Size(48, 20);
            this.polygonGroup.TabIndex = 2;
            this.toolTip1.SetToolTip(this.polygonGroup, "Determines polygon where this wall will be displayed on the minimap.");
            this.polygonGroup.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // wallBtn1
            // 
            this.wallBtn1.BackColor = System.Drawing.Color.White;
            this.wallBtn1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn1.Location = new System.Drawing.Point(7, 4);
            this.wallBtn1.Name = "wallBtn1";
            this.wallBtn1.Size = new System.Drawing.Size(64, 80);
            this.wallBtn1.TabIndex = 0;
            this.wallBtn1.UseVisualStyleBackColor = false;
            this.wallBtn1.Click += new System.EventHandler(this.wallBtn1_Click);
            // 
            // wallBtn4
            // 
            this.wallBtn4.BackColor = System.Drawing.Color.White;
            this.wallBtn4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn4.Location = new System.Drawing.Point(71, 4);
            this.wallBtn4.Name = "wallBtn4";
            this.wallBtn4.Size = new System.Drawing.Size(64, 80);
            this.wallBtn4.TabIndex = 1;
            this.wallBtn4.UseVisualStyleBackColor = false;
            this.wallBtn4.Click += new System.EventHandler(this.wallBtn4_Click);
            // 
            // wallBtn2
            // 
            this.wallBtn2.BackColor = System.Drawing.Color.White;
            this.wallBtn2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn2.Location = new System.Drawing.Point(7, 84);
            this.wallBtn2.Name = "wallBtn2";
            this.wallBtn2.Size = new System.Drawing.Size(64, 80);
            this.wallBtn2.TabIndex = 3;
            this.wallBtn2.UseVisualStyleBackColor = false;
            // 
            // wallBtn5
            // 
            this.wallBtn5.BackColor = System.Drawing.Color.White;
            this.wallBtn5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn5.Location = new System.Drawing.Point(71, 84);
            this.wallBtn5.Name = "wallBtn5";
            this.wallBtn5.Size = new System.Drawing.Size(64, 80);
            this.wallBtn5.TabIndex = 4;
            this.wallBtn5.UseVisualStyleBackColor = false;
            // 
            // wallBtn9
            // 
            this.wallBtn9.BackColor = System.Drawing.Color.White;
            this.wallBtn9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn9.Location = new System.Drawing.Point(135, 84);
            this.wallBtn9.Name = "wallBtn9";
            this.wallBtn9.Size = new System.Drawing.Size(64, 80);
            this.wallBtn9.TabIndex = 5;
            this.wallBtn9.UseVisualStyleBackColor = false;
            // 
            // wallBtn3
            // 
            this.wallBtn3.BackColor = System.Drawing.Color.White;
            this.wallBtn3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn3.Location = new System.Drawing.Point(7, 164);
            this.wallBtn3.Name = "wallBtn3";
            this.wallBtn3.Size = new System.Drawing.Size(64, 80);
            this.wallBtn3.TabIndex = 6;
            this.wallBtn3.UseVisualStyleBackColor = false;
            // 
            // wallBtn6
            // 
            this.wallBtn6.BackColor = System.Drawing.Color.White;
            this.wallBtn6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn6.Location = new System.Drawing.Point(71, 164);
            this.wallBtn6.Name = "wallBtn6";
            this.wallBtn6.Size = new System.Drawing.Size(64, 80);
            this.wallBtn6.TabIndex = 7;
            this.wallBtn6.UseVisualStyleBackColor = false;
            // 
            // wallBtn10
            // 
            this.wallBtn10.BackColor = System.Drawing.Color.White;
            this.wallBtn10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn10.Location = new System.Drawing.Point(135, 164);
            this.wallBtn10.Name = "wallBtn10";
            this.wallBtn10.Size = new System.Drawing.Size(64, 80);
            this.wallBtn10.TabIndex = 8;
            this.wallBtn10.UseVisualStyleBackColor = false;
            // 
            // wallBtn12
            // 
            this.wallBtn12.BackColor = System.Drawing.Color.White;
            this.wallBtn12.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn12.Location = new System.Drawing.Point(7, 244);
            this.wallBtn12.Name = "wallBtn12";
            this.wallBtn12.Size = new System.Drawing.Size(64, 80);
            this.wallBtn12.TabIndex = 9;
            this.wallBtn12.UseVisualStyleBackColor = false;
            // 
            // wallBtn7
            // 
            this.wallBtn7.BackColor = System.Drawing.Color.White;
            this.wallBtn7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn7.Location = new System.Drawing.Point(71, 244);
            this.wallBtn7.Name = "wallBtn7";
            this.wallBtn7.Size = new System.Drawing.Size(64, 80);
            this.wallBtn7.TabIndex = 10;
            this.wallBtn7.UseVisualStyleBackColor = false;
            // 
            // wallBtn11
            // 
            this.wallBtn11.BackColor = System.Drawing.Color.White;
            this.wallBtn11.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn11.Location = new System.Drawing.Point(135, 244);
            this.wallBtn11.Name = "wallBtn11";
            this.wallBtn11.Size = new System.Drawing.Size(64, 80);
            this.wallBtn11.TabIndex = 11;
            this.wallBtn11.UseVisualStyleBackColor = false;
            // 
            // wallBtn13
            // 
            this.wallBtn13.BackColor = System.Drawing.Color.White;
            this.wallBtn13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn13.Location = new System.Drawing.Point(7, 324);
            this.wallBtn13.Name = "wallBtn13";
            this.wallBtn13.Size = new System.Drawing.Size(64, 80);
            this.wallBtn13.TabIndex = 12;
            this.wallBtn13.UseVisualStyleBackColor = false;
            // 
            // wallBtnContainer
            // 
            this.wallBtnContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wallBtnContainer.Controls.Add(this.wallBtn13);
            this.wallBtnContainer.Controls.Add(this.wallBtn11);
            this.wallBtnContainer.Controls.Add(this.wallBtn7);
            this.wallBtnContainer.Controls.Add(this.wallBtn12);
            this.wallBtnContainer.Controls.Add(this.wallBtn10);
            this.wallBtnContainer.Controls.Add(this.wallBtn6);
            this.wallBtnContainer.Controls.Add(this.wallBtn3);
            this.wallBtnContainer.Controls.Add(this.wallBtn9);
            this.wallBtnContainer.Controls.Add(this.wallBtn5);
            this.wallBtnContainer.Controls.Add(this.wallBtn2);
            this.wallBtnContainer.Controls.Add(this.wallBtn8);
            this.wallBtnContainer.Controls.Add(this.wallBtn4);
            this.wallBtnContainer.Controls.Add(this.wallBtn1);
            this.wallBtnContainer.Location = new System.Drawing.Point(4, 193);
            this.wallBtnContainer.Name = "wallBtnContainer";
            this.wallBtnContainer.Size = new System.Drawing.Size(208, 418);
            this.wallBtnContainer.TabIndex = 8;
            this.wallBtnContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.wallBtnContainer_Paint);
            // 
            // wallBtn8
            // 
            this.wallBtn8.BackColor = System.Drawing.Color.White;
            this.wallBtn8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.wallBtn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wallBtn8.Location = new System.Drawing.Point(135, 4);
            this.wallBtn8.Name = "wallBtn8";
            this.wallBtn8.Size = new System.Drawing.Size(64, 80);
            this.wallBtn8.TabIndex = 2;
            this.wallBtn8.UseVisualStyleBackColor = false;
            // 
            // buttonMode
            // 
            this.buttonMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonMode.Location = new System.Drawing.Point(3, -1);
            this.buttonMode.Name = "buttonMode";
            this.buttonMode.Size = new System.Drawing.Size(12, 23);
            this.buttonMode.TabIndex = 18;
            this.buttonMode.Text = " ";
            this.buttonMode.UseVisualStyleBackColor = true;
            this.buttonMode.Visible = false;
            // 
            // WallMakeTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WallProp);
            this.Controls.Add(this.RecLinePanel);
            this.Controls.Add(this.Picker);
            this.Controls.Add(this.checkBlackWalls);
            this.Controls.Add(this.AutoWalltBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PlaceWalltBtn);
            this.Controls.Add(this.buttonMode);
            this.Controls.Add(this.labelSep2);
            this.Controls.Add(this.labelSep1);
            this.Controls.Add(this.numMapGroup);
            this.Controls.Add(this.numWallVari);
            this.Controls.Add(this.numWallVariMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.autovari);
            this.Controls.Add(this.wallBtnContainer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboWallSet);
            this.Controls.Add(this.label1);
            this.Name = "WallMakeTab";
            this.Size = new System.Drawing.Size(216, 614);
            this.Load += new System.EventHandler(this.WallMakeTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMapGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWallVari)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWallVariMax)).EndInit();
            this.RecLinePanel.ResumeLayout(false);
            this.RecLinePanel.PerformLayout();
            this.WallProp.ResumeLayout(false);
            this.WallProp.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCloseDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonGroup)).EndInit();
            this.wallBtnContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public System.Windows.Forms.NumericUpDown numWallVari;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numMapGroup;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboWallSet;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numWallVariMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSep1;
        private System.Windows.Forms.Label labelSep2;
        public SwitchModeButton buttonMode;
        public System.Windows.Forms.RadioButton AutoWalltBtn;
        public System.Windows.Forms.RadioButton PlaceWalltBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBlackWalls;
        public System.Windows.Forms.CheckBox Picker;
        private System.Windows.Forms.Panel RecLinePanel;
        public System.Windows.Forms.CheckBox RecWall;
        public System.Windows.Forms.CheckBox LineWall;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.CheckBox smartDraw;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox openWallBox;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckedListBox checkListFlags;
        public System.Windows.Forms.NumericUpDown numericCloseDelay;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.CheckBox checkDestructable;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown polygonGroup;
        private System.Windows.Forms.Button ok;
        public System.Windows.Forms.Panel WallProp;
        private System.Windows.Forms.Button wallBtn1;
        private System.Windows.Forms.Button wallBtn4;
        private System.Windows.Forms.Button wallBtn2;
        private System.Windows.Forms.Button wallBtn5;
        private System.Windows.Forms.Button wallBtn9;
        private System.Windows.Forms.Button wallBtn3;
        private System.Windows.Forms.Button wallBtn6;
        private System.Windows.Forms.Button wallBtn10;
        private System.Windows.Forms.Button wallBtn12;
        private System.Windows.Forms.Button wallBtn7;
        private System.Windows.Forms.Button wallBtn11;
        private System.Windows.Forms.Button wallBtn13;
        private System.Windows.Forms.Panel wallBtnContainer;
        private System.Windows.Forms.Button wallBtn8;
        public System.Windows.Forms.CheckBox autovari;
    }
}
