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
	/// Description of TransporterXfer.
	/// </summary>
	[Serializable]
	public class TransporterXfer : DefaultXfer
	{
		public int ExtentLink; // 0 = unlinked
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			byte[] tmp = new byte[4];
			mstream.Read(tmp, 0, 4);
			ExtentLink = BitConverter.ToInt32(tmp, 0);
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			mstream.Write(BitConverter.GetBytes(ExtentLink), 0, 4);
		}
		
		public override short MaxVersion
		{
			get
			{
				return 0x3c;
			}
		}
	}
}
