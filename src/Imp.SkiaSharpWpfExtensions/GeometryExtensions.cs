using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
	[PublicAPI]
	public static class GeometryExtensions
	{
		public static SKRect ToSkia(this System.Windows.Rect rect)
		{
			return new SKRect((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
		}

		public static SKRect ToSkia(this System.Drawing.Rectangle rect)
		{
			return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}

		public static SKRect ToSkia(this System.Drawing.RectangleF rect)
		{
			return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}

		public static SKPoint ToSkia(this System.Windows.Point point)
		{
			return new SKPoint((float)point.X, (float)point.Y);
		}

		public static SKPoint ToSkia(this System.Drawing.Point point)
		{
			return new SKPoint(point.X, point.Y);
		}

		public static SKPoint ToSkia(this System.Drawing.PointF point)
		{
			return new SKPoint(point.X, point.Y);
		}
	}
}
