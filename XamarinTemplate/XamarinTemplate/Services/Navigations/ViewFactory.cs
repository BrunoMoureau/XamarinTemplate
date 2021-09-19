using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Navigations.Factories;
using XamarinTemplate.Services.Containers;

namespace XamarinTemplate.Services.Navigations
{
    public class ViewFactory : IViewFactory
    {
        public TView Create<TView>() where TView : IView => ContainerService.Resolve<TView>();
    }
}