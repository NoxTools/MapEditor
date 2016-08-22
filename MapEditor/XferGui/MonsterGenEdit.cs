/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 23.10.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of MonsterGenEdit.
	/// </summary>
	public partial class MonsterGenEdit : XferEditor
	{
		NoxShared.ObjDataXfer.MonsterGeneratorXfer Xfer;
		const string EMPTY_MONSTER_SLOT = "(null)";
		bool blockrecur = false;
		
		public MonsterGenEdit()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.spawningAlgBox.SelectedIndex = 0;
			this.spawnLimitBox.SelectedIndex = 1;
			this.spawnRateBox.SelectedIndex = 1;
			
			// Add monsters
			foreach (ThingDb.Thing t in ThingDb.Things.Values)
			{
				if (t.HasClassFlag(ThingDb.Thing.ClassFlags.MONSTER))
					currentMonsterName.Items.Add(t.Name);
			}
		}
		
		public override void SetObject(NoxShared.Map.Object obj)
		{
			this.obj = obj;
			// теперь новый загрузчик гарантирует что ExtraData не будет null
			Xfer = obj.GetExtraData<MonsterGeneratorXfer>();
			Text += string.Format(" - {0}", obj);
			// ставим значения
			scriptCollided.Text = Xfer.ScriptOnCollide;
			scriptDamaged.Text = Xfer.ScriptOnDamage;
			scriptDestroyed.Text = Xfer.ScriptOnDestroy;
			scriptMSpawned.Text = Xfer.ScriptOnSpawn;
			spawningAlgBox.SelectedIndex = Xfer.GenerationFlags;
			// имена монстров
			foreach (string name in Xfer.MonsterNames)
			{
				if (name == null)
					monstersListBox.Items.Add(EMPTY_MONSTER_SLOT);
				else
					monstersListBox.Items.Add(name);
			}
		}
		
		void ButtonCancelClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		
		void UpdateMonsterListBox(object sender, EventArgs e)
		{
			int index = monstersListBox.SelectedIndex;
			if (index >= 0 && !blockrecur)
			{
				// обновляем данные
				string monsterName = (string) monstersListBox.SelectedItem;
				currentMonsterName.Text = monsterName;
				spawnRateBox.SelectedIndex = Xfer.MonsterSpawnRate[index];
				spawnLimitBox.SelectedIndex = Xfer.MonsterSpawnLimit[index];
				checkBoxEnable.Checked = (Xfer.MonsterData[index] != null);
				buttonMonsterProps.Enabled = checkBoxEnable.Checked;
			}
		}
		
		void GenerateMonsterData(Map.Object monster)
		{
			int index = monstersListBox.SelectedIndex;
			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms);
			bw.Write((short) 0x40); // pr1
			bw.Write((short) 0x40); // pr2
			bw.Write((int) (index + 1)); // Ext
			bw.Write((int) 0); // GID
			bw.Write((float) 0); // X
			bw.Write((float) 0); // Y
			bw.Write((byte) 0); // Term
			monster.GetExtraData<MonsterXfer>().WriteToStream(ms, 0x40, ThingDb.Things[monster.Name]);
			bw.Flush();
			byte[] result = ms.ToArray();
			bw.Close();
			Xfer.MonsterData[index] = result;
		}
		
		void ButtonMonsterPropsClick(object sender, EventArgs e)
		{
			int index = monstersListBox.SelectedIndex;
			if (index >= 0)
			{
				Map.Object virtualObj;
				MonsterEdit monsterEditor = new MonsterEdit();
				if (Xfer.MonsterData[index] == null)
				{
					// valid monster?
					if (currentMonsterName.SelectedIndex < 0) return;
					// new monster slot
					virtualObj = new Map.Object(currentMonsterName.Text, PointF.Empty);
					virtualObj.ReadRule1 = 0x40;
					// default data
					virtualObj.NewDefaultExtraData();
					virtualObj.GetExtraData<MonsterXfer>().InitForMonsterName(currentMonsterName.Text);
					monsterEditor.SetObject(virtualObj);
				}
				else
				{
					// modify existing one
					virtualObj = new Map.Object(currentMonsterName.Text, PointF.Empty);
					BinaryReader br = new BinaryReader(new MemoryStream(Xfer.MonsterData[index]));
					virtualObj.ReadRule1 = br.ReadInt16();
					br.BaseStream.Seek(19, SeekOrigin.Current);
					virtualObj.GetExtraData<MonsterXfer>().FromStream(br.BaseStream, virtualObj.ReadRule1, ThingDb.Things[virtualObj.Name]);
					monsterEditor.SetObject(virtualObj);
				}
				if (monsterEditor.ShowDialog() == DialogResult.OK)
				{
					virtualObj = monsterEditor.GetObject();
					GenerateMonsterData(virtualObj);
				}
			}
		}
		
		void ButtonSaveClick(object sender, EventArgs e)
		{
			Xfer.GenerationFlags = spawningAlgBox.SelectedIndex;
			Xfer.ScriptOnSpawn = scriptMSpawned.Text;
			Xfer.ScriptOnDestroy = scriptDestroyed.Text;
			Xfer.ScriptOnDamage = scriptDamaged.Text;
			Xfer.ScriptOnCollide = scriptCollided.Text;
			// store monster names
			for (int i = 0; i < 3; i++)
			{
				Xfer.MonsterNames[i] = (string) monstersListBox.Items[i];
			}
			DialogResult = DialogResult.OK;
			Close();
		}
		
		void CheckBoxEnableCheckedChanged(object sender, EventArgs e)
		{
			int index = monstersListBox.SelectedIndex;
			if (index >= 0)
			{
				if (checkBoxEnable.Checked && Xfer.MonsterData[index] == null)
				{
					// check is monster name chosen
					if (currentMonsterName.SelectedIndex < 0)
						checkBoxEnable.Checked = false;
					else 
					{
						buttonMonsterProps.Enabled = true;
						CurrentMonsterNameSelectedIndexChanged(sender, e);
						ButtonMonsterPropsClick(sender, e);
					}
				}
				else if (!checkBoxEnable.Checked)
				{
					// empty monster slot
					Xfer.MonsterData[index] = null;
					Xfer.MonsterNames[index] = null;
					monstersListBox.Items[index] = EMPTY_MONSTER_SLOT;
					buttonMonsterProps.Enabled = false;
				}
			}
		}
		
		void CurrentMonsterNameSelectedIndexChanged(object sender, EventArgs e)
		{
			int index = monstersListBox.SelectedIndex;
			if (index >= 0 && checkBoxEnable.Checked)
			{
				blockrecur = true;
				monstersListBox.Items[index] = currentMonsterName.Text;
				blockrecur = false;
			}
		}
		
		void SpawnRateBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			int index = monstersListBox.SelectedIndex;
			if (index >= 0)
			{
				Xfer.MonsterSpawnRate[index] = (byte) spawnRateBox.SelectedIndex;
			}
		}
		
		void SpawnLimitBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			int index = monstersListBox.SelectedIndex;
			if (index >= 0)
			{
				Xfer.MonsterSpawnLimit[index] = (byte) spawnLimitBox.SelectedIndex;
			}
		}
	}
}
