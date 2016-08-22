/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 01.12.2014
 */
namespace MapEditor.noxscript2
{
	partial class ScriptUserControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.functionsListBox = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.varSizeBox = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonDelVar = new System.Windows.Forms.Button();
			this.buttonNewVar = new System.Windows.Forms.Button();
			this.varTypeComboBox = new System.Windows.Forms.ComboBox();
			this.varNameTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.variablesListBox = new System.Windows.Forms.ListBox();
			this.codeBox = new MapEditor.noxscript2.CodeTextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.codeStatus = new System.Windows.Forms.Label();
			this.menuFuncOperation = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.decompileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.varSizeBox)).BeginInit();
			this.menuFuncOperation.SuspendLayout();
			this.SuspendLayout();
			// 
			// functionsListBox
			// 
			this.functionsListBox.FormattingEnabled = true;
			this.functionsListBox.Location = new System.Drawing.Point(8, 18);
			this.functionsListBox.Name = "functionsListBox";
			this.functionsListBox.Size = new System.Drawing.Size(166, 329);
			this.functionsListBox.TabIndex = 1;
			this.functionsListBox.SelectedIndexChanged += new System.EventHandler(this.FunctionsListBoxSelectedIndexChanged);
			this.functionsListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FunctionsListBoxMouseDown);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.functionsListBox);
			this.groupBox1.Location = new System.Drawing.Point(504, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(180, 357);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "User Script Functions";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.varSizeBox);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.buttonDelVar);
			this.groupBox3.Controls.Add(this.buttonNewVar);
			this.groupBox3.Controls.Add(this.varTypeComboBox);
			this.groupBox3.Controls.Add(this.varNameTextBox);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.variablesListBox);
			this.groupBox3.Location = new System.Drawing.Point(504, 367);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(140, 224);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Function Variables";
			// 
			// varSizeBox
			// 
			this.varSizeBox.Location = new System.Drawing.Point(64, 168);
			this.varSizeBox.Maximum = new decimal(new int[] {
			128,
			0,
			0,
			0});
			this.varSizeBox.Name = "varSizeBox";
			this.varSizeBox.Size = new System.Drawing.Size(64, 20);
			this.varSizeBox.TabIndex = 8;
			this.varSizeBox.ValueChanged += new System.EventHandler(this.VarSizeBoxValueChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 23);
			this.label6.TabIndex = 7;
			this.label6.Text = "Arr. Size";
			// 
			// buttonDelVar
			// 
			this.buttonDelVar.Location = new System.Drawing.Point(72, 192);
			this.buttonDelVar.Name = "buttonDelVar";
			this.buttonDelVar.Size = new System.Drawing.Size(56, 23);
			this.buttonDelVar.TabIndex = 6;
			this.buttonDelVar.Text = "Delete";
			this.buttonDelVar.UseVisualStyleBackColor = true;
			// 
			// buttonNewVar
			// 
			this.buttonNewVar.Location = new System.Drawing.Point(8, 192);
			this.buttonNewVar.Name = "buttonNewVar";
			this.buttonNewVar.Size = new System.Drawing.Size(56, 23);
			this.buttonNewVar.TabIndex = 5;
			this.buttonNewVar.Text = "New";
			this.buttonNewVar.UseVisualStyleBackColor = true;
			// 
			// varTypeComboBox
			// 
			this.varTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.varTypeComboBox.FormattingEnabled = true;
			this.varTypeComboBox.Items.AddRange(new object[] {
			"int",
			"float",
			"string"});
			this.varTypeComboBox.Location = new System.Drawing.Point(48, 144);
			this.varTypeComboBox.Name = "varTypeComboBox";
			this.varTypeComboBox.Size = new System.Drawing.Size(80, 21);
			this.varTypeComboBox.TabIndex = 4;
			this.varTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.VarTypeComboBoxSelectedIndexChanged);
			// 
			// varNameTextBox
			// 
			this.varNameTextBox.Location = new System.Drawing.Point(48, 120);
			this.varNameTextBox.Name = "varNameTextBox";
			this.varNameTextBox.Size = new System.Drawing.Size(80, 20);
			this.varNameTextBox.TabIndex = 3;
			this.varNameTextBox.TextChanged += new System.EventHandler(this.VarNameTextBoxTextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 23);
			this.label4.TabIndex = 2;
			this.label4.Text = "Type";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Name";
			// 
			// variablesListBox
			// 
			this.variablesListBox.FormattingEnabled = true;
			this.variablesListBox.Location = new System.Drawing.Point(8, 16);
			this.variablesListBox.Name = "variablesListBox";
			this.variablesListBox.ScrollAlwaysVisible = true;
			this.variablesListBox.Size = new System.Drawing.Size(120, 95);
			this.variablesListBox.TabIndex = 0;
			this.variablesListBox.SelectedIndexChanged += new System.EventHandler(this.VariablesListBoxSelectedIndexChanged);
			// 
			// codeBox
			// 
			this.codeBox.Font = new System.Drawing.Font("DejaVu Sans", 9.75F);
			this.codeBox.Location = new System.Drawing.Point(5, 25);
			this.codeBox.Name = "codeBox";
			this.codeBox.Size = new System.Drawing.Size(493, 540);
			this.codeBox.TabIndex = 8;
			this.codeBox.Text = "";
			this.codeBox.WordWrap = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(20, 4);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 7;
			this.label5.Text = "Script Code";
			// 
			// codeStatus
			// 
			this.codeStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.codeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.codeStatus.Location = new System.Drawing.Point(5, 568);
			this.codeStatus.Name = "codeStatus";
			this.codeStatus.Size = new System.Drawing.Size(493, 23);
			this.codeStatus.TabIndex = 9;
			// 
			// menuFuncOperation
			// 
			this.menuFuncOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.decompileToolStripMenuItem,
			this.compileToolStripMenuItem,
			this.renameEditToolStripMenuItem,
			this.deleteToolStripMenuItem,
			this.createNewToolStripMenuItem});
			this.menuFuncOperation.Name = "contextMenuStrip1";
			this.menuFuncOperation.ShowImageMargin = false;
			this.menuFuncOperation.Size = new System.Drawing.Size(128, 136);
			this.menuFuncOperation.Text = "Operation";
			// 
			// decompileToolStripMenuItem
			// 
			this.decompileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.decompileToolStripMenuItem.Name = "decompileToolStripMenuItem";
			this.decompileToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.decompileToolStripMenuItem.Text = "Decompile";
			this.decompileToolStripMenuItem.Click += new System.EventHandler(this.DecompileToolStripMenuItemClick);
			// 
			// compileToolStripMenuItem
			// 
			this.compileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
			this.compileToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.compileToolStripMenuItem.Text = "Compile";
			this.compileToolStripMenuItem.Click += new System.EventHandler(this.CompileToolStripMenuItemClick);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
			// 
			// createNewToolStripMenuItem
			// 
			this.createNewToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
			this.createNewToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.createNewToolStripMenuItem.Text = "Create New";
			this.createNewToolStripMenuItem.Click += new System.EventHandler(this.CreateNewToolStripMenuItemClick);
			// 
			// renameEditToolStripMenuItem
			// 
			this.renameEditToolStripMenuItem.Name = "renameEditToolStripMenuItem";
			this.renameEditToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.renameEditToolStripMenuItem.Text = "Rename/Edit";
			// 
			// ScriptUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.codeStatus);
			this.Controls.Add(this.codeBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Name = "ScriptUserControl";
			this.Size = new System.Drawing.Size(698, 612);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.varSizeBox)).EndInit();
			this.menuFuncOperation.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown varSizeBox;
		private System.Windows.Forms.Button buttonNewVar;
		private System.Windows.Forms.Button buttonDelVar;
		private System.Windows.Forms.TextBox varNameTextBox;
		private System.Windows.Forms.ComboBox varTypeComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox variablesListBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox functionsListBox;
		private System.Windows.Forms.Label label5;
		private MapEditor.noxscript2.CodeTextBox codeBox;
		private System.Windows.Forms.Label codeStatus;
		private System.Windows.Forms.ContextMenuStrip menuFuncOperation;
		private System.Windows.Forms.ToolStripMenuItem decompileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameEditToolStripMenuItem;
	}
}
