using System;

namespace NoxShared
{
	/// <summary>
	/// A set of script commands and data that can be saved into the Scripts sections of a map
	/// </summary>
	public class Script
	{
		public enum Operator : int
		{
			loadstr = 0x06,
			declvar = 0x01,
			storevar = 0x02,
			loadvar = 0x00,
			inti = 0x04,//immediate int value
			floati = 0x05,
			call = 0x45,
		}

		public enum BuiltInFunction : int
		{
			Timer = 0x0A,
			ObjectOn = 0x13,
			ObjectOff = 0x14,
			GetWall = 0x00,
			OpenWall = 0x01,
			MoveObjectToWaypoint = 0x0B,
		}

		/// <summary>
		/// Return the script in "human readable" form
		/// </summary>
		/// <returns>the script in human readable form</returns>
		public override string ToString()
		{
			return base.ToString ();
		}

		/// <summary>
		/// Convert the script to the format found in Nox Map files
		/// </summary>
		/// <returns>the script in "compiled" form</returns>
		public byte[] Compile()
		{
			return new byte[0];
		}
	}
}
