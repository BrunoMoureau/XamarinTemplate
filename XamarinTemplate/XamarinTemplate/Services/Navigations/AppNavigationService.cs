using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Forms;

namespace XamarinTemplate.Services.Navigations
{
    public class AppNavigationService : IAppNavigationService
    {
        private readonly INavigationCallback _navigationCallback;

        public AppNavigationService(INavigationCallback navigationCallback)
        {
            _navigationCallback = navigationCallback;
        }
        
        public void SetRootView<TView>(TView view) where TView : IRootView
        {
            Application.Current.MainPage = view as Page;
        }

        public void SetStackRootView<TView>(TView view) where TView : IStackView
        {
            var page = view as Page;
            var navigationPage = new NavigationPage(page);
            NavigationPage.SetHasNavigationBar(page, view.HasNavigationBar);

            Application.Current.MainPage = navigationPage;
        }

        public Task PushViewAsync<TView>(TView view, bool animated = true) where TView : IStackView
        {
            var page = view as Page;
            NavigationPage.SetHasNavigationBar(page, view.HasNavigationBar);
            return Application.Current.MainPage.Navigation?.PushAsync(page, animated);
        }

        public Task PushModalViewAsync<TView>(TView view, bool animated) where TView : IModalView
            => Application.Current.MainPage.Navigation?.PushModalAsync(view as Page, animated);

        public Task PopViewAsync<TView>(TView view, bool animated) where TView : IStackView
            => Application.Current.MainPage.Navigation?.PopAsync(animated);

        public Task PopModalViewAsync<TView>(TView view, bool animated) where TView : IModalView =>
            Application.Current.MainPage.Navigation?.PopModalAsync(animated);

        public void OnRootViewChanging()
        {
            var mainPage = Application.Current.MainPage;
            
            if (mainPage is NavigationPage navigationPage)
            {
                navigationPage.Pushed -= Pushed;
                navigationPage.Popped -= Popped;

                Application.Current.ModalPushed -= ModalPushed;
                Application.Current.ModalPopped -= ModalPopped;
            }
        }

        public void OnRootViewChanged()
        {
            var mainPage = Application.Current.MainPage;

            if (mainPage is NavigationPage navigationPage)
            {
                navigationPage.Pushed += Pushed;
                navigationPage.Popped += Popped;

                Application.Current.ModalPushed += ModalPushed;
                Application.Current.ModalPopped += ModalPopped;
            }
        }

        private void Pushed(object sender, NavigationEventArgs e)
        {
            var stackView = e.Page as IStackView;
            _navigationCallback.OnViewPushed(stackView);
        }

        private void Popped(object sender, NavigationEventArgs e)
        {
            var stackView = e.Page as IStackView;
            _navigationCallback.OnViewPopped(stackView);
        }

        private void ModalPushed(object sender, ModalPushedEventArgs e)
        {
            var modalView = e.Modal as IModalView;
            _navigationCallback.OnModalViewPushed(modalView);
        }

        private void ModalPopped(object sender, ModalPoppedEventArgs e)
        {
            var modalView = e.Modal as IModalView;
            _navigationCallback.OnModalViewPopped(modalView);
        }
    }
}