/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 03.11.2014
 */
namespace MapEditor.XferGui
{
	partial class QuestRewardEdit
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
			this.rewardTypes = new System.Windows.Forms.CheckedListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.spawnChance = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkRare = new System.Windows.Forms.CheckBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rewardTypes
			// 
			this.rewardTypes.FormattingEnabled = true;
			this.rewardTypes.Items.AddRange(new object[] {
									"Spell Book",
									"Ability Book",
									"Monster Scroll",
									"Weapon",
									"Armor",
									"Gem",
									"Potion",
									"Ankh/Gold"});
			this.rewardTypes.Location = new System.Drawing.Point(12, 35);
			this.rewardTypes.Name = "rewardTypes";
			this.rewardTypes.Size = new System.Drawing.Size(120, 94);
			this.rewardTypes.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Reward Types";
			// 
			// spawnChance
			// 
			this.spawnChance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.spawnChance.FormattingEnabled = true;
			this.spawnChance.Items.AddRange(new object[] {
									"100%",
									"75%",
									"50%",
									"25%",
									"5%"});
			this.spawnChance.Location = new System.Drawing.Point(11, 171);
			this.spawnChance.Name = "spawnChance";
			this.spawnChance.Size = new System.Drawing.Size(121, 21);
			this.spawnChance.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 145);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Spawn Chance";
			// 
			// checkRare
			// 
			this.checkRare.Location = new System.Drawing.Point(11, 198);
			this.checkRare.Name = "checkRare";
			this.checkRare.Size = new System.Drawing.Size(121, 24);
			this.checkRare.TabIndex = 4;
			this.checkRare.Text = "Rare / Enchanted";
			this.checkRare.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(34, 231);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
			// 
			// QuestRewardEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(145, 266);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.checkRare);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.spawnChance);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rewardTypes);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "QuestRewardEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Reward Marker";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkRare;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox spawnChance;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox rewardTypes;
	}
}
