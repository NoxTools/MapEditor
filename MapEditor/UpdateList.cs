using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MapEditor
{
    public partial class UpdateList : Form
    {
        public UpdateList()
        {
            InitializeComponent();
        }

        private void UpdateList_Load(object sender, EventArgs e)
        {
            Stream strm;
            Assembly asm = Assembly.GetExecutingAssembly();

            string fullName = null;
            foreach (string str in asm.GetManifestResourceNames())
            {
                if (str.EndsWith("Updates.txt"))
                {
                    fullName = str;
                    break;
                }
            }
            if (fullName == null)
                throw new Exception("Can not find " + "UpdateList" + " resource in container file");

            strm = asm.GetManifestResourceStream(fullName);

            if (strm == null)
                throw new Exception("Could not load " + "UpdateList");

            StreamReader rR = new StreamReader(strm);
            string update = rR.ReadToEnd();
            txtUpdate.Text = update;
        }
    }
}