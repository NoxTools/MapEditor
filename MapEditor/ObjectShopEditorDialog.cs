using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

using NoxShared;

namespace MapEditor
{
    public partial class ObjectShopEditorDialog : Form
    {
        public static ArrayList objEnchants = new ArrayList(new String[]{
																			"WeaponPower1",
																			"WeaponPower2",
																			"WeaponPower3",
																			"WeaponPower4",
																			"WeaponPower5",
																			"WeaponPower6",
																			"ArmorQuality1",
																			"ArmorQuality2",
																			"ArmorQuality3",
																			"ArmorQuality4",
																			"ArmorQuality5",
																			"ArmorQuality6",
																			"Material1",
																			"Material2",
																			"Material3",
																			"Material4",
																			"Material5",
																			"Material6",
																			"Material7",
																			"MaterialTeamRed",
																			"MaterialTeamGreen",
																			"MaterialTeamBlue",
																			"MaterialTeamYellow",
																			"MaterialTeamCyan",
																			"MaterialTeamViolet",
																			"MaterialTeamBlack",
																			"MaterialTeamWhite",
																			"MaterialTeamOrange",
																			"Stun1",
																			"Stun2",
																			"Stun3",
																			"Stun4",
																			"Fire1",
																			"Fire2",
																			"Fire3",
																			"Fire4",
																			"FireRing1",
																			"FireRing2",
																			"FireRing3",
																			"FireRing4",
																			"BlueFireRing1",
																			"BlueFireRing2",
																			"BlueFireRing3",
																			"BlueFireRing4",
																			"Impact1",
																			"Impact2",
																			"Impact3",
																			"Impact4",
																			"Confuse1",
																			"Confuse2",
																			"Confuse3",
																			"Confuse4",
																			"Lightning1",
																			"Lightning2",
																			"Lightning3",
																			"Lightning4",
																			"ManaSteal1",
																			"ManaSteal2",
																			"ManaSteal3",
																			"ManaSteal4",
																			"Vampirism1",
																			"Vampirism2",
																			"Vampirism3",
																			"Vampirism4",
																			"Venom1",
																			"Venom2",
																			"Venom3",
																			"Venom4",
																			"Brilliance1",
																			"FireProtect1",
																			"FireProtect2",
																			"FireProtect3",
																			"FireProtect4",
																			"LightningProtect1",
																			"LightningProtect2",
																			"LightningProtect3",
																			"LightningProtect4",
																			"Regeneration1",
																			"Regeneration2",
																			"Regeneration3",
																			"Regeneration4",
																			"PoisonProtect1",
																			"PoisonProtect2",
																			"PoisonProtect3",
																			"PoisonProtect4",
																			"Speed1",
																			"Speed2",
																			"Speed3",
																			"Speed4",
																			"Readiness1",
																			"Readiness2",
																			"Readiness3",
																			"Readiness4",
																			"ProjectileSpeed1",
																			"ProjectileSpeed2",
																			"ProjectileSpeed3",
																			"ProjectileSpeed4",
																			"Replenishment1",
																			"ContinualReplenishment1",
																			"UserColor1",
																			"UserColor2",
																			"UserColor3",
																			"UserColor4",
																			"UserColor5",
																			"UserColor6",
																			"UserColor7",
																			"UserColor8",
																			"UserColor9",
																			"UserColor10",
																			"UserColor11",
																			"UserColor12",
																			"UserColor13",
																			"UserColor14",
																			"UserColor15",
																			"UserColor16",
																			"UserColor17",
																			"UserColor18",
																			"UserColor19",
																			"UserColor20",
																			"UserColor21",
																			"UserColor22",
																			"UserColor23",
																			"UserColor24",
																			"UserColor25",
																			"UserColor26",
																			"UserColor27",
																			"UserColor28",
																			"UserColor29",
																			"UserColor30",
																			"UserColor31",
																			"UserColor32",
																			"UserColor33",
																			"UserMaterialColor1",
																			"UserMaterialColor2",
																			"UserMaterialColor3",
																			"UserMaterialColor4",
																			"UserMaterialColor5",
																			"UserMaterialColor6",
																			"UserMaterialColor7",
																			"UserMaterialColor8",
																			"UserMaterialColor9",
																			"UserMaterialColor10",
																			"UserMaterialColor11",
																			"UserMaterialColor12",
																			"UserMaterialColor13",
																			"UserMaterialColor14",
																			"UserMaterialColor15",
																			"UserMaterialColor16",
																			"UserMaterialColor17",
																			"UserMaterialColor18",
																			"UserMaterialColor19",
																			"UserMaterialColor20",
																			"UserMaterialColor21",
																			"UserMaterialColor22",
																			"UserMaterialColor23",
																			"UserMaterialColor24",
																			"UserMaterialColor25",
																			"UserMaterialColor26",
																			"UserMaterialColor27",
																			"UserMaterialColor28",
																			"UserMaterialColor29",
																			"UserMaterialColor30",
																			"UserMaterialColor31",
																			"UserMaterialColor32"
																		});
    	
    	public NoxShared.ObjDataXfer.MonsterXfer.ShopItemInfo ItemInfo;
    	
        public ObjectShopEditorDialog()
        {
            InitializeComponent();
            enchant1.Items.AddRange(objEnchants.ToArray());
            enchant2.Items.AddRange(objEnchants.ToArray());
            enchant3.Items.AddRange(objEnchants.ToArray());
            enchant4.Items.AddRange(objEnchants.ToArray());

            foreach (ThingDb.Thing tng in ThingDb.Things.Values)
                objname.Items.Add(tng.Name);

            ItemInfo = new NoxShared.ObjDataXfer.MonsterXfer.ShopItemInfo();
        }
        
        private void okButton_Click(object sender, EventArgs e)
        {
            ItemInfo.Ench1 = enchant1.Text;
            ItemInfo.Ench2 = enchant2.Text;
            ItemInfo.Ench3 = enchant3.Text;
            ItemInfo.Ench4 = enchant4.Text;

            ItemInfo.Name = objname.Text;

            ItemInfo.SpellID = cbospells.Text;

            // enforce using correct value
            byte defval;
            if (Byte.TryParse(numitems.Text, out defval))
            	ItemInfo.Count = defval;
			else
				ItemInfo.Count = 1;
            
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        void LoadRange()
        {
            cbospells.Items.Clear();
            switch (ThingDb.Things[objname.Text].Xfer)
            {
                //case "FieldGuideXfer":
                case "SpellRewardXfer":
                    cbospells.Items.Add("SPELL_INVALID");
                    foreach (ThingDb.Spell Spell in ThingDb.Spells.Values)
                        cbospells.Items.Add(Spell.Name);
                    cbospells.Enabled = true;
                    break;
                case "AbilityRewardXfer":
                    foreach (ThingDb.Ability Spell in ThingDb.Abilities.Values)
                        cbospells.Items.Add(Spell.Name);
                    cbospells.Enabled = true;
                    break;
                default:
                    cbospells.Text = "SPELL_INVALID";
                    cbospells.Enabled = false;
                    break;

            }
        }

        private void objname_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRange();
        }

        private void ObjectShopEditorDialog_Load(object sender, EventArgs e)
        {

        }
    }
}