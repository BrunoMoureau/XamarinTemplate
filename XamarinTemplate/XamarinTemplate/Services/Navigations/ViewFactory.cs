using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Views;
using XamarinTemplate.Services.Containers;

namespace XamarinTemplate.Services.Navigations
{
    public class ViewFactory : IViewFactory
    {
        private readonly IMyContainer _myContainer;

        public ViewFactory(IMyContainer myContainer)
        {
            _myContainer = myContainer;
        }
        
        public TView Create<TView>() where TView : IView => _myContainer.Resolve<TView>();
    }
}