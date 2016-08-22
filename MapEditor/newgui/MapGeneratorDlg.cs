/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 01.12.2014
 */
using System;
using MapEditor.MapInt;
using System.Windows.Forms;
using System.ComponentModel;
using MapEditor.mapgen;

namespace MapEditor.newgui
{
	/// <summary>
	/// Final version
	/// </summary>
	public partial class MapGeneratorDlg : Form
	{
		private MapView parent;
		private NoxShared.Map restoreMap;
		
		const string GENERATING_MESSAGE = "Sorry, you cannot close this window while the map is being generated.";
		
		public MapGeneratorDlg(MapView view)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			parent = view;
			numericMapSeed.Minimum = int.MinValue;
			numericMapSeed.Maximum = int.MaxValue;
			comboBoxMapType.SelectedIndex = 0;
		}
		
		void MapGeneratorDlgFormClosing(object sender, FormClosingEventArgs e)
		{
			// Oh no, please wait
			if (Generator.IsGenerating)
			{
				MessageBox.Show(GENERATING_MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				e.Cancel = true;
			}
		}
		
		void ButtonGenerateClick(object sender, EventArgs e)
		{
			restoreMap = MapInterface.TheMap;
			MapInterface.TheMap = null; // To avoid multithread problems
			// Disable button
			buttonGenerate.Enabled = false;
			// Setup config
			GeneratorConfig config = new GeneratorConfig();
			if (comboBoxMapType.SelectedIndex < 0) return;
			config.MapType = (GeneratorConfig.MapPreset) comboBoxMapType.SelectedIndex + 1;
			config.RandomSeed = (int) numericMapSeed.Value;
			config.Allow3SideWalls = checkBoxSmoothWalls.Checked;
			config.PopulateMap = checkBoxPopulate.Checked;
			Generator.SetConfig(config);
			// Setup worker handlers
			Generator.Worker.ProgressChanged += new ProgressChangedEventHandler(Generator_Worker_ProgressChanged);
			Generator.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Generator_Worker_RunWorkerCompleted);
			// Generate
			Generator.GenerateMap(restoreMap);
		}

		void Generator_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			textBoxAction.Text = "Map generated successfully";
			MapInterface.TheMap = restoreMap;
			buttonGenerate.Enabled = true;
		}

		void Generator_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBarGeneration.Value = e.ProgressPercentage;
			textBoxAction.Text = Generator.GetStatus();
		}
	}
}
