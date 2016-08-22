/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 15.04.2015
 */
namespace MapEditor.newgui
{
	partial class SettingsDialog
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
            this.checkBoxExt3d = new System.Windows.Forms.CheckBox();
            this.checkBoxScriptNames = new System.Windows.Forms.CheckBox();
            this.checkBoxThingNames = new System.Windows.Forms.CheckBox();
            this.checkBoxTexEdges = new System.Windows.Forms.CheckBox();
            this.checkBoxTileGrid = new System.Windows.Forms.CheckBox();
            this.checkBoxObjects = new System.Windows.Forms.CheckBox();
            this.checkBoxTiles = new System.Windows.Forms.CheckBox();
            this.checkBoxPolygons = new System.Windows.Forms.CheckBox();
            this.checkBoxWaypoints = new System.Windows.Forms.CheckBox();
            this.checkBoxWalls = new System.Windows.Forms.CheckBox();
            this.groupBoxMapfile = new System.Windows.Forms.GroupBox();
            this.checkBoxProtect = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveNXZ = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveScripts = new System.Windows.Forms.CheckBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabEditS = new System.Windows.Forms.TabPage();
            this.groupBoxEdit = new System.Windows.Forms.GroupBox();
            this.checkBoxAllowOver = new System.Windows.Forms.CheckBox();
            this.tabViewS = new System.Windows.Forms.TabPage();
            this.checkBoxComplexPrev = new System.Windows.Forms.CheckBox();
            this.checkBoxObjFacing = new System.Windows.Forms.CheckBox();
            this.checkBoxEnText = new System.Windows.Forms.CheckBox();
            this.groupBoxObjLbl = new System.Windows.Forms.GroupBox();
            this.checkBoxLabelTeams = new System.Windows.Forms.CheckBox();
            this.groupBoxElems = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxMapfile.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabEditS.SuspendLayout();
            this.groupBoxEdit.SuspendLayout();
            this.tabViewS.SuspendLayout();
            this.groupBoxObjLbl.SuspendLayout();
            this.groupBoxElems.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxExt3d
            // 
            this.checkBoxExt3d.Location = new System.Drawing.Point(6, 6);
            this.checkBoxExt3d.Name = "checkBoxExt3d";
            this.checkBoxExt3d.Size = new System.Drawing.Size(164, 24);
            this.checkBoxExt3d.TabIndex = 10;
            this.checkBoxExt3d.Text = "3D Object Extents (F8)";
            this.checkBoxExt3d.UseVisualStyleBackColor = true;
            this.checkBoxExt3d.CheckedChanged += new System.EventHandler(this.checkBoxExt3d_CheckedChanged);
            // 
            // checkBoxScriptNames
            // 
            this.checkBoxScriptNames.Location = new System.Drawing.Point(6, 49);
            this.checkBoxScriptNames.Name = "checkBoxScriptNames";
            this.checkBoxScriptNames.Size = new System.Drawing.Size(141, 24);
            this.checkBoxScriptNames.TabIndex = 9;
            this.checkBoxScriptNames.Text = "Show Custom names";
            this.checkBoxScriptNames.UseVisualStyleBackColor = true;
            // 
            // checkBoxThingNames
            // 
            this.checkBoxThingNames.Location = new System.Drawing.Point(6, 19);
            this.checkBoxThingNames.Name = "checkBoxThingNames";
            this.checkBoxThingNames.Size = new System.Drawing.Size(141, 24);
            this.checkBoxThingNames.TabIndex = 8;
            this.checkBoxThingNames.Text = "Show Thing types";
            this.checkBoxThingNames.UseVisualStyleBackColor = true;
            // 
            // checkBoxTexEdges
            // 
            this.checkBoxTexEdges.Location = new System.Drawing.Point(6, 66);
            this.checkBoxTexEdges.Name = "checkBoxTexEdges";
            this.checkBoxTexEdges.Size = new System.Drawing.Size(164, 24);
            this.checkBoxTexEdges.TabIndex = 7;
            this.checkBoxTexEdges.Text = "Render Edges in Preview";
            this.checkBoxTexEdges.UseVisualStyleBackColor = true;
            // 
            // checkBoxTileGrid
            // 
            this.checkBoxTileGrid.Location = new System.Drawing.Point(6, 150);
            this.checkBoxTileGrid.Name = "checkBoxTileGrid";
            this.checkBoxTileGrid.Size = new System.Drawing.Size(104, 24);
            this.checkBoxTileGrid.TabIndex = 1;
            this.checkBoxTileGrid.Text = "Grid (F5)";
            this.checkBoxTileGrid.UseVisualStyleBackColor = true;
            // 
            // checkBoxObjects
            // 
            this.checkBoxObjects.Location = new System.Drawing.Point(6, 122);
            this.checkBoxObjects.Name = "checkBoxObjects";
            this.checkBoxObjects.Size = new System.Drawing.Size(104, 24);
            this.checkBoxObjects.TabIndex = 6;
            this.checkBoxObjects.Text = "Objects (F7)";
            this.checkBoxObjects.UseVisualStyleBackColor = true;
            // 
            // checkBoxTiles
            // 
            this.checkBoxTiles.Location = new System.Drawing.Point(6, 96);
            this.checkBoxTiles.Name = "checkBoxTiles";
            this.checkBoxTiles.Size = new System.Drawing.Size(104, 24);
            this.checkBoxTiles.TabIndex = 5;
            this.checkBoxTiles.Text = "Tiles";
            this.checkBoxTiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxPolygons
            // 
            this.checkBoxPolygons.Location = new System.Drawing.Point(6, 70);
            this.checkBoxPolygons.Name = "checkBoxPolygons";
            this.checkBoxPolygons.Size = new System.Drawing.Size(104, 24);
            this.checkBoxPolygons.TabIndex = 4;
            this.checkBoxPolygons.Text = "Polygons";
            this.checkBoxPolygons.UseVisualStyleBackColor = true;
            // 
            // checkBoxWaypoints
            // 
            this.checkBoxWaypoints.Location = new System.Drawing.Point(6, 44);
            this.checkBoxWaypoints.Name = "checkBoxWaypoints";
            this.checkBoxWaypoints.Size = new System.Drawing.Size(104, 24);
            this.checkBoxWaypoints.TabIndex = 3;
            this.checkBoxWaypoints.Text = "Waypoints";
            this.checkBoxWaypoints.UseVisualStyleBackColor = true;
            // 
            // checkBoxWalls
            // 
            this.checkBoxWalls.Location = new System.Drawing.Point(6, 18);
            this.checkBoxWalls.Name = "checkBoxWalls";
            this.checkBoxWalls.Size = new System.Drawing.Size(104, 24);
            this.checkBoxWalls.TabIndex = 2;
            this.checkBoxWalls.Text = "Walls (F6)";
            this.checkBoxWalls.UseVisualStyleBackColor = true;
            this.checkBoxWalls.CheckedChanged += new System.EventHandler(this.checkBoxWalls_CheckedChanged);
            // 
            // groupBoxMapfile
            // 
            this.groupBoxMapfile.Controls.Add(this.checkBoxProtect);
            this.groupBoxMapfile.Controls.Add(this.checkBoxSaveNXZ);
            this.groupBoxMapfile.Controls.Add(this.checkBoxSaveScripts);
            this.groupBoxMapfile.Location = new System.Drawing.Point(195, 6);
            this.groupBoxMapfile.Name = "groupBoxMapfile";
            this.groupBoxMapfile.Size = new System.Drawing.Size(112, 96);
            this.groupBoxMapfile.TabIndex = 1;
            this.groupBoxMapfile.TabStop = false;
            this.groupBoxMapfile.Text = "File";
            // 
            // checkBoxProtect
            // 
            this.checkBoxProtect.Enabled = false;
            this.checkBoxProtect.Location = new System.Drawing.Point(8, 64);
            this.checkBoxProtect.Name = "checkBoxProtect";
            this.checkBoxProtect.Size = new System.Drawing.Size(96, 24);
            this.checkBoxProtect.TabIndex = 13;
            this.checkBoxProtect.Text = "Protect Map";
            this.checkBoxProtect.UseVisualStyleBackColor = true;
            this.checkBoxProtect.CheckedChanged += new System.EventHandler(this.checkBoxProtect_CheckedChanged);
            // 
            // checkBoxSaveNXZ
            // 
            this.checkBoxSaveNXZ.Location = new System.Drawing.Point(8, 16);
            this.checkBoxSaveNXZ.Name = "checkBoxSaveNXZ";
            this.checkBoxSaveNXZ.Size = new System.Drawing.Size(96, 24);
            this.checkBoxSaveNXZ.TabIndex = 11;
            this.checkBoxSaveNXZ.Text = "Make .NXZ";
            this.checkBoxSaveNXZ.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaveScripts
            // 
            this.checkBoxSaveScripts.Enabled = false;
            this.checkBoxSaveScripts.Location = new System.Drawing.Point(8, 40);
            this.checkBoxSaveScripts.Name = "checkBoxSaveScripts";
            this.checkBoxSaveScripts.Size = new System.Drawing.Size(96, 24);
            this.checkBoxSaveScripts.TabIndex = 12;
            this.checkBoxSaveScripts.Text = "Save scripts";
            this.checkBoxSaveScripts.UseVisualStyleBackColor = true;
            // 
            // buttonDone
            // 
            this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDone.Location = new System.Drawing.Point(64, 331);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(80, 23);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.ButtonKClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabEditS);
            this.tabControl1.Controls.Add(this.tabViewS);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(321, 309);
            this.tabControl1.TabIndex = 2;
            // 
            // tabEditS
            // 
            this.tabEditS.Controls.Add(this.groupBoxEdit);
            this.tabEditS.Controls.Add(this.groupBoxMapfile);
            this.tabEditS.Location = new System.Drawing.Point(4, 22);
            this.tabEditS.Name = "tabEditS";
            this.tabEditS.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditS.Size = new System.Drawing.Size(313, 283);
            this.tabEditS.TabIndex = 0;
            this.tabEditS.Text = "Editor";
            this.tabEditS.UseVisualStyleBackColor = true;
            // 
            // groupBoxEdit
            // 
            this.groupBoxEdit.Controls.Add(this.checkBoxAllowOver);
            this.groupBoxEdit.Location = new System.Drawing.Point(6, 6);
            this.groupBoxEdit.Name = "groupBoxEdit";
            this.groupBoxEdit.Size = new System.Drawing.Size(183, 96);
            this.groupBoxEdit.TabIndex = 2;
            this.groupBoxEdit.TabStop = false;
            this.groupBoxEdit.Text = "Editing";
            // 
            // checkBoxAllowOver
            // 
            this.checkBoxAllowOver.Location = new System.Drawing.Point(6, 19);
            this.checkBoxAllowOver.Name = "checkBoxAllowOver";
            this.checkBoxAllowOver.Size = new System.Drawing.Size(171, 24);
            this.checkBoxAllowOver.TabIndex = 0;
            this.checkBoxAllowOver.Text = "Auto override existing Ts/Ws";
            this.checkBoxAllowOver.UseVisualStyleBackColor = true;
            // 
            // tabViewS
            // 
            this.tabViewS.Controls.Add(this.checkBoxComplexPrev);
            this.tabViewS.Controls.Add(this.checkBoxObjFacing);
            this.tabViewS.Controls.Add(this.checkBoxExt3d);
            this.tabViewS.Controls.Add(this.checkBoxEnText);
            this.tabViewS.Controls.Add(this.checkBoxTexEdges);
            this.tabViewS.Controls.Add(this.groupBoxObjLbl);
            this.tabViewS.Controls.Add(this.groupBoxElems);
            this.tabViewS.Location = new System.Drawing.Point(4, 22);
            this.tabViewS.Name = "tabViewS";
            this.tabViewS.Padding = new System.Windows.Forms.Padding(3);
            this.tabViewS.Size = new System.Drawing.Size(313, 283);
            this.tabViewS.TabIndex = 1;
            this.tabViewS.Text = "Map View";
            this.tabViewS.UseVisualStyleBackColor = true;
            // 
            // checkBoxComplexPrev
            // 
            this.checkBoxComplexPrev.Location = new System.Drawing.Point(6, 96);
            this.checkBoxComplexPrev.Name = "checkBoxComplexPrev";
            this.checkBoxComplexPrev.Size = new System.Drawing.Size(164, 24);
            this.checkBoxComplexPrev.TabIndex = 12;
            this.checkBoxComplexPrev.Text = "Color NPCs/Items in Preview";
            this.checkBoxComplexPrev.UseVisualStyleBackColor = true;
            // 
            // checkBoxObjFacing
            // 
            this.checkBoxObjFacing.Location = new System.Drawing.Point(6, 36);
            this.checkBoxObjFacing.Name = "checkBoxObjFacing";
            this.checkBoxObjFacing.Size = new System.Drawing.Size(164, 24);
            this.checkBoxObjFacing.TabIndex = 11;
            this.checkBoxObjFacing.Text = "Show Object facing";
            this.checkBoxObjFacing.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnText
            // 
            this.checkBoxEnText.Location = new System.Drawing.Point(12, 139);
            this.checkBoxEnText.Name = "checkBoxEnText";
            this.checkBoxEnText.Size = new System.Drawing.Size(104, 24);
            this.checkBoxEnText.TabIndex = 3;
            this.checkBoxEnText.Text = "Enable Text";
            this.checkBoxEnText.UseVisualStyleBackColor = true;
            this.checkBoxEnText.CheckedChanged += new System.EventHandler(this.CheckBoxEnTextCheckedChanged);
            // 
            // groupBoxObjLbl
            // 
            this.groupBoxObjLbl.Controls.Add(this.checkBoxLabelTeams);
            this.groupBoxObjLbl.Controls.Add(this.checkBoxThingNames);
            this.groupBoxObjLbl.Controls.Add(this.checkBoxScriptNames);
            this.groupBoxObjLbl.Location = new System.Drawing.Point(6, 169);
            this.groupBoxObjLbl.Name = "groupBoxObjLbl";
            this.groupBoxObjLbl.Size = new System.Drawing.Size(153, 108);
            this.groupBoxObjLbl.TabIndex = 2;
            this.groupBoxObjLbl.TabStop = false;
            this.groupBoxObjLbl.Text = "Object labels";
            // 
            // checkBoxLabelTeams
            // 
            this.checkBoxLabelTeams.Location = new System.Drawing.Point(6, 79);
            this.checkBoxLabelTeams.Name = "checkBoxLabelTeams";
            this.checkBoxLabelTeams.Size = new System.Drawing.Size(141, 24);
            this.checkBoxLabelTeams.TabIndex = 10;
            this.checkBoxLabelTeams.Text = "Label existing Teams";
            this.checkBoxLabelTeams.UseVisualStyleBackColor = true;
            // 
            // groupBoxElems
            // 
            this.groupBoxElems.Controls.Add(this.checkBoxWalls);
            this.groupBoxElems.Controls.Add(this.checkBoxWaypoints);
            this.groupBoxElems.Controls.Add(this.checkBoxPolygons);
            this.groupBoxElems.Controls.Add(this.checkBoxTiles);
            this.groupBoxElems.Controls.Add(this.checkBoxTileGrid);
            this.groupBoxElems.Controls.Add(this.checkBoxObjects);
            this.groupBoxElems.Location = new System.Drawing.Point(176, 6);
            this.groupBoxElems.Name = "groupBoxElems";
            this.groupBoxElems.Size = new System.Drawing.Size(131, 178);
            this.groupBoxElems.TabIndex = 1;
            this.groupBoxElems.TabStop = false;
            this.groupBoxElems.Text = "Draw elements";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(196, 331);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 366);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Editor Settings";
            this.groupBoxMapfile.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabEditS.ResumeLayout(false);
            this.groupBoxEdit.ResumeLayout(false);
            this.tabViewS.ResumeLayout(false);
            this.groupBoxObjLbl.ResumeLayout(false);
            this.groupBoxElems.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.CheckBox checkBoxProtect;
		private System.Windows.Forms.CheckBox checkBoxExt3d;
		private System.Windows.Forms.Button buttonDone;
		private System.Windows.Forms.CheckBox checkBoxSaveScripts;
		private System.Windows.Forms.CheckBox checkBoxSaveNXZ;
		private System.Windows.Forms.GroupBox groupBoxMapfile;
		private System.Windows.Forms.CheckBox checkBoxThingNames;
		private System.Windows.Forms.CheckBox checkBoxScriptNames;
		private System.Windows.Forms.CheckBox checkBoxTexEdges;
		private System.Windows.Forms.CheckBox checkBoxObjects;
		private System.Windows.Forms.CheckBox checkBoxWaypoints;
		private System.Windows.Forms.CheckBox checkBoxPolygons;
		private System.Windows.Forms.CheckBox checkBoxTiles;
		private System.Windows.Forms.CheckBox checkBoxWalls;
		private System.Windows.Forms.CheckBox checkBoxTileGrid;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabEditS;
		private System.Windows.Forms.TabPage tabViewS;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.GroupBox groupBoxElems;
		private System.Windows.Forms.CheckBox checkBoxEnText;
		private System.Windows.Forms.GroupBox groupBoxObjLbl;
        private System.Windows.Forms.CheckBox checkBoxObjFacing;
		private System.Windows.Forms.CheckBox checkBoxLabelTeams;
		private System.Windows.Forms.GroupBox groupBoxEdit;
        private System.Windows.Forms.CheckBox checkBoxAllowOver;
		private System.Windows.Forms.CheckBox checkBoxComplexPrev;
	}
}
