/*
 * NoxShared
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 30.06.2015
 */
using System;
using System.IO;
//using System.Windows.Forms;
namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Description of MoverXfer.
	/// </summary>
	[Serializable]
	public class MoverXfer : DefaultXfer
	{
		// Скорость перемещения обьекта
		public int MovingSpeed;
		// Первый вейпоинт, где начнется перемещение
		public int WaypointID;
		// Обьект, который будет двигаться
		public int MovedObjExtent;
		/*	MoveType
 		 * Переключается в коде игры. На большинстве карт равняется 3, активация происходит через скрипт
		 * Режим 0: Задает перемещение обьекта до WaypointID с указанной MoveSpeed. Включает режим 1
		 * Режим 1: Постоянно перемещать обьект по WaypointID и всем подключенным к нему вейпоинтам
		 * Если по пути встретится выключенный вейпоинт, включается режим 3
		 * Режим 2: Перемещает САМ Mover к подключенному обьекту
		 * Режим 3+: Mover находится в неактивном состоянии, перемещения не происходит
		 */
		public byte MoveType;
		// Эти два поля по идее используются только в сохранениях
		public int WaypointStartID; 
		public int WaypointEndID;
		// Не совсем понятно, зачем эти два поля нужны
		public float MoverAcceleration;
		public float MoverSpeed;
		
		public override bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryReader rdr = new BinaryReader(mstream);
			
			MovingSpeed = rdr.ReadInt32();
			WaypointID = rdr.ReadInt32();
			MovedObjExtent = rdr.ReadInt32();
			if (ParsingRule >= 41)
			{
				MoveType = rdr.ReadByte();
				WaypointStartID = rdr.ReadInt32();
				WaypointEndID = rdr.ReadInt32();
			}
			if (ParsingRule >= 42)
			{
				MoverAcceleration = rdr.ReadSingle();
				MoverSpeed = rdr.ReadSingle();
			}

			return true;
		}
		
		public override void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			BinaryWriter bw = new BinaryWriter(mstream);
			bw.Write(MovingSpeed);
			bw.Write(WaypointID);
			bw.Write(MovedObjExtent);
			bw.Write(MoveType);
			bw.Write(WaypointStartID);
			bw.Write(WaypointEndID);
			bw.Write(MoverAcceleration);
			bw.Write(MoverSpeed);
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
