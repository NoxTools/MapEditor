/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 24.01.2015
 */
namespace MapEditor.newgui
{
	partial class WallProperties
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
            this.buttonDone = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboWallState = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkListFlags = new System.Windows.Forms.CheckedListBox();
            this.numericCloseDelay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.polygonGroup = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkDestructable = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openWallBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCloseDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonGroup)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(87, 177);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.ButtonDoneClick);
            this.buttonDone.MouseEnter += new System.EventHandler(this.buttonDone_MouseEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.openWallBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkListFlags);
            this.groupBox1.Controls.Add(this.numericCloseDelay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 113);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Secret Wall";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // comboWallState
            // 
            this.comboWallState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWallState.FormattingEnabled = true;
            this.comboWallState.Items.AddRange(new object[] {
            "Closed",
            "Closing",
            "Open",
            "Opening"});
            this.comboWallState.Location = new System.Drawing.Point(92, 212);
            this.comboWallState.Name = "comboWallState";
            this.comboWallState.Size = new System.Drawing.Size(46, 21);
            this.comboWallState.TabIndex = 4;
            this.comboWallState.SelectedIndexChanged += new System.EventHandler(this.ComboWallStateSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(107, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Wall status && flags";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // checkListFlags
            // 
            this.checkListFlags.FormattingEnabled = true;
            this.checkListFlags.Items.AddRange(new object[] {
            "Scripted",
            "Auto-Open",
            "Auto-Close",
            "UnkFlag8"});
            this.checkListFlags.Location = new System.Drawing.Point(108, 30);
            this.checkListFlags.Name = "checkListFlags";
            this.checkListFlags.Size = new System.Drawing.Size(104, 64);
            this.checkListFlags.TabIndex = 2;
            this.checkListFlags.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckListFlagsItemCheck);
            this.checkListFlags.SelectedIndexChanged += new System.EventHandler(this.checkListFlags_SelectedIndexChanged);
            // 
            // numericCloseDelay
            // 
            this.numericCloseDelay.Location = new System.Drawing.Point(6, 80);
            this.numericCloseDelay.Name = "numericCloseDelay";
            this.numericCloseDelay.Size = new System.Drawing.Size(88, 20);
            this.numericCloseDelay.TabIndex = 1;
            this.numericCloseDelay.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericCloseDelay.ValueChanged += new System.EventHandler(this.NumericCloseDelayValueChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Open/close delay";
            // 
            // polygonGroup
            // 
            this.polygonGroup.Location = new System.Drawing.Point(160, 121);
            this.polygonGroup.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.polygonGroup.Name = "polygonGroup";
            this.polygonGroup.Size = new System.Drawing.Size(48, 20);
            this.polygonGroup.TabIndex = 2;
            this.polygonGroup.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.polygonGroup.ValueChanged += new System.EventHandler(this.PolygonGroupValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Polygon (Minimap) Group";
            // 
            // checkDestructable
            // 
            this.checkDestructable.Location = new System.Drawing.Point(5, 144);
            this.checkDestructable.Name = "checkDestructable";
            this.checkDestructable.Size = new System.Drawing.Size(160, 24);
            this.checkDestructable.TabIndex = 4;
            this.checkDestructable.Text = "Destructable (Breakable)";
            this.checkDestructable.UseVisualStyleBackColor = true;
            this.checkDestructable.CheckedChanged += new System.EventHandler(this.CheckDestructableCheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.checkDestructable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.polygonGroup);
            this.panel1.Location = new System.Drawing.Point(6, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 170);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // openWallBox
            // 
            this.openWallBox.AutoSize = true;
            this.openWallBox.Enabled = false;
            this.openWallBox.Location = new System.Drawing.Point(50, 30);
            this.openWallBox.Name = "openWallBox";
            this.openWallBox.Size = new System.Drawing.Size(52, 17);
            this.openWallBox.TabIndex = 5;
            this.openWallBox.Text = "Open";
            this.openWallBox.UseVisualStyleBackColor = true;
            this.openWallBox.CheckedChanged += new System.EventHandler(this.openWallBox_CheckedChanged);
            // 
            // WallProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 203);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboWallState);
            this.Controls.Add(this.buttonDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WallProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wall Properties";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.WallProperties_Load);
            this.Leave += new System.EventHandler(this.WallProperties_Leave);
            this.MouseLeave += new System.EventHandler(this.WallProperties_MouseLeave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCloseDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.polygonGroup)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDone;
        public System.Windows.Forms.CheckBox checkDestructable;
        public System.Windows.Forms.NumericUpDown polygonGroup;
        public System.Windows.Forms.ComboBox comboWallState;
        public System.Windows.Forms.CheckedListBox checkListFlags;
        public System.Windows.Forms.NumericUpDown numericCloseDelay;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox openWallBox;
	}
}
