using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
	[PublicAPI]
	public static class ColorExtensions
	{
		public static SKColor ToSkia(this System.Windows.Media.SolidColorBrush brush)
		{
			return new SKColor(brush.Color.R, brush.Color.G, brush.Color.B, brush.Color.A);
		}

		public static SKColor ToSkia(this System.Windows.Media.Color color)
		{
			return new SKColor(color.R, color.G, color.B, color.A);
		}

		public static SKColor ToSkia(this System.Drawing.Color color)
		{
			return new SKColor(color.R, color.G, color.B, color.A);
		}
	}
}
