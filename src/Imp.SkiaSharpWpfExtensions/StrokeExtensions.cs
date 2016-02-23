using System;
using System.Windows.Media;
using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
	[PublicAPI]
	public static class StrokeExtensions
	{
		public static SKStrokeJoin ToSkia(this PenLineJoin penLinejoin)
		{
			switch (penLinejoin)
			{
				case PenLineJoin.Miter:
					return SKStrokeJoin.Mitter;
				case PenLineJoin.Bevel:
					return SKStrokeJoin.Bevel;
				case PenLineJoin.Round:
					return SKStrokeJoin.Round;
				default:
					throw new ArgumentOutOfRangeException(nameof(penLinejoin), penLinejoin, null);
			}
		}

		public static SKStrokeCap ToSkia(this PenLineCap penLineCap)
		{
			switch (penLineCap)
			{
				case PenLineCap.Flat:
					return SKStrokeCap.Butt;
				case PenLineCap.Square:
					return SKStrokeCap.Square;
				case PenLineCap.Round:
					return SKStrokeCap.Round;
				case PenLineCap.Triangle:
					throw new NotSupportedException("Triangle stroke cap is not supported in Skia");
				default:
					throw new ArgumentOutOfRangeException(nameof(penLineCap), penLineCap, null);
			}
		}
	}
}
