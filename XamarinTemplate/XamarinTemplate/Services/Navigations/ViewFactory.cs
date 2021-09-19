using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Views;
using XamarinTemplate.Services.Containers;

namespace XamarinTemplate.Services.Navigations
{
    public class ViewFactory : IViewFactory
    {
        public TView Create<TView>() where TView : IView => ContainerService.Resolve<TView>();
    }
}