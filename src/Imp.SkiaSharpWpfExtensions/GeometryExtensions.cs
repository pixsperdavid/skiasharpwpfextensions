using System.Windows;
using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
	[PublicAPI]
	public static class GeometryExtensions
	{
		public static SKRect ToSkia(this Rect rect)
		{
			return new SKRect((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
		}

		public static SKPoint ToSkia(this Point point)
		{
			return new SKPoint((float)point.X, (float)point.Y);
		}
	}
}
