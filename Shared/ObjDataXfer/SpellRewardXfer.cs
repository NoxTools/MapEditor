/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;
using System.Text;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of SpellRewardXfer.
	/// </summary>
	[Serializable]
	public class SpellRewardXfer : DefaultXfer
	{
		// The string does not contain null terminator - it is added by the game when loading the map
		// Game will cancel loading Xfer if string is longer than 128 bytes
		public string SpellName;
		
		public SpellRewardXfer()
		{
			SpellName = "SPELL_INVALID";
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			if (ParsingRule < 41) return false; // unlikely
			SpellName = br.ReadString();
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			byte[] result = new byte[SpellName.Length + 1];
			result[0] = (byte) SpellName.Length;
			byte[] str = Encoding.ASCII.GetBytes(SpellName);
			Array.Copy(str, 0, result, 1, str.Length);
			mstream.Write(result, 0, result.Length);
		}
	}
}
