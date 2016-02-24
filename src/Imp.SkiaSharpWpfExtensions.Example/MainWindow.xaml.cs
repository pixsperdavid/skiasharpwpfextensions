using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Imp.SkiaSharpWpfExtensions.Example
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			SnapshotButton.Click += snapshotClicked;
		}

		private void snapshotClicked(object sender, RoutedEventArgs e)
		{
			e.Handled = true;

			var imageSource = RandomRectsControl.SnapshotToBitmapSource();

			if (imageSource == null)
			{
				MessageBox.Show("Failed to snapshot image");
				return;
			}

			var dialog = new SaveFileDialog
			{
				FileName = "Snapshot.png", Filter = "PNG File (*.png)|*.png"
			};

			if (dialog.ShowDialog(this) == true)
			{
				using (var fs = new FileStream(dialog.FileName, FileMode.Create))
				{
					var encoder = new PngBitmapEncoder();
					encoder.Frames.Add(BitmapFrame.Create(imageSource));
					encoder.Save(fs);
				}
			}
		}
	}
}
