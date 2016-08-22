/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 30.06.2015
 */
namespace MapEditor.XferGui
{
	partial class KnowledgeRewardEdit
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
			this.buttonDone = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.typeOfKnowledge = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// buttonDone
			// 
			this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonDone.Location = new System.Drawing.Point(112, 48);
			this.buttonDone.Name = "buttonDone";
			this.buttonDone.Size = new System.Drawing.Size(75, 23);
			this.buttonDone.TabIndex = 0;
			this.buttonDone.Text = "Done";
			this.buttonDone.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Spell/Ability/Monster ID";
			// 
			// typeOfKnowledge
			// 
			this.typeOfKnowledge.FormattingEnabled = true;
			this.typeOfKnowledge.Location = new System.Drawing.Point(142, 16);
			this.typeOfKnowledge.Name = "typeOfKnowledge";
			this.typeOfKnowledge.Size = new System.Drawing.Size(152, 21);
			this.typeOfKnowledge.TabIndex = 2;
			// 
			// KnowledgeRewardEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(303, 81);
			this.Controls.Add(this.typeOfKnowledge);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonDone);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "KnowledgeRewardEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Knowledge reward edtior";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ComboBox typeOfKnowledge;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDone;
	}
}
