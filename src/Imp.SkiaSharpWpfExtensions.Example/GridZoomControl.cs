using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using SkiaSharp;
using SkiaSharp.WpfExtensions;

namespace Imp.SkiaSharpWpfExtensions.Example
{
	// Based on http://beesandbombs.tumblr.com/post/98090159954/grid-zoom by Dave Whyte

	class GridZoomControl : SkiaControl
	{
		private const int Fps = 60;
		private const int LengthSec = 4;
		private float _frame;

		private readonly DispatcherTimer _timer = new DispatcherTimer();

		static GridZoomControl()
		{
			CanvasClearProperty.OverrideMetadata(typeof(GridZoomControl), 
				new FrameworkPropertyMetadata(new SolidColorBrush(new Color { R = 32, G = 32, B = 32, A = 255 })));
		}

		public GridZoomControl()
		{
			_timer.Tick += (o, args) => InvalidateVisual();
			_timer.Interval = TimeSpan.FromMilliseconds(1000d / Fps);
			_timer.Start();
		}


		[Category("Brush")]
		public SolidColorBrush Foreground
		{
			get { return (SolidColorBrush)GetValue(ForegroundProperty); }
			set { SetValue(ForegroundProperty, value); }
		}

		public static readonly DependencyProperty ForegroundProperty =
			DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(GridZoomControl), 
				new PropertyMetadata(new SolidColorBrush(new Color { R = 248, G = 248, B = 248, A = 255 })));


		[Category("Appearance")]
		public double StrokeWidth
		{
			get { return (double)GetValue(StrokeWidthProperty); }
			set { SetValue(StrokeWidthProperty, value); }
		}

		public static readonly DependencyProperty StrokeWidthProperty =
			DependencyProperty.Register("StrokeWidth", typeof(double), typeof(GridZoomControl), new PropertyMetadata(1.5d));






		protected override void Draw(SKCanvas canvas, int width, int height)
		{
			float time = _frame / (LengthSec * Fps);

			using (var paint = new SKPaint())
			{
				paint.Color = Foreground.ToSkia();
				paint.StrokeWidth = (float)StrokeWidth;
				paint.IsAntialias = true;
				paint.IsStroke = true;

				for (int a = 0; a < 3; ++a)
				{
					var matrix = SKMatrix.MakeRotation(2 * (float)Math.PI * time / 6 + 2 * (float)Math.PI * a / 3);
					matrix.TransX = width / 2f;
					matrix.TransY = height / 2f;

					canvas.SetMatrix(matrix);

					const int n = 12;
					const int sp = 39;

					for (int i = -n; i <= n; ++i)
					{
						float y = (float)(i * sp * Math.Pow(2, time));
						float tt = (float)Math.Min(1, Math.Max(0, 1.09 * time - 0.00275 * Math.Abs(y) + 0.075));

						float x;

						if (i % 2 == 0)
							x = width;
						else
							x = width * tt;

						if (x > 0)
							canvas.DrawLine(-x, y, x, y, paint);
					}
				}
			}

			++_frame;
			if (_frame > LengthSec * Fps)
				_frame = 0;
		}
	}
}
