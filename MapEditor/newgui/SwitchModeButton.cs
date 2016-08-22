using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using MapEditor;
using MapEditor.MapInt;

public class SwitchModeButton : Button
{
	private EditMode[] validModes = null;
	private int currentMode = -1;
	
	static Dictionary<EditMode, String> MODE_BUTTON_NAMES;
	
	static SwitchModeButton()
	{
		MODE_BUTTON_NAMES = new Dictionary<EditMode, String>();
		MODE_BUTTON_NAMES.Add(EditMode.OBJECT_PLACE, "Place/Remove");
		MODE_BUTTON_NAMES.Add(EditMode.OBJECT_SELECT, "Select/Move");
		MODE_BUTTON_NAMES.Add(EditMode.WALL_BRUSH, "Auto Brush");
		MODE_BUTTON_NAMES.Add(EditMode.WALL_CHANGE, "Special Edit");
		MODE_BUTTON_NAMES.Add(EditMode.WALL_PLACE, "Place/Remove");
		MODE_BUTTON_NAMES.Add(EditMode.FLOOR_PLACE, "Place/Remove");
		MODE_BUTTON_NAMES.Add(EditMode.FLOOR_BRUSH, "Auto Brush");
		MODE_BUTTON_NAMES.Add(EditMode.WAYPOINT_PLACE, "Place/Remove");
		MODE_BUTTON_NAMES.Add(EditMode.WAYPOINT_SELECT, "Select/Move");
		MODE_BUTTON_NAMES.Add(EditMode.WAYPOINT_CONNECT, "Make Path");
	}
	
	public SwitchModeButton() : base()
	{
		Font = new Font(Font.Name, Font.Size, FontStyle.Bold);
	}
	
	public void SetStates(EditMode[] valid)
	{
		validModes = valid;
		ToggleMode();
	}
	
	public EditMode SelectedMode
	{
		get
		{
			return validModes[currentMode];
		}
	}
	
	private void ToggleMode()
	{
		if (validModes == null) return;
		currentMode++;
		if (currentMode >= validModes.Length) currentMode = 0;
		// alter mode
		MapInterface.CurrentMode = SelectedMode;
		// alter button text
		if (MODE_BUTTON_NAMES.ContainsKey(SelectedMode))
			Text = MODE_BUTTON_NAMES[SelectedMode];
		else
			Text = "???";
	}
	
	protected override void OnClick(EventArgs e)
	{
		// switch to next mode
		ToggleMode();
		
		base.OnClick(e);
	}
}