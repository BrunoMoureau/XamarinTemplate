using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Abstractions.Photos;
using XamarinTemplate.Abstractions.Photos.Models;

namespace XamarinTemplate.Features.Gallery
{
    public class GalleryViewModel : ObservableObject, IViewModel
    {
        private readonly IPhotoService _photoService;
        private readonly BackgroundService _backgroundService = new();

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
        
        public void Load()
        {
        }
        
        public async Task InitializeAsync(object @params)
        {
            try
            {
                var photos = await _backgroundService.CallAsync(c => _photoService.GetPhotosAsync(c));
                Photos = photos;
            }
            catch (OperationCanceledException)
            {
            }
        }
        
        public void Unload()
        {
            _backgroundService.Cancel();
        }
    }
}