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
	/// Description of ReadableXfer.
	/// </summary>
	[Serializable]
	public class ReadableXfer : DefaultXfer
	{
		public string Text;
		
		public ReadableXfer()
		{
			Text = "_aTest2.map:ShopkeeperDialog";
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			Text = "";
			BinaryReader br = new BinaryReader(mstream);
			int len = br.ReadInt32();
			Text = Encoding.UTF8.GetString(br.ReadBytes(len));
			Text = Text.TrimEnd('\0');
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryWriter bw = new BinaryWriter(mstream);
            byte[] bytes = Encoding.UTF8.GetBytes(Text);
            if (bytes.Length == 0 || bytes[bytes.Length - 1] != 0)
            {
                byte [] tmp = new byte[bytes.Length + 1];
                bytes.CopyTo(tmp, 0);
                tmp[bytes.Length] = 0;
                bytes = tmp;
            }
            bw.Write(bytes.Length);
            bw.Write(bytes);
		}
	}
}
