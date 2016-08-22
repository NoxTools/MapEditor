/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 12.11.2014
 */
namespace MapEditor.XferGui
{
	partial class ObeliskEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.manaStored = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.manaStored)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(37, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mana";
            // 
            // manaStored
            // 
            this.manaStored.Location = new System.Drawing.Point(92, 12);
            this.manaStored.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.manaStored.Name = "manaStored";
            this.manaStored.Size = new System.Drawing.Size(70, 20);
            this.manaStored.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(67, 47);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // ObeliskEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 82);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.manaStored);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObeliskEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ObeliskXfer";
            this.Load += new System.EventHandler(this.ObeliskEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.manaStored)).EndInit();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.NumericUpDown manaStored;
        private System.Windows.Forms.Label label1;
	}
}
