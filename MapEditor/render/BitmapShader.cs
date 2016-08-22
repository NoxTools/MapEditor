/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - PUBLIC DOMAIN
 * Дата: 11.10.2014
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MapEditor.render
{
	/// <summary>
	/// Предназначено для выполнения объёмных операций с картинками
	/// </summary>
	public class BitmapShader
	{
		protected Bitmap bitmap;
		protected BitmapData bitData;
		protected bool locked = false;
		
		public BitmapShader(Bitmap bitmap)
		{
			this.bitmap = (Bitmap) bitmap.Clone();
		}
		
		public void LockBitmap()
		{
			if (!locked)
			{
				locked = true;
				bitData = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			}
		}
		
		public Bitmap UnlockBitmap()
		{
			Bitmap result = null;
			if (locked)
			{
				locked = false;
				bitmap.UnlockBits(bitData);
				result = bitmap;
			}
			return result;
		}
		
		public void ColorShade(Color color, float percent)
		{
			if (percent > 1F) percent = 1F;
			if (locked)
			{
				byte[] bitarray = new byte[bitData.Stride * bitData.Height];
				Marshal.Copy(bitData.Scan0, bitarray, 0, bitarray.Length);
				
				byte R, G, B;
				byte colR = color.R;
				byte colG = color.G;
				byte colB = color.B;
				float perc2 = 1F - percent;
				for (int x = 0; x < bitarray.Length; x += 4)
				{
					if (bitarray[x + 3] != 1)
					{
						B = bitarray[x];
						G = bitarray[x + 1];
						R = bitarray[x + 2];
						bitarray[x] = (byte) (colB * percent + B * perc2);
						bitarray[x + 1] = (byte) (colG * percent + G * perc2);
						bitarray[x + 2] = (byte) (colR * percent + R * perc2);
					}
				}
				
				Marshal.Copy(bitarray, 0, bitData.Scan0, bitarray.Length);
			}
		}
		
		public void ColorGradWaves(Color color, float detail, int increment)
		{
			if (detail > 1F) detail = 1F;
			if (locked)
			{
				byte[] bitarray = new byte[bitData.Stride * bitData.Height];
				Marshal.Copy(bitData.Scan0, bitarray, 0, bitarray.Length);
				
				byte R, G, B;
				byte colR = color.R;
				byte colG = color.G;
				byte colB = color.B;
				float max = detail * (bitarray.Length / 8f);
				for (int x = 0; x < bitarray.Length; x += 4)
				{
					float percent = ((increment + x) % max) / max;
					if (percent > 0.5F) percent = 1F - percent;
					float perc2 = 1F - percent;
					if (bitarray[x + 3] != 1)
					{
						B = bitarray[x];
						G = bitarray[x + 1];
						R = bitarray[x + 2];
						bitarray[x] = (byte) (colB * percent + B * perc2);
						bitarray[x + 1] = (byte) (colG * percent + G * perc2);
						bitarray[x + 2] = (byte) (colR * percent + R * perc2);
					}
				}
				
				Marshal.Copy(bitarray, 0, bitData.Scan0, bitarray.Length);
			}
		}
		
		public void MakeSemitransparent(int trans = 128)
		{
			if (locked)
			{
				byte[] bitarray = new byte[bitData.Stride * bitData.Height];
				Marshal.Copy(bitData.Scan0, bitarray, 0, bitarray.Length);
				
				for (int x = 0; x < bitarray.Length; x += 4)
				{
					if (bitarray[x + 3] > trans)
						bitarray[x + 3] = (byte)trans;
				}
				
				Marshal.Copy(bitarray, 0, bitData.Scan0, bitarray.Length);
			}
		}
	}
}
