namespace MapEditor.newgui
{
    partial class Saving
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.maxWrong = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mapName
            // 
            this.mapName.Location = new System.Drawing.Point(31, 37);
            this.mapName.MaxLength = 8;
            this.mapName.Name = "mapName";
            this.mapName.Size = new System.Drawing.Size(120, 20);
            this.mapName.TabIndex = 0;
            this.mapName.TextChanged += new System.EventHandler(this.mapName_TextChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(65, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Map Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // maxWrong
            // 
            this.maxWrong.AutoSize = true;
            this.maxWrong.Location = new System.Drawing.Point(84, 15);
            this.maxWrong.Name = "maxWrong";
            this.maxWrong.Size = new System.Drawing.Size(71, 13);
            this.maxWrong.TabIndex = 3;
            this.maxWrong.Text = " (8 char. max)";
            // 
            // Saving
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(182, 98);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxWrong);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mapName);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Saving";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Saving_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox mapName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label maxWrong;

    }
}