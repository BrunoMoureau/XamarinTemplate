using System.Linq;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Navigations.Services;
using Xamarin.Forms;

namespace XamarinTemplate.Navigations
{
    public class CurrentNavigation : ICurrentNavigation
    {
        public IView[] GetViews()
        {
            var mainPage = Application.Current.MainPage;
            return mainPage switch
            {
                ContentPage contentPage => new[] {contentPage as IView},
                NavigationPage navigationPage => navigationPage.Navigation.NavigationStack?.OfType<IView>().ToArray(),
                _ => new IView[0]
            };
        }

        public void SetRootView<TView>(TView view) where TView : IRootView
            => Application.Current.MainPage = view as Page;

        public void SetStackView<TView>(TView view) where TView : IStackView
        {
            var page = view as Page;
            var navigationPage = new NavigationPage(page);
            NavigationPage.SetHasNavigationBar(page, view.HasNavigationBar);

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
            return Application.Current.MainPage.Navigation.PushAsync(page, animated);
        }

        public Task PushModalViewAsync<TView>(TView view, bool animated) where TView : IModalView
            => Application.Current.MainPage.Navigation.PushModalAsync(view as Page, animated);

        public Task PopViewAsync(bool animated) => Application.Current.MainPage.Navigation.PopAsync(animated);
        public Task PopModalViewAsync(bool animated) => Application.Current.MainPage.Navigation.PopModalAsync(animated);
        public Task PopAllAsync(bool animated) => Application.Current.MainPage.Navigation.PopToRootAsync(animated);
        public bool HasModalView() => Application.Current.MainPage.Navigation?.ModalStack?.Any() ?? false;
    }
}