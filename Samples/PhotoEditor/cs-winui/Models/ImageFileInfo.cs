using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace PhotoEditor.Models
{
    public class ImageFileInfo : INotifyPropertyChanged
    {
        private string _imageTitle;
        private uint _imageRating;

        public StorageFile ImageFile { get; }
        public ImageProperties ImageProperties { get; }

        public string ImageName { get; }
        public string ImageFileType { get; }

        public string ImageDimensions => $"{ImageProperties.Width} x {ImageProperties.Height}";

        public string ImageTitle
        {
            get => string.IsNullOrEmpty(_imageTitle) ? ImageName : _imageTitle;
            set
            {
                if (SetProperty(ref _imageTitle, value))
                {
                    ImageProperties.Title = value;
                }
            }
        }

        public uint ImageRating
        {
            get => _imageRating;
            set
            {
                if (SetProperty(ref _imageRating, value))
                {
                    ImageProperties.Rating = (uint)value;
                }
            }
        }

        private float _exposure = 0;
        public float Exposure
        {
            get => _exposure;
            set => SetEditingProperty(ref _exposure, value);
        }

        private float _temperature = 0;
        public float Temperature
        {
            get => _temperature;
            set => SetEditingProperty(ref _temperature, value);
        }

        private float _tint = 0;
        public float Tint
        {
            get => _tint;
            set => SetEditingProperty(ref _tint, value);
        }

        private float _contrast = 0;
        public float Contrast
        {
            get => _contrast;
            set => SetEditingProperty(ref _contrast, value);
        }

        private float _saturation = 1;
        public float Saturation
        {
            get => _saturation;
            set => SetEditingProperty(ref _saturation, value);
        }

        private float _blur = 0;
        public float Blur
        {
            get => _blur;
            set => SetEditingProperty(ref _blur, value);
        }

        private bool _needsSaved = false;
        public bool NeedsSaved
        {
            get => _needsSaved;
            set => SetProperty(ref _needsSaved, value);
        }

        public ImageFileInfo(ImageProperties properties, StorageFile imageFile, string name, string type)
        {
            ImageProperties = properties ?? throw new ArgumentNullException(nameof(properties));
            ImageFile = imageFile ?? throw new ArgumentNullException(nameof(imageFile));
            ImageName = name ?? throw new ArgumentNullException(nameof(name));
            ImageFileType = type ?? throw new ArgumentNullException(nameof(type));

            // Initialize from existing properties
            _imageRating = properties.Rating;
            _imageTitle = properties.Title;
        }

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetEditingProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (SetProperty(ref storage, value, propertyName))
            {
                if (Exposure != 0 || Temperature != 0 || Tint != 0 || Contrast != 0 || Saturation != 1 || Blur != 0)
                {
                    NeedsSaved = true;
                }
                else
                {
                    NeedsSaved = false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
