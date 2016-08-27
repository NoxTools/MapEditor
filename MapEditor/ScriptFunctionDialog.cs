#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using NoxShared;
using SyntaxHighlighter;
using System.Diagnostics;

#endregion

namespace MapEditor
{
       public class ScriptFunctionDialog : System.Windows.Forms.Form
    {
        public bool hasLoaded = false;

        protected Map.ScriptFunction sf;
        private bool isinited = false;

        //List<string> CommentList = new List<string>();
        private ContextMenuStrip treeMenu;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
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
                if (sct != null)
                {
                    treeNode1.Nodes.Clear();
                    foreach (String s in sct.SctStr)
                        treeNode1.Nodes.Add(s);

                    treeNode2.Nodes.Clear();
                    int i = 0;
                    foreach (Map.ScriptFunction sf in sct.Funcs)
                    {
                        treeNode2.Nodes.Add(String.Format("{0}: {1}", i, sf.name));
                        i++;
                    }
                    scriptTree.Nodes.Add(treeNode1);
                    scriptTree.Nodes.Add(treeNode2);
                }
            }
        }


        public Map.ScriptFunction ScriptFunc
        {
            get
            {
                return sf;
            }
            set
            {
                Stack<String> args = new Stack<String>();
                List<int> jumps = new List<int>();
                sf = value;
                nameBox.Text = sf.name;
                int index = 0;
                //int varNumber = 0;
                scriptBox.m_bPaint = false;
                //scriptBox.Visible = false;
                
                scriptBox.Clear();
               
                while (index < sf.vars.Count)
                {
                    symbBox.Text += String.Format("var{0}[{1}]\r\n", index, sf.vars[index]);
                    index++;
                }
                
                index = 0;
                //foreach (byte b in sf.code)
                // {
                //   scriptBox.Text += String.Format("{0:x2} ", b);
                //}

                string human = "";
                string equals;
                int funccode, opcode, global;
                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(sf.code);
                    System.IO.BinaryReader rdr = new System.IO.BinaryReader(ms);
                    while (ms.Position < ms.Length)
                    {
                        if (jumps.Contains((int)ms.Position / 4))
                        {
                            index = human.Length;
                            foreach (string s in args.ToArray())
                                human = human.Insert(index, s + "\r\n");
                            human += String.Format(":{0}\r\n", ms.Position / 4);
                            args.Clear();
                        }
                        opcode = rdr.ReadInt32();
                        switch (opcode)
                        {
                            case 0:
                            case 3:
                                global = rdr.ReadInt32();
                                System.Diagnostics.Debug.Assert(global == 0 || global == 1);
                                equals = global == 0 ? "var" : "Gvar";
                                args.Push(equals + rdr.ReadInt32().ToString());
                                break;
                            case 1:
                                global = rdr.ReadInt32();
                                System.Diagnostics.Debug.Assert(global == 0 || global == 1);
                                equals = global == 0 ? "varF" : "GvarF";
                                args.Push(equals + rdr.ReadInt32().ToString());
                                break;
                            case 2:
                                global = rdr.ReadInt32();
                                System.Diagnostics.Debug.Assert(global == 0 || global == 1);
                                equals = global == 0 ? "var" : "Gvar";
                                args.Push(equals + rdr.ReadInt32().ToString());
                                break;
                            case 4:
                                args.Push(rdr.ReadInt32().ToString());
                                break;
                            case 5:
                                System.Globalization.NumberFormatInfo nfi_float = new System.Globalization.NumberFormatInfo();
                                nfi_float.NumberDecimalSeparator = ".";
                                args.Push("f" + rdr.ReadSingle().ToString(nfi_float));
                                break;
                            case 6:
                                args.Push('"' + (string)Scripts.SctStr[rdr.ReadInt32()] + '"');
                                break;
                            case 7:
                                equals = args.Pop();
                                args.Push(args.Pop() + " + " + equals);
                                break;
                            case 8:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f+ " + equals);
                                break;
                            case 9:
                                equals = args.Pop();
                                args.Push(args.Pop() + " - " + equals);
                                break;
                            case 0xa:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f- " + equals);
                                break;
                            case 0xb:
                                equals = args.Pop();
                                args.Push(args.Pop() + " * " + equals);
                                break;
                            case 0xc:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f* " + equals);
                                break;
                            case 0xd:
                                equals = args.Pop();
                                args.Push(args.Pop() + " / " + equals);
                                break;
                            case 0xe:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f/ " + equals);
                                break;
                            case 0xf:
                                equals = args.Pop();
                                args.Push(args.Pop() + " % " + equals);
                                break;
                            case 0x10:
                                equals = args.Pop();
                                args.Push(args.Pop() + " & " + equals);
                                break;
                            case 0x11:
                                equals = args.Pop();
                                args.Push(args.Pop() + " | " + equals);
                                break;
                            case 0x12:
                                equals = args.Pop();
                                args.Push(args.Pop() + " ^ " + equals);
                                break;
                            case 0x26:
                                equals = args.Pop();
                                args.Push(args.Pop() + " << " + equals);
                                break;
                            case 0x27:
                                equals = args.Pop();
                                args.Push(args.Pop() + " >> " + equals);
                                break;
                            case 0x13:
                                opcode = rdr.ReadInt32();
                                if (!jumps.Contains(opcode))
                                    jumps.Add(opcode);
                                args.Push("jump " + opcode.ToString());
                                break;
                            case 0x14:
                                opcode = rdr.ReadInt32();
                                if (!jumps.Contains(opcode))
                                    jumps.Add(opcode);
                                args.Push("if " + args.Pop() + " jump " + opcode.ToString());
                                break;
                            case 0x15:
                                opcode = rdr.ReadInt32();
                                if (!jumps.Contains(opcode))
                                    jumps.Add(opcode);
                                args.Push("if not " + args.Pop() + " jump " + opcode.ToString());
                                break;
                            case 0x23:
                                equals = args.Pop();
                                args.Push(args.Pop() + " == " + equals);
                                break;
                            case 0x24:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f== " + equals);
                                break;
                            case 0x28:
                                equals = args.Pop();
                                args.Push(args.Pop() + " < " + equals);
                                break;
                            case 0x29:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f< " + equals);
                                break;
                            case 0x2B:
                                equals = args.Pop();
                                args.Push(args.Pop() + " > " + equals);
                                break;
                            case 0x2C:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f> " + equals);
                                break;
                            case 0x2E:
                                equals = args.Pop();
                                args.Push(args.Pop() + " <= " + equals);
                                break;
                            case 0x2F:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f<= " + equals);
                                break;
                            case 0x31:
                                equals = args.Pop();
                                args.Push(args.Pop() + " >= " + equals);
                                break;
                            case 0x32:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f>= " + equals);
                                break;
                            case 0x34:
                                equals = args.Pop();
                                args.Push(args.Pop() + " != " + equals);
                                break;
                            case 0x35:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f!= " + equals);
                                break;
                            case 0x37:
                                equals = args.Pop();
                                args.Push(args.Pop() + " && " + equals);
                                break;
                            case 0x38:
                                equals = args.Pop();
                                args.Push(args.Pop() + " || " + equals);
                                break;
                            case 0x3f:
                                args.Push("NOT " + args.Pop());
                                break;
                            case 0x40:
                                args.Push("NEG " + args.Pop());
                                break;
                            case 0x16:
                                equals = args.Pop();
                                args.Push(args.Pop() + " = " + equals);
                                break;
                            case 0x17:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f= " + equals);
                                break;
                            case 0x19:
                                equals = args.Pop();
                                args.Push(args.Pop() + " *= " + equals);
                                break;
                            case 0x1A:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f*= " + equals);
                                break;
                            case 0x1B:
                                equals = args.Pop();
                                args.Push(args.Pop() + " /= " + equals);
                                break;
                            case 0x1C:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f/= " + equals);
                                break;
                            case 0x1D:
                                equals = args.Pop();
                                args.Push(args.Pop() + " += " + equals);
                                break;
                            case 0x1E:
                                equals = args.Pop();
                                args.Push(args.Pop() + " f+= " + equals);
                                break;
                            case 0x41:
                                args.Push("-" + args.Pop());
                                break;
                            case 0x44:
                                equals = args.Pop();
                                args.Push(args.Pop() + "[" + equals + "]");
                                break;
                            case 0x45:
                                funccode = rdr.ReadInt32();
                                if (Enum.IsDefined(typeof(methods), funccode))
                                {
                                    equals = Enum.GetName(typeof(methods), funccode) + "(";
                                    index = equals.Length;
                                    for (int i = 0; i < (int)Enum.Parse(typeof(numArgs), Enum.GetName(typeof(methods), funccode)); i++)
                                        equals = equals.Insert(index, (i == 0) ? args.Pop() : args.Pop() + ",");
                                    equals += ')';
                                    args.Push(equals);
                                }
                                else
                                    System.Diagnostics.Debug.WriteLine(String.Format("Funccode:{0:x2} does not exist!", funccode));
                                break;
                            case 0x46:
                                args.Push("call " + Scripts.Funcs[rdr.ReadInt32()].name);
                                break;
                            case 0x48:
                                args.Push("return");
                                break;
                            case 0x42:
                                equals = args.Pop();
                                args.Push(args.Pop() + "[" + equals + "]");
                                break;
                            default:
                                System.Diagnostics.Debug.WriteLine(String.Format("Unknown opcode:{0:x2}", opcode), "ScriptFunctionDialog");
                                break;
                        }
                    }
                    index = human.Length;
                    foreach (string s in args.ToArray())
                        human = human.Insert(index, s + "\r\n");
                    scriptBox.Text = human;
                    scriptBox.ProcessBox();
                }
                catch (Exception)
                {
                    scriptBox.m_bPaint = true;
                    scriptBox.Visible = true;
                    return;
                }
                scriptBox.m_bPaint = true;
                //scriptBox.Visible = true;
            }

        }
        protected List<String> strings;
        private RichTextBox rtxtDesc;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private TextBox symbBox;
        private Button okButton;
        private Label label1;
        private Button cancelButton;
        private GroupBox groupBox2;
        private TreeView scriptTree;
        private Label label2;
        private TextBox nameBox;
        private Label label3;
        private SyntaxRichTextBox scriptBox;
        private ListBox listMethods;
        public List<String> ScriptStrings
        {
            get
            {
                return strings;
            }
            set
            {
                strings = value;
            }
        }
        protected List<Map.ScriptFunction> funcs;
        public List<Map.ScriptFunction> ScriptFunctions
        {
            get
            {
                return funcs;
            }
            set
            {
                funcs = value;
            }
        }
        public ScriptFunctionDialog()
        {
            InitializeComponent();
            isinited = true;
            listMethods.Items.AddRange(Enum.GetNames(typeof(methods)));
            foreach (string str in Enum.GetNames(typeof(methods)))
            {
                scriptBox.Settings.Keywords.Add(str);
            }
            string[] words = new string[]
        {
            "int",
            "void",
            "null",
            "not",
            "return",
            "jump",
            "Gvar",
            ">>",
            "-",
            "^",
            "|",
            "&",
            "%",
            "/",
            "*",
            "||",
            "&&",
            "!=",
            "<=",
            ">=",
            ">",
            "==",
            "not",
            "neg",
            ":",
            "jump",
            "call",
            "if",
            "=",
            "+"
        };

            foreach (string str in words)
            {
                scriptBox.Settings.Keywords2.Add(str);
            }
            scriptBox.Settings.Comment = "//";
            scriptBox.Settings.CommentColor = Color.Green;
            scriptBox.Settings.EnableComments = true;

            scriptBox.Settings.EnableIntegers = true;
            scriptBox.Settings.EnableStrings = true;
            scriptBox.Settings.IntegerColor = Color.Blue;
            scriptBox.Settings.Keyword2Color = Color.Blue;
            scriptBox.Settings.KeywordColor = Color.SteelBlue;
            scriptBox.Settings.StringColor = Color.Red;
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.components = new System.ComponentModel.Container();
            this.treeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtxtDesc = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.symbBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scriptTree = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.scriptBox = new SyntaxHighlighter.SyntaxRichTextBox();
            this.listMethods = new System.Windows.Forms.ListBox();
            this.treeMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeMenu
            // 
            this.treeMenu.AllowDrop = true;
            this.treeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // rtxtDesc
            // 
            this.rtxtDesc.Location = new System.Drawing.Point(6, 13);
            this.rtxtDesc.Name = "rtxtDesc";
            this.rtxtDesc.ReadOnly = true;
            this.rtxtDesc.Size = new System.Drawing.Size(673, 102);
            this.rtxtDesc.TabIndex = 7;
            this.rtxtDesc.Text = "";
            this.rtxtDesc.TextChanged += new System.EventHandler(this.rtxtDesc_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtDesc);
            this.groupBox1.Location = new System.Drawing.Point(4, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 123);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.symbBox);
            this.groupBox3.Controls.Add(this.okButton);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cancelButton);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.nameBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.scriptBox);
            this.groupBox3.Controls.Add(this.listMethods);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(4, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox3.Size = new System.Drawing.Size(685, 454);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // symbBox
            // 
            this.symbBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.symbBox.Location = new System.Drawing.Point(6, 38);
            this.symbBox.Multiline = true;
            this.symbBox.Name = "symbBox";
            this.symbBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.symbBox.Size = new System.Drawing.Size(287, 88);
            this.symbBox.TabIndex = 2;
            this.symbBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.symbBox_MouseClick);
            this.symbBox.TextChanged += new System.EventHandler(this.symbBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(104, 424);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(476, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Function Name";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(301, 424);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scriptTree);
            this.groupBox2.Location = new System.Drawing.Point(482, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 361);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // scriptTree
            // 
            this.scriptTree.ContextMenuStrip = this.treeMenu;
            this.scriptTree.LabelEdit = true;
            this.scriptTree.Location = new System.Drawing.Point(6, 19);
            this.scriptTree.Name = "scriptTree";
            this.scriptTree.Size = new System.Drawing.Size(185, 336);
            this.scriptTree.TabIndex = 12;
            this.scriptTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.scriptTree_BeforeLabelEdit);
            this.scriptTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.scriptTree_AfterLabelEdit);
            this.scriptTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.scriptTree_AfterSelect);
            this.scriptTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.scriptTree_NodeMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Variables";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(482, 38);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(197, 20);
            this.nameBox.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Methods";
            // 
            // scriptBox
            // 
            this.scriptBox.BackColor = System.Drawing.Color.White;
            this.scriptBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptBox.ForeColor = System.Drawing.Color.Black;
            this.scriptBox.HideSelection = false;
            this.scriptBox.Location = new System.Drawing.Point(6, 132);
            this.scriptBox.Name = "scriptBox";
            this.scriptBox.Size = new System.Drawing.Size(470, 286);
            this.scriptBox.TabIndex = 3;
            this.scriptBox.Text = "";
            this.scriptBox.WordWrap = false;
            this.scriptBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.scriptBox_MouseClick);
            this.scriptBox.TextChanged += new System.EventHandler(this.scriptBox_TextChanged);
            this.scriptBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scriptBox_KeyPress);
            this.scriptBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.scriptBox_KeyUp);
            // 
            // listMethods
            // 
            this.listMethods.FormattingEnabled = true;
            this.listMethods.Location = new System.Drawing.Point(299, 44);
            this.listMethods.Name = "listMethods";
            this.listMethods.Size = new System.Drawing.Size(177, 82);
            this.listMethods.Sorted = true;
            this.listMethods.TabIndex = 6;
            this.listMethods.SelectedIndexChanged += new System.EventHandler(this.listMethods_SelectedIndexChanged);
            this.listMethods.DoubleClick += new System.EventHandler(this.listMethods_DoubleClick);
            // 
            // ScriptFunctionDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(684, 573);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 1200);
            this.MinimumSize = new System.Drawing.Size(487, 600);
            this.Name = "ScriptFunctionDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Scripting";
            this.Load += new System.EventHandler(this.ScriptFunctionDialog_Load);
            this.Resize += new System.EventHandler(this.ScriptFunctionDialog_Resize);
            this.treeMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private void okButton_Click(object sender, EventArgs e)
        {

            try
            {

                ParseFunction();
            }
            catch {
                MessageBox.Show("Wrong Syntax!");
                return;
            }
            Close();
        }



        private void ParseFunction()
        {
            if (sf == null)
            {
                return;
            }

            sf.name = nameBox.Text;

            MemoryStream ms = new MemoryStream();
            BinaryWriter wtr = new BinaryWriter(ms);
            if (scriptBox.Text.Contains("00 00 00"))
            {
                Regex bytes = new Regex("[0-9|a-f|A-F]{2}");
                foreach (Match match in bytes.Matches(scriptBox.Text))
                    wtr.Write(Convert.ToByte(match.Value, 16));
            }
            else
            {
                string s = "", scr = scriptBox.Text;
                int start, end, flags, linenum = 0;
                Dictionary<int, string> jumps = new Dictionary<int, string>();
                Dictionary<string, int> labels = new Dictionary<string, int>();

                while ((start = scr.IndexOf('"')) != -1)
                {
                    end = scr.IndexOf('"', start + 1);
                    s = scr.Substring(start + 1, end - start - 1);
                    if (!Scripts.SctStr.Contains(s))
                        Scripts.SctStr.Add(s);
                    scr = scr.Remove(start, end - start + 1).Insert(start, String.Format("*{0}", Scripts.SctStr.IndexOf(s)));
                }
                foreach (String str in scr.Split('\n'))
                {
                    linenum++;
                    flags = 0;
                    string line = str.Trim();
                    if (line.StartsWith("Gvar") && line.Contains("=")) // Set global variable
                    {
                        int line_len = line.StartsWith("GvarF") ? 5 : 4;
                        wtr.Write(2);
                        wtr.Write(1);
                        if (line.Substring(line_len + 1, 1) == " ")
                        wtr.Write(Int32.Parse(line.Substring(line_len, line.IndexOf('[') > -1 ? line.IndexOf('[') - line_len : line.IndexOf(' ') - line_len)));
                        else
                            wtr.Write(Int32.Parse(line.Substring(line_len, line.IndexOf('[') > -1 ? line.IndexOf('[') - line_len : line.IndexOf('=') - line_len)));
                        if (line.IndexOf('[') > -1)
                        {
                            try
                            {
                                parseWord(wtr, 0, line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1));
                            }
                            catch 
                            {

                                MessageBox.Show("Wromg Syntax!");
                                return;
                            }   
                                wtr.Write(0x44);
                        }
                        switch (line[line.IndexOf("=") - 1])
                        {
                            case 'f':
                                flags = 0x17;
                                break;
                            case '+':
                                flags = (line[line.IndexOf("=") - 2]) == 'f' ? 0x1E : 0x1D;
                                break;
                            default:
                                flags = 0x16;
                                break;
                        }
                        line = line.Split('=')[1].Trim();
                    }
                    else if (line.StartsWith("var") && line.Contains("=")) // Set local variable
                    {

                        int line_len = line.StartsWith("varF") ? 4 : 3;
                        wtr.Write(2);
                        wtr.Write(0);//var0 = Ob
                        //MessageBox.Show(line.Substring(line_len+1,1));
                        if (line.Substring(line_len+1,1) == " ")
                        wtr.Write(Int32.Parse(line.Substring(line_len, line.IndexOf('[') > -1 ? line.IndexOf('[') - line_len : line.IndexOf(' ') - line_len)));
                        else
                        wtr.Write(Int32.Parse(line.Substring(line_len, line.IndexOf('[') > -1 ? line.IndexOf('[') - line_len : line.IndexOf('=') - line_len)));
                        if (line.IndexOf('[') > -1)
                        {

                            try
                            {
                                parseWord(wtr, 0, line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1));
                            }
                            catch
                            {

                                MessageBox.Show("Wrong Syntax!");
                                return;
                            }   
                            wtr.Write(0x44);
                        }
                        switch (line[line.IndexOf("=") - 1])
                        {
                            case 'f':
                                flags = 0x17;
                                break;
                            case '+':
                                flags = (line[line.IndexOf("=") - 2]) == 'f' ? 0x1E : 0x1D;
                                break;
                            default:
                                flags = 0x16;
                                break;
                        }
                        line = line.Split('=')[1].Trim();
                    }
                    else if (line.StartsWith("if")) // If - jump statement
                    {
                        flags = 0x14;
                        if (line.StartsWith("if not"))
                        {
                            flags++;
                            line = line.Remove(0, 6).Trim();
                        }
                        else
                        {
                            line = line.Remove(0, 2).Trim();
                        }
                        s = line.Substring(line.IndexOf("jump ") + 5).Trim();
                        line = line.Remove(line.IndexOf("jump ")).Trim();
                    }
                    else if (line.Equals("return"))
                    {
                        wtr.Write(0x48);
                        line = "";
                    }
                    else if (line.StartsWith("call "))
                    {
                        wtr.Write(0x46);
                        wtr.Write(Scripts.Funcs.IndexOf(new Map.ScriptFunction(line.Substring(line.IndexOf("call ") + 5))));
                        line = "";
                    }
                    else if (line.StartsWith("jump "))
                    {
                        s = line.Substring(line.IndexOf("jump ") + 5).Trim();
                        wtr.Write(0x13);
                        jumps.Add((int)ms.Position, s);
                        wtr.Write(0);
                        line = "";
                    }
                    else if (line.StartsWith(":"))
                    {
                        labels.Add(line.Substring(1), (int)ms.Position / 4);
                        line = "";
                    }
                    while (line.Length > 0)
                    {




                        try {
                        line = parseWord(wtr, 0, line);
                        }
                        catch {
                            MessageBox.Show("Wrong Syntax!");
                            return;
                        }
                    }
                    if (flags != 0)
                        wtr.Write(flags);
                    if (flags == 0x14 || flags == 0x15)
                    {
                        jumps.Add((int)ms.Position, s);
                        wtr.Write(0);
                    }
                }
                try
                {
                    foreach (KeyValuePair<int, string> kv in jumps)
                    {
                        ms.Seek(kv.Key, SeekOrigin.Begin);
                        wtr.Write(labels[kv.Value]);
                    }
                }
                catch
                {
                    MessageBox.Show("Wrong Syntax!");
                    return;
                }
            }
            sf.code = ms.ToArray();
            sf.vars.Clear();
            if (symbBox.Text.Length > 0)
            {
                string[] vars;
                vars = symbBox.Text.Split('\n');
                foreach (string s in vars)
                    if (s.IndexOf('[') > -1)
                        sf.vars.Add(Int32.Parse(s.Remove(s.IndexOf(']'), 1).Remove(0, s.IndexOf('[') + 1).Trim()));
            }
        }
        private string parseWord(BinaryWriter wtr, int wordi, string line)
        {
            string[] words = line.Split(' '), args;
            string word = line.Split(' ')[wordi], s, name, s2, array = "";
            float tempF = 0;
            int tempI = 0;
            s = Join(wordi, line);
            if (word.Length > 0)
            {
                switch (word)
                {
                    case "NEG":
                        s = s.Remove(s.IndexOf("NEG"), 4);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x40);
                        break;
                    case "NOT":

                        s = s.Remove(s.IndexOf("NOT"), 4);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x3F);
                        break;
                    case "==":

                        s = s.Remove(s.IndexOf("=="), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x23);
                        break;
                    case "f==":

                        s = s.Remove(s.IndexOf("f=="), 4);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x24);
                        break;
                    case "<":

                        s = s.Remove(s.IndexOf("<"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x28);
                        break;
                    case "f<":

                        s = s.Remove(s.IndexOf("f<"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x29);
                        break;
                    case ">":

                        s = s.Remove(s.IndexOf(">"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x2B);
                        break;
                    case "f>":

                        s = s.Remove(s.IndexOf("f>"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x2C);
                        break;
                    case "<=":

                        s = s.Remove(s.IndexOf("<="), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x2E);
                        break;
                    case "f<=":

                        s = s.Remove(s.IndexOf("f<="), 4);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x2F);
                        break;
                    case ">=":

                        s = s.Remove(s.IndexOf(">="), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x31);
                        break;
                    case "f>=":

                        s = s.Remove(s.IndexOf("f>="), 4);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x32);
                        break;
                    case "!=":

                        s = s.Remove(s.IndexOf("!="), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x34);
                        break;
                    case "f!=":

                        s = s.Remove(s.IndexOf("f!="), 4);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x35);
                        break;
                    case "&&":

                        s = s.Remove(s.IndexOf("&&"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x37);
                        break;
                    case "||":

                        s = s.Remove(s.IndexOf("||"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x38);
                        break;
                    case "+":

                        s = s.Remove(s.IndexOf("+"), 2);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x7);
                        break;
                    case "f+":

                        s = s.Remove(s.IndexOf("f+"), 3);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x8);
                        break;
                    case "-":

                        s = s.Remove(s.IndexOf("-"), 2);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x9);
                        break;
                    case "f-":

                        s = s.Remove(s.IndexOf("f-"), 3);

                        s = parseWord(wtr, 0, s);
                        wtr.Write(0xA);
                        break;
                    case "*":

                        s = s.Remove(s.IndexOf("*"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0xb);
                        break;
                    case "f*":

                        s = s.Remove(s.IndexOf("f*"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0xc);
                        break;
                    case "/":

                        s = s.Remove(s.IndexOf("/"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0xd);
                        break;
                    case "f/":

                        s = s.Remove(s.IndexOf("f/"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0xe);
                        break;
                    case "%":

                        s = s.Remove(s.IndexOf("%"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0xf);
                        break;
                    case "&":

                        s = s.Remove(s.IndexOf("&"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x10);
                        break;
                    case "|":

                        s = s.Remove(s.IndexOf("|"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x11);
                        break;
                    case "^":

                        s = s.Remove(s.IndexOf("^"), 2);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x12);
                        break;
                    case "<<":

                        s = s.Remove(s.IndexOf("<<"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x26);
                        break;
                    case ">>":

                        s = s.Remove(s.IndexOf(">>"), 3);
                        s = Join(wordi, s);
                        s = parseWord(wtr, 0, s);
                        wtr.Write(0x27);
                        break;
                    default:
                        if (word.StartsWith("-"))
                        {
                            s = s.Remove(s.IndexOf('-'), 1);
                            s = Join(wordi, s);
                            s = parseWord(wtr, 0, s);
                            wtr.Write(0x41);
                        }
                        else if (word.StartsWith("Gvar")) // Set global variable
                        {

                            if (word.EndsWith("]"))
                            {
                                array = word.Substring(word.IndexOf('[') + 1, word.IndexOf(']') - word.IndexOf('[') - 1);
                                wtr.Write(2);
                                word = word.Remove(word.IndexOf('['), word.IndexOf(']') - word.IndexOf('[') + 1);
                            }
                            else if (word[4] == 'F')
                            {
                                wtr.Write(1);
                                word = word.Remove(4, 1);
                            }
                            else
                                wtr.Write(0);
                            wtr.Write(1);
                            wtr.Write(Int32.Parse(word.Substring(4)));
                            if (array.Length > 0)
                            {
                                while (array.Length > 0)
                                    array = parseWord(wtr, 0, array);
                                wtr.Write(0x44);
                            }
                            s = Join(wordi + 1, line);
                        }
                        else if (word.StartsWith("var")) // Set local variable
                        {
                            if (word.EndsWith("]"))
                            {
                                array = word.Substring(word.IndexOf('[') + 1, word.IndexOf(']') - word.IndexOf('[') - 1);
                                wtr.Write(2);
                                word = word.Remove(word.IndexOf('['), word.IndexOf(']') - word.IndexOf('[') + 1);
                            }
                            else if (word[3] == 'F')
                            {
                                wtr.Write(1);
                                word = word.Remove(3, 1);
                            }
                            else
                                wtr.Write(0);
                            wtr.Write(0);
                            wtr.Write(Int32.Parse(word.Substring(3)));
                            if (array.Length > 0)
                            {
                                while (array.Length > 0)
                                    array = parseWord(wtr, 0, array);
                                wtr.Write(0x44);
                            }
                            s = Join(wordi + 1, line);
                        }
                        else if (word.Contains("("))
                        {
                            name = s.Remove(s.IndexOf('('));
                            int end = findClosing(s.Substring(s.IndexOf('(') + 1));
                            if (end > 0)
                                args = s.Substring(s.IndexOf('(') + 1, end - 1).Split(',');
                            else
                                args = new string[0];
                            foreach (string arg in args)
                            {
                                s2 = arg;
                                while (s2.Length > 0)
                                    s2 = parseWord(wtr, 0, s2);
                            }
                            wtr.Write(0x45);
                            wtr.Write((int)Enum.Parse(typeof(methods), name, true));
                            s = s.Remove(0, end + 1 + s.IndexOf('(')).Trim();
                        }
                        else if (word[0] == '*' && word[1] != ' ')
                        {
                            wtr.Write(6);
                            wtr.Write(Int32.Parse(word.Remove(0, 1)));
                            s = Join(wordi + 1, line);
                        }
                        else if (word[0] == 'f' && Single.TryParse(word.Remove(0, 1), out tempF))
                        {
                            wtr.Write(5);
                            wtr.Write(tempF);
                            s = Join(wordi + 1, line);
                        }
                        else if (Int32.TryParse(word, out tempI))
                        {
                            wtr.Write(4);
                            wtr.Write(tempI);
                            s = Join(wordi + 1, line);
                        }
                        else if (word.Length > 0)
                        {
                            return "";
                        }
                        break;
                }
            }
            return s;
        }
        private string Join(int wordi, string line)
        {
            string[] words = line.Split(' ');
            string newline = "";
            for (int i = wordi; i < words.Length; i++)
                newline += words[i] + " ";
            return newline.Trim();
        }
        private int findClosing(string code)
        {
            int count = 0, i = 0;
            for (i = 0; i < code.Length && count > -1; i++)
            {
                if (code[i] == '(')
                    count++;
                if (code[i] == ')')
                    count--;
            }
            return i;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rtxtDesc.Clear();
                string m_ExePath = Process.GetCurrentProcess().MainModule.FileName;
                m_ExePath = Path.GetDirectoryName(m_ExePath);
                if (File.Exists(m_ExePath + "\\functiondescs\\" + listMethods.SelectedItem.ToString().ToLower() + ".rtf"))
                {
                    rtxtDesc.LoadFile(m_ExePath + "\\functiondescs\\" + listMethods.SelectedItem.ToString().ToLower() + ".rtf");
                }
                else
                    rtxtDesc.Text += "No method description found";
            }
            catch
            { }
        }

        private void scriptBox_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void scriptBox_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void listMethods_DoubleClick(object sender, EventArgs e)
        {

            string func = (string)listMethods.SelectedItem;
            int num = 0;
            try
            {
                num = (int)Enum.Parse(typeof(numArgs), (string)listMethods.SelectedItem);
            }
            catch
            { }
            func += "(";

            for (int i = 1; i <= num; i++)
            {
                if (i == 1)
                    func += " ";

                func += "Arg";

                if (i < num)
                    func += ", ";
                else
                    func += " ";
            }

            func += ")";
            scriptBox.SelectedText = func;
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
            if (e == null)
                return;
            if (e.Node.Parent == treeNode1)
            {
                if (e.Label != null && e.Label.Length > 0)
                {
                    name = e.Label;
                    Scripts.SctStr[e.Node.Index] = name;
                }
                else
                {
                    treeNode1.Nodes[e.Node.Index].Name = prevname;
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button hok = sender as Button;
            if (scriptTree.SelectedNode != null)
            {
                if (scriptTree.SelectedNode == treeNode1)
                {
                    treeNode1.Nodes.Add("New String");
                    Scripts.SctStr.Add("New String");
                }
                if (scriptTree.SelectedNode == treeNode2)
                {
                    treeNode2.Nodes.Add(String.Format("{0}: {1}", Scripts.Funcs.Count, "New Function"));
                    Map.ScriptFunction sf = new Map.ScriptFunction();
                    sf.name = "New Function";
                    sf.code = new byte[0];
                    Scripts.Funcs.Add(sf);

                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scriptTree.SelectedNode != null)
            {
                if (scriptTree.SelectedNode.Parent == treeNode1)
                {
                    Scripts.SctStr.Remove(scriptTree.SelectedNode.Text);
                    treeNode1.Nodes.Remove(scriptTree.SelectedNode);
                }
                if (scriptTree.SelectedNode.Parent == treeNode2)
                {
                    Map.ScriptFunction toDelete = null;
                    toDelete = (Map.ScriptFunction)Scripts.Funcs[scriptTree.SelectedNode.Index];
                    if (toDelete != null)
                    {
                        Scripts.Funcs.Remove(toDelete);
                        treeNode2.Nodes.Clear();
                        int i = 0;
                        foreach (Map.ScriptFunction sf in Scripts.Funcs)
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
                try
                {

                    ParseFunction();
                }
                catch
                {
                    MessageBox.Show("Wrong Syntax!");

                }

                symbBox.Clear();
                scriptBox.m_bPaint = false;
                ScriptFunc = (Map.ScriptFunction)Scripts.Funcs[e.Node.Index];
                scriptBox.m_bPaint = true;
            }
        }

        private void scriptTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void ScriptFunctionDialog_Resize(object sender, EventArgs e)
        {
            if (!isinited)
                return;
            int sizeup = this.Height - groupBox3.Height - 20;
            groupBox3.Top = this.Height - groupBox3.Height - 30;
            groupBox1.Height = sizeup;
            rtxtDesc.Height = groupBox1.Height - rtxtDesc.Top - 20;



        }

        private void ScriptFunctionDialog_Load(object sender, EventArgs e)
        {
            //int tempval;
        }

        private void rtxtDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void symbBox_MouseClick(object sender, MouseEventArgs e)
        {


           // if (symbBox.Text != "")
               // generateVars();
/*
            List<string> vars = new List<string>();
            string s = "nene", scr = scriptBox.Text;
            int Numlength;
            foreach (String str in scr.Split('\n'))
            {
         
                string line = str.Trim();
                if (line.StartsWith("var") && line.Contains("="))
                {
                    s = line.Substring(line.IndexOf("var") + 3).Trim();
                    s = string.Join("", s.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                    //MessageBox.Show(s);
                       Numlength = s.IndexOf("=");
                       s = s.Substring(0, Numlength);
                       s = "var" + s + "[1]" + System.Environment.NewLine;
                    if(!vars.Contains(s))   
                    vars.Add(s);
                    
                    vars.Sort();

               
                } 
                
            }



           
            string addVars = "";
            
            for (int i = 0; i < vars.Count; i++)
            {
                addVars += vars[i];//"var" + i.ToString() + "[1]" + System.Environment.NewLine;

            }
            symbBox.Text = addVars;
            */

        }
        private void generateVars()
        {


            symbBox.Clear();
            //if (symbBox.Text != "")
               // return;

            List<string> vars = new List<string>();
            string s = "nene", scr = scriptBox.Text;
            int Numlength;
            int index = 0;
            string svarnum;
            foreach (String str in scr.Split('\n'))
            {

                string line = str.Trim();
                if (line.StartsWith("var") && line.Contains("="))
                {
                    s = line.Substring(line.IndexOf("var") + 3).Trim();
                    s = string.Join("", s.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                   // MessageBox.Show(s);
                    if(s.Contains("+="))
                    Numlength = s.IndexOf("+=");
                    else if (s.Contains("-="))
                        Numlength = s.IndexOf("-=");
                    else
                        Numlength = s.IndexOf("=");
                    svarnum = s.Substring(0, Numlength);
                    s = "var" + svarnum + "[1]" + System.Environment.NewLine;
                   // if (index.ToString() != svarnum)
                     //   MessageBox.Show("index:" + index + " svarnum:" + svarnum);

                    if (!vars.Contains(s))
                    {
                        vars.Add(s);
                        index++;
                    }


                    vars.Sort();


                }

            }




            string addVars = "";

            for (int i = 0; i < vars.Count; i++)
            {
                addVars += vars[i];//"var" + i.ToString() + "[1]" + System.Environment.NewLine;

            }
            symbBox.Text = addVars;

        }
        private void symbBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void scriptBox_KeyUp(object sender, KeyEventArgs e)
        {
            generateVars();

        }

        private void scriptBox_KeyPress(object sender, KeyPressEventArgs e)
        {
           // if (scriptBox.Text.Length < 2)  e.Handled = true;
            //return; 
               
            
        }
    }
}