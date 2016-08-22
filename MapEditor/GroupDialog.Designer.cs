namespace MapEditor
{
    partial class GroupDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.groupList = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBoxTypes = new System.Windows.Forms.GroupBox();
            this.waypointRadio = new System.Windows.Forms.RadioButton();
            this.wallRadio = new System.Windows.Forms.RadioButton();
            this.objectRadio = new System.Windows.Forms.RadioButton();
            this.groupId = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.delButton = new System.Windows.Forms.Button();
            this.itemList = new System.Windows.Forms.TextBox();
            this.groupBoxTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupList
            // 
            this.groupList.FormattingEnabled = true;
            this.groupList.Location = new System.Drawing.Point(7, 3);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(273, 21);
            this.groupList.TabIndex = 1;
            this.groupList.TextChanged += new System.EventHandler(this.groupList_SelectedValueChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(27, 236);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBoxTypes
            // 
            this.groupBoxTypes.Controls.Add(this.waypointRadio);
            this.groupBoxTypes.Controls.Add(this.wallRadio);
            this.groupBoxTypes.Controls.Add(this.objectRadio);
            this.groupBoxTypes.Location = new System.Drawing.Point(7, 31);
            this.groupBoxTypes.Name = "groupBoxTypes";
            this.groupBoxTypes.Size = new System.Drawing.Size(81, 87);
            this.groupBoxTypes.TabIndex = 3;
            this.groupBoxTypes.TabStop = false;
            this.groupBoxTypes.Text = "Type";
            // 
            // waypointRadio
            // 
            this.waypointRadio.AutoSize = true;
            this.waypointRadio.Location = new System.Drawing.Point(7, 64);
            this.waypointRadio.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.waypointRadio.Name = "waypointRadio";
            this.waypointRadio.Size = new System.Drawing.Size(70, 17);
            this.waypointRadio.TabIndex = 2;
            this.waypointRadio.Text = "Waypoint";
            this.waypointRadio.CheckedChanged += new System.EventHandler(this.waypointRadio_CheckedChanged);
            // 
            // wallRadio
            // 
            this.wallRadio.AutoSize = true;
            this.wallRadio.Location = new System.Drawing.Point(7, 43);
            this.wallRadio.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.wallRadio.Name = "wallRadio";
            this.wallRadio.Size = new System.Drawing.Size(51, 17);
            this.wallRadio.TabIndex = 1;
            this.wallRadio.Text = "Walls";
            this.wallRadio.CheckedChanged += new System.EventHandler(this.wallRadio_CheckedChanged);
            // 
            // objectRadio
            // 
            this.objectRadio.AutoSize = true;
            this.objectRadio.Location = new System.Drawing.Point(7, 20);
            this.objectRadio.Name = "objectRadio";
            this.objectRadio.Size = new System.Drawing.Size(61, 17);
            this.objectRadio.TabIndex = 0;
            this.objectRadio.Text = "Objects";
            this.objectRadio.CheckedChanged += new System.EventHandler(this.objectRadio_CheckedChanged);
            // 
            // groupId
            // 
            this.groupId.Enabled = false;
            this.groupId.Location = new System.Drawing.Point(7, 125);
            this.groupId.Name = "groupId";
            this.groupId.Size = new System.Drawing.Size(69, 20);
            this.groupId.TabIndex = 5;
            this.groupId.TextChanged += new System.EventHandler(this.groupId_TextChanged);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(109, 236);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // delButton
            // 
            this.delButton.Location = new System.Drawing.Point(191, 236);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(75, 23);
            this.delButton.TabIndex = 6;
            this.delButton.Text = "Delete";
            this.delButton.Click += new System.EventHandler(this.delButton_Click);
            // 
            // itemList
            // 
            this.itemList.Location = new System.Drawing.Point(94, 31);
            this.itemList.Multiline = true;
            this.itemList.Name = "itemList";
            this.itemList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.itemList.Size = new System.Drawing.Size(186, 200);
            this.itemList.TabIndex = 7;
            // 
            // GroupDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.itemList);
            this.Controls.Add(this.delButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupId);
            this.Controls.Add(this.groupBoxTypes);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupList);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.Name = "GroupDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GroupDialog";
            this.groupBoxTypes.ResumeLayout(false);
            this.groupBoxTypes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox groupList;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox groupBoxTypes;
        private System.Windows.Forms.RadioButton objectRadio;
		private System.Windows.Forms.RadioButton wallRadio;
        private System.Windows.Forms.TextBox groupId;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.RadioButton waypointRadio;
		private System.Windows.Forms.Button delButton;
		private System.Windows.Forms.TextBox itemList;


    }
}