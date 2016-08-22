/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;
using System.Drawing;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// // TODO finish field research
	/// </summary>
	[Serializable]
	public class InvisibleLightXfer2 : DefaultXfer
	{
		public int NumOfColors; // 0x88
		public float LightIntensity;
		public int LightRadius;
		public int UnknownVal; // 0x94
		public byte[] Unknown2;
		public short Unknown3; // 0xA4
		public short Unknown4; // 0xA6
		public int Unknown5; // 0xA8
		public short Unknown6; // 0xB0
		public Color[] ChangeColors; // [16]
		public byte[] ChangeIntensity; // [16]
		public byte[] ChangeRadius; // [16]
		public short ColorChangeIndex; // 0x102
		public short IntensityChangeIndex; // 0x104
		public short RadiusChangeIndex; // 0x106
		public int ObjExtentID; // 0x108
		public short Unknown13; // 0x10E
		public short Unknown14; // 0x110
		public byte Unknown15; // 0x112
		public int IsAntiLight; // 0xAC Emits light?
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			
			NumOfColors = br.ReadInt32(); // 2 normally
			LightIntensity = br.ReadSingle();
			LightRadius = br.ReadInt32();
			UnknownVal = br.ReadInt32(); // unused?
			Unknown2 = br.ReadBytes(12);
			Unknown3 = br.ReadInt16(); // Color Flags?
			Unknown4 = br.ReadInt16();
			Unknown5 = br.ReadInt32(); // Flags2?
			Unknown6 = br.ReadInt16(); // Some flag
			ChangeColors = new Color[16];
			for (int i = 0; i < 16; i++) ChangeColors[i] = br.ReadColor();
			ChangeIntensity = new byte[16];
			for (int i = 0; i < 16; i++) ChangeIntensity[i] = br.ReadByte();
			ChangeRadius = new byte[16];
			for (int i = 0; i < 16; i++) ChangeRadius[i] = br.ReadByte();
			ColorChangeIndex = br.ReadInt16();
			IntensityChangeIndex = br.ReadInt16();
			RadiusChangeIndex = br.ReadInt16(); 
			ObjExtentID = br.ReadInt32();
			Unknown13 = br.ReadInt16(); // some flags
			Unknown14 = br.ReadInt16();
			Unknown15 = br.ReadByte();
			// Probably type of light (0 = emitter, 1 = absorber?)
			if (ParsingRule >= 42)
				IsAntiLight = br.ReadInt32(); 
			else
				IsAntiLight = br.ReadByte();
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			
			bw.Write(NumOfColors);
			bw.Write(LightIntensity);
			bw.Write(LightRadius);
			bw.Write(UnknownVal);
			bw.Write(Unknown2);
			bw.Write(Unknown3);
			bw.Write(Unknown4);
			bw.Write(Unknown5);
			bw.Write(Unknown6);
			for (int i = 0; i < 16; i++) bw.WriteColor(ChangeColors[i]);
			for (int i = 0; i < 16; i++) bw.Write(ChangeIntensity[i]);
			for (int i = 0; i < 16; i++) bw.Write(ChangeRadius[i]);
			bw.Write(ColorChangeIndex);
			bw.Write(IntensityChangeIndex);
			bw.Write(RadiusChangeIndex);
			bw.Write(ObjExtentID);
			bw.Write(Unknown13);
			bw.Write(Unknown14);
			bw.Write(Unknown15);
			bw.Write(IsAntiLight);
		}
	}
}
