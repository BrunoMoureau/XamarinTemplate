using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Basics.CancellationToken;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Api;
using XamarinTemplate.Views.List.Models;

namespace XamarinTemplate.Views.List
{
    public class ListViewModel : ObservableObject, IViewModel
    {
        private readonly IPhotoService _photoService;
        private CancellationTokenSource _getPhotosCancellationTokenSource;

        private List<Photo> _photos;

        public List<Photo> Photos
        {
            get => _photos;
            set => SetProperty(ref _photos, value);
        }

        public ListViewModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public void Open()
        {
        }

        public async Task InitializeAsync(object @params)
        {
            CancellationTokenHelper.GenerateTokenSource(ref _getPhotosCancellationTokenSource);
            var photos = await Task.Run(() => _photoService.GetPhotosAsync(_getPhotosCancellationTokenSource.Token),
                _getPhotosCancellationTokenSource.Token);

            Photos = photos;
        }

        public void Close()
        {
            CancellationTokenHelper.CancelTokenSource(_getPhotosCancellationTokenSource);
        }
    }
}