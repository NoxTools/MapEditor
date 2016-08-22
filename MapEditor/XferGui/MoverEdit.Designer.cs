/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 15.11.2014
 */
namespace MapEditor.XferGui
{
	partial class MoverEdit
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
            this.moverStatusBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.movingSpeed = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.waypointID = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.movedObjExtent = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.loopWaypointA = new System.Windows.Forms.NumericUpDown();
            this.loopWaypointB = new System.Windows.Forms.NumericUpDown();
            this.moverAccel = new System.Windows.Forms.TextBox();
            this.moverSpeed = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.movingSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movedObjExtent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopWaypointA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopWaypointB)).BeginInit();
            this.SuspendLayout();
            // 
            // moverStatusBox
            // 
            this.moverStatusBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moverStatusBox.FormattingEnabled = true;
            this.moverStatusBox.Items.AddRange(new object[] {
            "Starting movement",
            "Move loop",
            "Mover pos update",
            "Inactive"});
            this.moverStatusBox.Location = new System.Drawing.Point(134, 90);
            this.moverStatusBox.Name = "moverStatusBox";
            this.moverStatusBox.Size = new System.Drawing.Size(121, 21);
            this.moverStatusBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Moving speed";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // movingSpeed
            // 
            this.movingSpeed.Location = new System.Drawing.Point(136, 12);
            this.movingSpeed.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.movingSpeed.Name = "movingSpeed";
            this.movingSpeed.Size = new System.Drawing.Size(120, 20);
            this.movingSpeed.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start waypoint ID";
            // 
            // waypointID
            // 
            this.waypointID.Location = new System.Drawing.Point(136, 38);
            this.waypointID.Name = "waypointID";
            this.waypointID.Size = new System.Drawing.Size(120, 20);
            this.waypointID.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Linked object extent";
            // 
            // movedObjExtent
            // 
            this.movedObjExtent.Location = new System.Drawing.Point(135, 64);
            this.movedObjExtent.Name = "movedObjExtent";
            this.movedObjExtent.Size = new System.Drawing.Size(120, 20);
            this.movedObjExtent.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mover status";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Waypoint loop A";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 9;
            this.label6.Text = "Waypoint loop B";
            // 
            // loopWaypointA
            // 
            this.loopWaypointA.Location = new System.Drawing.Point(134, 119);
            this.loopWaypointA.Name = "loopWaypointA";
            this.loopWaypointA.Size = new System.Drawing.Size(122, 20);
            this.loopWaypointA.TabIndex = 10;
            // 
            // loopWaypointB
            // 
            this.loopWaypointB.Location = new System.Drawing.Point(134, 145);
            this.loopWaypointB.Name = "loopWaypointB";
            this.loopWaypointB.Size = new System.Drawing.Size(122, 20);
            this.loopWaypointB.TabIndex = 11;
            // 
            // moverAccel
            // 
            this.moverAccel.Location = new System.Drawing.Point(134, 171);
            this.moverAccel.Name = "moverAccel";
            this.moverAccel.Size = new System.Drawing.Size(120, 20);
            this.moverAccel.TabIndex = 12;
            // 
            // moverSpeed
            // 
            this.moverSpeed.Location = new System.Drawing.Point(134, 197);
            this.moverSpeed.Name = "moverSpeed";
            this.moverSpeed.Size = new System.Drawing.Size(121, 20);
            this.moverSpeed.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 14;
            this.label7.Text = "Unused FP 1";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(12, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 15;
            this.label8.Text = "Unused FP 2";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(93, 228);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // MoverEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 263);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.moverSpeed);
            this.Controls.Add(this.moverAccel);
            this.Controls.Add(this.loopWaypointB);
            this.Controls.Add(this.loopWaypointA);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.movedObjExtent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.waypointID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.movingSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.moverStatusBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoverEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MoverXfer";
            this.Load += new System.EventHandler(this.MoverEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.movingSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movedObjExtent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopWaypointA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopWaypointB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox moverSpeed;
		private System.Windows.Forms.TextBox moverAccel;
		private System.Windows.Forms.NumericUpDown loopWaypointB;
		private System.Windows.Forms.NumericUpDown loopWaypointA;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown movedObjExtent;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown waypointID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown movingSpeed;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox moverStatusBox;
	}
}
