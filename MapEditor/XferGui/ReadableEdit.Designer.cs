/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 28.10.2014
 */
namespace MapEditor.XferGui
{
	partial class ReadableEdit
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
			this.readableText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.labelPreview = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Text";
			// 
			// readableText
			// 
			this.readableText.Location = new System.Drawing.Point(83, 22);
			this.readableText.Name = "readableText";
			this.readableText.Size = new System.Drawing.Size(166, 20);
			this.readableText.TabIndex = 1;
			this.readableText.TextChanged += new System.EventHandler(this.ReadableTextTextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "nox.csf";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(93, 71);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
			// 
			// labelPreview
			// 
			this.labelPreview.Location = new System.Drawing.Point(83, 45);
			this.labelPreview.Name = "labelPreview";
			this.labelPreview.Size = new System.Drawing.Size(166, 23);
			this.labelPreview.TabIndex = 4;
			// 
			// ReadableEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(261, 107);
			this.Controls.Add(this.labelPreview);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.readableText);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReadableEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Readable Object Properties";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label labelPreview;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox readableText;
		private System.Windows.Forms.Label label1;
	}
}
