using SkiaSharp;
using System;
using Xamarin.Forms;

namespace CarameloApp.Resources
{
	/// <summary>
	/// Classe contendo as cores do projetos e métodos auxiliares
	/// </summary>
	public static class CarameloColors
	{
		public static Color InitialBackgroundColor = Color.FromHex("F2B749");
		public static Color BackgroundColor = Color.FromHex("f8e6ba");
		public static Color SecondBackgroundColor = Color.FromHex("F8DE9F");
		public static Color TextColor = Color.FromHex("731702");
		public static Color TabsColor = Color.FromHex("bf7534");

		private static readonly Random _random = new Random();

		public static Color GetRandom() => Color.FromHex(string.Format("#{0:X6}", _random.Next(0x1000000)));

		public static SKColor ToSKColor(this Color color) => SKColor.Parse(color.GetHexString());

		private static string GetHexString(this Color color)
		{
			var red = (int)(color.R * 255);
			var green = (int)(color.G * 255);
			var blue = (int)(color.B * 255);
			var alpha = (int)(color.A * 255);
			var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

			return hex;
		}
	}
}
