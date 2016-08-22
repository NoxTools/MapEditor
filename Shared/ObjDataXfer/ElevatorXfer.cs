/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;
using NoxShared;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of ElevatorXfer.
	/// </summary>
	[Serializable]
	public class ElevatorXfer : DefaultXfer
	{
		public int ExtentLink; // 0 = unlinked
		public int Height; // max 64
		public byte Status; // 0 - waiting down, 1 - moving down, 2 - waiting up, 3 - moving up
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryReader rdr = new BinaryReader(mstream);
			
			ExtentLink = rdr.ReadInt32();
			if (thing.HasClassFlag(ThingDb.Thing.ClassFlags.ELEVATOR))
			{
				if (ParsingRule >= 0x29) Height = rdr.ReadInt32();
				if (ParsingRule >= 0x3D) Status = rdr.ReadByte();
			}
			return true;
		}
		
		public override void WriteToStream(Stream baseStream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryWriter bw = new BinaryWriter(baseStream);
			bw.Write(ExtentLink);
			if (thing.HasClassFlag(ThingDb.Thing.ClassFlags.ELEVATOR))
			{
				bw.Write(Height);
				if (ParsingRule >= 0x3D) bw.Write(Status);
			}
		}
		
		public override short MaxVersion
		{
			get
			{
				return 0x3c; // 0x3d
				// HACK: ElevatorXfer actually implements ElevatorShaftXfer too
				// but ElevatorXfer may have version 0x3d while ElevatorShaftXfer up to 0x3c
				// we are thus forced to always ignore Status field for ElevatorXfer
			}
		}
	}
}
