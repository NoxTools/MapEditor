/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 06.11.2014
 */
namespace MapEditor.XferGui
{
	partial class TeleportEdit
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
            this.checkIsLinked = new System.Windows.Forms.CheckBox();
            this.transpSelect = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkIsLinked
            // 
            this.checkIsLinked.Location = new System.Drawing.Point(70, 12);
            this.checkIsLinked.Name = "checkIsLinked";
            this.checkIsLinked.Size = new System.Drawing.Size(104, 24);
            this.checkIsLinked.TabIndex = 0;
            this.checkIsLinked.Text = "Linked";
            this.checkIsLinked.UseVisualStyleBackColor = true;
            this.checkIsLinked.CheckedChanged += new System.EventHandler(this.CheckIsLinkedCheckedChanged);
            // 
            // transpSelect
            // 
            this.transpSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transpSelect.Enabled = false;
            this.transpSelect.FormattingEnabled = true;
            this.transpSelect.Location = new System.Drawing.Point(12, 42);
            this.transpSelect.Name = "transpSelect";
            this.transpSelect.Size = new System.Drawing.Size(212, 21);
            this.transpSelect.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(83, 73);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // TeleportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 108);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.transpSelect);
            this.Controls.Add(this.checkIsLinked);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TeleportEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TransporterXfer";
            this.Load += new System.EventHandler(this.TeleportEdit_Load);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.ComboBox transpSelect;
		private System.Windows.Forms.CheckBox checkIsLinked;
	}
}
