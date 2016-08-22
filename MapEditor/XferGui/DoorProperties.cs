using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Summary description for DoorProperties.
	/// </summary>
	public class DoorProperties : XferEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox dirBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox lockBox;
        private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;

		public DoorProperties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
           
			dirBox.Items.AddRange(Enum.GetNames(typeof(DoorXfer.DOORS_DIR)));
			lockBox.Items.AddRange(Enum.GetNames(typeof(DoorXfer.DOORS_LOCK)));
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoorProperties));
            this.dirBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lockBox = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dirBox
            // 
            resources.ApplyResources(this.dirBox, "dirBox");
            this.dirBox.Name = "dirBox";
            this.dirBox.SelectedIndexChanged += new System.EventHandler(this.dirBox_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lockBox
            // 
            resources.ApplyResources(this.lockBox, "lockBox");
            this.lockBox.Name = "lockBox";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // DoorProperties
            // 
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lockBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dirBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DoorProperties";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.DoorProperties_Load);
            this.ResumeLayout(false);

		}
		#endregion
		
		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			DoorXfer xfer = obj.GetExtraData<DoorXfer>();

           
           xfer.Direction = (DoorXfer.DOORS_DIR)Enum.Parse(typeof(DoorXfer.DOORS_DIR), dirBox.Text);
            xfer.LockType = (DoorXfer.DOORS_LOCK) Enum.Parse(typeof(DoorXfer.DOORS_LOCK), lockBox.Text);
            Close();
		}
		
		public override void SetObject(Map.Object obj)
		{
			this.obj = obj;
			DoorXfer xfer = obj.GetExtraData<DoorXfer>();
			dirBox.Text = xfer.Direction.ToString();
           // dir2.Text = ((byte)xfer.Direction).ToString();
			lockBox.Text = xfer.LockType.ToString();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

        private void DoorProperties_Load(object sender, EventArgs e)
        {

        }

        private void customDir_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dirBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dir2_TextChanged(object sender, EventArgs e)
        {

        }

        private void customDir_CheckedChanged(object sender, EventArgs e)
        {

        }
	}
}
