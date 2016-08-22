/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 12.07.2015
 */
namespace MapEditor.XferGui
{
	partial class BomberSpells
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
            this.comboBoxSpell1 = new System.Windows.Forms.ComboBox();
            this.comboBoxSpell2 = new System.Windows.Forms.ComboBox();
            this.comboBoxSpell3 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDone.Location = new System.Drawing.Point(40, 88);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.ButtonDoneClick);
            // 
            // comboBoxSpell1
            // 
            this.comboBoxSpell1.FormattingEnabled = true;
            this.comboBoxSpell1.Location = new System.Drawing.Point(16, 8);
            this.comboBoxSpell1.Name = "comboBoxSpell1";
            this.comboBoxSpell1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSpell1.TabIndex = 1;
            // 
            // comboBoxSpell2
            // 
            this.comboBoxSpell2.FormattingEnabled = true;
            this.comboBoxSpell2.Location = new System.Drawing.Point(16, 32);
            this.comboBoxSpell2.Name = "comboBoxSpell2";
            this.comboBoxSpell2.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSpell2.TabIndex = 2;
            // 
            // comboBoxSpell3
            // 
            this.comboBoxSpell3.FormattingEnabled = true;
            this.comboBoxSpell3.Location = new System.Drawing.Point(16, 56);
            this.comboBoxSpell3.Name = "comboBoxSpell3";
            this.comboBoxSpell3.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSpell3.TabIndex = 3;
            // 
            // BomberSpells
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 120);
            this.Controls.Add(this.comboBoxSpell3);
            this.Controls.Add(this.comboBoxSpell2);
            this.Controls.Add(this.comboBoxSpell1);
            this.Controls.Add(this.buttonDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BomberSpells";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BomberSpells";
            this.Load += new System.EventHandler(this.BomberSpells_Load);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.ComboBox comboBoxSpell3;
		private System.Windows.Forms.ComboBox comboBoxSpell2;
		private System.Windows.Forms.ComboBox comboBoxSpell1;
		private System.Windows.Forms.Button buttonDone;
	}
}
