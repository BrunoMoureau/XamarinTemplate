using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Services;
using Xamarin.Basics.Services.Messagings;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Abstractions.Photos;
using XamarinTemplate.Abstractions.Photos.Models;

namespace XamarinTemplate.Features.Gallery
{
    public class PhotoMessage : IMessage
    {
    }
    
    public class GalleryViewModel : ObservableObject, IViewModel
    {
        private readonly IPhotoService _photoService;
        private readonly BackgroundService _getPhotos = new();

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
                Photos = await _getPhotos.RunAsync(c => _photoService.GetPhotosAsync(c));
            }
            catch (OperationCanceledException)
            {
            }
        }
        
        public void Unload()
        {
            _getPhotos.Cancel();
        }
    }
}