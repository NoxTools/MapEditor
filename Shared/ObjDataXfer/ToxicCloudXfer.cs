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
	/// Description of ToxicCloudXfer.
	/// </summary>
	[Serializable]
	public class ToxicCloudXfer : DefaultXfer
	{
		public int Lifetime;
		
		public ToxicCloudXfer()
		{
			Lifetime = 1;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			byte[] tmp = new byte[4];
			mstream.Read(tmp, 0, 4);
			Lifetime = BitConverter.ToInt32(tmp, 0);
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			mstream.Write(BitConverter.GetBytes(Lifetime), 0, 4);
		}
		
		public override short MaxVersion
		{
			get
			{
				return 0x3d;
			}
		}
	}
}
