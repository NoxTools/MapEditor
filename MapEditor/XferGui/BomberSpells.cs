/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 12.07.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of BomberSpells.
	/// </summary>
	public partial class BomberSpells : Form
	{
		MonsterXfer xfer;
		
		public BomberSpells(MonsterXfer xfer)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.xfer = xfer;
			FillComboBox(comboBoxSpell1);
			FillComboBox(comboBoxSpell2);
			FillComboBox(comboBoxSpell3);
		}
		
		private void FillComboBox(ComboBox box)
		{
			box.Items.Add("SPELL_INVALID");
			box.SelectedIndex = 0;
			foreach (ThingDb.Spell s in ThingDb.Spells.Values)
				box.Items.Add(s.Name);
		}
		
		void ButtonDoneClick(object sender, EventArgs e)
		{
			xfer.TrapSpell1 = comboBoxSpell1.Text;
			xfer.TrapSpell2 = comboBoxSpell2.Text;
			xfer.TrapSpell3 = comboBoxSpell3.Text;
		}

        private void BomberSpells_Load(object sender, EventArgs e)
        {

        }
	}
}
