/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 15.04.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.newgui
{
	/// <summary>
	/// Description of SettingDialog.
	/// </summary>
	public partial class SettingsDialog : Form
	{
		public SettingsDialog()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			Read();
		}
		
		private void Read()
		{
			checkBoxExt3d.Checked = EditorSettings.Default.Draw_Extents_3D;
			checkBoxObjects.Checked = EditorSettings.Default.Draw_Objects;
			checkBoxPolygons.Checked = EditorSettings.Default.Draw_Polygons;
			checkBoxScriptNames.Checked = EditorSettings.Default.Draw_ObjCustomLabels;
			checkBoxTexEdges.Checked = EditorSettings.Default.Draw_PreviewTexEdges;
			checkBoxThingNames.Checked = EditorSettings.Default.Draw_ObjThingNames;
			checkBoxTileGrid.Checked = EditorSettings.Default.Draw_Grid;
			checkBoxTiles.Checked = EditorSettings.Default.Draw_FloorTiles;
			checkBoxWalls.Checked = EditorSettings.Default.Draw_Walls;
			checkBoxWaypoints.Checked = EditorSettings.Default.Draw_Waypoints;
			checkBoxSaveNXZ.Checked = EditorSettings.Default.Save_ExportNXZ;
			checkBoxSaveScripts.Checked = EditorSettings.Default.Save_EnableScripts;
			checkBoxProtect.Checked = EditorSettings.Default.Save_ProtectMap;
			checkBoxEnText.Checked = EditorSettings.Default.Draw_AllText;
			checkBoxObjFacing.Checked = EditorSettings.Default.Draw_ObjectFacing;
			checkBoxLabelTeams.Checked = EditorSettings.Default.Draw_ObjTeams;
			checkBoxAllowOver.Checked = EditorSettings.Default.Edit_AllowOverride;
			checkBoxComplexPrev.Checked = EditorSettings.Default.Draw_ComplexPreview;
		}
		
		private void Save()
		{
			EditorSettings.Default.Draw_Extents_3D = checkBoxExt3d.Checked;
			EditorSettings.Default.Draw_Objects = checkBoxObjects.Checked;
			EditorSettings.Default.Draw_Polygons = checkBoxPolygons.Checked;
			EditorSettings.Default.Draw_ObjCustomLabels = checkBoxScriptNames.Checked;
			EditorSettings.Default.Draw_PreviewTexEdges = checkBoxTexEdges.Checked;
			EditorSettings.Default.Draw_ObjThingNames = checkBoxThingNames.Checked;
			EditorSettings.Default.Draw_Grid = checkBoxTileGrid.Checked;
			EditorSettings.Default.Draw_FloorTiles = checkBoxTiles.Checked;
			EditorSettings.Default.Draw_Walls = checkBoxWalls.Checked;
			EditorSettings.Default.Draw_Waypoints = checkBoxWaypoints.Checked;
			EditorSettings.Default.Save_ExportNXZ = checkBoxSaveNXZ.Checked;
			EditorSettings.Default.Save_EnableScripts = checkBoxSaveScripts.Checked;
			EditorSettings.Default.Save_ProtectMap = checkBoxProtect.Checked;
			EditorSettings.Default.Draw_AllText = checkBoxEnText.Checked;
			EditorSettings.Default.Draw_ObjectFacing = checkBoxObjFacing.Checked;
			EditorSettings.Default.Draw_ObjTeams = checkBoxLabelTeams.Checked;
			EditorSettings.Default.Edit_AllowOverride = checkBoxAllowOver.Checked;
			EditorSettings.Default.Draw_ComplexPreview = checkBoxComplexPrev.Checked;
			EditorSettings.Default.Save();
		}
		
		void CheckBoxEnTextCheckedChanged(object sender, EventArgs e)
		{
			groupBoxObjLbl.Enabled = checkBoxEnText.Checked;
		}
		
		void ButtonKClick(object sender, EventArgs e)
		{
			Save();
			Close();
			// enforce new render settings
			MainWindow.Instance.mapView.MapRenderer.UpdateCanvas(true, true);
            MainWindow.Instance.Invalidate(true);
		}
		
		void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}

        private void checkBoxWalls_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabScripts_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxExt3d_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxProtect_CheckedChanged(object sender, EventArgs e)
        {

        }
	}
}
