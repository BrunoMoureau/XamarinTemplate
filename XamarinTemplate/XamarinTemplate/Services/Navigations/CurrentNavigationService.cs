using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XamarinTemplate.Services.Navigations
{
    public class CurrentNavigationService : ICurrentNavigationService
    {
        public event EventHandler<IView> ViewPushed;
        public event EventHandler<IView> ViewPopped;

        public IView[] GetViews()
        {
            var mainPage = Application.Current.MainPage;
            return mainPage switch
            {
                ContentPage contentPage => new[] { contentPage as IView },
                NavigationPage navigationPage => navigationPage.Navigation.NavigationStack?.OfType<IView>().ToArray(),
                _ => Array.Empty<IView>()
            };
        }

        public void SetRootView<TView>(TView view) where TView : IRootView
            => Application.Current.MainPage = view as Page;

        public void SetStackView<TView>(TView view) where TView : IStackView
        {
            var page = view as Page;
            var navigationPage = new NavigationPage(page);
            NavigationPage.SetHasNavigationBar(page, view.HasNavigationBar);
            
            //todo unregister this
            navigationPage.Pushed += Pushed;
            navigationPage.Popped += Popped;
            Application.Current.ModalPushed += ModalPushed;
            Application.Current.ModalPopped += ModalPopped;
            Application.Current.MainPage = navigationPage;
        }

        public bool HasRootStackView() => Application.Current.MainPage is NavigationPage;

        public IView GetLastViewOrDefault() =>
            Application.Current.MainPage.Navigation?.NavigationStack?.LastOrDefault() as IView;

        public IView GetLastModalViewOrDefault() =>
            Application.Current.MainPage.Navigation?.ModalStack?.LastOrDefault() as IView;

        public Task PushViewAsync<TView>(TView view, bool animated = true) where TView : IStackView
        {
            var page = view as Page;
            NavigationPage.SetHasNavigationBar(page, view.HasNavigationBar);
            return Application.Current.MainPage.Navigation?.PushAsync(page, animated);
        }

        public Task PushModalViewAsync<TView>(TView view, bool animated) where TView : IModalView
            => Application.Current.MainPage.Navigation?.PushModalAsync(view as Page, animated);

        public Task PopViewAsync(bool animated) => Application.Current.MainPage.Navigation?.PopAsync(animated);
        public Task PopModalViewAsync(bool animated) => Application.Current.MainPage.Navigation?.PopModalAsync(animated);
        public async Task PopAllAsync(bool animated)
        {
            var pageCount = Application.Current.MainPage.Navigation?.NavigationStack.Count;
            for (int i = 0; i < pageCount; i++)
            {
                await Application.Current.MainPage.Navigation?.PopAsync(animated);
            }
            
            var modalCount = Application.Current.MainPage.Navigation?.ModalStack.Count;
            for (int i = 0; i < modalCount; i++)
            {
                Application.Current.MainPage.Navigation?.PopModalAsync(animated);
            }
        }

        public bool HasModalView() => Application.Current.MainPage.Navigation?.ModalStack?.Any() ?? false;
        
        private void Pushed(object sender, NavigationEventArgs e)
        {
            var view = e.Page as IView;
            ViewPushed?.Invoke(this, view);
        }
        
        private void Popped(object sender, NavigationEventArgs e)
        {
            var view = e.Page as IView;
            ViewPopped?.Invoke(this, view);
        }

        private void ModalPushed(object sender, ModalPushedEventArgs e)
        {
            var view = e.Modal as IView;
            ViewPushed?.Invoke(this, view);
        }

        private void ModalPopped(object sender, ModalPoppedEventArgs e)
        {
            var view = e.Modal as IView;
            ViewPopped?.Invoke(this, view);
        }
    }
}