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
	/// Description of HoleXfer.
	/// </summary>
	[Serializable]
	public class HoleXfer : DefaultXfer
	{
		public string UnknownScriptHandler;
		public int FallX;
		public int FallY;
		public int ScriptTime1;
		public short ScriptTimeout;
		public int ScriptActivated;
	
		public HoleXfer()
		{
			UnknownScriptHandler = "";
			ScriptTimeout = 4;
			FallX = 2885;
			FallY = 2885;
		}
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryReader br = new NoxBinaryReader(mstream);
			
			if (ParsingRule < 42) throw new NotSupportedException("Where did you find this map?");
			ScriptActivated = br.ReadInt32();
			// какой-то скриптовый обработчик
			UnknownScriptHandler = br.ReadScriptEventString();
			// координаты места падения
			FallX = br.ReadInt32();
			FallY = br.ReadInt32();
			// таймаут вызова скрипта
			ScriptTime1 = br.ReadInt32();
			ScriptTimeout = br.ReadInt16();
			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			NoxBinaryWriter bw = new NoxBinaryWriter(mstream, CryptApi.NoxCryptFormat.NONE);
			bw.Write(ScriptActivated);
			// script event
			bw.Write((short) 1);
			bw.Write(UnknownScriptHandler.Length);
			bw.Write(Encoding.ASCII.GetBytes(UnknownScriptHandler));
			bw.Write((int) 0);
			// coordinates
			bw.Write(FallX);
			bw.Write(FallY);
			// script delay
			bw.Write(ScriptTime1);
			bw.Write(ScriptTimeout);
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
