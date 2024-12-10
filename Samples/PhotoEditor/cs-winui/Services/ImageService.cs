using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace PhotoEditor.Services
{
    public class ImageService
    {
        public async Task<BitmapImage> GetImageSourceAsync(StorageFile imageFile)
        {
            if (imageFile == null) throw new ArgumentNullException(nameof(imageFile));

            using IRandomAccessStream fileStream = await imageFile.OpenReadAsync();
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(fileStream);
            return bitmapImage;
        }

        public async Task<BitmapImage> GetImageThumbnailAsync(StorageFile imageFile)
        {
            if (imageFile == null) throw new ArgumentNullException(nameof(imageFile));

            using var thumbnail = await imageFile.GetThumbnailAsync(ThumbnailMode.PicturesView);
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(thumbnail);
            return bitmapImage;
        }

        public async Task SaveImagePropertiesAsync(ImageProperties imageProperties, string title, uint rating)
        {
            if (imageProperties == null) throw new ArgumentNullException(nameof(imageProperties));

            imageProperties.Title = title;
            imageProperties.Rating = rating;
            await imageProperties.SavePropertiesAsync();
        }
    }
}
