using System;
using System.Windows.Media;
using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
	/// <summary>
	///     Extension methods for Skia color types
	/// </summary>
	[PublicAPI]
	public static class ColorExtensions
	{
		/// <summary>
		///     Converts a <see cref="SolidColorBrush"></see> into it's equivalent <see cref="SKColor"/>
		/// </summary>
		public static SKColor ToSkia([NotNull] this SolidColorBrush brush)
		{
		    if (brush == null)
		        throw new ArgumentNullException(nameof(brush));

		    return new SKColor(brush.Color.R, brush.Color.G, brush.Color.B, brush.Color.A);
		}

	    /// <summary>
        ///     Converts a <see cref="Color"/> into it's equivalent <see cref="SKColor"/>
        /// </summary>
		public static SKColor ToSkia(this Color color)
		{
			return new SKColor(color.R, color.G, color.B, color.A);
		}
	}
}
