using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NoxShared;

namespace MapEditor
{
	/// <summary>
	/// Summary description for ObjectInventoryDialog.
	/// </summary>
	public class ObjectInventoryDialog : System.Windows.Forms.Form
	{
		protected Map.Object obj;
		public Map.Object Object
		{
			get
			{
				return obj;
			}
			set
			{
				obj = value;
				UpdateList();
			}
		}
		private System.Windows.Forms.ListBox objectsList;

        private System.Windows.Forms.Button addButton;
        private Button deleteButton;

        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ObjectInventoryDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectInventoryDialog));
            this.objectsList = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
// 
// objectsList
// 
            this.objectsList.FormattingEnabled = true;
            resources.ApplyResources(this.objectsList, "objectsList");
            this.objectsList.Name = "objectsList";
            this.objectsList.DoubleClick += new System.EventHandler(this.objectsList_DoubleClick);
// 
// addButton
// 
            resources.ApplyResources(this.addButton, "addButton");
            this.addButton.Name = "addButton";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
// 
// deleteButton
// 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
// 
// ObjectInventoryDialog
// 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.objectsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectInventoryDialog";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
		#endregion

		private void addButton_Click(object sender, System.EventArgs e)
		{
			Map.Object o = new Map.Object();
			o.Extent = 0;
			ObjectPropertiesDialog propDlg = new ObjectPropertiesDialog();
			propDlg.Object = o;
			propDlg.ShowDialog();
			obj.InventoryList.Add(propDlg.Object);
			UpdateList();
		}

		private void objectsList_DoubleClick(object sender, System.EventArgs e)
		{
			if(objectsList.SelectedItem != null)
			{	
				int ndx = obj.InventoryList.IndexOf((Map.Object) objectsList.SelectedItem);
				ObjectPropertiesDialog propDlg = new ObjectPropertiesDialog();
				propDlg.Object = obj.InventoryList[ndx];
				propDlg.ShowDialog();
				// Update reference because object has been cloned
				obj.InventoryList[ndx] = propDlg.Object;
				UpdateList();
			}
		}

		private void UpdateList()
		{
			objectsList.Items.Clear();
			foreach (Map.Object o in obj.InventoryList) objectsList.Items.Add(o);
		}

        private void deleteButton_Click(object sender, EventArgs e)
        {
			obj.InventoryList.Remove((Map.Object)objectsList.SelectedItem);
			UpdateList();
        }
	}
}
