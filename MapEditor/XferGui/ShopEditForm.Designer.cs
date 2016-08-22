/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 25.10.2014
 */
namespace MapEditor.XferGui
{
	partial class ShopEditForm
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
			this.label2 = new System.Windows.Forms.Label();
			this.priceBuyMul = new System.Windows.Forms.TextBox();
			this.priceSellMul = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dialogText = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.objCount = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.enchant4 = new System.Windows.Forms.ComboBox();
			this.enchant3 = new System.Windows.Forms.ComboBox();
			this.enchant2 = new System.Windows.Forms.ComboBox();
			this.enchant1 = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.spellID = new System.Windows.Forms.TextBox();
			this.objectID = new System.Windows.Forms.TextBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.itemsListBox = new System.Windows.Forms.ListBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.objCount)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 325);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Buy Price Multiplier";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 348);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Sell Price Multiplier";
			// 
			// priceBuyMul
			// 
			this.priceBuyMul.Location = new System.Drawing.Point(153, 322);
			this.priceBuyMul.Name = "priceBuyMul";
			this.priceBuyMul.Size = new System.Drawing.Size(106, 20);
			this.priceBuyMul.TabIndex = 2;
			// 
			// priceSellMul
			// 
			this.priceSellMul.Location = new System.Drawing.Point(153, 345);
			this.priceSellMul.Name = "priceSellMul";
			this.priceSellMul.Size = new System.Drawing.Size(106, 20);
			this.priceSellMul.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 371);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(117, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Dialog Text";
			// 
			// dialogText
			// 
			this.dialogText.Location = new System.Drawing.Point(147, 368);
			this.dialogText.Name = "dialogText";
			this.dialogText.Size = new System.Drawing.Size(112, 20);
			this.dialogText.TabIndex = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.objCount);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.enchant4);
			this.groupBox1.Controls.Add(this.enchant3);
			this.groupBox1.Controls.Add(this.enchant2);
			this.groupBox1.Controls.Add(this.enchant1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.buttonRemove);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.spellID);
			this.groupBox1.Controls.Add(this.objectID);
			this.groupBox1.Controls.Add(this.buttonAdd);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.itemsListBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(259, 304);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Items";
			// 
			// objCount
			// 
			this.objCount.Location = new System.Drawing.Point(135, 94);
			this.objCount.Maximum = new decimal(new int[] {
									255,
									0,
									0,
									0});
			this.objCount.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.objCount.Name = "objCount";
			this.objCount.Size = new System.Drawing.Size(93, 20);
			this.objCount.TabIndex = 13;
			this.objCount.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(141, 68);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 23);
			this.label7.TabIndex = 12;
			this.label7.Text = "Count";
			// 
			// enchant4
			// 
			this.enchant4.Enabled = false;
			this.enchant4.FormattingEnabled = true;
			this.enchant4.Location = new System.Drawing.Point(135, 272);
			this.enchant4.Name = "enchant4";
			this.enchant4.Size = new System.Drawing.Size(106, 21);
			this.enchant4.TabIndex = 11;
			// 
			// enchant3
			// 
			this.enchant3.Enabled = false;
			this.enchant3.FormattingEnabled = true;
			this.enchant3.Location = new System.Drawing.Point(135, 245);
			this.enchant3.Name = "enchant3";
			this.enchant3.Size = new System.Drawing.Size(106, 21);
			this.enchant3.TabIndex = 10;
			// 
			// enchant2
			// 
			this.enchant2.Enabled = false;
			this.enchant2.FormattingEnabled = true;
			this.enchant2.Location = new System.Drawing.Point(135, 218);
			this.enchant2.Name = "enchant2";
			this.enchant2.Size = new System.Drawing.Size(106, 21);
			this.enchant2.TabIndex = 9;
			// 
			// enchant1
			// 
			this.enchant1.Enabled = false;
			this.enchant1.FormattingEnabled = true;
			this.enchant1.Location = new System.Drawing.Point(135, 192);
			this.enchant1.Name = "enchant1";
			this.enchant1.Size = new System.Drawing.Size(106, 21);
			this.enchant1.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(141, 166);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(87, 23);
			this.label6.TabIndex = 7;
			this.label6.Text = "Enchantments";
			// 
			// buttonRemove
			// 
			this.buttonRemove.Location = new System.Drawing.Point(62, 256);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(64, 23);
			this.buttonRemove.TabIndex = 6;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.ButtonRemoveClick);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(141, 117);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 5;
			this.label5.Text = "Spell / Monster ID";
			// 
			// spellID
			// 
			this.spellID.Enabled = false;
			this.spellID.Location = new System.Drawing.Point(135, 143);
			this.spellID.Name = "spellID";
			this.spellID.Size = new System.Drawing.Size(106, 20);
			this.spellID.TabIndex = 4;
			// 
			// objectID
			// 
			this.objectID.Location = new System.Drawing.Point(135, 45);
			this.objectID.Name = "objectID";
			this.objectID.Size = new System.Drawing.Size(106, 20);
			this.objectID.TabIndex = 3;
			this.objectID.TextChanged += new System.EventHandler(this.ObjectIDTextChanged);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(6, 256);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(50, 23);
			this.buttonAdd.TabIndex = 2;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(141, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "Object ID";
			// 
			// itemsListBox
			// 
			this.itemsListBox.FormattingEnabled = true;
			this.itemsListBox.Location = new System.Drawing.Point(6, 19);
			this.itemsListBox.Name = "itemsListBox";
			this.itemsListBox.Size = new System.Drawing.Size(120, 225);
			this.itemsListBox.TabIndex = 0;
			this.itemsListBox.SelectedIndexChanged += new System.EventHandler(this.ItemsListBoxSelectedIndexChanged);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(101, 397);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
			// 
			// ShopEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 430);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dialogText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.priceSellMul);
			this.Controls.Add(this.priceBuyMul);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShopEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Shop Editor";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.objCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown objCount;
		private System.Windows.Forms.ComboBox enchant1;
		private System.Windows.Forms.ComboBox enchant2;
		private System.Windows.Forms.ComboBox enchant3;
		private System.Windows.Forms.ComboBox enchant4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.TextBox objectID;
		private System.Windows.Forms.TextBox spellID;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox itemsListBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox dialogText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox priceSellMul;
		private System.Windows.Forms.TextBox priceBuyMul;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
