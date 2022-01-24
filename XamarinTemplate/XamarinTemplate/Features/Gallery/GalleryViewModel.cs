using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Basics.Helpers;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Abstractions.Photos;
using XamarinTemplate.Abstractions.Photos.Models;

namespace XamarinTemplate.Features.Gallery
{
    public class Service
    {
        private CancellationTokenSource _cancellationTokenSource;

        public Task<TResult> CallAsync<TResult>(Func<CancellationToken, Task<TResult>> func)
        {
            CancellationTokenHelper.GenerateTokenSource(ref _cancellationTokenSource);
            return Task.Run(() => func(_cancellationTokenSource.Token));
        }

        public void Cancel()
        {
            CancellationTokenHelper.CancelTokenSource(_cancellationTokenSource);
        }
    }

    public class GalleryViewModel : ObservableObject, IViewModel
    {
        private readonly IPhotoService _photoService;
        private readonly Service _service = new();

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
                var photos = await _service.CallAsync(c => _photoService.GetPhotosAsync(c));
                Photos = photos;
            }
            catch (OperationCanceledException)
            {
                //todo set this in service (callAsync takes ICancelHandler as FireAndForget one)
            }
        }
        
        public void Unload()
        {
            _service.Cancel();
        }
    }
}