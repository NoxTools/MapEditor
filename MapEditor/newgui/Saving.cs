using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NoxShared;
using MapEditor.MapInt;
namespace MapEditor.newgui
{
    public partial class Saving : Form
    {
        public Saving()
        {
            InitializeComponent();
           
        }
        private Map map
        {
            get
            {
                return MapInterface.TheMap;
            }
        }
        private void Saving_Load(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //progressBar1.Value = map.progress;
            //progLabel.Text = map.progress.ToString();
            //this.Invalidate();
          // this.Parent.Invalidate();
           //this.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void mapName_TextChanged(object sender, EventArgs e)
        {

            if (mapName.Text.Length > 0 && mapName.Text.Length <= 8)
            {
                button1.Enabled = true;
                mapName.ForeColor = SystemColors.ControlText;
                maxWrong.ForeColor = SystemColors.ControlText;
            }
            else
            {
                button1.Enabled = false;
                mapName.ForeColor = Color.Red;
                maxWrong.ForeColor = mapName.Text.Length > 8 ? Color.Red : SystemColors.ControlText;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
