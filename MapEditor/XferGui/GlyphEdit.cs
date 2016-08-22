/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 12.07.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of GlyphEdit.
	/// </summary>
	public partial class GlyphEdit : XferEditor
	{
		static NumberFormatInfo floatFormat = NumberFormatInfo.InvariantInfo;
		ComboBox[] spellNameBoxes;
		GlyphXfer xfer;
		
		public GlyphEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// init boxes, put in spell names
			spellNameBoxes = new ComboBox[] { comboBoxSpell1, comboBoxSpell2, comboBoxSpell3 };
			foreach (ComboBox b in spellNameBoxes) FillComboBox(b);
		}
		
		private void FillComboBox(ComboBox box)
		{
			foreach (ThingDb.Spell s in ThingDb.Spells.Values)
				box.Items.Add(s.Name);
		}
		
		public override void SetObject(Map.Object obj)
		{
			base.SetObject(obj);
			
			xfer = obj.GetExtraData<GlyphXfer>();
			textBoxTargX.Text = xfer.TargX.ToString(floatFormat);
			textBoxTargY.Text = xfer.TargY.ToString(floatFormat);
			
			for (int i = 0; i < spellNameBoxes.Length; i++)
			{
				if (xfer.Spells.Count <= i) break;
				spellNameBoxes[i].Text = xfer.Spells[i];
			}
		}
		
		public override Map.Object GetObject()
		{
			xfer.TargX = float.Parse(textBoxTargX.Text, floatFormat);
			xfer.TargY = float.Parse(textBoxTargY.Text, floatFormat);
			
			// calculate angle
			double rads = Math.Atan2(xfer.TargY - obj.Location.Y, xfer.TargX - obj.Location.Y);
			xfer.Angle = (byte)((rads / (Math.PI * 2D)) * 255D);
			
			// export spell names
			xfer.Spells.Clear(); string spell;
			for (int i = 0; i < spellNameBoxes.Length; i++)
			{
				spell = spellNameBoxes[i].Text;
				if (spell.Length > 0) xfer.Spells.Add(spell);
			}
			
			return base.GetObject();
		}

        private void GlyphEdit_Load(object sender, EventArgs e)
        {

        }
	}
}
