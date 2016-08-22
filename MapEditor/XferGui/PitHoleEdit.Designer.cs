/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 05.11.2014
 */
namespace MapEditor.XferGui
{
	partial class PitHoleEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.exitY = new System.Windows.Forms.TextBox();
            this.exitX = new System.Windows.Forms.TextBox();
            this.lCoordY = new System.Windows.Forms.Label();
            this.lCoordX = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scriptTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scriptFn = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCenter = new System.Windows.Forms.Button();
            this.pasteButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pasteButton);
            this.groupBox1.Controls.Add(this.exitY);
            this.groupBox1.Controls.Add(this.exitX);
            this.groupBox1.Controls.Add(this.lCoordY);
            this.groupBox1.Controls.Add(this.lCoordX);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exit coordinates";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // exitY
            // 
            this.exitY.Location = new System.Drawing.Point(40, 53);
            this.exitY.Name = "exitY";
            this.exitY.Size = new System.Drawing.Size(98, 20);
            this.exitY.TabIndex = 3;
            // 
            // exitX
            // 
            this.exitX.Location = new System.Drawing.Point(40, 23);
            this.exitX.Name = "exitX";
            this.exitX.Size = new System.Drawing.Size(98, 20);
            this.exitX.TabIndex = 2;
            // 
            // lCoordY
            // 
            this.lCoordY.Location = new System.Drawing.Point(10, 56);
            this.lCoordY.Name = "lCoordY";
            this.lCoordY.Size = new System.Drawing.Size(25, 23);
            this.lCoordY.TabIndex = 1;
            this.lCoordY.Text = "Y";
            // 
            // lCoordX
            // 
            this.lCoordX.Location = new System.Drawing.Point(10, 26);
            this.lCoordX.Name = "lCoordX";
            this.lCoordX.Size = new System.Drawing.Size(25, 23);
            this.lCoordX.TabIndex = 0;
            this.lCoordX.Text = "X";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scriptTime);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.scriptFn);
            this.groupBox2.Location = new System.Drawing.Point(12, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Script event - Pit broken (Enabled)";
            // 
            // scriptTime
            // 
            this.scriptTime.Location = new System.Drawing.Point(79, 39);
            this.scriptTime.Name = "scriptTime";
            this.scriptTime.Size = new System.Drawing.Size(121, 20);
            this.scriptTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Timeout";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Function";
            // 
            // scriptFn
            // 
            this.scriptFn.Location = new System.Drawing.Point(79, 16);
            this.scriptFn.Name = "scriptFn";
            this.scriptFn.Size = new System.Drawing.Size(121, 20);
            this.scriptFn.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(34, 184);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 25);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCenter
            // 
            this.buttonCenter.Location = new System.Drawing.Point(129, 184);
            this.buttonCenter.Name = "buttonCenter";
            this.buttonCenter.Size = new System.Drawing.Size(71, 23);
            this.buttonCenter.TabIndex = 3;
            this.buttonCenter.Text = "Center";
            this.buttonCenter.UseVisualStyleBackColor = true;
            this.buttonCenter.Click += new System.EventHandler(this.ButtonCenterClick);
            // 
            // pasteButton
            // 
            this.pasteButton.Location = new System.Drawing.Point(149, 35);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(53, 23);
            this.pasteButton.TabIndex = 4;
            this.pasteButton.Text = "Paste";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // PitHoleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 219);
            this.Controls.Add(this.buttonCenter);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PitHoleEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HoleXfer";
            this.Load += new System.EventHandler(this.PitHoleEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button buttonCenter;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox scriptFn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox scriptTime;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lCoordX;
		private System.Windows.Forms.Label lCoordY;
		private System.Windows.Forms.TextBox exitX;
		private System.Windows.Forms.TextBox exitY;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button pasteButton;
	}
}
