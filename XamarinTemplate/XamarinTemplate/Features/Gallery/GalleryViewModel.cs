using System;
using System.Threading.Tasks;
using Xamarin.Basics.Extensions.Tasks;
using Xamarin.Basics.Mvvm.ViewModels;
using Xamarin.Basics.Services;
using Xamarin.Basics.Services.Loggers;
using Xamarin.Basics.Services.Messagings;
using Xamarin.Basics.Services.Toasts;
using Xamarin.CommunityToolkit.ObjectModel;
using XamarinTemplate.Abstractions.Photos;
using XamarinTemplate.Abstractions.Photos.Models;
using XamarinTemplate.Api.Filters;
using XamarinTemplate.Resources.Languages;

namespace XamarinTemplate.Features.Gallery
{
    public class PhotoMessage : IMessage
    {
    }

    public class GalleryViewModel : ObservableObject, IViewModel
    {
        private readonly IPhotoService _photoService;
        private readonly IToastService _toastService;
        private readonly ILoggerService _loggerService;
        private readonly BackgroundTask _getPhotosTask = new();

        private bool _isGalleryLoading;

        public bool IsGalleryLoading
        {
            get => _isGalleryLoading;
            set => SetProperty(ref _isGalleryLoading, value);
        }

        public ObservableRangeCollection<Photo> Photos { get; } = new();

        public GalleryViewModel(IPhotoService photoService, IToastService toastService, ILoggerService loggerService)
        {
            _photoService = photoService;
            _toastService = toastService;
            _loggerService = loggerService;
        }

        public void Load()
        {
        }

        public Task InitializeAsync(object @params)
        {
            return LoadGalleryAsync();
        }

        private async Task LoadGalleryAsync()
        {
            IsGalleryLoading = true;

            try
            {
                var photos = await _getPhotosTask.RunAsync(c => _photoService.GetPhotosAsync(c));
                Photos.ReplaceRange(photos);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception) when (HttpExceptionFilter.NoConnection(exception))
            {
                _toastService.ShowAsync(AppResources.Error_Connection_None).FireAndForgetSafeAsync();
            }
            catch (Exception exception) when (HttpExceptionFilter.LostConnection(exception))
            {
                _toastService.ShowAsync(AppResources.Error_Connection_Lost).FireAndForgetSafeAsync();
            }
            catch (Exception exception)
            {
                _loggerService.Log(exception);
                _toastService.ShowAsync(AppResources.Error_Generic).FireAndForgetSafeAsync();
            }
            finally
            {
                IsGalleryLoading = false;
            }
        }

        public void Unload()
        {
            _getPhotosTask.Cancel();
        }
    }
}