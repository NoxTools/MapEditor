/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 06.10.2014
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NoxShared;

namespace MapEditor.XferGui
{
	/// <summary>
	/// Description of StatusCheckList.
	/// </summary>
	public class StatusCheckList : CheckedListBox
	{
		private uint[] FlagValues = (uint[]) Enum.GetValues(typeof(NoxEnums.MonsterStatus));
		
		public StatusCheckList() : base() { }
		
		public void SetCheckedFlags(NoxEnums.MonsterStatus ms)
		{
			uint flags = (uint) ms;
			
			for (int i = 0; i < Items.Count; i++)
			{
				if ((flags & FlagValues[i]) == FlagValues[i])
				{
					SetItemChecked(i, true);
				}
			}
		}
		
		public NoxEnums.MonsterStatus GetCheckedFlags()
		{
			uint flagsSum = 0;
			
			for (int i = 0; i < Items.Count; i++)
			{
				if (GetItemChecked(i))
				{
					flagsSum |= FlagValues[i];
				}
			}
			
			return (NoxEnums.MonsterStatus) flagsSum;
		}
		
		public static NoxEnums.MonsterStatus GetFlagsFromString(string str)
		{
			NoxEnums.MonsterStatus result = (NoxEnums.MonsterStatus) 0;
			// смотрим что в строке
			string[] statusflags = str.Split('+');
			foreach (string s in statusflags)
			{
				switch (s)
				{
					case "DESTROY_WHEN_DEAD":
						result |= NoxEnums.MonsterStatus.DESTROY_WHEN_DEAD;
						break;
					case "CAN_BLOCK":
						result |= NoxEnums.MonsterStatus.CAN_BLOCK;
						break;
					case "CAN_DODGE":
						result |= NoxEnums.MonsterStatus.CAN_DODGE;
						break;
					case "CAN_CAST_SPELLS":
						result |= NoxEnums.MonsterStatus.CAN_CAST_SPELLS;
						break;
					case "NEVER_RUN":
						result |= NoxEnums.MonsterStatus.NEVER_RUN;
						break;
				}
			}
			return result;
		}
	}
}
