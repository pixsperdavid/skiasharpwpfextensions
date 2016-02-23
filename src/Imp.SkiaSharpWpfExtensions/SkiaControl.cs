using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;

namespace SkiaSharp.WpfExtensions
{
	/// <summary>
	///     Abstract class used to create WPF controls which are drawn using Skia
	/// </summary>
	[PublicAPI]
	public abstract class SkiaControl : FrameworkElement
	{
		private Bitmap _bitmap;
		private SKColor _canvasClearColor;

		static SkiaControl()
		{
			FocusableProperty.OverrideMetadata(typeof(SkiaControl), new FrameworkPropertyMetadata(true));
		}

		protected SkiaControl()
		{
			recreateBitmap();
			SizeChanged += (o, args) => recreateBitmap();
			Dispatcher.ShutdownStarted += (o, args) => _bitmap?.Dispose();
		}



		/// <summary>
		///     Color used to clear canvas before each call to <see cref="Draw" /> if <see cref="IsClearCanvas" /> is true
		/// </summary>
		[Category("Brush")]
		[Description("Gets or sets a color used to clear canvas before each render if IsClearCanvas is true")]
		public SolidColorBrush CanvasClear
		{
			get { return (SolidColorBrush)GetValue(CanvasClearProperty); }
			set { SetValue(CanvasClearProperty, value); }
		}

		public static readonly DependencyProperty CanvasClearProperty =
			DependencyProperty.Register("CanvasClear", typeof(SolidColorBrush), typeof(SkiaControl),
				new PropertyMetadata(new SolidColorBrush(Colors.Transparent), canvasClearPropertyChanged));

		private static void canvasClearPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
		{
			var control = (SkiaControl)o;
			control._canvasClearColor = control.CanvasClear.ToSkia();
		}

		/// <summary>
		///     When enabled, canvas will be cleared before each call to <see cref="Draw" /> with the value of
		///     <see cref="CanvasClear" />
		/// </summary>
		[Category("Appearance")]
		[Description(
			"Gets or sets a bool to determine if canvas should be cleared before each render with the value of CanvasClear")]
		public bool IsClearCanvas
		{
			get { return (bool)GetValue(IsClearCanvasProperty); }
			set { SetValue(IsClearCanvasProperty, value); }
		}

		public static readonly DependencyProperty IsClearCanvasProperty =
			DependencyProperty.Register("IsClearCanvas", typeof(bool), typeof(SkiaControl), new PropertyMetadata(true));



		protected override void OnRender(DrawingContext dc)
		{
			if (_bitmap == null)
				return;

			var data = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite,
				_bitmap.PixelFormat);
			using (
				var surface = SKSurface.Create(_bitmap.Width, _bitmap.Height, SKColorType.N_32, SKAlphaType.Premul, data.Scan0,
					_bitmap.Width * 4))
			{
				if (IsClearCanvas)
					surface.Canvas.Clear(_canvasClearColor);

				Draw(surface.Canvas);
			}

			var bitmapSource = BitmapSource.Create(data.Width, data.Height, 96, 96, PixelFormats.Pbgra32, null,
				data.Scan0, data.Stride * data.Height, data.Stride);

			_bitmap.UnlockBits(data);

			dc.DrawImage(bitmapSource, new Rect(0, 0, ActualWidth, ActualHeight));
		}

		protected override void OnVisualParentChanged(DependencyObject oldParent)
		{
			recreateBitmap();
		}

		/// <summary>
		///     Override this method to implement the drawing routine for the control
		/// </summary>
		/// <param name="canvas">The Skia canvas</param>
		protected abstract void Draw(SKCanvas canvas);

		private void recreateBitmap()
		{
			_bitmap?.Dispose();

			int width = (int)ActualWidth;
			int height = (int)ActualHeight;

			if (height > 0 && width > 0 && Parent != null)
				_bitmap = new Bitmap((int)ActualWidth, (int)ActualHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
			else
				_bitmap = null;
		}
	}
}