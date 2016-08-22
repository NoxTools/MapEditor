/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 12.07.2015
 */
namespace MapEditor.XferGui
{
	partial class GlyphEdit
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxTargX = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxTargY = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxSpell3 = new System.Windows.Forms.ComboBox();
			this.comboBoxSpell2 = new System.Windows.Forms.ComboBox();
			this.comboBoxSpell1 = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonDone
			// 
			this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonDone.Location = new System.Drawing.Point(72, 144);
			this.buttonDone.Name = "buttonDone";
			this.buttonDone.Size = new System.Drawing.Size(75, 23);
			this.buttonDone.TabIndex = 0;
			this.buttonDone.Text = "Done";
			this.buttonDone.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Target X";
			// 
			// textBoxTargX
			// 
			this.textBoxTargX.Location = new System.Drawing.Point(72, 8);
			this.textBoxTargX.Name = "textBoxTargX";
			this.textBoxTargX.Size = new System.Drawing.Size(40, 20);
			this.textBoxTargX.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(120, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Y";
			// 
			// textBoxTargY
			// 
			this.textBoxTargY.Location = new System.Drawing.Point(152, 8);
			this.textBoxTargY.Name = "textBoxTargY";
			this.textBoxTargY.Size = new System.Drawing.Size(40, 20);
			this.textBoxTargY.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxSpell3);
			this.groupBox1.Controls.Add(this.comboBoxSpell2);
			this.groupBox1.Controls.Add(this.comboBoxSpell1);
			this.groupBox1.Location = new System.Drawing.Point(8, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 104);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spells";
			// 
			// comboBoxSpell3
			// 
			this.comboBoxSpell3.FormattingEnabled = true;
			this.comboBoxSpell3.Location = new System.Drawing.Point(16, 72);
			this.comboBoxSpell3.Name = "comboBoxSpell3";
			this.comboBoxSpell3.Size = new System.Drawing.Size(168, 21);
			this.comboBoxSpell3.TabIndex = 2;
			// 
			// comboBoxSpell2
			// 
			this.comboBoxSpell2.FormattingEnabled = true;
			this.comboBoxSpell2.Location = new System.Drawing.Point(16, 48);
			this.comboBoxSpell2.Name = "comboBoxSpell2";
			this.comboBoxSpell2.Size = new System.Drawing.Size(168, 21);
			this.comboBoxSpell2.TabIndex = 1;
			// 
			// comboBoxSpell1
			// 
			this.comboBoxSpell1.FormattingEnabled = true;
			this.comboBoxSpell1.Location = new System.Drawing.Point(16, 24);
			this.comboBoxSpell1.Name = "comboBoxSpell1";
			this.comboBoxSpell1.Size = new System.Drawing.Size(168, 21);
			this.comboBoxSpell1.TabIndex = 0;
			// 
			// GlyphEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(215, 176);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBoxTargY);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxTargX);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonDone);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GlyphEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Wizard\'s trap (GlyphXfer)";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ComboBox comboBoxSpell1;
		private System.Windows.Forms.ComboBox comboBoxSpell2;
		private System.Windows.Forms.ComboBox comboBoxSpell3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxTargY;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxTargX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDone;
	}
}
