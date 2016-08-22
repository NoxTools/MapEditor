/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 23.01.2015
 */
namespace MapEditor.newgui
{
	partial class EdgeMakeTab
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
            this.components = new System.ComponentModel.Container();
            this.comboEdgeType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listEdgeImages = new System.Windows.Forms.ListView();
            this.ignoreAllBox = new System.Windows.Forms.CheckBox();
            this.AutoEgeBox = new System.Windows.Forms.CheckBox();
            this.preserveBox = new System.Windows.Forms.CheckBox();
            this.AutoEdgeGrp = new System.Windows.Forms.GroupBox();
            this.Picker = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.AutoEdgeGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboEdgeType
            // 
            this.comboEdgeType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboEdgeType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboEdgeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEdgeType.FormattingEnabled = true;
            this.comboEdgeType.Location = new System.Drawing.Point(88, 8);
            this.comboEdgeType.MaxDropDownItems = 20;
            this.comboEdgeType.Name = "comboEdgeType";
            this.comboEdgeType.Size = new System.Drawing.Size(121, 21);
            this.comboEdgeType.TabIndex = 3;
            this.comboEdgeType.SelectedIndexChanged += new System.EventHandler(this.UpdateListView);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Edge type:";
            // 
            // listEdgeImages
            // 
            this.listEdgeImages.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listEdgeImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listEdgeImages.GridLines = true;
            this.listEdgeImages.Location = new System.Drawing.Point(6, 146);
            this.listEdgeImages.Name = "listEdgeImages";
            this.listEdgeImages.Size = new System.Drawing.Size(205, 390);
            this.listEdgeImages.TabIndex = 4;
            this.listEdgeImages.TileSize = new System.Drawing.Size(46, 46);
            this.listEdgeImages.UseCompatibleStateImageBehavior = false;
            this.listEdgeImages.VirtualMode = true;
            this.listEdgeImages.SelectedIndexChanged += new System.EventHandler(this.SelectEdgeDirection);
            this.listEdgeImages.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listEdgeImages_MouseClick);
            // 
            // ignoreAllBox
            // 
            this.ignoreAllBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ignoreAllBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ignoreAllBox.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ignoreAllBox.Location = new System.Drawing.Point(6, 19);
            this.ignoreAllBox.Name = "ignoreAllBox";
            this.ignoreAllBox.Size = new System.Drawing.Size(197, 18);
            this.ignoreAllBox.TabIndex = 5;
            this.ignoreAllBox.Text = "Ignore all except";
            this.ignoreAllBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ignoreAllBox.UseVisualStyleBackColor = true;
            this.ignoreAllBox.CheckedChanged += new System.EventHandler(this.ignoreAllBox_CheckedChanged);
            // 
            // AutoEgeBox
            // 
            this.AutoEgeBox.AutoSize = true;
            this.AutoEgeBox.Location = new System.Drawing.Point(10, 47);
            this.AutoEgeBox.Name = "AutoEgeBox";
            this.AutoEgeBox.Size = new System.Drawing.Size(76, 17);
            this.AutoEgeBox.TabIndex = 6;
            this.AutoEgeBox.Text = "Auto Edge";
            this.AutoEgeBox.UseVisualStyleBackColor = true;
            this.AutoEgeBox.CheckedChanged += new System.EventHandler(this.AutoEgeBox_CheckedChanged_1);
            // 
            // preserveBox
            // 
            this.preserveBox.AutoSize = true;
            this.preserveBox.Location = new System.Drawing.Point(6, 42);
            this.preserveBox.Name = "preserveBox";
            this.preserveBox.Size = new System.Drawing.Size(68, 17);
            this.preserveBox.TabIndex = 7;
            this.preserveBox.Text = "Preserve";
            this.preserveBox.UseVisualStyleBackColor = true;
            // 
            // AutoEdgeGrp
            // 
            this.AutoEdgeGrp.Controls.Add(this.preserveBox);
            this.AutoEdgeGrp.Controls.Add(this.ignoreAllBox);
            this.AutoEdgeGrp.Enabled = false;
            this.AutoEdgeGrp.Location = new System.Drawing.Point(6, 73);
            this.AutoEdgeGrp.Name = "AutoEdgeGrp";
            this.AutoEdgeGrp.Size = new System.Drawing.Size(205, 65);
            this.AutoEdgeGrp.TabIndex = 8;
            this.AutoEdgeGrp.TabStop = false;
            this.AutoEdgeGrp.Text = "Auto Edge options";
            this.AutoEdgeGrp.Enter += new System.EventHandler(this.AutoEdgeGrp_Enter);
            // 
            // Picker
            // 
            this.Picker.Appearance = System.Windows.Forms.Appearance.Button;
            this.Picker.BackgroundImage = global::MapEditor.Properties.Resources.color_picker;
            this.Picker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Picker.Location = new System.Drawing.Point(179, 37);
            this.Picker.Name = "Picker";
            this.Picker.Size = new System.Drawing.Size(30, 30);
            this.Picker.TabIndex = 39;
            this.toolTip1.SetToolTip(this.Picker, "Tile Picker. (Ctrl+A)");
            this.Picker.UseVisualStyleBackColor = true;
            this.Picker.CheckedChanged += new System.EventHandler(this.Picker_CheckedChanged);
            // 
            // EdgeMakeTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Picker);
            this.Controls.Add(this.AutoEgeBox);
            this.Controls.Add(this.AutoEdgeGrp);
            this.Controls.Add(this.listEdgeImages);
            this.Controls.Add(this.comboEdgeType);
            this.Controls.Add(this.label1);
            this.Name = "EdgeMakeTab";
            this.Size = new System.Drawing.Size(216, 543);
            this.Load += new System.EventHandler(this.EdgeMakeTab_Load);
            this.AutoEdgeGrp.ResumeLayout(false);
            this.AutoEdgeGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ListView listEdgeImages;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox ignoreAllBox;
        public System.Windows.Forms.CheckBox AutoEgeBox;
        public System.Windows.Forms.CheckBox preserveBox;
        private System.Windows.Forms.GroupBox AutoEdgeGrp;
        public System.Windows.Forms.CheckBox Picker;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ComboBox comboEdgeType;
	}
}
