using System;
using System.Windows.Threading;
using SkiaSharp;
using SkiaSharp.WpfExtensions;

namespace Imp.SkiaSharpWpfExtensions.Example
{
	class RandomRectsControl : SkiaControl
	{
		const int RectCount = 100;

		private readonly DispatcherTimer _timer = new DispatcherTimer();

		public RandomRectsControl()
		{
			_timer.Tick += (o, args) => InvalidateVisual();
			_timer.Interval = TimeSpan.FromMilliseconds(500d);
			_timer.Start();
		}

		protected override void Draw(SKCanvas canvas)
		{
			var paint = new SKPaint
			{
				Color = SKColors.Blue,
				IsStroke = true
			};

			var rnd = new Random();
			var rects = new SKRect[RectCount];

			for (int n = 0; n < RectCount; ++n)
			{
				rects[n] = SKRect.Create((float)(rnd.NextDouble() * ActualWidth), (float)(rnd.NextDouble() * ActualHeight), 10, 10);
			}

			foreach (var rect in rects)
			{
				canvas.DrawRect(rect, paint);
			}
		}
	}
}
