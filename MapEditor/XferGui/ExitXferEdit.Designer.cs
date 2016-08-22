/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 19.02.2015
 */
namespace MapEditor.XferGui
{
	partial class ExitXferEdit
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
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxMapName = new System.Windows.Forms.TextBox();
            this.textBoxSpawnX = new System.Windows.Forms.TextBox();
            this.textBoxSpawnY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Redirect to Map";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Spawn X";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Spawn Y";
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(88, 104);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 3;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.ButtonDoneClick);
            // 
            // textBoxMapName
            // 
            this.textBoxMapName.Location = new System.Drawing.Point(120, 16);
            this.textBoxMapName.Name = "textBoxMapName";
            this.textBoxMapName.Size = new System.Drawing.Size(128, 20);
            this.textBoxMapName.TabIndex = 4;
            // 
            // textBoxSpawnX
            // 
            this.textBoxSpawnX.Location = new System.Drawing.Point(120, 40);
            this.textBoxSpawnX.Name = "textBoxSpawnX";
            this.textBoxSpawnX.Size = new System.Drawing.Size(100, 20);
            this.textBoxSpawnX.TabIndex = 5;
            this.textBoxSpawnX.TextChanged += new System.EventHandler(this.textBoxSpawnX_TextChanged);
            // 
            // textBoxSpawnY
            // 
            this.textBoxSpawnY.Location = new System.Drawing.Point(120, 64);
            this.textBoxSpawnY.Name = "textBoxSpawnY";
            this.textBoxSpawnY.Size = new System.Drawing.Size(100, 20);
            this.textBoxSpawnY.TabIndex = 6;
            // 
            // ExitXferEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 133);
            this.Controls.Add(this.textBoxSpawnY);
            this.Controls.Add(this.textBoxSpawnX);
            this.Controls.Add(this.textBoxMapName);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExitXferEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExitXfer";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.TextBox textBoxSpawnY;
		private System.Windows.Forms.TextBox textBoxSpawnX;
		private System.Windows.Forms.TextBox textBoxMapName;
		private System.Windows.Forms.Button buttonDone;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
