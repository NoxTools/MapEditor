using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using NoxShared;

namespace MapEditor
{
	/// <summary>
	/// Summary description for ScriptDialog.
	/// </summary>

	public class ScriptDialog : System.Windows.Forms.Form
	{
        System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Strings");
        System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Functions");
        string prevname;
        protected Map.ScriptObject sct;
		public Map.ScriptObject Scripts
		{
			get
			{
				return sct;
			}
			set
			{
				sct = value;
                foreach (String s in sct.SctStr)
                    treeNode1.Nodes.Add(s);
				int i = 0;
                foreach (Map.ScriptFunction sf in sct.Funcs)
				{
                    treeNode2.Nodes.Add(String.Format("{0}: {1}",i,sf.name));
					i++;
				}
            }
        }
        private TreeView scriptTree;
        private ContextMenuStrip treeMenu;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private IContainer components;

        public ScriptDialog()
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
            this.components = new System.ComponentModel.Container();
            treeNode1 = new System.Windows.Forms.TreeNode("Strings");
            treeNode2 = new System.Windows.Forms.TreeNode("Functions");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptDialog));
            this.scriptTree = new System.Windows.Forms.TreeView();
            this.treeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeMenu.SuspendLayout();
            this.SuspendLayout();
// 
// scriptTree
// 
            this.scriptTree.ContextMenuStrip = this.treeMenu;
            resources.ApplyResources(this.scriptTree, "scriptTree");
            this.scriptTree.LabelEdit = true;
            this.scriptTree.Name = "scriptTree";
            resources.ApplyResources(treeNode1, "treeNode1");
            resources.ApplyResources(treeNode2, "treeNode2");
            this.scriptTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.scriptTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.scriptTree_BeforeLabelEdit);
            this.scriptTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.scriptTree_AfterLabelEdit);
            this.scriptTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.scriptTree_NodeMouseDoubleClick);
// 
// treeMenu
// 
            this.treeMenu.AllowDrop = true;
            this.treeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
            resources.ApplyResources(this.treeMenu, "treeMenu");
            this.treeMenu.Name = "treeMenu";
// 
// addToolStripMenuItem
// 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            //this.addToolStripMenuItem.SettingsKey = "ScriptDialog.addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
// 
// deleteToolStripMenuItem
// 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            //this.deleteToolStripMenuItem.SettingsKey = "ScriptDialog.deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
// 
// ScriptDialog
// 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.scriptTree);
            this.Name = "ScriptDialog";
            this.treeMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

		#endregion

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void scriptTree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Parent == treeNode1)
                prevname = e.Node.Text;
            else
                e.Node.EndEdit(true);
        }

        private void scriptTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string name;
            if (e.Node.Parent == treeNode1)
            {
                name = e.Label;
                sct.SctStr[sct.SctStr.IndexOf(prevname)] = name;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptTree.SelectedNode != null)
            {
                if (scriptTree.SelectedNode == treeNode1)
                {
                    treeNode1.Nodes.Add("New String");
                    sct.SctStr.Add("New String");
                }
                if (scriptTree.SelectedNode == treeNode2)
                {
                    treeNode2.Nodes.Add(String.Format("{0}: {1}",sct.Funcs.Count,"New Function"));
                    Map.ScriptFunction sf = new Map.ScriptFunction();
                    sf.name = "New Function";
                    sf.code = new byte[0];
                    sct.Funcs.Add(sf);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptTree.SelectedNode != null)
            {
                if (scriptTree.SelectedNode.Parent == treeNode1)
                {
                    sct.SctStr.Remove(scriptTree.SelectedNode.Text);
                    treeNode1.Nodes.Remove(scriptTree.SelectedNode);
                }
                if (scriptTree.SelectedNode.Parent == treeNode2)
                {
                    Map.ScriptFunction toDelete = null;
					toDelete = (Map.ScriptFunction)sct.Funcs[scriptTree.SelectedNode.Index];
                    if (toDelete != null)
                    {
                        sct.Funcs.Remove(toDelete);
						treeNode2.Nodes.Clear();
						int i = 0;
						foreach (Map.ScriptFunction sf in sct.Funcs)
						{
							treeNode2.Nodes.Add(String.Format("{0}: {1}", i, sf.name));
							i++;
						}
                    }
                }
            }
        }

        private void scriptTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == treeNode2)
            {
                ScriptFunctionDialog sfd = new ScriptFunctionDialog();
                sfd.ScriptStrings = sct.SctStr;
                sfd.ScriptFunctions = sct.Funcs;
                sfd.ScriptFunc = (Map.ScriptFunction)sct.Funcs[e.Node.Index];
                sfd.ShowDialog();
				treeNode2.Nodes.Clear();
				int i = 0;
				foreach (Map.ScriptFunction sf in sct.Funcs)
				{
					treeNode2.Nodes.Add(String.Format("{0}: {1}", i, sf.name));
					i++;
				}
            }
        }
	}
}
