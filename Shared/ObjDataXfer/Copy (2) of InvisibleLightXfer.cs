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
    public class InvisibleLightXfer22 : DefaultXfer
    {
        public byte[] Unknown2;
        public int NumOfColors; // 0x88
        public byte LightIntensity;
        public int LightRadius;
        public byte[] UnknownVal; // 0x94
        public byte R1;
        public byte G1; // 0xA4
        public byte B1; // 0xA6
        public byte R2;
        public byte G2;
        public byte B2;

        public Color Color1;
        public Color Color2;

        public byte[] Unknown3; // 0xA8
        public byte[] Unknown4; // 0xA8
        public byte PulseSpeed;
        public int Unknown5; // 0xB0
        public short Unknown6; // 0xB0
        public Color[] ChangeColors; // [16]
        public short ChangeIntensity; // [16]
        public byte[] ChangeRadius; // [16]
        public short ColorChangeIndex; // 0x102
        public short IntensityChangeIndex; // 0x104
        public short RadiusChangeIndex; // 0x106
        public int ObjExtentID; // 0x108
        public short Unknown13; // 0x10E
        public short Unknown14; // 0x110
        public byte Unknown15; // 0x112
        public int IsAntiLight; // 0xAC Emits light?

        public InvisibleLightXfer22()
		{

			Color1 = Color.FromArgb(90, 90, 90);
			Color2 = Color.FromArgb(10, 10, 10);

		}
        public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
        {
            NoxBinaryReader br = new NoxBinaryReader(mstream);
            
            Unknown2 = br.ReadBytes(6);
            LightRadius = br.ReadInt32();
            NumOfColors = br.ReadInt32(); //
            LightIntensity = br.ReadByte(); ;//gradient amount 32 00
            UnknownVal = br.ReadBytes(21);
           // Unknown5 = br.ReadInt32(); //
           // Unknown6 = br.ReadInt16(); //
            ChangeIntensity = br.ReadInt16(); // Color change speed 15
            Color1 = br.ReadColor();
            Color2 = br.ReadColor();
            /*
            R1 = br.ReadByte();
            G1 = br.ReadByte(); // Flags2?
            B1 = br.ReadByte(); // Some flag
            R2 = br.ReadByte();
            G2 = br.ReadByte(); // Flags2?
            B2 = br.ReadByte(); // Some flag
            */
      

            Unknown3 = br.ReadBytes(74);
            PulseSpeed = br.ReadByte();
            Unknown4 = br.ReadBytes(18);
            /*
            //ChangeColors = new Color[16];18
            //for (int i = 0; i < 16; i++) ChangeColors[i] = br.ReadColor();
          //  Unknown3 = new byte[16];
            //for (int i = 0; i < 16; i++) ChangeIntensity[i] = br.ReadByte();
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
            */
              return true;
              
        }

        public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
        {
            NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);

            bw.Write(Unknown2);
            bw.Write(LightRadius);
            bw.Write(NumOfColors);
            bw.Write(LightIntensity);
            bw.Write(UnknownVal);
            //bw.Write(Unknown5);
            //bw.Write(Unknown6);
            bw.Write(ChangeIntensity);
            /*
            bw.Write(R1);
            bw.Write(G1);
            bw.Write(B1);
            bw.Write(R2);
            bw.Write(G2);
            bw.Write(B2);
             * */
            bw.WriteColor(Color1);
            bw.WriteColor(Color2);

            bw.Write(Unknown3);
            bw.Write(PulseSpeed);
            bw.Write(Unknown4);
            //for (int i = 0; i < 16; i++) bw.WriteColor(ChangeColors[i]);
            // for (int i = 0; i < 16; i++) bw.Write(ChangeIntensity[i]);
           // for (int i = 0; i < 16; i++) bw.Write(ChangeRadius[i]);
           // bw.Write(ColorChangeIndex);
           // bw.Write(IntensityChangeIndex);
           // bw.Write(RadiusChangeIndex);
           // bw.Write(ObjExtentID);
          //  bw.Write(Unknown13);
          //  bw.Write(Unknown14);
          //  bw.Write(Unknown15);
          //  bw.Write(IsAntiLight);
        }
    }
}
