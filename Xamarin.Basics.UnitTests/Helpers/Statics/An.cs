using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Tests.Helpers.Builders;

namespace Xamarin.Basics.Tests.Helpers.Statics
{
    public static class An
    {
        public static MockBuilder<IAppNavigationService> AppNavigationService => new();
    }
}