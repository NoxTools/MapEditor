/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 15.01.2015
 */
namespace MapEditor.newgui
{
	partial class TileMakeTab
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileMakeTab));
            this.label1 = new System.Windows.Forms.Label();
            this.comboTileType = new System.Windows.Forms.ComboBox();
            this.listTileImages = new System.Windows.Forms.ListView();
            this.BrushSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboIgnoreTile = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WallBlockBrush = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelSep1 = new System.Windows.Forms.Label();
            this.checkAutoVari = new System.Windows.Forms.CheckBox();
            this.AutoTileBtn = new System.Windows.Forms.RadioButton();
            this.PlaceTileBtn = new System.Windows.Forms.RadioButton();
            this.Picker = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.miniEdges = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edgeBox = new System.Windows.Forms.ComboBox();
            this.buttonMode = new SwitchModeButton();
            ((System.ComponentModel.ISupportInitialize)(this.BrushSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.miniEdges.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tile type:";
            // 
            // comboTileType
            // 
            this.comboTileType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboTileType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboTileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTileType.FormattingEnabled = true;
            this.comboTileType.Location = new System.Drawing.Point(77, 7);
            this.comboTileType.MaxDropDownItems = 20;
            this.comboTileType.Name = "comboTileType";
            this.comboTileType.Size = new System.Drawing.Size(128, 21);
            this.comboTileType.TabIndex = 1;
            this.comboTileType.SelectedIndexChanged += new System.EventHandler(this.UpdateListView);
            // 
            // listTileImages
            // 
            this.listTileImages.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTileImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listTileImages.GridLines = true;
            this.listTileImages.Location = new System.Drawing.Point(5, 286);
            this.listTileImages.Name = "listTileImages";
            this.listTileImages.Size = new System.Drawing.Size(205, 322);
            this.listTileImages.TabIndex = 3;
            this.listTileImages.TileSize = new System.Drawing.Size(46, 46);
            this.listTileImages.UseCompatibleStateImageBehavior = false;
            this.listTileImages.VirtualMode = true;
            this.listTileImages.SelectedIndexChanged += new System.EventHandler(this.ChangeTileType);
            // 
            // BrushSize
            // 
            this.BrushSize.Location = new System.Drawing.Point(169, 116);
            this.BrushSize.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.BrushSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BrushSize.Name = "BrushSize";
            this.BrushSize.Size = new System.Drawing.Size(39, 20);
            this.BrushSize.TabIndex = 9;
            this.BrushSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BrushSize.ValueChanged += new System.EventHandler(this.BrushSize_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Brush size:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboIgnoreTile
            // 
            this.comboIgnoreTile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboIgnoreTile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboIgnoreTile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIgnoreTile.FormattingEnabled = true;
            this.comboIgnoreTile.Location = new System.Drawing.Point(83, 22);
            this.comboIgnoreTile.MaxDropDownItems = 20;
            this.comboIgnoreTile.Name = "comboIgnoreTile";
            this.comboIgnoreTile.Size = new System.Drawing.Size(116, 21);
            this.comboIgnoreTile.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WallBlockBrush);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboIgnoreTile);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(5, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 77);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Brush Options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // WallBlockBrush
            // 
            this.WallBlockBrush.AutoSize = true;
            this.WallBlockBrush.Location = new System.Drawing.Point(14, 50);
            this.WallBlockBrush.Name = "WallBlockBrush";
            this.WallBlockBrush.Size = new System.Drawing.Size(76, 17);
            this.WallBlockBrush.TabIndex = 13;
            this.WallBlockBrush.Text = "Wall block";
            this.toolTip1.SetToolTip(this.WallBlockBrush, "Prevents placing edges behind a wall.");
            this.WallBlockBrush.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tile to Ignore:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSep1
            // 
            this.labelSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSep1.Location = new System.Drawing.Point(9, 69);
            this.labelSep1.Name = "labelSep1";
            this.labelSep1.Size = new System.Drawing.Size(201, 2);
            this.labelSep1.TabIndex = 17;
            this.labelSep1.Click += new System.EventHandler(this.labelSep1_Click);
            // 
            // checkAutoVari
            // 
            this.checkAutoVari.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkAutoVari.AutoSize = true;
            this.checkAutoVari.Checked = true;
            this.checkAutoVari.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAutoVari.Location = new System.Drawing.Point(12, 116);
            this.checkAutoVari.Name = "checkAutoVari";
            this.checkAutoVari.Size = new System.Drawing.Size(91, 17);
            this.checkAutoVari.TabIndex = 2;
            this.checkAutoVari.Text = "Auto variation";
            this.checkAutoVari.UseVisualStyleBackColor = true;
            // 
            // AutoTileBtn
            // 
            this.AutoTileBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.AutoTileBtn.BackColor = System.Drawing.Color.LightGray;
            this.AutoTileBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AutoTileBtn.Location = new System.Drawing.Point(113, 76);
            this.AutoTileBtn.Name = "AutoTileBtn";
            this.AutoTileBtn.Size = new System.Drawing.Size(95, 24);
            this.AutoTileBtn.TabIndex = 33;
            this.AutoTileBtn.Text = "Tile Brush";
            this.AutoTileBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.AutoTileBtn, "Placing Tiles and automatically generates surrounded edges. (Switch: ~))");
            this.AutoTileBtn.UseVisualStyleBackColor = false;
            this.AutoTileBtn.CheckedChanged += new System.EventHandler(this.PlaceTileBtn_CheckedChanged);
            // 
            // PlaceTileBtn
            // 
            this.PlaceTileBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.PlaceTileBtn.BackColor = System.Drawing.Color.LightGray;
            this.PlaceTileBtn.Checked = true;
            this.PlaceTileBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PlaceTileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaceTileBtn.Location = new System.Drawing.Point(10, 76);
            this.PlaceTileBtn.Name = "PlaceTileBtn";
            this.PlaceTileBtn.Size = new System.Drawing.Size(98, 24);
            this.PlaceTileBtn.TabIndex = 32;
            this.PlaceTileBtn.TabStop = true;
            this.PlaceTileBtn.Text = "Place/Remove";
            this.PlaceTileBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.PlaceTileBtn, "Selecting/Removing Tiles (Switch: ~)");
            this.PlaceTileBtn.UseVisualStyleBackColor = false;
            this.PlaceTileBtn.CheckedChanged += new System.EventHandler(this.PlaceTileBtn_CheckedChanged);
            // 
            // Picker
            // 
            this.Picker.Appearance = System.Windows.Forms.Appearance.Button;
            this.Picker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Picker.BackgroundImage")));
            this.Picker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Picker.Location = new System.Drawing.Point(175, 33);
            this.Picker.Name = "Picker";
            this.Picker.Size = new System.Drawing.Size(30, 30);
            this.Picker.TabIndex = 38;
            this.toolTip1.SetToolTip(this.Picker, "Tile Picker. (Ctrl+A)");
            this.Picker.UseVisualStyleBackColor = true;
            this.Picker.CheckedChanged += new System.EventHandler(this.Picker_CheckedChanged);
            // 
            // miniEdges
            // 
            this.miniEdges.Controls.Add(this.label2);
            this.miniEdges.Controls.Add(this.edgeBox);
            this.miniEdges.Enabled = false;
            this.miniEdges.Location = new System.Drawing.Point(5, 223);
            this.miniEdges.Name = "miniEdges";
            this.miniEdges.Size = new System.Drawing.Size(205, 52);
            this.miniEdges.TabIndex = 39;
            this.miniEdges.TabStop = false;
            this.miniEdges.Text = "Edges";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current edge:";
            // 
            // edgeBox
            // 
            this.edgeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edgeBox.FormattingEnabled = true;
            this.edgeBox.Location = new System.Drawing.Point(83, 19);
            this.edgeBox.MaxDropDownItems = 20;
            this.edgeBox.Name = "edgeBox";
            this.edgeBox.Size = new System.Drawing.Size(116, 21);
            this.edgeBox.TabIndex = 0;
            this.edgeBox.SelectedIndexChanged += new System.EventHandler(this.edgeBox_SelectedIndexChanged);
            // 
            // buttonMode
            // 
            this.buttonMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonMode.Location = new System.Drawing.Point(10, 31);
            this.buttonMode.Name = "buttonMode";
            this.buttonMode.Size = new System.Drawing.Size(15, 23);
            this.buttonMode.TabIndex = 19;
            this.buttonMode.Text = " ";
            this.buttonMode.UseVisualStyleBackColor = true;
            this.buttonMode.Visible = false;
            this.buttonMode.Click += new System.EventHandler(this.buttonMode_Click);
            // 
            // TileMakeTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.miniEdges);
            this.Controls.Add(this.Picker);
            this.Controls.Add(this.BrushSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PlaceTileBtn);
            this.Controls.Add(this.AutoTileBtn);
            this.Controls.Add(this.buttonMode);
            this.Controls.Add(this.labelSep1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listTileImages);
            this.Controls.Add(this.comboTileType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkAutoVari);
            this.Name = "TileMakeTab";
            this.Size = new System.Drawing.Size(216, 617);
            this.Load += new System.EventHandler(this.TileMakeTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BrushSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.miniEdges.ResumeLayout(false);
            this.miniEdges.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ListView listTileImages;
		public System.Windows.Forms.ComboBox comboTileType;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown BrushSize;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboIgnoreTile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox WallBlockBrush;
        private System.Windows.Forms.Label labelSep1;
        private System.Windows.Forms.CheckBox checkAutoVari;
        public SwitchModeButton buttonMode;
        public System.Windows.Forms.RadioButton AutoTileBtn;
        public System.Windows.Forms.RadioButton PlaceTileBtn;
        public System.Windows.Forms.CheckBox Picker;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox miniEdges;
        public System.Windows.Forms.ComboBox edgeBox;
        private System.Windows.Forms.Label label2;
	}
}
