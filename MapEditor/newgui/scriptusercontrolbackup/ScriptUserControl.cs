/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 01.12.2014
 */
using System;
using System.Windows.Forms;
using ScriptFunction = MapEditor.noxscript2.ScriptObjContainer.ScriptFunction;

namespace MapEditor.noxscript2
{
	/// <summary>
	/// User interface providing script editing capabilities
	/// </summary>
	public partial class ScriptUserControl : UserControl
	{
		private ScriptObjContainer scriptContainer;
		private int selectedFunctionIndex = -1;
		private int selectedVariableIndex = -1;
		
		const string FORMAT_SCRIPT_FUNC = "{0}: {1}";
		
		public ScriptUserControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			

		}
		
		/// <summary>
		/// Updates list of user script functions
		/// </summary>
		private void UpdateFunctionsList()
		{			
			functionsListBox.Items.Clear();
			int id = 0;
			// Repopulate functions list
			foreach (ScriptFunction sf in scriptContainer.Functions)
			{
				functionsListBox.Items.Add(String.Format(FORMAT_SCRIPT_FUNC, id, sf.Name));
				id++;
			}
			
			// Update selection
			int limit = functionsListBox.Items.Count - 1;
			if (selectedFunctionIndex > limit) selectedFunctionIndex = limit; 
			functionsListBox.SelectedIndex = selectedFunctionIndex;
		}
		
		/// <summary>
		/// Updates list of variables for currently selected functions
		/// </summary>
		private void UpdateVariablesList()
		{
			ScriptFunction sf = scriptContainer.Functions[selectedFunctionIndex];
			
			variablesListBox.Items.Clear();
			// Repopulate list
			foreach (ScriptFunction.ScriptVariable v in sf.Variables)
				variablesListBox.Items.Add(v.Name);
			
			// Update selection
			int limit = variablesListBox.Items.Count - 1;
			if (selectedVariableIndex > limit) selectedVariableIndex = limit; 
			variablesListBox.SelectedIndex = selectedVariableIndex;
		}
		
		/// <summary>
		/// Updates control, building new script container for specified map
		/// </summary>
		public void UpdateForMap(NoxShared.Map map)
		{
			scriptContainer = new ScriptObjContainer(map.Scripts);
			UpdateFunctionsList();
			codeBox.SetScriptContainer(scriptContainer);
		}
		
		/// <summary>
		/// Called when user selected any function from the list.
		/// </summary>
		void FunctionsListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			int index = functionsListBox.SelectedIndex;
			if (index < 0) return;
			selectedFunctionIndex = index;
			
			// Find out hich one is selected
			ScriptFunction sf = scriptContainer.Functions[index];
			// Update variables list for this function
			UpdateVariablesList();
		}
		
		/// <summary>
		/// User selected variable from the list. Update variable info
		/// </summary>
		void VariablesListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			int index = variablesListBox.SelectedIndex;
			if (index < 0) return;
			selectedVariableIndex = index;
			
			// Which variable
			ScriptFunction sf = scriptContainer.Functions[selectedFunctionIndex];
			ScriptFunction.ScriptVariable sv = sf.Variables[index];
			
			// Update data
			varNameTextBox.Text = sv.Name;
			varTypeComboBox.SelectedIndex = (int) sv.Type;
			varSizeBox.Value = sv.ArraySize;
		}
		
		void VarNameTextBoxTextChanged(object sender, EventArgs e)
		{
			if (selectedFunctionIndex < 0) return;
			if (selectedVariableIndex < 0) return;
			ScriptFunction sf = scriptContainer.Functions[selectedFunctionIndex];
			ScriptFunction.ScriptVariable sv = sf.Variables[selectedVariableIndex];
			if (varNameTextBox.Text.Length <= 0)
			{
				// Prevent making unnamed variables
				varNameTextBox.Text = sv.Name;
				return;
			}
			sv.Name = varNameTextBox.Text;
			variablesListBox.Items[selectedVariableIndex] = sv.Name;
		}
		
		void VarTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if (selectedFunctionIndex < 0) return;
			if (selectedVariableIndex < 0) return;
			ScriptFunction sf = scriptContainer.Functions[selectedFunctionIndex];
			ScriptFunction.ScriptVariable sv = sf.Variables[selectedVariableIndex];
			sv.Type = (ScriptObjContainer.VarType) varTypeComboBox.SelectedIndex;
		}
		
		void VarSizeBoxValueChanged(object sender, EventArgs e)
		{
			if (selectedFunctionIndex < 0) return;
			if (selectedVariableIndex < 0) return;
			ScriptFunction sf = scriptContainer.Functions[selectedFunctionIndex];
			ScriptFunction.ScriptVariable sv = sf.Variables[selectedVariableIndex];
			sv.ArraySize = (int) varSizeBox.Value;
		}

		void FunctionsListBoxMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				bool allow = true;
				if (scriptContainer.Functions.Count < selectedFunctionIndex || selectedFunctionIndex < 0)
					allow = false;
				
				foreach (ToolStripMenuItem item in menuFuncOperation.Items)
					item.Enabled = allow;
					
				menuFuncOperation.Show(functionsListBox, e.Location);
			}
		}
		
		void DecompileToolStripMenuItemClick(object sender, EventArgs e)
		{
			codeBox.Text = scriptContainer.Decompile(selectedFunctionIndex);
		}
		
		void CompileToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void DeleteToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		
		void CreateNewToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
	}
}
