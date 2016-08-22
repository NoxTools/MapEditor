using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace MapEditor
{
	public class AboutDialog : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components = null;

		public AboutDialog()
		{
			InitializeComponent();
            versionLabel.Text = "Version: 1.0b";// lets call it like that, its more user friendly// string.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version);
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.aboutLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aboutLabel
            // 
            this.aboutLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.aboutLabel.Location = new System.Drawing.Point(12, 97);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(241, 84);
            this.aboutLabel.TabIndex = 0;
            this.aboutLabel.Text = resources.GetString("aboutLabel.Text");
            // 
            // versionLabel
            // 
            this.versionLabel.Location = new System.Drawing.Point(12, 188);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(241, 22);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "-version-";
            this.versionLabel.Click += new System.EventHandler(this.versionLabel_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 76);
            this.label1.TabIndex = 2;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // AboutDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(265, 217);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.aboutLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutDialog_Load);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.Label aboutLabel;
		#endregion

        private void versionLabel_Click(object sender, EventArgs e)
        {

        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {

        }
	}
}
