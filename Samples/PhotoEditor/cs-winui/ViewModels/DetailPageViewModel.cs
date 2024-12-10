using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Microsoft.Graphics.Canvas;
using System;

namespace PhotoEditor
{
    public class DetailPageViewModel : INotifyPropertyChanged
    {
        private LoadedImageBrush _imageEffectsBrush;
        private BitmapImage _imageSource;
        private StorageFile _imageFile;
        private bool _needsSaved;
        private double _imageWidth;
        private double _imageHeight;

        public event PropertyChangedEventHandler PropertyChanged;

        public DetailPageViewModel()
        {
            _imageEffectsBrush = new LoadedImageBrush();
            ResetEffects();
        }

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public double ImageWidth
        {
            get => _imageWidth;
            set
            {
                _imageWidth = value;
                OnPropertyChanged();
            }
        }

        public double ImageHeight
        {
            get => _imageHeight;
            set
            {
                _imageHeight = value;
                OnPropertyChanged();
            }
        }

        public double BlurAmount
        {
            get => _imageEffectsBrush.BlurAmount;
            set
            {
                _imageEffectsBrush.BlurAmount = value;
                OnPropertyChanged();
            }
        }

        public double ContrastAmount
        {
            get => _imageEffectsBrush.ContrastAmount;
            set
            {
                _imageEffectsBrush.ContrastAmount = value;
                OnPropertyChanged();
            }
        }

        public double SaturationAmount
        {
            get => _imageEffectsBrush.SaturationAmount;
            set
            {
                _imageEffectsBrush.SaturationAmount = value;
                OnPropertyChanged();
            }
        }

        public double ExposureAmount
        {
            get => _imageEffectsBrush.ExposureAmount;
            set
            {
                _imageEffectsBrush.ExposureAmount = value;
                OnPropertyChanged();
            }
        }

        public double TintAmount
        {
            get => _imageEffectsBrush.TintAmount;
            set
            {
                _imageEffectsBrush.TintAmount = value;
                OnPropertyChanged();
            }
        }

        public double TemperatureAmount
        {
            get => _imageEffectsBrush.TemperatureAmount;
            set
            {
                _imageEffectsBrush.TemperatureAmount = value;
                OnPropertyChanged();
            }
        }

        public bool NeedsSaved
        {
            get => _needsSaved;
            set
            {
                _needsSaved = value;
                OnPropertyChanged();
            }
        }

        // Initialize view model (e.g., load image, set up brush)
        public async Task InitializeAsync(StorageFile imageFile)
        {
            await LoadImageAsync(imageFile);
            await LoadBrushAsync(imageFile);
        }

        // Load image asynchronously
        public async Task LoadImageAsync(StorageFile imageFile)
        {
            _imageFile = imageFile;
            var stream = await imageFile.OpenReadAsync();
            _imageEffectsBrush.LoadImageFromStream(stream);

            var bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(stream);
            ImageSource = bitmap;

            // Use BitmapDecoder to get image properties (Width & Height)
            var decoder = await BitmapDecoder.CreateAsync(stream);
            ImageWidth = decoder.PixelWidth;
            ImageHeight = decoder.PixelHeight;
        }

        // Load brush asynchronously
        public async Task LoadBrushAsync(StorageFile imageFile)
        {
            var stream = await imageFile.OpenReadAsync();
            _imageEffectsBrush.LoadImageFromStream(stream);
        }

        // Reset image effects to default
        public void ResetEffects()
        {
            BlurAmount = 0;
            ContrastAmount = 0;
            SaturationAmount = 1;
            ExposureAmount = 0;
            TintAmount = 0;
            TemperatureAmount = 0;
            NeedsSaved = false;
        }

        // Save the image to a file
        public async Task SaveImageAsync(StorageFile targetFile)
        {
            if (_imageFile != null)
            {
                var stream = await targetFile.OpenAsync(FileAccessMode.ReadWrite);
                var device = CanvasDevice.GetSharedDevice();
                var renderTarget = new CanvasRenderTarget(device, (float)_imageWidth, (float)_imageHeight, 96);

                using (var ds = renderTarget.CreateDrawingSession())
                {
                    ds.Clear(Microsoft.UI.Colors.Black);
                    ds.DrawImage(_imageEffectsBrush.Image);
                }

                await renderTarget.SaveAsync(stream, CanvasBitmapFileFormat.Jpeg);
                NeedsSaved = false;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
