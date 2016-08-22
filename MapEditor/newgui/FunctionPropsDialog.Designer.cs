/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 30.12.2015
 */
namespace MapEditor.newgui
{
	partial class FunctionPropsDialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFuncName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericArguments;
		private System.Windows.Forms.CheckBox checkBoxReturns;
		private System.Windows.Forms.Button buttonDone;
		
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
			this.textBoxFuncName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.numericArguments = new System.Windows.Forms.NumericUpDown();
			this.checkBoxReturns = new System.Windows.Forms.CheckBox();
			this.buttonDone = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericArguments)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Function Name:";
			// 
			// textBoxFuncName
			// 
			this.textBoxFuncName.Location = new System.Drawing.Point(118, 12);
			this.textBoxFuncName.Name = "textBoxFuncName";
			this.textBoxFuncName.Size = new System.Drawing.Size(143, 20);
			this.textBoxFuncName.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Number of Arguments:";
			// 
			// numericArguments
			// 
			this.numericArguments.Location = new System.Drawing.Point(140, 38);
			this.numericArguments.Maximum = new decimal(new int[] {
			8,
			0,
			0,
			0});
			this.numericArguments.Name = "numericArguments";
			this.numericArguments.Size = new System.Drawing.Size(63, 20);
			this.numericArguments.TabIndex = 3;
			// 
			// checkBoxReturns
			// 
			this.checkBoxReturns.Location = new System.Drawing.Point(12, 66);
			this.checkBoxReturns.Name = "checkBoxReturns";
			this.checkBoxReturns.Size = new System.Drawing.Size(104, 24);
			this.checkBoxReturns.TabIndex = 4;
			this.checkBoxReturns.Text = "Returns Value";
			this.checkBoxReturns.UseVisualStyleBackColor = true;
			// 
			// buttonDone
			// 
			this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonDone.Location = new System.Drawing.Point(140, 67);
			this.buttonDone.Name = "buttonDone";
			this.buttonDone.Size = new System.Drawing.Size(75, 23);
			this.buttonDone.TabIndex = 5;
			this.buttonDone.Text = "Done";
			this.buttonDone.UseVisualStyleBackColor = true;
			// 
			// FunctionPropsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(274, 99);
			this.Controls.Add(this.buttonDone);
			this.Controls.Add(this.checkBoxReturns);
			this.Controls.Add(this.numericArguments);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxFuncName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FunctionPropsDialog";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "User Function Properties";
			((System.ComponentModel.ISupportInitialize)(this.numericArguments)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
