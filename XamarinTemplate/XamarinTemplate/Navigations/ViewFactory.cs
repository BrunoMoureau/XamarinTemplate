using Autofac;
using Xamarin.Basics.Mvvm.Contracts.Views;
using Xamarin.Basics.Navigations.Factories;

namespace XamarinTemplate.Navigations
{
    public class ViewFactory : IViewFactory
    {
        public TView Create<TView>() where TView : IView => App.Container.Resolve<TView>();
    }
}