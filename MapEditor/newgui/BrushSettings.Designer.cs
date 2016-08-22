/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 17.04.2015
 */
namespace MapEditor.newgui
{
	partial class BrushSettings
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
            this.buttonK = new System.Windows.Forms.Button();
            this.comboBoxTileType = new System.Windows.Forms.ComboBox();
            this.comboBoxEdgeType = new System.Windows.Forms.ComboBox();
            this.numericSquareSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericSquareSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tile Type";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Edge Type";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Square Size";
            // 
            // buttonK
            // 
            this.buttonK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonK.Location = new System.Drawing.Point(72, 96);
            this.buttonK.Name = "buttonK";
            this.buttonK.Size = new System.Drawing.Size(75, 23);
            this.buttonK.TabIndex = 3;
            this.buttonK.Text = "Done";
            this.buttonK.UseVisualStyleBackColor = true;
            this.buttonK.Click += new System.EventHandler(this.ButtonKClick);
            // 
            // comboBoxTileType
            // 
            this.comboBoxTileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTileType.FormattingEnabled = true;
            this.comboBoxTileType.Location = new System.Drawing.Point(88, 16);
            this.comboBoxTileType.MaxDropDownItems = 12;
            this.comboBoxTileType.Name = "comboBoxTileType";
            this.comboBoxTileType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTileType.TabIndex = 4;
            // 
            // comboBoxEdgeType
            // 
            this.comboBoxEdgeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEdgeType.FormattingEnabled = true;
            this.comboBoxEdgeType.Location = new System.Drawing.Point(88, 40);
            this.comboBoxEdgeType.MaxDropDownItems = 12;
            this.comboBoxEdgeType.Name = "comboBoxEdgeType";
            this.comboBoxEdgeType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEdgeType.TabIndex = 5;
            // 
            // numericSquareSize
            // 
            this.numericSquareSize.Location = new System.Drawing.Point(88, 64);
            this.numericSquareSize.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericSquareSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSquareSize.Name = "numericSquareSize";
            this.numericSquareSize.Size = new System.Drawing.Size(56, 20);
            this.numericSquareSize.TabIndex = 6;
            this.numericSquareSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSquareSize.ValueChanged += new System.EventHandler(this.numericSquareSize_ValueChanged);
            // 
            // BrushSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 127);
            this.Controls.Add(this.numericSquareSize);
            this.Controls.Add(this.comboBoxEdgeType);
            this.Controls.Add(this.comboBoxTileType);
            this.Controls.Add(this.buttonK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BrushSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tile Brush Settings";
            this.Load += new System.EventHandler(this.BrushSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericSquareSize)).EndInit();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.NumericUpDown numericSquareSize;
		private System.Windows.Forms.ComboBox comboBoxEdgeType;
		private System.Windows.Forms.ComboBox comboBoxTileType;
		private System.Windows.Forms.Button buttonK;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
