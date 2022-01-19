using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Views;
using XamarinTemplate.Services.Containers;

namespace XamarinTemplate.Services.Navigations
{
    public class ViewFactory : IViewFactory
    {
        private readonly IAppContainer _appContainer;

        public ViewFactory(IAppContainer appContainer)
        {
            _appContainer = appContainer;
        }
        
        public TView Create<TView>() where TView : IView => _appContainer.Resolve<TView>();
    }
}