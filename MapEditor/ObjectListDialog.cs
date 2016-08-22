using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.MapInt;
using NoxShared;
namespace MapEditor
{
	/// <summary>
	/// Summary description for ObjectListDialog.
	/// </summary>
	public class ObjectListDialog : System.Windows.Forms.Form
    {
        private IContainer components;

        private DataGridViewColumn setting;
		protected DataTable objList;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem goToObjectToolStripMenuItem;
        private ToolStripMenuItem editObjectToolStripMenuItem;
        private DataGridView dataGrid1;
		public Map.ObjectTable objTable
		{
			set
			{
				objList = new DataTable("objList");
				objList.Columns.Add("Extent",Type.GetType("System.UInt32"));
				objList.Columns.Add("X-Coor.",Type.GetType("System.Single"));
				objList.Columns.Add("Y-Coor.",Type.GetType("System.Single"));
                objList.Columns.Add("Name", Type.GetType("System.Object"));
				objList.Columns.Add("Scr. Name", Type.GetType("System.String"));
                
                foreach (Map.Object obj in value)
                {
                    objList.Rows.Add(new Object[] {obj.Extent, obj.Location.X, obj.Location.Y, obj, obj.Scr_Name });
                    dataGrid1.DataSource = objList;
                }
			}
		}
        public Map.ObjectTable objTable2;
        private Timer Helpmark;
		public MapView Map;
       	public ObjectListDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            dataGrid1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dataGrid1_ColumnHeaderMouseClick);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectListDialog));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.goToObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.Helpmark = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToObjectToolStripMenuItem,
            this.editObjectToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // goToObjectToolStripMenuItem
            // 
            this.goToObjectToolStripMenuItem.Name = "goToObjectToolStripMenuItem";
            resources.ApplyResources(this.goToObjectToolStripMenuItem, "goToObjectToolStripMenuItem");
            this.goToObjectToolStripMenuItem.Click += new System.EventHandler(this.goToObjectToolStripMenuItem_Click);
            // 
            // editObjectToolStripMenuItem
            // 
            this.editObjectToolStripMenuItem.Name = "editObjectToolStripMenuItem";
            resources.ApplyResources(this.editObjectToolStripMenuItem, "editObjectToolStripMenuItem");
            this.editObjectToolStripMenuItem.Click += new System.EventHandler(this.editObjectToolStripMenuItem_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGrid1, "dataGrid1");
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid1_CellContentClick);
            // 
            // Helpmark
            // 
            this.Helpmark.Interval = 120;
            this.Helpmark.Tick += new System.EventHandler(this.Helpmark_Tick);
            // 
            // ObjectListDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ObjectListDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ObjectListDialog_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        
		private void dataGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Map.CenterAtPoint(new Point((int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["X-Coor."]), (int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["Y-Coor."])));
		
           
        }
        private void dataGrid1_Navigate(object sender, NavigateEventArgs ne)
        {
            MessageBox.Show("NAV");
        }

        void dataGrid1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DatatableSync();

        }

        void DatatableSync()
        {

            if (dataGrid1.SortedColumn == null || dataGrid1.SortedColumn.Name.Length <= 0) return;

            if (dataGrid1.SortOrder == SortOrder.Descending)
            {
                objList.DefaultView.Sort = dataGrid1.SortedColumn.Name + " DESC";
            }
            else
            {
                objList.DefaultView.Sort = dataGrid1.SortedColumn.Name + " ASC";
            }
            objList = objList.DefaultView.ToTable();
        }

        private void goToObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {



            Point target = new Point((int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["X-Coor."]), (int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["Y-Coor."]));
            Map.CenterAtPoint(target);
            Helpmark.Enabled = true;
            Map.highlightUndoRedo = target;
            Map.Object P = (Map.Object)(objList.Rows[dataGrid1.CurrentRow.Index][3]);
            Map.SelectedObjects.Items.Clear();
            Map.SelectedObjects.Items.Add(P);
        }

        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            // map.RemoveObject(new Point((int)((float)objList.Rows[dataGrid1.CurrentRowIndex]["X-Coor."]), (int)((float)objList.Rows[dataGrid1.CurrentRowIndex]["Y-Coor."])));
            /*
            int i = 0;
            foreach (Map.Object obj in MapInterface.TheMap.Objects)
            {
                i++;
                int bob = Convert.ToInt32((objList.Rows[dataGrid1.CurrentRow.Index][0]));
                if (i == bob)
                {
                    MapInterface.ObjectRemove(obj);
                    break;

                }
            }
            */

        }

        private void ObjectListDialog_Load(object sender, EventArgs e)
        {

        }

        private void editObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListSortDirection sorted = ListSortDirection.Ascending;
            int curIndex = dataGrid1.CurrentRow.Index;
            int vscroll = dataGrid1.VerticalScrollingOffset;
            if (dataGrid1.SortOrder == SortOrder.Ascending) sorted = ListSortDirection.Ascending;
            if (dataGrid1.SortOrder == SortOrder.Descending) sorted = ListSortDirection.Descending;
            setting = dataGrid1.SortedColumn;
            Map.Object P = (Map.Object)(objList.Rows[dataGrid1.CurrentRow.Index][3]);
            Map.ShowObjectProperties(P);
            this.objTable = objTable2;

            if (setting != null)
            {
                if (dataGrid1.Columns[setting.Name] != null)
                    dataGrid1.Sort(dataGrid1.Columns[setting.Name], sorted);

            }
                if (curIndex >= 0)
                {
                    dataGrid1.ClearSelection();
                    dataGrid1.Rows[curIndex].Selected = true;
                    dataGrid1.CurrentCell = dataGrid1.Rows[curIndex].Cells[0];
                }


            DatatableSync();
            


        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
           // MessageBox.Show("CEL");
        }

        private void dataGrid1_ParentRowsLabelStyleChanged(object sender, EventArgs e)
        {
            MessageBox.Show("LABELCHANGED");
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.50;
        }

        private void Helpmark_Tick(object sender, EventArgs e)
        {
            Map.higlightRad -= 30;

            if (Map.higlightRad > 40) return;
            Map.highlightUndoRedo = new Point();
            Map.higlightRad = 150;
            Helpmark.Enabled = false;
        }
	}
}
