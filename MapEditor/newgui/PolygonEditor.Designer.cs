/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 12.02.2015
 */
namespace MapEditor.newgui
{
	partial class PolygonEditor
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
            this.listBoxPolygons = new System.Windows.Forms.ListBox();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonPoints = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.ambientColors = new System.Windows.Forms.CheckBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.LockedBox = new System.Windows.Forms.CheckBox();
            this.snapPoly = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxPolygons
            // 
            this.listBoxPolygons.FormattingEnabled = true;
            this.listBoxPolygons.Location = new System.Drawing.Point(8, 12);
            this.listBoxPolygons.Name = "listBoxPolygons";
            this.listBoxPolygons.ScrollAlwaysVisible = true;
            this.listBoxPolygons.Size = new System.Drawing.Size(128, 134);
            this.listBoxPolygons.TabIndex = 0;
            this.listBoxPolygons.SelectedIndexChanged += new System.EventHandler(this.listBoxPolygons_SelectedIndexChanged);
            this.listBoxPolygons.SelectedValueChanged += new System.EventHandler(this.listBoxPolygons_SelectedValueChanged);
            this.listBoxPolygons.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.listBoxPolygons_ControlAdded);
            this.listBoxPolygons.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.listBoxPolygons_ControlRemoved);
            this.listBoxPolygons.DoubleClick += new System.EventHandler(this.ButtonModifyClick);
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(64, 14);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(48, 23);
            this.buttonDown.TabIndex = 3;
            this.buttonDown.Text = "Down";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.ButtonDownClick);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(8, 14);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(48, 23);
            this.buttonUp.TabIndex = 4;
            this.buttonUp.Text = "Up";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.ButtonUpClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDown);
            this.groupBox1.Controls.Add(this.buttonUp);
            this.groupBox1.Location = new System.Drawing.Point(144, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 44);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Priority";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonPoints);
            this.groupBox2.Controls.Add(this.buttonNew);
            this.groupBox2.Controls.Add(this.ambientColors);
            this.groupBox2.Controls.Add(this.buttonDelete);
            this.groupBox2.Controls.Add(this.buttonModify);
            this.groupBox2.Location = new System.Drawing.Point(144, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 80);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operations";
            // 
            // buttonPoints
            // 
            this.buttonPoints.Location = new System.Drawing.Point(62, 20);
            this.buttonPoints.Name = "buttonPoints";
            this.buttonPoints.Size = new System.Drawing.Size(48, 23);
            this.buttonPoints.TabIndex = 3;
            this.buttonPoints.Text = "Points";
            this.buttonPoints.UseVisualStyleBackColor = true;
            this.buttonPoints.Click += new System.EventHandler(this.ButtonPointsClick);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(10, 20);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(48, 23);
            this.buttonNew.TabIndex = 2;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.ButtonNewClick);
            // 
            // ambientColors
            // 
            this.ambientColors.AutoSize = true;
            this.ambientColors.Location = new System.Drawing.Point(6, 73);
            this.ambientColors.Name = "ambientColors";
            this.ambientColors.Size = new System.Drawing.Size(122, 17);
            this.ambientColors.TabIndex = 9;
            this.ambientColors.Text = "show ambient colors";
            this.ambientColors.UseVisualStyleBackColor = true;
            this.ambientColors.Visible = false;
            this.ambientColors.CheckedChanged += new System.EventHandler(this.ambientColors_CheckedChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(62, 49);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(48, 23);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDeleteClick);
            // 
            // buttonModify
            // 
            this.buttonModify.Enabled = false;
            this.buttonModify.Location = new System.Drawing.Point(10, 49);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(48, 23);
            this.buttonModify.TabIndex = 0;
            this.buttonModify.Text = "Modify";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.ButtonModifyClick);
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(168, 163);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(72, 23);
            this.buttonDone.TabIndex = 8;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.ButtonDoneClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(143, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Shift+Rclick to add a point";
            // 
            // LockedBox
            // 
            this.LockedBox.AutoSize = true;
            this.LockedBox.Location = new System.Drawing.Point(8, 171);
            this.LockedBox.Name = "LockedBox";
            this.LockedBox.Size = new System.Drawing.Size(127, 17);
            this.LockedBox.TabIndex = 11;
            this.LockedBox.Text = "Lock current Polygon";
            this.LockedBox.UseVisualStyleBackColor = true;
            this.LockedBox.CheckedChanged += new System.EventHandler(this.LockedBox_CheckedChanged);
            // 
            // snapPoly
            // 
            this.snapPoly.AutoSize = true;
            this.snapPoly.Location = new System.Drawing.Point(8, 151);
            this.snapPoly.Name = "snapPoly";
            this.snapPoly.Size = new System.Drawing.Size(92, 17);
            this.snapPoly.TabIndex = 12;
            this.snapPoly.Text = "Polygon Snap";
            this.snapPoly.UseVisualStyleBackColor = true;
            // 
            // PolygonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(273, 191);
            this.Controls.Add(this.snapPoly);
            this.Controls.Add(this.LockedBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxPolygons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PolygonEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PolygonEditor";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.PolygonEditor_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PolygonEditor_FormClosing);
            this.Load += new System.EventHandler(this.PolygonEditorLoad);
            this.Shown += new System.EventHandler(this.PolygonEditor_Shown);
            this.VisibleChanged += new System.EventHandler(this.PolygonEditor_VisibleChanged);
            this.MouseLeave += new System.EventHandler(this.PolygonEditor_MouseLeave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button buttonModify;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Button buttonPoints;
		private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        public System.Windows.Forms.CheckBox ambientColors;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox listBoxPolygons;
        public System.Windows.Forms.CheckBox LockedBox;
        public System.Windows.Forms.CheckBox snapPoly;
	}
}
