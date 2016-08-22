#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NoxShared;

#endregion

namespace MapEditor
{
    partial class GroupDialog : Form
    {
        private Map.Group selected;
        private Map.Group.GroupTypes newType;

        protected Map.GroupData groupData;
        public Map.GroupData GroupD
        {
            get
            {
                return groupData;
            }
            set
            {
                groupData = value;
                foreach (Map.Group g in groupData.Values)
                    groupList.Items.Add(g.name);
            }
        }

        public GroupDialog()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (selected != null && itemList.Text.Length>0)
			{
				if (!groupData.ContainsKey(selected.name))
					groupData.Add(selected.name, selected);
				selected.type = newType;
				switch (selected.type)
				{
					case Map.Group.GroupTypes.objects:
						selected.Clear();
						foreach (String s in itemList.Text.Trim().Split('\n'))
							selected.Add(Int32.Parse(s.Trim()));
						break;
					case Map.Group.GroupTypes.walls:
						selected.Clear();
						foreach (String s in itemList.Text.Split('\n'))
						{
							string[] loc = s.Trim().Split(',');
							if (loc.Length != 2)
								break;
							selected.Add(new Point(Int32.Parse(loc[0]), Int32.Parse(loc[1])));
						}
						break;
					case Map.Group.GroupTypes.waypoint:
						selected.Clear();
						foreach (String s in itemList.Text.Split('\n'))
							selected.Add(Int32.Parse(s.Trim()));
						break;
					default:
						break;
				}
				selected.id = Int32.Parse(groupId.Text);

				groupList.Items.Clear();
				foreach (Map.Group g in groupData.Values)
					groupList.Items.Add(g.name);
			}
        }

		private void groupList_SelectedValueChanged(object sender, EventArgs e)
        {
			itemList.Clear();

			if (groupList.Text.Length > 0)
			{
				selected = groupData.ContainsKey((string)groupList.Text) ? groupData[(string)groupList.Text] : new Map.Group((string)groupList.Text, Map.Group.GroupTypes.objects, 0);
				switch (selected.type)
				{
					case Map.Group.GroupTypes.objects:
						foreach (Int32 i in selected)
							itemList.Text += String.Format("{0}\r\n", i);
						wallRadio.Checked = false;
						objectRadio.Checked = true;
                        waypointRadio.Checked = false;
                        break;
					case Map.Group.GroupTypes.waypoint:
						foreach (Int32 i in selected)
							itemList.Text += String.Format("{0}\r\n", i);
						wallRadio.Checked = false;
						objectRadio.Checked = false;
                        waypointRadio.Checked = true;
						break;
					case Map.Group.GroupTypes.walls:
						foreach (Point pt in selected)
							itemList.Text += String.Format("{0},{1}\r\n", pt.X, pt.Y);
						wallRadio.Checked = true;
						objectRadio.Checked = false;
                        waypointRadio.Checked = false;
						break;
				}
				groupId.Text = selected.id.ToString();
			}
			else
				selected = null;
        }

        private void objectRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (objectRadio.Checked)
            {
                wallRadio.Checked = false;
                waypointRadio.Checked = false;
                newType = Map.Group.GroupTypes.objects;
            }
        }

        private void wallRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (wallRadio.Checked)
            {
                objectRadio.Checked = false;
                waypointRadio.Checked = false;
                newType = Map.Group.GroupTypes.walls;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void waypointRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (waypointRadio.Checked)
            {
                objectRadio.Checked = false;
                wallRadio.Checked = false;
                newType = Map.Group.GroupTypes.waypoint;
            }
        }

		private void delButton_Click(object sender, EventArgs e)
		{
            if (selected == null) return;
			if (groupData.ContainsKey(selected.name))
			{
				groupData.Remove(selected.name);
				groupList.Text = "";
				groupList.Items.Clear();
				foreach (Map.Group g in groupData.Values)
					groupList.Items.Add(g.name);
			}
		}

        private void groupId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}