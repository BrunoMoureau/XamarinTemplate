using MAUI.Basics.Mvvm.Navigations.Factories;
using IView = MAUI.Basics.Mvvm.Views.IView;

namespace MAUI.Template.Services.Navigations
{
    public class ViewFactory : IViewFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public TView Create<TView>() where TView : IView => _serviceProvider.GetService<TView>();
    }
}