/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 01.12.2014
 */
namespace MapEditor.newgui
{
	partial class MapGeneratorDlg
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
			this.progressBarGeneration = new System.Windows.Forms.ProgressBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxAction = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxMapType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.numericMapSeed = new System.Windows.Forms.NumericUpDown();
			this.buttonGenerate = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxSmoothWalls = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.checkBoxPopulate = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericMapSeed)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// progressBarGeneration
			// 
			this.progressBarGeneration.Location = new System.Drawing.Point(16, 24);
			this.progressBarGeneration.Name = "progressBarGeneration";
			this.progressBarGeneration.Size = new System.Drawing.Size(264, 15);
			this.progressBarGeneration.Step = 1;
			this.progressBarGeneration.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBarGeneration.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxAction);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.progressBarGeneration);
			this.groupBox1.Location = new System.Drawing.Point(72, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(296, 80);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Progress";
			// 
			// textBoxAction
			// 
			this.textBoxAction.Location = new System.Drawing.Point(72, 48);
			this.textBoxAction.Name = "textBoxAction";
			this.textBoxAction.ReadOnly = true;
			this.textBoxAction.Size = new System.Drawing.Size(208, 20);
			this.textBoxAction.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Action:";
			// 
			// comboBoxMapType
			// 
			this.comboBoxMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMapType.FormattingEnabled = true;
			this.comboBoxMapType.Items.AddRange(new object[] {
									"Crossroads",
									"Dungeon"});
			this.comboBoxMapType.Location = new System.Drawing.Point(88, 32);
			this.comboBoxMapType.Name = "comboBoxMapType";
			this.comboBoxMapType.Size = new System.Drawing.Size(96, 21);
			this.comboBoxMapType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Map Type";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Map Seed";
			// 
			// numericMapSeed
			// 
			this.numericMapSeed.Location = new System.Drawing.Point(88, 56);
			this.numericMapSeed.Name = "numericMapSeed";
			this.numericMapSeed.Size = new System.Drawing.Size(96, 20);
			this.numericMapSeed.TabIndex = 5;
			// 
			// buttonGenerate
			// 
			this.buttonGenerate.Location = new System.Drawing.Point(64, 96);
			this.buttonGenerate.Name = "buttonGenerate";
			this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
			this.buttonGenerate.TabIndex = 6;
			this.buttonGenerate.Text = "Generate";
			this.buttonGenerate.UseVisualStyleBackColor = true;
			this.buttonGenerate.Click += new System.EventHandler(this.ButtonGenerateClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxPopulate);
			this.groupBox2.Controls.Add(this.checkBoxSmoothWalls);
			this.groupBox2.Location = new System.Drawing.Point(224, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(208, 160);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Optional settings";
			// 
			// checkBoxSmoothWalls
			// 
			this.checkBoxSmoothWalls.Enabled = false;
			this.checkBoxSmoothWalls.Location = new System.Drawing.Point(16, 120);
			this.checkBoxSmoothWalls.Name = "checkBoxSmoothWalls";
			this.checkBoxSmoothWalls.Size = new System.Drawing.Size(168, 24);
			this.checkBoxSmoothWalls.TabIndex = 0;
			this.checkBoxSmoothWalls.Text = "Make triple-sided walls";
			this.checkBoxSmoothWalls.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.comboBoxMapType);
			this.groupBox3.Controls.Add(this.buttonGenerate);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.numericMapSeed);
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 128);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Main settings";
			// 
			// checkBoxPopulate
			// 
			this.checkBoxPopulate.Location = new System.Drawing.Point(16, 24);
			this.checkBoxPopulate.Name = "checkBoxPopulate";
			this.checkBoxPopulate.Size = new System.Drawing.Size(168, 24);
			this.checkBoxPopulate.TabIndex = 1;
			this.checkBoxPopulate.Text = "Populate with objects";
			this.checkBoxPopulate.UseVisualStyleBackColor = true;
			// 
			// MapGeneratorDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(443, 262);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MapGeneratorDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Random Map Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapGeneratorDlgFormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericMapSeed)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox checkBoxPopulate;
		private System.Windows.Forms.CheckBox checkBoxSmoothWalls;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonGenerate;
		private System.Windows.Forms.NumericUpDown numericMapSeed;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxMapType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxAction;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ProgressBar progressBarGeneration;
	}
}
