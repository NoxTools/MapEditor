/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 23.10.2014
 */
namespace MapEditor.XferGui
{
	partial class MonsterGenEdit
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.scriptMSpawned = new System.Windows.Forms.TextBox();
			this.scriptCollided = new System.Windows.Forms.TextBox();
			this.scriptDestroyed = new System.Windows.Forms.TextBox();
			this.scriptDamaged = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.currentMonsterName = new System.Windows.Forms.ComboBox();
			this.checkBoxEnable = new System.Windows.Forms.CheckBox();
			this.buttonMonsterProps = new System.Windows.Forms.Button();
			this.spawnLimitBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.spawnRateBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.monstersListBox = new System.Windows.Forms.ListBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.spawningAlgBox = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.scriptMSpawned);
			this.groupBox1.Controls.Add(this.scriptCollided);
			this.groupBox1.Controls.Add(this.scriptDestroyed);
			this.groupBox1.Controls.Add(this.scriptDamaged);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(302, 110);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Script event handlers";
			// 
			// scriptMSpawned
			// 
			this.scriptMSpawned.Location = new System.Drawing.Point(125, 82);
			this.scriptMSpawned.Name = "scriptMSpawned";
			this.scriptMSpawned.Size = new System.Drawing.Size(152, 20);
			this.scriptMSpawned.TabIndex = 7;
			// 
			// scriptCollided
			// 
			this.scriptCollided.Location = new System.Drawing.Point(125, 59);
			this.scriptCollided.Name = "scriptCollided";
			this.scriptCollided.Size = new System.Drawing.Size(152, 20);
			this.scriptCollided.TabIndex = 6;
			// 
			// scriptDestroyed
			// 
			this.scriptDestroyed.Location = new System.Drawing.Point(125, 36);
			this.scriptDestroyed.Name = "scriptDestroyed";
			this.scriptDestroyed.Size = new System.Drawing.Size(152, 20);
			this.scriptDestroyed.TabIndex = 5;
			// 
			// scriptDamaged
			// 
			this.scriptDamaged.Location = new System.Drawing.Point(125, 13);
			this.scriptDamaged.Name = "scriptDamaged";
			this.scriptDamaged.Size = new System.Drawing.Size(152, 20);
			this.scriptDamaged.TabIndex = 4;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6, 85);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 23);
			this.label7.TabIndex = 3;
			this.label7.Text = "Monster spawned";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 62);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 23);
			this.label6.TabIndex = 2;
			this.label6.Text = "Collided";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 39);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 1;
			this.label5.Text = "Destroyed";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Damaged";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.currentMonsterName);
			this.groupBox2.Controls.Add(this.checkBoxEnable);
			this.groupBox2.Controls.Add(this.buttonMonsterProps);
			this.groupBox2.Controls.Add(this.spawnLimitBox);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.spawnRateBox);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.monstersListBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 157);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(302, 187);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Spawned monsters";
			// 
			// currentMonsterName
			// 
			this.currentMonsterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.currentMonsterName.FormattingEnabled = true;
			this.currentMonsterName.Location = new System.Drawing.Point(144, 40);
			this.currentMonsterName.Name = "currentMonsterName";
			this.currentMonsterName.Size = new System.Drawing.Size(136, 21);
			this.currentMonsterName.TabIndex = 11;
			this.currentMonsterName.SelectedIndexChanged += new System.EventHandler(this.CurrentMonsterNameSelectedIndexChanged);
			// 
			// checkBoxEnable
			// 
			this.checkBoxEnable.Location = new System.Drawing.Point(16, 128);
			this.checkBoxEnable.Name = "checkBoxEnable";
			this.checkBoxEnable.Size = new System.Drawing.Size(104, 24);
			this.checkBoxEnable.TabIndex = 10;
			this.checkBoxEnable.Text = "Not Empty";
			this.checkBoxEnable.UseVisualStyleBackColor = true;
			this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.CheckBoxEnableCheckedChanged);
			// 
			// buttonMonsterProps
			// 
			this.buttonMonsterProps.Location = new System.Drawing.Point(8, 152);
			this.buttonMonsterProps.Name = "buttonMonsterProps";
			this.buttonMonsterProps.Size = new System.Drawing.Size(111, 23);
			this.buttonMonsterProps.TabIndex = 9;
			this.buttonMonsterProps.Text = "Monster Properties";
			this.buttonMonsterProps.UseVisualStyleBackColor = true;
			this.buttonMonsterProps.Click += new System.EventHandler(this.ButtonMonsterPropsClick);
			// 
			// spawnLimitBox
			// 
			this.spawnLimitBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.spawnLimitBox.FormattingEnabled = true;
			this.spawnLimitBox.Items.AddRange(new object[] {
									"High",
									"Normal",
									"Low",
									"Singular"});
			this.spawnLimitBox.Location = new System.Drawing.Point(143, 144);
			this.spawnLimitBox.Name = "spawnLimitBox";
			this.spawnLimitBox.Size = new System.Drawing.Size(121, 21);
			this.spawnLimitBox.TabIndex = 8;
			this.spawnLimitBox.SelectedIndexChanged += new System.EventHandler(this.SpawnLimitBoxSelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(143, 118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(121, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "Spawning Limit";
			// 
			// spawnRateBox
			// 
			this.spawnRateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.spawnRateBox.FormattingEnabled = true;
			this.spawnRateBox.Items.AddRange(new object[] {
									"High",
									"Normal",
									"Low",
									"Very Low",
									"Very Very Low"});
			this.spawnRateBox.Location = new System.Drawing.Point(143, 94);
			this.spawnRateBox.Name = "spawnRateBox";
			this.spawnRateBox.Size = new System.Drawing.Size(121, 21);
			this.spawnRateBox.TabIndex = 6;
			this.spawnRateBox.SelectedIndexChanged += new System.EventHandler(this.SpawnRateBoxSelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(143, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Spawn Rate";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(143, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Monster ID";
			// 
			// monstersListBox
			// 
			this.monstersListBox.FormattingEnabled = true;
			this.monstersListBox.Location = new System.Drawing.Point(6, 19);
			this.monstersListBox.Name = "monstersListBox";
			this.monstersListBox.Size = new System.Drawing.Size(120, 108);
			this.monstersListBox.TabIndex = 0;
			this.monstersListBox.SelectedIndexChanged += new System.EventHandler(this.UpdateMonsterListBox);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(56, 352);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(107, 23);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Save changes";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(182, 352);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(39, 131);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 23);
			this.label8.TabIndex = 7;
			this.label8.Text = "Spawning algorithm";
			// 
			// spawningAlgBox
			// 
			this.spawningAlgBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.spawningAlgBox.FormattingEnabled = true;
			this.spawningAlgBox.Items.AddRange(new object[] {
									"No spawning",
									"Randomized",
									"Player-dependent",
									"Combined"});
			this.spawningAlgBox.Location = new System.Drawing.Point(172, 128);
			this.spawningAlgBox.Name = "spawningAlgBox";
			this.spawningAlgBox.Size = new System.Drawing.Size(121, 21);
			this.spawningAlgBox.TabIndex = 6;
			// 
			// MonsterGenEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(326, 384);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.spawningAlgBox);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(332, 412);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(332, 412);
			this.Name = "MonsterGenEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MonsterGeneratorXfer";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox checkBoxEnable;
		private System.Windows.Forms.ComboBox spawningAlgBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TextBox scriptDamaged;
		private System.Windows.Forms.TextBox scriptDestroyed;
		private System.Windows.Forms.TextBox scriptCollided;
		private System.Windows.Forms.TextBox scriptMSpawned;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox spawnLimitBox;
		private System.Windows.Forms.Button buttonMonsterProps;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox spawnRateBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox currentMonsterName;
		private System.Windows.Forms.ListBox monstersListBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
