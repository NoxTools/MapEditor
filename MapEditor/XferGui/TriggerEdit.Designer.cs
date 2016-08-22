/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 09.11.2014
 */
namespace MapEditor.XferGui
{
	partial class TriggerEdit
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
            this.groupBoxArea = new System.Windows.Forms.GroupBox();
            this.plateEdgeColor = new System.Windows.Forms.Button();
            this.sizeY = new System.Windows.Forms.NumericUpDown();
            this.sizeX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scriptCollided = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.scriptReleased = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.scriptActivated = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.flagsBox = new System.Windows.Forms.CheckedListBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeX)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxArea
            // 
            this.groupBoxArea.Controls.Add(this.plateEdgeColor);
            this.groupBoxArea.Controls.Add(this.sizeY);
            this.groupBoxArea.Controls.Add(this.sizeX);
            this.groupBoxArea.Controls.Add(this.label2);
            this.groupBoxArea.Controls.Add(this.label1);
            this.groupBoxArea.Location = new System.Drawing.Point(12, 106);
            this.groupBoxArea.Name = "groupBoxArea";
            this.groupBoxArea.Size = new System.Drawing.Size(211, 102);
            this.groupBoxArea.TabIndex = 0;
            this.groupBoxArea.TabStop = false;
            this.groupBoxArea.Text = "Trigger/PressurePlate only";
            this.groupBoxArea.Enter += new System.EventHandler(this.groupBoxArea_Enter);
            // 
            // plateEdgeColor
            // 
            this.plateEdgeColor.Location = new System.Drawing.Point(36, 71);
            this.plateEdgeColor.Name = "plateEdgeColor";
            this.plateEdgeColor.Size = new System.Drawing.Size(130, 23);
            this.plateEdgeColor.TabIndex = 1;
            this.plateEdgeColor.Text = "PressurePlate Color";
            this.plateEdgeColor.UseVisualStyleBackColor = true;
            this.plateEdgeColor.Click += new System.EventHandler(this.PlateEdgeColorClick);
            // 
            // sizeY
            // 
            this.sizeY.Location = new System.Drawing.Point(89, 45);
            this.sizeY.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.sizeY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sizeY.Name = "sizeY";
            this.sizeY.Size = new System.Drawing.Size(103, 20);
            this.sizeY.TabIndex = 3;
            this.sizeY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // sizeX
            // 
            this.sizeX.Location = new System.Drawing.Point(89, 19);
            this.sizeX.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.sizeX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sizeX.Name = "sizeX";
            this.sizeX.Size = new System.Drawing.Size(103, 20);
            this.sizeX.TabIndex = 2;
            this.sizeX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scriptCollided);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.scriptReleased);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.scriptActivated);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Script event handlers";
            // 
            // scriptCollided
            // 
            this.scriptCollided.Location = new System.Drawing.Point(89, 62);
            this.scriptCollided.Name = "scriptCollided";
            this.scriptCollided.Size = new System.Drawing.Size(114, 20);
            this.scriptCollided.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Touched";
            // 
            // scriptReleased
            // 
            this.scriptReleased.Location = new System.Drawing.Point(89, 39);
            this.scriptReleased.Name = "scriptReleased";
            this.scriptReleased.Size = new System.Drawing.Size(114, 20);
            this.scriptReleased.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Released";
            // 
            // scriptActivated
            // 
            this.scriptActivated.Location = new System.Drawing.Point(89, 16);
            this.scriptActivated.Name = "scriptActivated";
            this.scriptActivated.Size = new System.Drawing.Size(114, 20);
            this.scriptActivated.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Activated";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.flagsBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 214);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(211, 158);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Allowed Activators";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Allowed team";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(80, 127);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown1.TabIndex = 3;
            // 
            // flagsBox
            // 
            this.flagsBox.FormattingEnabled = true;
            this.flagsBox.Items.AddRange(new object[] {
            "MONSTER",
            "PLAYER",
            "MISSILE",
            "OBSTACLE",
            "NPC",
            "FOOD (Solo Only)",
            "WEAPON (Solo Only)",
            "ARMOR (Solo Only)",
            "TREASURE",
            "WAND (Solo Only)"});
            this.flagsBox.Location = new System.Drawing.Point(6, 19);
            this.flagsBox.Name = "flagsBox";
            this.flagsBox.Size = new System.Drawing.Size(199, 94);
            this.flagsBox.TabIndex = 2;
            this.flagsBox.SelectedIndexChanged += new System.EventHandler(this.flagsBox_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(75, 378);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // TriggerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 413);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TriggerEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TriggerXfer";
            this.Load += new System.EventHandler(this.TriggerEdit_Load);
            this.groupBoxArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeX)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox scriptReleased;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox scriptCollided;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox scriptActivated;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button plateEdgeColor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown sizeX;
		private System.Windows.Forms.NumericUpDown sizeY;
        private System.Windows.Forms.GroupBox groupBoxArea;
        private System.Windows.Forms.CheckedListBox flagsBox;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label6;
	}
}
