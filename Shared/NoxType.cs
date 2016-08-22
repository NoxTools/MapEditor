using System;
using System.Drawing;
using System.Collections;

namespace NoxShared
{
	namespace NoxType
	{
		public class UserColor
		{
			public int Code;

			public UserColor(int code)
			{
				Code = code;
			}

			public static ArrayList Colors = new ArrayList( new Color[] {
												  Color.FromArgb(214, 91, 91),
												  Color.FromArgb(214, 164, 85),
												  Color.FromArgb(213, 214, 85),
												  Color.FromArgb(85, 214, 149),
												  Color.FromArgb(86, 167, 214),
												  Color.FromArgb(214, 85, 213),
												  Color.FromArgb(167, 120, 88),
												  Color.FromArgb(124, 124, 124),
												  Color.FromArgb(150, 0, 0),
												  Color.FromArgb(143, 71, 1),
												  Color.FromArgb(131, 107, 13),
												  Color.FromArgb(13, 131, 61),
												  Color.FromArgb(17, 72, 127),
												  Color.FromArgb(101, 25, 119),
												  Color.FromArgb(127, 17, 57),
												  Color.FromArgb(97, 97, 97),
												  Color.FromArgb(113, 0, 0),
												  Color.FromArgb(107, 53, 0),
												  Color.FromArgb(98, 81, 9),
												  Color.FromArgb(9, 98, 45),
												  Color.FromArgb(12, 53, 95),
												  Color.FromArgb(75, 17, 90),
												  Color.FromArgb(95, 12, 42),
												  Color.FromArgb(70, 70, 70),
												  Color.FromArgb(76, 0, 0),
												  Color.FromArgb(71, 34, 0),
												  Color.FromArgb(65, 53, 4),
												  Color.FromArgb(4, 65, 30),
												  Color.FromArgb(6, 34, 63),
												  Color.FromArgb(48, 10, 53),
												  Color.FromArgb(63, 6, 27),
												  Color.FromArgb(42, 42, 42),
												  Color.FromArgb(50, 50, 50)
											  });

			//colorNumber must be from 1 to 33
			public static Color GetColor(int colorNumber)
			{
				return (Color) Colors[colorNumber - 1];
			}

			public static implicit operator Color(UserColor color)
			{
				return (Color) Colors[color.Code - 1];
			}

			public static explicit operator UserColor(Color color)
			{
				return new UserColor(UserColor.Colors.IndexOf(color) + 1);
			}
		}

		public class UserMaterialColor
		{
			protected int colorCode;
			public UserMaterialColor(int color)
			{
				colorCode = color;
			}

			protected static Color[] colors = {
												  Color.FromArgb(187, 0, 0),
												  Color.FromArgb(176, 88, 3),
												  Color.FromArgb(162, 134, 17),
												  Color.FromArgb(17, 162, 75),
												  Color.FromArgb(23, 90, 157),
												  Color.FromArgb(125, 32, 147),
												  Color.FromArgb(157, 23, 72),
												  Color.FromArgb(124, 124, 124),
												  Color.FromArgb(150, 0, 0),
												  Color.FromArgb(143, 71, 1),
												  Color.FromArgb(131, 107, 13),
												  Color.FromArgb(13, 131, 61),
												  Color.FromArgb(17, 72, 127),
												  Color.FromArgb(101, 25, 119),
												  Color.FromArgb(127, 17, 57),
												  Color.FromArgb(97, 97, 97),
												  Color.FromArgb(113, 0, 0),
												  Color.FromArgb(107, 53, 0),
												  Color.FromArgb(98, 81, 9),
												  Color.FromArgb(9, 98, 45),
												  Color.FromArgb(12, 53, 95),
												  Color.FromArgb(75, 17, 90),
												  Color.FromArgb(95, 12, 42),
												  Color.FromArgb(70, 70, 70),
												  Color.FromArgb(76, 0, 0),
												  Color.FromArgb(71, 34, 0),
												  Color.FromArgb(65, 53, 4),
												  Color.FromArgb(4, 65, 30),
												  Color.FromArgb(6, 34, 63),
												  Color.FromArgb(48, 10, 53),
												  Color.FromArgb(63, 6, 27),
												  Color.FromArgb(42, 42, 42)
											  };

			//colorNumber must be from 1 to 32
			public static Color GetColor(int colorNumber)
			{
				return colors[colorNumber - 1];
			}

			public static implicit operator Color(UserMaterialColor color)
			{
				return colors[color.colorCode - 1];
			}
		}
	}
}
