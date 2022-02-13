using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces
{
    public interface INavigationCallback
    {
        void OnViewPushed(IStackView stackView);
        void OnViewPopped(IStackView stackView);

        void OnModalViewPushed(IModalView modalView);
        void OnModalViewPopped(IModalView modalView);
    }
}