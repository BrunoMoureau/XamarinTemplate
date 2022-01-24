using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Forms;

namespace XamarinTemplate.Services.Navigations
{
    public class AppNavigationService : IAppNavigationService
    {
        public event EventHandler<IView> ViewAdded;
        public event EventHandler<IView> ViewRemoved;

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
        {
            OnMainPageChanging(Application.Current.MainPage);
            Application.Current.MainPage = view as Page;
            OnMainPageChanged(Application.Current.MainPage);
        }

        public void SetStackView<TView>(TView view) where TView : IStackView
        {
            var page = view as Page;
            var navigationPage = new NavigationPage(page);
            NavigationPage.SetHasNavigationBar(page, view.HasNavigationBar);

            OnMainPageChanging(Application.Current.MainPage);
            Application.Current.MainPage = navigationPage;
            OnMainPageChanged(Application.Current.MainPage);
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

        public Task PopModalViewAsync(bool animated) =>
            Application.Current.MainPage.Navigation?.PopModalAsync(animated);

        public bool HasModalView() => Application.Current.MainPage.Navigation?.ModalStack?.Any() ?? false;

        private void OnMainPageChanging(Page mainPage)
        {
            if (mainPage is NavigationPage navigationPage)
            {
                navigationPage.Pushed -= Pushed;
                navigationPage.Popped -= Popped;

                Application.Current.ModalPushed -= ModalPushed;
                Application.Current.ModalPopped -= ModalPopped;

                var views = mainPage.Navigation?.NavigationStack?.OfType<IView>().ToArray()
                            ?? Array.Empty<IView>();

                foreach (var view in views)
                {
                    ViewRemoved?.Invoke(this, view);
                }

                var modalViews = mainPage.Navigation?.ModalStack?.OfType<IView>().ToArray()
                                 ?? Array.Empty<IView>();

                foreach (var modalView in modalViews)
                {
                    ViewRemoved?.Invoke(this, modalView);
                }
            }

            if (mainPage is IRootView rootView)
            {
                ViewRemoved?.Invoke(this, rootView);
            }
        }

        private void OnMainPageChanged(Page mainPage)
        {
            if (mainPage is NavigationPage navigationPage)
            {
                navigationPage.Pushed += Pushed;
                navigationPage.Popped += Popped;

                Application.Current.ModalPushed += ModalPushed;
                Application.Current.ModalPopped += ModalPopped;

                var rootPage = mainPage.Navigation?.NavigationStack?.FirstOrDefault();
                if (rootPage is IView view)
                    ViewAdded?.Invoke(this, view);
            }

            if (mainPage is IRootView rootView)
            {
                ViewAdded?.Invoke(this, rootView);
            }
        }

        private void Pushed(object sender, NavigationEventArgs e)
        {
            var view = e.Page as IView;
            ViewAdded?.Invoke(this, view);
        }

        private void Popped(object sender, NavigationEventArgs e)
        {
            var view = e.Page as IView;
            ViewRemoved?.Invoke(this, view);
        }

        private void ModalPushed(object sender, ModalPushedEventArgs e)
        {
            var view = e.Modal as IView;
            ViewAdded?.Invoke(this, view);
        }

        private void ModalPopped(object sender, ModalPoppedEventArgs e)
        {
            var view = e.Modal as IView;
            ViewRemoved?.Invoke(this, view);
        }
    }
}