/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of GoldXfer.
	/// </summary>
	[Serializable]
	public class GoldXfer : DefaultXfer
	{
		public int Amount;
		
		public GoldXfer()
		{
			Amount = 1;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			byte[] tmp = new byte[4];
			mstream.Read(tmp, 0, 4);
			Amount = BitConverter.ToInt32(tmp, 0);
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			mstream.Write(BitConverter.GetBytes(Amount), 0, 4);
		}
	}
}
