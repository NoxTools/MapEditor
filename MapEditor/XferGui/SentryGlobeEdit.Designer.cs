/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 14.11.2014
 */
namespace MapEditor.XferGui
{
	partial class SentryGlobeEdit
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
            this.sentryAngle = new System.Windows.Forms.TextBox();
            this.sentrySpeed = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Angle (radians)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rotation speed";
            // 
            // sentryAngle
            // 
            this.sentryAngle.Location = new System.Drawing.Point(110, 12);
            this.sentryAngle.Name = "sentryAngle";
            this.sentryAngle.Size = new System.Drawing.Size(100, 20);
            this.sentryAngle.TabIndex = 2;
            // 
            // sentrySpeed
            // 
            this.sentrySpeed.Location = new System.Drawing.Point(110, 35);
            this.sentrySpeed.Name = "sentrySpeed";
            this.sentrySpeed.Size = new System.Drawing.Size(100, 20);
            this.sentrySpeed.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(73, 67);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // SentryGlobeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 102);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.sentrySpeed);
            this.Controls.Add(this.sentryAngle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SentryGlobeEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SentryGlobeXfer";
            this.Load += new System.EventHandler(this.SentryGlobeEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox sentrySpeed;
		private System.Windows.Forms.TextBox sentryAngle;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
