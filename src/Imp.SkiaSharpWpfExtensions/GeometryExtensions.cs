using System.Windows;
using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
    /// <summary>
    ///     Extension methods for Skia geometry types
    /// </summary>
	[PublicAPI]
	public static class GeometryExtensions
	{
        /// <summary>
        ///     Converts a <see cref="Rect"/> to it's equivalent <see cref="SKRect"/>
        /// </summary>
		public static SKRect ToSkia(this Rect rect)
		{
			return new SKRect((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
		}

        /// <summary>
        ///     Converts a <see cref="Point"/> to it's equivalent <see cref="SKPoint"/>
        /// </summary>
		public static SKPoint ToSkia(this Point point)
		{
			return new SKPoint((float)point.X, (float)point.Y);
		}
	}
}
