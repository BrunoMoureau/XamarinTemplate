using Moq;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Interfaces;
using Xamarin.Basics.Mvvm.Navigations.Factories;
using Xamarin.Basics.Mvvm.Navigations.Services;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Tests.Helpers.Statics;
using Xunit;

namespace Xamarin.Basics.Tests.Mvvm.Navigations
{
    public partial class NavigationServiceTests
    {
        [Fact]
        public void Uses_RootViewController_On_RootView_Set()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;
            Mock<INavigationController> navigationController = A.NavigationController;

            Mock<IRootView> rootView = A.RootView;
            Mock<IViewFactory> viewFactory = A.ViewFactory
                .Calling(m => m.Create<IRootView>())
                .Returns(rootView);

            var navigationService = new NavigationService(
                appNavigationService.Object,
                navigationController.Object,
                viewFactory.Object);

            // Act
            navigationService.SetRootAsync<IRootView>();

            // Assert
            navigationController.Verify(m => m.UseRootViewController(rootView.Object), Times.Once);
        }

        [Fact]
        public void Uses_StackViewController_On_RootView_Set()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;
            Mock<INavigationController> navigationController = A.NavigationController;

            Mock<IStackView> stackView = A.StackView;
            Mock<IViewFactory> viewFactory = A.ViewFactory
                .Calling(m => m.Create<IStackView>())
                .Returns(stackView);

            var navigationService = new NavigationService(
                appNavigationService.Object,
                navigationController.Object,
                viewFactory.Object);

            // Act
            navigationService.SetStackRootAsync<IStackView>();

            // Assert
            navigationController.Verify(m => m.UseStackRootViewController(stackView.Object), Times.Once);
        }
    }
}