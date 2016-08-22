/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 30.06.2015
 */
namespace MapEditor.XferGui
{
	partial class EquipmentEdit
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
            this.durability = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.enchantment1 = new System.Windows.Forms.ComboBox();
            this.enchantment2 = new System.Windows.Forms.ComboBox();
            this.enchantment3 = new System.Windows.Forms.ComboBox();
            this.enchantment4 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ammoMin = new System.Windows.Forms.NumericUpDown();
            this.ammoMax = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.durability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ammoMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ammoMax)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDone.Location = new System.Drawing.Point(48, 248);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(48, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Durability";
            // 
            // durability
            // 
            this.durability.Enabled = false;
            this.durability.Location = new System.Drawing.Point(32, 160);
            this.durability.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.durability.Name = "durability";
            this.durability.Size = new System.Drawing.Size(104, 20);
            this.durability.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(40, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enchantments";
            // 
            // enchantment1
            // 
            this.enchantment1.FormattingEnabled = true;
            this.enchantment1.Location = new System.Drawing.Point(24, 32);
            this.enchantment1.Name = "enchantment1";
            this.enchantment1.Size = new System.Drawing.Size(121, 21);
            this.enchantment1.TabIndex = 4;
            // 
            // enchantment2
            // 
            this.enchantment2.FormattingEnabled = true;
            this.enchantment2.Location = new System.Drawing.Point(24, 56);
            this.enchantment2.Name = "enchantment2";
            this.enchantment2.Size = new System.Drawing.Size(121, 21);
            this.enchantment2.TabIndex = 5;
            // 
            // enchantment3
            // 
            this.enchantment3.FormattingEnabled = true;
            this.enchantment3.Location = new System.Drawing.Point(24, 80);
            this.enchantment3.Name = "enchantment3";
            this.enchantment3.Size = new System.Drawing.Size(121, 21);
            this.enchantment3.TabIndex = 6;
            // 
            // enchantment4
            // 
            this.enchantment4.FormattingEnabled = true;
            this.enchantment4.Location = new System.Drawing.Point(24, 104);
            this.enchantment4.Name = "enchantment4";
            this.enchantment4.Size = new System.Drawing.Size(121, 21);
            this.enchantment4.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Charges/Arrows";
            // 
            // ammoMin
            // 
            this.ammoMin.Enabled = false;
            this.ammoMin.Location = new System.Drawing.Point(32, 208);
            this.ammoMin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ammoMin.Name = "ammoMin";
            this.ammoMin.Size = new System.Drawing.Size(48, 20);
            this.ammoMin.TabIndex = 9;
            // 
            // ammoMax
            // 
            this.ammoMax.Enabled = false;
            this.ammoMax.Location = new System.Drawing.Point(88, 208);
            this.ammoMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ammoMax.Name = "ammoMax";
            this.ammoMax.Size = new System.Drawing.Size(48, 20);
            this.ammoMax.TabIndex = 10;
            // 
            // EquipmentEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(169, 281);
            this.Controls.Add(this.ammoMax);
            this.Controls.Add(this.ammoMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.enchantment4);
            this.Controls.Add(this.enchantment3);
            this.Controls.Add(this.enchantment2);
            this.Controls.Add(this.enchantment1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.durability);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EquipmentEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Equipment";
            this.Load += new System.EventHandler(this.EquipmentEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.durability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ammoMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ammoMax)).EndInit();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.NumericUpDown ammoMax;
		private System.Windows.Forms.NumericUpDown ammoMin;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox enchantment4;
		private System.Windows.Forms.ComboBox enchantment3;
		private System.Windows.Forms.ComboBox enchantment2;
		private System.Windows.Forms.ComboBox enchantment1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown durability;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDone;
	}
}
