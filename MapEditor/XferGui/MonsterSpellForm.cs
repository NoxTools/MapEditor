/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 24.10.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
    /// <summary>
    /// Description of MonsterSpellForm.
    /// </summary>
    public partial class MonsterSpellForm : Form
    {
        private string[] spellIDArray;
        private uint[] spellUseFlags;

        public MonsterSpellForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            // индексируем флаги
            spellUseFlags = (uint[])Enum.GetValues(typeof(NoxEnums.NPCSpellCastFlags));
            // добавляем флаги в чеклистбокс
            usageCheckBox.Items.AddRange(Enum.GetNames(typeof(NoxEnums.NPCSpellCastFlags)));
            // добавляем имена спеллов в листбокс
            spellIDArray = new string[ThingDb.Spells.Count];
            int i = 0;
            string humanSpellName;
            foreach (ThingDb.Spell spell in ThingDb.Spells.Values)
            {
                spellIDArray[i] = spell.Name; i++;
                humanSpellName = spell.NameString;
                int di = humanSpellName.IndexOf(':') + 1;
                humanSpellName = humanSpellName.Remove(0, di);
                spellsListBox.Items.Add(humanSpellName);
            }
            spellsListBox.SelectedIndex = 0;
        }

        public void SetSpell(MonsterXfer.SpellEntry spell)
        {
            // указываем имя заклинания
            spellsListBox.SelectedIndex = Array.IndexOf(spellIDArray, spell.SpellName);
            // чекаем флаги
            if ((spell.UseFlags & 0x08000000) == 0x08000000) usageCheckBox.SetItemChecked(0, true);
            if ((spell.UseFlags & 0x10000000) == 0x10000000) usageCheckBox.SetItemChecked(1, true);
            if ((spell.UseFlags & 0x20000000) == 0x20000000) usageCheckBox.SetItemChecked(2, true);
            if ((spell.UseFlags & 0x40000000) == 0x40000000) usageCheckBox.SetItemChecked(3, true);
            if ((spell.UseFlags & 0x80000000) == 0x80000000) usageCheckBox.SetItemChecked(4, true);
        }

        public MonsterXfer.SpellEntry GetSpell()
        {
            int spellIndex = spellsListBox.SelectedIndex;
            if (spellIndex < 0) spellIndex = 0;
            // смотрим флаги
            uint flags = 0;
            for (int i = 0; i < 5; i++)
            {
                if (usageCheckBox.GetItemChecked(i))
                    flags |= spellUseFlags[i];
            }
            // возвращаем результат
            return new MonsterXfer.SpellEntry(spellIDArray[spellIndex], flags);
        }

        void ButtonOKClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MonsterSpellForm_Load(object sender, EventArgs e)
        {

        }
    }
}
