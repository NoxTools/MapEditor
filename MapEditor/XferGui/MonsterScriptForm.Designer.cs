/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 24.10.2014
 */
namespace MapEditor.XferGui
{
	partial class MonsterScriptForm
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
			this.script1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.script2 = new System.Windows.Forms.TextBox();
			this.script3 = new System.Windows.Forms.TextBox();
			this.script4 = new System.Windows.Forms.TextBox();
			this.script5 = new System.Windows.Forms.TextBox();
			this.script6 = new System.Windows.Forms.TextBox();
			this.script7 = new System.Windows.Forms.TextBox();
			this.script8 = new System.Windows.Forms.TextBox();
			this.script9 = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.script10 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enemy Spotted";
			// 
			// script1
			// 
			this.script1.Location = new System.Drawing.Point(109, 6);
			this.script1.Name = "script1";
			this.script1.Size = new System.Drawing.Size(163, 20);
			this.script1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Slain";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Fight Initiated";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(91, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "Action Changed";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 101);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(91, 23);
			this.label5.TabIndex = 5;
			this.label5.Text = "Damage Taken";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 124);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(91, 23);
			this.label6.TabIndex = 6;
			this.label6.Text = "Retreat";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 147);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(91, 23);
			this.label7.TabIndex = 7;
			this.label7.Text = "Collided";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 170);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(91, 23);
			this.label8.TabIndex = 8;
			this.label8.Text = "Pointed By Plr";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12, 193);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(91, 23);
			this.label9.TabIndex = 9;
			this.label9.Text = "Report Complete";
			// 
			// script2
			// 
			this.script2.Location = new System.Drawing.Point(109, 29);
			this.script2.Name = "script2";
			this.script2.Size = new System.Drawing.Size(163, 20);
			this.script2.TabIndex = 10;
			// 
			// script3
			// 
			this.script3.Location = new System.Drawing.Point(109, 52);
			this.script3.Name = "script3";
			this.script3.Size = new System.Drawing.Size(163, 20);
			this.script3.TabIndex = 11;
			// 
			// script4
			// 
			this.script4.Location = new System.Drawing.Point(109, 75);
			this.script4.Name = "script4";
			this.script4.Size = new System.Drawing.Size(163, 20);
			this.script4.TabIndex = 12;
			// 
			// script5
			// 
			this.script5.Location = new System.Drawing.Point(109, 98);
			this.script5.Name = "script5";
			this.script5.Size = new System.Drawing.Size(163, 20);
			this.script5.TabIndex = 13;
			// 
			// script6
			// 
			this.script6.Location = new System.Drawing.Point(109, 121);
			this.script6.Name = "script6";
			this.script6.Size = new System.Drawing.Size(163, 20);
			this.script6.TabIndex = 14;
			// 
			// script7
			// 
			this.script7.Location = new System.Drawing.Point(109, 144);
			this.script7.Name = "script7";
			this.script7.Size = new System.Drawing.Size(163, 20);
			this.script7.TabIndex = 15;
			// 
			// script8
			// 
			this.script8.Location = new System.Drawing.Point(109, 167);
			this.script8.Name = "script8";
			this.script8.Size = new System.Drawing.Size(163, 20);
			this.script8.TabIndex = 16;
			// 
			// script9
			// 
			this.script9.Location = new System.Drawing.Point(109, 190);
			this.script9.Name = "script9";
			this.script9.Size = new System.Drawing.Size(163, 20);
			this.script9.TabIndex = 17;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(100, 245);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 18;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.Button1Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 216);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(91, 23);
			this.label10.TabIndex = 19;
			this.label10.Text = "Enemy Lost";
			// 
			// script10
			// 
			this.script10.Location = new System.Drawing.Point(109, 213);
			this.script10.Name = "script10";
			this.script10.Size = new System.Drawing.Size(163, 20);
			this.script10.TabIndex = 20;
			// 
			// MonsterScriptForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 280);
			this.Controls.Add(this.script10);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.script9);
			this.Controls.Add(this.script8);
			this.Controls.Add(this.script7);
			this.Controls.Add(this.script6);
			this.Controls.Add(this.script5);
			this.Controls.Add(this.script4);
			this.Controls.Add(this.script3);
			this.Controls.Add(this.script2);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.script1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MonsterScriptForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Script Event Handlers";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox script10;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox script9;
		private System.Windows.Forms.TextBox script8;
		private System.Windows.Forms.TextBox script7;
		private System.Windows.Forms.TextBox script6;
		private System.Windows.Forms.TextBox script5;
		private System.Windows.Forms.TextBox script4;
		private System.Windows.Forms.TextBox script3;
		private System.Windows.Forms.TextBox script2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox script1;
		private System.Windows.Forms.Label label1;
	}
}
