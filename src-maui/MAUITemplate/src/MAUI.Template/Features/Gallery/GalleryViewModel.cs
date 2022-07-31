using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Basics.Extensions.Tasks;
using MAUI.Basics.Mvvm.Collections;
using MAUI.Basics.Mvvm.ViewModels;
using MAUI.Basics.Services.Loggers;
using MAUI.Basics.Services.Toasts;
using MAUI.Template.Abstractions.Photos;
using MAUI.Template.Abstractions.Photos.Models;
using MAUI.Template.Api.Filters;
using MAUI.Template.Resources.Languages;

namespace MAUI.Template.Features.Gallery
{
    public class GalleryViewModel : ObservableObject, IViewModel
    {
        private readonly IPhotoService _photoService;
        private readonly IToastService _toastService;
        private readonly ILoggerService _loggerService;

        public ObservableRangeCollection<Photo> Photos { get; } = new();

        public IAsyncRelayCommand RefreshCommand { get; }

        public GalleryViewModel(IPhotoService photoService, IToastService toastService, ILoggerService loggerService)
        {
            _photoService = photoService;
            _toastService = toastService;
            _loggerService = loggerService;

            RefreshCommand = new AsyncRelayCommand(LoadGalleryAsync, () => RefreshCommand.IsRunning == false);
        }

        public void Load()
        {
        }

        public Task InitializeAsync(object @params)
        {
            return RefreshCommand.ExecuteAsync(null);
        }

        private async Task LoadGalleryAsync(CancellationToken cancellationToken)
        {
            try
            {
                var photos = await Task.Run(() => 
                    _photoService.GetPhotosAsync(cancellationToken),
                    cancellationToken);
                
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
        }

        public void Unload()
        {
            RefreshCommand.Cancel();
        }
    }
}