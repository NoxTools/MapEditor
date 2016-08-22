namespace MapEditor
{
    partial class ObjectShopEditorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.enchant4 = new System.Windows.Forms.ComboBox();
            this.enchant3 = new System.Windows.Forms.ComboBox();
            this.enchant2 = new System.Windows.Forms.ComboBox();
            this.enchant1 = new System.Windows.Forms.ComboBox();
            this.objname = new System.Windows.Forms.ComboBox();
            this.numitems = new System.Windows.Forms.ComboBox();
            this.cbospells = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.okButton.Location = new System.Drawing.Point(41, 304);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(74, 30);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "Ok";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // enchant4
            // 
            this.enchant4.FormattingEnabled = true;
            this.enchant4.ItemHeight = 13;
            this.enchant4.Location = new System.Drawing.Point(16, 109);
            this.enchant4.Name = "enchant4";
            this.enchant4.Size = new System.Drawing.Size(112, 21);
            this.enchant4.Sorted = true;
            this.enchant4.TabIndex = 12;
            // 
            // enchant3
            // 
            this.enchant3.FormattingEnabled = true;
            this.enchant3.ItemHeight = 13;
            this.enchant3.Location = new System.Drawing.Point(16, 82);
            this.enchant3.Name = "enchant3";
            this.enchant3.Size = new System.Drawing.Size(112, 21);
            this.enchant3.Sorted = true;
            this.enchant3.TabIndex = 11;
            // 
            // enchant2
            // 
            this.enchant2.FormattingEnabled = true;
            this.enchant2.ItemHeight = 13;
            this.enchant2.Location = new System.Drawing.Point(16, 55);
            this.enchant2.Name = "enchant2";
            this.enchant2.Size = new System.Drawing.Size(112, 21);
            this.enchant2.Sorted = true;
            this.enchant2.TabIndex = 10;
            // 
            // enchant1
            // 
            this.enchant1.FormattingEnabled = true;
            this.enchant1.ItemHeight = 13;
            this.enchant1.Location = new System.Drawing.Point(16, 28);
            this.enchant1.Name = "enchant1";
            this.enchant1.Size = new System.Drawing.Size(112, 21);
            this.enchant1.Sorted = true;
            this.enchant1.TabIndex = 9;
            // 
            // objname
            // 
            this.objname.FormattingEnabled = true;
            this.objname.Location = new System.Drawing.Point(12, 28);
            this.objname.Name = "objname";
            this.objname.Size = new System.Drawing.Size(138, 21);
            this.objname.TabIndex = 15;
            this.objname.SelectedIndexChanged += new System.EventHandler(this.objname_SelectedIndexChanged);
            // 
            // numitems
            // 
            this.numitems.FormattingEnabled = true;
            this.numitems.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32"});
            this.numitems.Location = new System.Drawing.Point(12, 71);
            this.numitems.Name = "numitems";
            this.numitems.Size = new System.Drawing.Size(74, 21);
            this.numitems.TabIndex = 16;
            // 
            // cbospells
            // 
            this.cbospells.Enabled = false;
            this.cbospells.FormattingEnabled = true;
            this.cbospells.Location = new System.Drawing.Point(16, 19);
            this.cbospells.Name = "cbospells";
            this.cbospells.Size = new System.Drawing.Size(112, 21);
            this.cbospells.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enchant1);
            this.groupBox1.Controls.Add(this.enchant2);
            this.groupBox1.Controls.Add(this.enchant3);
            this.groupBox1.Controls.Add(this.enchant4);
            this.groupBox1.Location = new System.Drawing.Point(12, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 145);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enchantments";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbospells);
            this.groupBox2.Location = new System.Drawing.Point(12, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 49);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Spell / Ability";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Object Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Num";
            // 
            // ObjectShopEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 338);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numitems);
            this.Controls.Add(this.objname);
            this.Controls.Add(this.okButton);
            this.Name = "ObjectShopEditorDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Object Editor";
            this.Load += new System.EventHandler(this.ObjectShopEditorDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox enchant4;
        private System.Windows.Forms.ComboBox enchant3;
        private System.Windows.Forms.ComboBox enchant2;
        private System.Windows.Forms.ComboBox enchant1;
        private System.Windows.Forms.ComboBox objname;
        private System.Windows.Forms.ComboBox numitems;
        private System.Windows.Forms.ComboBox cbospells;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}