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
	/// Description of SentryGlobeXfer.
	/// </summary>
	[Serializable]
	public class SentryXfer : DefaultXfer
	{
		public float BasePosRadian;
		public float RotateSpeed;
		public float CurrentPosRadian; // только в сохраненных картах
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryReader rdr = new BinaryReader(mstream);
			BasePosRadian = rdr.ReadSingle();
			RotateSpeed = rdr.ReadSingle();
			if (ParsingRule >= 0x3D) CurrentPosRadian = rdr.ReadSingle();
			
			// Validate
			if (BasePosRadian < 0) BasePosRadian = 0;
			if (RotateSpeed < 0) RotateSpeed = 0;
			if (CurrentPosRadian < 0) CurrentPosRadian = 0;
			if (BasePosRadian > 7) BasePosRadian = 6;
			if (RotateSpeed > 7) RotateSpeed = 6;
			if (CurrentPosRadian > 7) CurrentPosRadian = 6;
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryWriter bw = new BinaryWriter(mstream);
			bw.Write(BasePosRadian);
			bw.Write(RotateSpeed);
			bw.Write(CurrentPosRadian);
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
