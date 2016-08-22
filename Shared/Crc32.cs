using System;

namespace NoxShared
{
	public class Crc32
	{
		protected static uint[] table = new uint[256];

		static Crc32()
		{
			uint dwPolynomial = 0xEDB88320;//official PKZIP polynomial
			uint dwCrc;
			for(uint i = 0; i < 256; i++)
			{
				dwCrc = i;
				for(int j = 8; j > 0; j--)
				{
					if((dwCrc & 1) != 0)
						dwCrc = (dwCrc >> 1) ^ dwPolynomial;
					else
						dwCrc >>= 1;
				}
				table[i] = dwCrc;
			}
		}

		public static int Calculate(byte[] data)
		{
			uint crc32 = 0xFFFFFFFF;
			foreach (byte b in data)
				crc32 = (crc32 >> 8) ^ table[b ^ (crc32 & 0xFF)];
			return (int)~crc32;
		}
	}
}
