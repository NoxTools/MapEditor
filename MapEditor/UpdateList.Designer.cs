namespace MapEditor
{
    partial class UpdateList
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
            this.txtUpdate = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtUpdate
            // 
            this.txtUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUpdate.Location = new System.Drawing.Point(0, 0);
            this.txtUpdate.Name = "txtUpdate";
            this.txtUpdate.Size = new System.Drawing.Size(792, 573);
            this.txtUpdate.TabIndex = 0;
            this.txtUpdate.Text = "";
            // 
            // UpdateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.txtUpdate);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "UpdateList";
            this.Text = "Updates";
            this.Load += new System.EventHandler(this.UpdateList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtUpdate;
    }
}