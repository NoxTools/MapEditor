/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 25.10.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of ShopEditForm.
	/// </summary>
	public partial class ShopEditForm : Form
	{
		private static NumberFormatInfo floatFormat = NumberFormatInfo.InvariantInfo;
		// TODO загружать это все из modifier.bin
		private static string[] enchants = new string[] {
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
			"Brilliance2",
			"Brilliance3",
			"Brilliance4",
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
			"Replenishment2",
			"Replenishment3",
			"Replenishment4",
			"ContinualReplenishment1",
			"ContinualReplenishment2",
			"ContinualReplenishment3",
			"ContinualReplenishment4",
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
		};
		
		public ShopEditForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			enchant1.Items.AddRange(enchants);
			enchant2.Items.AddRange(enchants);
			enchant3.Items.AddRange(enchants);
			enchant4.Items.AddRange(enchants);
		}
		
		public void SetShopInfo(MonsterXfer.ShopkeeperInfoStruct sinfo)
		{
			dialogText.Text = sinfo.ShopkeeperGreetingText;
			priceBuyMul.Text = sinfo.BuyValueMultiplier.ToString(floatFormat);
			priceSellMul.Text = sinfo.SellValueMultiplier.ToString(floatFormat);
			foreach (MonsterXfer.ShopItemInfo item in sinfo.ShopItems)
				itemsListBox.Items.Add(item);
		}
		
		public MonsterXfer.ShopkeeperInfoStruct GetShopInfo()
		{
			MonsterXfer.ShopkeeperInfoStruct result = new MonsterXfer.ShopkeeperInfoStruct();
			result.ShopkeeperGreetingText = dialogText.Text;
			result.BuyValueMultiplier = float.Parse(priceBuyMul.Text, floatFormat);
			result.SellValueMultiplier = float.Parse(priceSellMul.Text, floatFormat);
			List<MonsterXfer.ShopItemInfo> items = new List<MonsterXfer.ShopItemInfo>();
			foreach (MonsterXfer.ShopItemInfo item in itemsListBox.Items)
				items.Add(item);
			result.ShopItems = items.ToArray();
			return result;
		}
		
		void ButtonOKClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		
		private void DisableTextboxes()
		{
			spellID.Enabled = false;
			spellID.Text = "";
			enchant1.Enabled = false;
			enchant1.Text = "";
			enchant2.Enabled = false;
			enchant2.Text = "";
			enchant3.Enabled = false;
			enchant3.Text = "";
			enchant4.Enabled = false;
			enchant4.Text = "";
		}
		
		void ItemsListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			object selected = itemsListBox.SelectedItem;
			if (selected != null)
			{
				MonsterXfer.ShopItemInfo item = (MonsterXfer.ShopItemInfo) selected;
				objectID.Text = item.Name;
				objCount.Value = item.Count;
				DisableTextboxes();
				
				if (ThingDb.Things.ContainsKey(item.Name))
				{
					ThingDb.Thing tt = ThingDb.Things[item.Name];
					// FieldGuide/SpellBook/etc
					if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.INFO_BOOK))
					{
						spellID.Text = item.SpellID;
						spellID.Enabled = true;
					}
					// броня/оружие
					if (tt.Init == "ModifierInit")
					{
					 	enchant1.Text = item.Ench1;
						enchant2.Text = item.Ench2;
						enchant3.Text = item.Ench3;
						enchant4.Text = item.Ench4;
						enchant1.Enabled = true;
						enchant2.Enabled = true;
						enchant3.Enabled = true;
						enchant4.Enabled = true;
					}
				}
			}
		}
		
		void ObjectIDTextChanged(object sender, EventArgs e)
		{
			string name = objectID.Text;
			DisableTextboxes();
			if (ThingDb.Things.ContainsKey(name))
			{
				ThingDb.Thing tt = ThingDb.Things[name];
				
				if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.INFO_BOOK))
				{
					spellID.Enabled = true;
				}
				if (tt.Init == "ModifierInit")
				{
					enchant1.Enabled = true;
					enchant2.Enabled = true;
					enchant3.Enabled = true;
					enchant4.Enabled = true;
				}
			}
		}
		
		void ButtonRemoveClick(object sender, EventArgs e)
		{
			int index = itemsListBox.SelectedIndex;
			
			if (index >= 0)
				itemsListBox.Items.RemoveAt(index);
		}
		
		void ButtonAddClick(object sender, EventArgs e)
		{
			int index = itemsListBox.SelectedIndex;
			
			MonsterXfer.ShopItemInfo item = new MonsterXfer.ShopItemInfo();
			item.Name = objectID.Text;
			if (!ThingDb.Things.ContainsKey(item.Name))
			{
				string msg = string.Format("There is no object with ID '{0}'. Are you really sure you want to add it to the list?", item.Name);
				if (MessageBox.Show(msg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
					return;
			}
			item.Count = (byte) objCount.Value;
			item.SpellID = spellID.Text;
			item.Ench1 = enchant1.Text;
			item.Ench2 = enchant2.Text;
			item.Ench3 = enchant3.Text;
			item.Ench4 = enchant4.Text;
			
			if (index >= 0)
				itemsListBox.Items.Insert(index, item);
			else
				itemsListBox.Items.Add(item);
		}
	}
}
