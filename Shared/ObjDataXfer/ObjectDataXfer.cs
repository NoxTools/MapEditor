/*
 * NoxShared
 * Пользователь: AngryKirC
 * Дата: 29.06.2015
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace NoxShared.ObjDataXfer
{
	/// <summary>
	/// Default container for a transferred map object data.
	/// </summary>
	[Serializable]
	public class DefaultXfer : ICloneable
	{
		/// <summary>
		/// Reads an object's extra data from specfied Stream
		/// </summary>
		public virtual bool FromStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			return false;
		}
		
		/// <summary>
		/// Writes an object's extra data to specfied Stream
		/// </summary>
		public virtual void WriteToStream(Stream mstream, short ParsingRule, ThingDb.Thing thing)
		{
			;
		}
		
		/// <summary>
		/// This value will be used for writing Xfer data upon map saving (ReadRule1)
		/// </summary>
		public virtual short MaxVersion
		{
			get
			{
				return 0x3c; // 60
			}
		}
		
		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}

namespace NoxShared
{
	public static class ObjectXferProvider
	{
		private static Dictionary<string, Type> Providers;
		
		/// <summary>
		/// Returns ObjectDataXfer implementation for specified Nox type
		/// </summary>
		public static ObjDataXfer.DefaultXfer Get(string xferName)
		{
			if (xferName != null) // null = DefaultXfer
			{
				if (Providers.ContainsKey(xferName))
				{
					return (ObjDataXfer.DefaultXfer) Activator.CreateInstance(Providers[xferName]);
				}
			}
			// Not found, assume DefaultXfer
			return new ObjDataXfer.DefaultXfer();
		}
		
		static ObjectXferProvider()
		{
			Providers = new Dictionary<string, Type>();
			/* -- Here goes list of known Xfer providers -- */
			// DefaultXfer...
			Providers.Add("SpellPagePedestalXfer", typeof(ObjDataXfer.SpellPagePedestalXfer));
			Providers.Add("SpellRewardXfer", typeof(ObjDataXfer.SpellRewardXfer));
			Providers.Add("AbilityRewardXfer", typeof(ObjDataXfer.AbilityRewardXfer));
			Providers.Add("FieldGuideXfer", typeof(ObjDataXfer.FieldGuideXfer));
			Providers.Add("ReadableXfer", typeof(ObjDataXfer.ReadableXfer));
			Providers.Add("ExitXfer", typeof(ObjDataXfer.ExitXfer));
			Providers.Add("DoorXfer", typeof(ObjDataXfer.DoorXfer));
			Providers.Add("TriggerXfer", typeof(ObjDataXfer.TriggerXfer));
			Providers.Add("MonsterXfer", typeof(ObjDataXfer.MonsterXfer));
			Providers.Add("HoleXfer", typeof(ObjDataXfer.HoleXfer));
			Providers.Add("TransporterXfer", typeof(ObjDataXfer.TransporterXfer));
			Providers.Add("ElevatorXfer", typeof(ObjDataXfer.ElevatorXfer));
			Providers.Add("ElevatorShaftXfer", typeof(ObjDataXfer.ElevatorXfer));
			Providers.Add("MoverXfer", typeof(ObjDataXfer.MoverXfer));
			Providers.Add("GlyphXfer", typeof(ObjDataXfer.GlyphXfer));
			Providers.Add("InvisibleLightXfer", typeof(ObjDataXfer.InvisibleLightXfer));
			Providers.Add("SentryXfer", typeof(ObjDataXfer.SentryXfer));
			Providers.Add("WeaponXfer", typeof(ObjDataXfer.WeaponXfer));
			Providers.Add("ArmorXfer", typeof(ObjDataXfer.ArmorXfer));
			Providers.Add("TeamXfer", typeof(ObjDataXfer.TeamXfer));
			Providers.Add("GoldXfer", typeof(ObjDataXfer.GoldXfer));
			Providers.Add("AmmoXfer", typeof(ObjDataXfer.AmmoXfer));
			Providers.Add("NPCXfer", typeof(ObjDataXfer.NPCXfer));
			Providers.Add("ObeliskXfer", typeof(ObjDataXfer.ObeliskXfer));
			Providers.Add("ToxicCloudXfer", typeof(ObjDataXfer.ToxicCloudXfer));
			Providers.Add("MonsterGeneratorXfer", typeof(ObjDataXfer.MonsterGeneratorXfer));
			Providers.Add("RewardMarkerXfer", typeof(ObjDataXfer.RewardMarkerXfer));
		}
	}
}
