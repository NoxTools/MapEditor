using System;
using System.Reflection;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

using MapEditor;
using MapEditor.XferGui;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor
{
	public class ObjectPropertiesDialog : System.Windows.Forms.Form
	{
		protected Map.Object obj;
		protected ThingDb.Thing objPs;
        string m_ExePath;
        
		public Map.Object Object
		{
			get
			{
				return obj;
			}
			set
			{
				obj = (Map.Object) value.Clone();
				nameBox.Text = obj.Name;
				xBox.Text = obj.Location.X.ToString();
				yBox.Text = obj.Location.Y.ToString();
				extentBox.Text = obj.Extent.ToString();
				teamBox.Text = obj.Team.ToString();
				scrNameBox.Text = obj.Scr_Name;
                xtraBox.Checked = (obj.Terminator > 0);
                xtraBox_CheckedChanged(null, EventArgs.Empty);
				animFlBox.Text = String.Format("{0:x}", obj.AnimFlags); // 0x40 "shiny sparks"
                pickupBox.Text = obj.pickup_func;


 
                // AngryKirC: ставим флаг?
                flagsListBox.SetItemChecked(0, (obj.CreateFlags & 1) == 1); // BELOW
                flagsListBox.SetItemChecked(1, (obj.CreateFlags & 0x1000000) == 0x1000000); // ENABLED
                flagsListBox.SetItemChecked(2, (obj.CreateFlags & 0x20) == 0x20); // DESTROYED
                flagsListBox.SetItemChecked(3, (obj.CreateFlags & 0x40) == 0x40); // NO_COLLIDE
                flagsListBox.SetItemChecked(4, (obj.CreateFlags & 0x100) == 0x100); // EQUIPPED
                flagsListBox.SetItemChecked(5, (obj.CreateFlags & 0x8000) == 0x8000); // DEAD
                flagsListBox.SetItemChecked(6, (obj.CreateFlags & 2) == 2); // NO_UPDATE
                flagsListBox.SetItemChecked(7, (obj.CreateFlags & 0x10000000) == 0x10000000); // NO_AUTO_DROP
                flagsListBox.SetItemChecked(8, (obj.CreateFlags & 0x2000) == 0x2000); // NO_PUSH_CHARACTERS
                flagsListBox.SetItemChecked(9, (obj.CreateFlags &  0x4000) ==  0x4000); // AIRBORNE
                flagsListBox.SetItemChecked(10, (obj.CreateFlags & 0x10000) == 0x10000); // SHADOW
                flagsListBox.SetItemChecked(11, (obj.CreateFlags & 0x40000) == 0x40000); // IN HOLE
                flagsListBox.SetItemChecked(12, (obj.CreateFlags & 0x100000) == 0x100000); // ON OBJECT
                flagsListBox.SetItemChecked(13, (obj.CreateFlags & 0x800000) == 0x800000); // BOUNCY
                flagsListBox.SetItemChecked(14, (obj.CreateFlags & 0x2000000) == 0x2000000); // PENDING
                flagsListBox.SetItemChecked(15, (obj.CreateFlags & 0x4000000) == 0x4000000); // TRANSLUCENT
                flagsListBox.SetItemChecked(16, (obj.CreateFlags & 0x20000000) == 0x20000000); // FLICKER
                flagsListBox.SetItemChecked(17, (obj.CreateFlags & 0x400000) == 0x400000); // TRANSIENT
                flagsListBox.SetItemChecked(18, (obj.CreateFlags & 0x40000000) ==  0x40000000); // SELECTED
                flagsListBox.SetItemChecked(19, (obj.CreateFlags & 0x80000000) == 0x80000000); // MARKED



				
				objPs = ((ThingDb.Thing)ThingDb.Things[nameBox.Text]);
            }
		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox xBox;
		private System.Windows.Forms.TextBox yBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox extentBox;
		private System.Windows.Forms.ComboBox nameBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox teamBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox scrNameBox;
		private System.Windows.Forms.CheckBox xtraBox;
		private System.Windows.Forms.Button invenButton;
		private System.Windows.Forms.Label label7;
        private TextBox pickupBox;
        private Label label8;


		public ObjectPropertiesDialog()
		{
			InitializeComponent();
            m_ExePath = Process.GetCurrentProcess().MainModule.FileName;
            m_ExePath = Path.GetDirectoryName(m_ExePath);
			foreach (string s in ThingDb.Things.Keys)
				nameBox.Items.Add(s);
			
			//TODO ToolTip tt = new ToolTip();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xBox = new System.Windows.Forms.TextBox();
            this.yBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.extentBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.teamBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.scrNameBox = new System.Windows.Forms.TextBox();
            this.invenButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pickupBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.animFlBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonXData = new System.Windows.Forms.Button();
            this.xtraGroupBox = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.flagsListBox = new System.Windows.Forms.CheckedListBox();
            this.xtraBox = new System.Windows.Forms.CheckBox();
            this.xtraGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOK.Location = new System.Drawing.Point(55, 303);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCancel.Location = new System.Drawing.Point(176, 303);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(18, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(91, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Y";
            // 
            // xBox
            // 
            this.xBox.Location = new System.Drawing.Point(40, 48);
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(40, 20);
            this.xBox.TabIndex = 6;
            // 
            // yBox
            // 
            this.yBox.Location = new System.Drawing.Point(116, 48);
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(40, 20);
            this.yBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(170, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Extent";
            // 
            // extentBox
            // 
            this.extentBox.Location = new System.Drawing.Point(216, 48);
            this.extentBox.Name = "extentBox";
            this.extentBox.Size = new System.Drawing.Size(40, 20);
            this.extentBox.TabIndex = 9;
            // 
            // nameBox
            // 
            this.nameBox.DropDownWidth = 200;
            this.nameBox.FormattingEnabled = true;
            this.nameBox.ItemHeight = 13;
            this.nameBox.Location = new System.Drawing.Point(64, 16);
            this.nameBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.nameBox.MaxDropDownItems = 16;
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(216, 21);
            this.nameBox.TabIndex = 11;
            this.nameBox.SelectedIndexChanged += new System.EventHandler(this.nameBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(178, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "Team ?";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // teamBox
            // 
            this.teamBox.Location = new System.Drawing.Point(232, 93);
            this.teamBox.MaxLength = 1;
            this.teamBox.Name = "teamBox";
            this.teamBox.Size = new System.Drawing.Size(30, 20);
            this.teamBox.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(8, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Script Name";
            // 
            // scrNameBox
            // 
            this.scrNameBox.Location = new System.Drawing.Point(88, 151);
            this.scrNameBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.scrNameBox.Name = "scrNameBox";
            this.scrNameBox.Size = new System.Drawing.Size(112, 20);
            this.scrNameBox.TabIndex = 19;
            // 
            // invenButton
            // 
            this.invenButton.Enabled = false;
            this.invenButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.invenButton.Location = new System.Drawing.Point(16, 56);
            this.invenButton.Name = "invenButton";
            this.invenButton.Size = new System.Drawing.Size(99, 23);
            this.invenButton.TabIndex = 21;
            this.invenButton.Text = "View Inventory";
            this.invenButton.Click += new System.EventHandler(this.invenButton_Click);
            // 
            // label7
            // 
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(196, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Properties";
            // 
            // pickupBox
            // 
            this.pickupBox.Location = new System.Drawing.Point(84, 120);
            this.pickupBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pickupBox.Name = "pickupBox";
            this.pickupBox.Size = new System.Drawing.Size(116, 20);
            this.pickupBox.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(7, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "Pickup Func.";
            // 
            // animFlBox
            // 
            this.animFlBox.Location = new System.Drawing.Point(84, 92);
            this.animFlBox.Name = "animFlBox";
            this.animFlBox.Size = new System.Drawing.Size(76, 20);
            this.animFlBox.TabIndex = 31;
            this.animFlBox.TextChanged += new System.EventHandler(this.animFlBox_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(7, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 23);
            this.label10.TabIndex = 32;
            this.label10.Text = "AnimFlags:";
            // 
            // buttonXData
            // 
            this.buttonXData.Location = new System.Drawing.Point(136, 80);
            this.buttonXData.Name = "buttonXData";
            this.buttonXData.Size = new System.Drawing.Size(136, 23);
            this.buttonXData.TabIndex = 31;
            this.buttonXData.Text = "Special Properties";
            this.buttonXData.UseVisualStyleBackColor = true;
            this.buttonXData.Click += new System.EventHandler(this.Button2Click);
            // 
            // xtraGroupBox
            // 
            this.xtraGroupBox.Controls.Add(this.label11);
            this.xtraGroupBox.Controls.Add(this.flagsListBox);
            this.xtraGroupBox.Controls.Add(this.label10);
            this.xtraGroupBox.Controls.Add(this.pickupBox);
            this.xtraGroupBox.Controls.Add(this.invenButton);
            this.xtraGroupBox.Controls.Add(this.label8);
            this.xtraGroupBox.Controls.Add(this.animFlBox);
            this.xtraGroupBox.Controls.Add(this.teamBox);
            this.xtraGroupBox.Controls.Add(this.label5);
            this.xtraGroupBox.Controls.Add(this.scrNameBox);
            this.xtraGroupBox.Controls.Add(this.label6);
            this.xtraGroupBox.Location = new System.Drawing.Point(8, 112);
            this.xtraGroupBox.Name = "xtraGroupBox";
            this.xtraGroupBox.Size = new System.Drawing.Size(280, 183);
            this.xtraGroupBox.TabIndex = 34;
            this.xtraGroupBox.TabStop = false;
            this.xtraGroupBox.Text = "Extra Bytes";
            this.xtraGroupBox.Enter += new System.EventHandler(this.xtraGroupBox_Enter);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(17, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 23);
            this.label11.TabIndex = 35;
            this.label11.Text = "Object flags:";
            // 
            // flagsListBox
            // 
            this.flagsListBox.FormattingEnabled = true;
            this.flagsListBox.Items.AddRange(new object[] {
            "BELOW",
            "ENABLED",
            "DESTROYED",
            "NO_COLLIDE",
            "EQUIPPED",
            "DEAD",
            "NO_UPDATE",
            "NO_AUTO_DROP",
            "NO_PUSH_CHARACTERS",
            "AIRBORNE",
            "SHADOW",
            "IN HOLE",
            "ON OBJECT",
            "BOUNCY",
            "PENDING",
            "TRANSLUCENT",
            "FLICKER",
            "TRANSIENT",
            "SELECTED",
            "MARKED"});
            this.flagsListBox.Location = new System.Drawing.Point(124, 19);
            this.flagsListBox.Name = "flagsListBox";
            this.flagsListBox.Size = new System.Drawing.Size(145, 64);
            this.flagsListBox.TabIndex = 34;
            this.flagsListBox.SelectedIndexChanged += new System.EventHandler(this.flagsListBox_SelectedIndexChanged);
            // 
            // xtraBox
            // 
            this.xtraBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.xtraBox.Location = new System.Drawing.Point(16, 80);
            this.xtraBox.Name = "xtraBox";
            this.xtraBox.Size = new System.Drawing.Size(84, 23);
            this.xtraBox.TabIndex = 20;
            this.xtraBox.Text = "Extra Bytes";
            this.xtraBox.CheckedChanged += new System.EventHandler(this.xtraBox_CheckedChanged);
            // 
            // ObjectPropertiesDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(296, 335);
            this.Controls.Add(this.buttonXData);
            this.Controls.Add(this.xtraGroupBox);
            this.Controls.Add(this.xtraBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.extentBox);
            this.Controls.Add(this.yBox);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectPropertiesDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Object Properties";
            this.Load += new System.EventHandler(this.ObjectPropertiesDialog_Load);
            this.xtraGroupBox.ResumeLayout(false);
            this.xtraGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		private System.Windows.Forms.CheckedListBox flagsListBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox xtraGroupBox;
		private System.Windows.Forms.Button buttonXData;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox animFlBox;
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			//verify that we have valid input
			if (ThingDb.GetThing(nameBox.Text) == null)
			{
				MessageBox.Show("Invalid object name.", "Error");
				return;
			}
			//commit the changes

            obj.Name = nameBox.Text;

            obj.Location.X = Single.Parse(xBox.Text);
			obj.Location.Y = Single.Parse(yBox.Text);
            
            //REMOVEOBJECT,ADDOBJECT
			obj.Extent = Int32.Parse(extentBox.Text);
			obj.Terminator = (byte)(xtraBox.Checked==true? 0xFF : 0x00);
			obj.Team = Byte.Parse(teamBox.Text);
			obj.Scr_Name = scrNameBox.Text;
            obj.pickup_func = pickupBox.Text;
			obj.AnimFlags = UInt16.Parse(animFlBox.Text, System.Globalization.NumberStyles.HexNumber);

            uint[] flags = { 1, 0x1000000, 0x20, 0x40, 0x100, 0x8000, 2, 0x10000000, 0x2000, 0x4000, 0x10000, 0x40000, 0x100000, 0x800000, 0x2000000, 0x4000000, 0x20000000, 0x400000, 0x40000000, 0x80000000 };

           
            obj.CreateFlags = 0;
			foreach (int i in flagsListBox.CheckedIndices)
			{
				obj.CreateFlags |= flags[i];
			}
			
			this.Visible = false;
		}

		private void nameBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			objPs = ((ThingDb.Thing)ThingDb.Things[nameBox.Text]);
            
			if (nameBox.Text != obj.Name)
            {
				obj.Name = nameBox.Text;
				obj.NewDefaultExtraData();
            }
            xtraBox.Checked = (obj.Terminator > 0);
		}

		private void xtraBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if(xtraBox.Checked)
			{
				scrNameBox.Enabled = true;
				teamBox.Enabled = true;
				invenButton.Enabled = true;
                pickupBox.Enabled = true;
                flagsListBox.Enabled = true;
                animFlBox.Enabled = true;

            }
			else
			{
				scrNameBox.Enabled = false;
                pickupBox.Enabled = false;
                teamBox.Enabled = false;
				invenButton.Enabled = false;
				flagsListBox.Enabled = false;
				animFlBox.Enabled = false;
			}
		}

		private void invenButton_Click(object sender, System.EventArgs e)
		{
			ObjectInventoryDialog invenDlg = new ObjectInventoryDialog();
			invenDlg.Object = obj;
			invenDlg.ShowDialog();
		}
		
		private void Button2Click(object sender, EventArgs e)
		{
			if (objPs.Xfer != null) // DefaultXfer
			{
				XferEditor editor = XferEditors.GetEditorForXfer(objPs.Xfer);
				if (editor != null)
				{
					try
					{
						editor.SetObject(obj);
						if (editor.ShowDialog() == DialogResult.OK)
						{
							obj = editor.GetObject();
						}
					}
					catch (Exception) 
					{
						#if DEBUG
						throw;
						#endif
					}
				}
				else
            	{
                	MessageBox.Show("There is no internal mod-gen for that object.");
            	}
			}
		}

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void xtraGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void flagsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ObjectPropertiesDialog_Load(object sender, EventArgs e)
        {

        }

        private void animFlBox_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
