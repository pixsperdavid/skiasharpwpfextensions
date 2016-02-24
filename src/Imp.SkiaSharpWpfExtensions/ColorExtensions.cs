using System.Windows.Media;
using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
	[PublicAPI]
	public static class ColorExtensions
	{
		public static SKColor ToSkia(this SolidColorBrush brush)
		{
			return new SKColor(brush.Color.R, brush.Color.G, brush.Color.B, brush.Color.A);
		}

		public static SKColor ToSkia(this Color color)
		{
			return new SKColor(color.R, color.G, color.B, color.A);
		}
	}
}
