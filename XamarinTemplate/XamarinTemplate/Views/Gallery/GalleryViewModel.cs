using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Basics.CancellationToken;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Api;
using XamarinTemplate.Views.Gallery.Models;

namespace XamarinTemplate.Views.Gallery
{
    public class GalleryViewModel : ObservableObject, IViewModel
    {
        private readonly IPhotoService _photoService;
        private CancellationTokenSource _getPhotosCancellationTokenSource;

        private List<Photo> _photos;

        public List<Photo> Photos
        {
            get => _photos;
            set => SetProperty(ref _photos, value);
        }

        public GalleryViewModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task InitializeAsync(object @params)
        {
            CancellationTokenHelper.GenerateTokenSource(ref _getPhotosCancellationTokenSource);
            var photos = await Task.Run(() => _photoService.GetPhotosAsync(_getPhotosCancellationTokenSource.Token),
                _getPhotosCancellationTokenSource.Token);

            Photos = photos;
        }

        public void Unload()
        {
            CancellationTokenHelper.CancelTokenSource(_getPhotosCancellationTokenSource);
        }
    }
}