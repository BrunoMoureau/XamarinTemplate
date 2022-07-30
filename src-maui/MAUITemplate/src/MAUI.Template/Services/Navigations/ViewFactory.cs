using MAUI.Basics.Mvvm.Navigations.Factories;
using MAUI.Template.Services.Containers;
using IView = MAUI.Basics.Mvvm.Views.IView;

namespace MAUI.Template.Services.Navigations
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