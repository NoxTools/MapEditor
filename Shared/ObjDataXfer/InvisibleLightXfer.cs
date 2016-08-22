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
    public class InvisibleLightXfer : DefaultXfer
    {
        public byte[] Unknown2;
        public int NumOfColors; // 0x88
        public short LightIntensity;
        public short LightRadius;
        public byte[] UnknownVal; // 0x94
        public byte R;
        public byte G; // 0xA4
        public byte B; // 0xA6
        public byte R2;
        public byte G2;
        public byte B2;
        public byte R3;
        public byte G3;
        public byte B3;

        public byte MinRadius2;
        public byte MaxRadius2;
        public byte MinRadius3;
        public byte MaxRadius3;
        public byte MinRadius4;
        public byte MaxRadius4;
        public byte MinRadius5;
        public byte MaxRadius5;
        public byte MinRadius6;
        public byte MaxRadius6;
        public byte MinRadius7;
        public byte MaxRadius7;
        public byte MinRadius8;
        public byte MaxRadius8;
        public byte MinRadius9;
        public byte MaxRadius9;
        public byte MinRadius10;
        public byte MaxRadius10;



        public Color Color1;
        public Color Color2;
        public Color Color3;
        public Color Color4;

        public Color Color5;
        public Color Color6;
        public Color Color7;
        public Color Color8;
        public Color Color9;
        public Color Color10;

        public byte[] Unknown3; // 0xA8
        public byte[] Unknown4; // 0xA8
        public byte UnknownR;
        public byte UnknownG;
        public byte UnknownB;
        public byte UnknownR2;
        public byte UnknownG2;
        public byte UnknownB2;
        public byte PulseSpeed;
        public byte PulseSpeedSingle;
        public byte ChangeIntensitySingle;
        public byte Unknown11;
        public byte type;
        public int Unknown5; // 0xB0
        public short Unknown6; // 0xB0
        public Color[] ChangeColors; // [16]
        public short ChangeIntensity; // [16]
        public byte[] ChangeRadius; // [16]
        public short ColorChangeIndex; // 0x102
        public short IntensityChangeIndex; // 0x104
        public short RadiusChangeIndex; // 0x106
        public byte Unknown7; // 0x10E
        public byte Unknown8; // 0x110
        public byte Unknown10; // 0x112
        public int IsAntiLight; // 0xAC Emits light?
        public byte Unknown80;
        public byte[] Unknown30;
        public byte[] Unknown9;
        public InvisibleLightXfer()
		{



            //Unknown2 = 0x020000000000;
            LightRadius = 180;
            R = 0xff;
            G = 0xff;
            B = 0xff;
            LightIntensity = 60;
           // Color1 = Color.Black;
           // Color2 = Color.Black;
            Unknown10 = 0x14;
            Unknown80 = 0x80;
            Unknown2 = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x00 };
            //UnknownR = new byte[] { 0x00, 0x00 };
           // UnknownG = new byte[] { 0x00, 0x00 };
           // UnknownB = new byte[] { 0x00, 0x00 };
            ChangeIntensity = 21;
            Unknown9 = new byte[] { 0x00, 0x00, 0x00 };;
            UnknownVal = new byte[] { 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
            Unknown3 = new byte[18]; for (int i = 0; i < Unknown3.Length; i++) Unknown3[i] = 0x00;
            Unknown4 = new byte[11]; for (int i = 0; i < Unknown4.Length; i++) Unknown4[i] = 0x00;
            Unknown30 = new byte[12]; for (int i = 0; i < Unknown30.Length; i++) Unknown30[i] = 0x00;
        }
        public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
        {
            NoxBinaryReader br = new NoxBinaryReader(mstream);
            
            Unknown2 = br.ReadBytes(6);
            Unknown7 = br.ReadByte();
            Unknown8 = br.ReadByte();
            LightRadius = br.ReadInt16();
            NumOfColors = br.ReadInt32(); //
            LightIntensity = br.ReadInt16(); ; ;//gradient amount 32 00
            R = br.ReadByte();
            R2 = br.ReadByte();

            UnknownR = br.ReadByte();
            UnknownR2 = br.ReadByte();

            G = br.ReadByte();
            G2 = br.ReadByte();
            UnknownG = br.ReadByte();
            UnknownG2 = br.ReadByte();

            B = br.ReadByte();
            B2 = br.ReadByte();
            UnknownB = br.ReadByte();
            UnknownB2 = br.ReadByte();

            UnknownVal = br.ReadBytes(8);

            ChangeIntensity = br.ReadInt16(); // Color change speed 15
            Color1 = br.ReadColor();
            Color2 = br.ReadColor();
            Color3 = br.ReadColor();
            Color4 = br.ReadColor();
            Color5 = br.ReadColor();
            Color6 = br.ReadColor();
            Color7 = br.ReadColor(); 
            Color8 = br.ReadColor();
            Color9 = br.ReadColor();
            Color10 = br.ReadColor();

            Unknown3 = br.ReadBytes(18);//-21//39
            ChangeIntensitySingle = br.ReadByte();//max radius
            Unknown10 = br.ReadByte();//min radius
            
            MaxRadius2 = br.ReadByte();
            MinRadius2 = br.ReadByte();

            MaxRadius3 = br.ReadByte();
            MinRadius3 = br.ReadByte();

            MaxRadius4 = br.ReadByte();
            MinRadius4 = br.ReadByte();

            MaxRadius5 = br.ReadByte();
            MinRadius5 = br.ReadByte();

            MaxRadius6 = br.ReadByte();
            MinRadius6 = br.ReadByte();

            MaxRadius7 = br.ReadByte();
            MinRadius7 = br.ReadByte();

            MaxRadius8 = br.ReadByte();
            MinRadius8 = br.ReadByte();

            MaxRadius9 = br.ReadByte();
            MinRadius9 = br.ReadByte();
            
            MaxRadius10 = br.ReadByte();
            MinRadius10 = br.ReadByte();

            Unknown30 = br.ReadBytes(12);//30
            PulseSpeed = br.ReadByte();
            Unknown11 = br.ReadByte();
            PulseSpeedSingle = br.ReadByte();
            Unknown4 = br.ReadBytes(11);//13


            Unknown80 = br.ReadByte();
            type = br.ReadByte();
            Unknown9 = br.ReadBytes(3);
              return true;
              
        }

        public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
        {
            NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);

            bw.Write(Unknown2);
            bw.Write(Unknown7);
            bw.Write(Unknown8);
            bw.Write(LightRadius);
            bw.Write(NumOfColors);
            bw.Write(LightIntensity);

            bw.Write(R);
            bw.Write(R2);
            bw.Write(UnknownR);
            bw.Write(UnknownR2);
            bw.Write(G);
            bw.Write(G2);
            bw.Write(UnknownG);
            bw.Write(UnknownG2);
            bw.Write(B);
            bw.Write(B2);
            bw.Write(UnknownB);
            bw.Write(UnknownB2);
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
            bw.WriteColor(Color3);

            bw.WriteColor(Color4);
            bw.WriteColor(Color5);
            bw.WriteColor(Color6);
            bw.WriteColor(Color7);
            bw.WriteColor(Color8);
            bw.WriteColor(Color9);
            bw.WriteColor(Color10);


            bw.Write(Unknown3);
            bw.Write(ChangeIntensitySingle);
            bw.Write(Unknown10);

            bw.Write(MaxRadius2);
            bw.Write(MinRadius2);

            bw.Write(MaxRadius3);
            bw.Write(MinRadius3);

            bw.Write(MaxRadius4);
            bw.Write(MinRadius4);

            bw.Write(MaxRadius5);
            bw.Write(MinRadius5);

            bw.Write(MaxRadius6);
            bw.Write(MinRadius6);

            bw.Write(MaxRadius7);
            bw.Write(MinRadius7);

            bw.Write(MaxRadius8);
            bw.Write(MinRadius8);

            bw.Write(MaxRadius9);
            bw.Write(MinRadius9);

            bw.Write(MaxRadius10);
            bw.Write(MinRadius10);


            bw.Write(Unknown30);
            bw.Write(PulseSpeed);
            bw.Write(Unknown11);
            bw.Write(PulseSpeedSingle);
            bw.Write(Unknown4);

            bw.Write(Unknown80);
            bw.Write(type);
            bw.Write(Unknown9);
 
        }
    }
}
