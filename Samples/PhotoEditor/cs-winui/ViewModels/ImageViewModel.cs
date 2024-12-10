using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using PhotoEditor.Models;
using PhotoEditor.Services;

namespace PhotoEditor.ViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        private readonly ImageService _imageService;

        public ImageFileInfo ImageInfo { get; }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private BitmapImage _thumbnail;
        public BitmapImage Thumbnail
        {
            get => _thumbnail;
            set => SetProperty(ref _thumbnail, value);
        }

        public ImageViewModel(ImageFileInfo imageInfo, ImageService imageService)
        {
            ImageInfo = imageInfo ?? throw new ArgumentNullException(nameof(imageInfo));
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        }

        public async Task LoadImageAsync()
        {
            ImageSource = await _imageService.GetImageSourceAsync(ImageInfo.ImageFile);
            Thumbnail = await _imageService.GetImageThumbnailAsync(ImageInfo.ImageFile);
        }

        public async Task SaveImagePropertiesAsync()
        {
            await _imageService.SaveImagePropertiesAsync(
                ImageInfo.ImageProperties,
                ImageInfo.ImageTitle,
                (uint)ImageInfo.ImageRating
            );
        }
    }
}
