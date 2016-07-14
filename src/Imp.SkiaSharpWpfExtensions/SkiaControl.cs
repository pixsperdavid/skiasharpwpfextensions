using System.ComponentModel;
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
	    [CanBeNull] private WriteableBitmap _bitmap;
		private SKColor _canvasClearColor;

		/// <summary>
		///     Constructor for abstract base
		/// </summary>
		protected SkiaControl()
		{
			cacheCanvasClearColor();
			createBitmap();
			SizeChanged += (o, args) => createBitmap();
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

		/// <summary>
		///		Static dependency property for <see cref="CanvasClear"/>
		/// </summary>
		public static readonly DependencyProperty CanvasClearProperty =
			DependencyProperty.Register("CanvasClear", typeof(SolidColorBrush), typeof(SkiaControl),
				new PropertyMetadata(new SolidColorBrush(Colors.Transparent), canvasClearPropertyChanged));

		private static void canvasClearPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
		{
			((SkiaControl)o).cacheCanvasClearColor();
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

		/// <summary>
		///		Static dependency property for <see cref="IsClearCanvas"/>
		/// </summary>
		public static readonly DependencyProperty IsClearCanvasProperty =
			DependencyProperty.Register("IsClearCanvas", typeof(bool), typeof(SkiaControl), new PropertyMetadata(true));

		/// <summary>
		///     Capture the most recent control render to an image
		/// </summary>
		/// <returns>An <see cref="ImageSource" /> containing the captured area</returns>
		[CanBeNull]
		public BitmapSource SnapshotToBitmapSource() => _bitmap?.Clone();

		/// <summary>
		///     When overridden in a derived class, participates in rendering operations that are directed by the layout
		///     system. The rendering instructions for this element are not used directly when this method is invoked, and are
		///     instead preserved for later asynchronous use by layout and drawing.
		/// </summary>
		/// <param name="drawingContext">
		///     The drawing instructions for a specific element. This context is provided to the layout
		///     system.
		/// </param>
		protected override void OnRender(DrawingContext drawingContext)
		{
			if (_bitmap == null)
				return;

			_bitmap.Lock();

			using (var surface = SKSurface.Create((int)_bitmap.Width, (int)_bitmap.Height,
				SKColorType.N_32, SKAlphaType.Premul, _bitmap.BackBuffer, _bitmap.BackBufferStride))
			{
				if (IsClearCanvas)
					surface.Canvas.Clear(_canvasClearColor);

				Draw(surface.Canvas, (int)_bitmap.Width, (int)_bitmap.Height);
			}

			_bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)_bitmap.Width, (int)_bitmap.Height));
			_bitmap.Unlock();

			drawingContext.DrawImage(_bitmap, new Rect(0, 0, ActualWidth, ActualHeight));
		}

		/// <summary>
		///     Override this method to implement the drawing routine for the control
		/// </summary>
		/// <param name="canvas">The Skia canvas</param>
		/// <param name="width">Canvas width</param>
		/// <param name="height">Canvas height</param>
		protected abstract void Draw([NotNull] SKCanvas canvas, int width, int height);

		private void createBitmap()
		{
			int width = (int)ActualWidth;
			int height = (int)ActualHeight;

			if (height > 0 && width > 0 && Parent != null)
				_bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Pbgra32, null);
			else
				_bitmap = null;
		}

		private void cacheCanvasClearColor()
		{
			_canvasClearColor = CanvasClear.ToSkia();
		}
	}
}