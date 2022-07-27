using MAUI.Basics.Mvvm.Navigations.Services;
using MAUI.Basics.UnitTests.Helpers.Builders;

namespace MAUI.Basics.UnitTests.Helpers.Statics
{
    public static class An
    {
        public static MockBuilder<IAppNavigationService> AppNavigationService => new();
    }
}