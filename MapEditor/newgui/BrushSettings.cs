/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 17.04.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using NoxShared;

namespace MapEditor.newgui
{
	/// <summary>
	/// Description of BrushSettings.
	/// </summary>
	public partial class BrushSettings : Form
	{
		public BrushSettings()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			List<string> sortedTileNames = new List<string>(ThingDb.FloorTileNames.ToArray());
			List<string> sortedEdgeNames = new List<string>(ThingDb.EdgeTileNames.ToArray());
			sortedTileNames.Sort();
			sortedEdgeNames.Sort();
			
			comboBoxTileType.Items.AddRange(sortedTileNames.ToArray());
			comboBoxEdgeType.Items.AddRange(sortedEdgeNames.ToArray());
			comboBoxTileType.SelectedIndex = 0;
			comboBoxEdgeType.SelectedIndex = 0;
		}
		
		void ButtonKClick(object sender, EventArgs e)
		{
			TileBrushConfig.SetData((string) comboBoxTileType.SelectedItem, (string) comboBoxEdgeType.SelectedItem, (int) numericSquareSize.Value);
			Close();
		}

        private void BrushSettings_Load(object sender, EventArgs e)
        {

        }

        private void numericSquareSize_ValueChanged(object sender, EventArgs e)
        {

        }
	}
	
	public static class TileBrushConfig
	{
		public static string TileMaterialID;
		public static string EdgeMaterialID;
		public static int BrushSize = 1;
		
		public static void SetData(string tile, string edge, int size)
		{
			TileMaterialID = tile;
			EdgeMaterialID = edge;
			BrushSize = size;
		}
	}
}
