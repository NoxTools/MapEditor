/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 24.10.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of MonsterScriptForm.
	/// </summary>
	public partial class MonsterScriptForm : Form
	{
		private TextBox[] scriptBoxes;
		private const int SCRIPTS_N = 10;
		
		public MonsterScriptForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			scriptBoxes = new TextBox[]
			{
				script1, script2, script3, script4, script5, script6, script7, script8, script9, script10
			};
		}
		
		public void SetScriptStrings(string[] scripts)
		{
			for (int i = 0; i < SCRIPTS_N; i++)
				scriptBoxes[i].Text = scripts[i];
		}
		
		public string[] GetScriptStrings()
		{
			string[] scripts = new string[SCRIPTS_N];
			for (int i = 0; i < SCRIPTS_N; i++)
				scripts[i] = scriptBoxes[i].Text;
			return scripts;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
