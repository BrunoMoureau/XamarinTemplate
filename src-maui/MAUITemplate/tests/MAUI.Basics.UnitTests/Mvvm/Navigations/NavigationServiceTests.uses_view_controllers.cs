using MAUI.Basics.Mvvm.Navigations;
using MAUI.Basics.Mvvm.Navigations.Controllers;
using MAUI.Basics.Mvvm.Navigations.Controllers.Interfaces;
using MAUI.Basics.Mvvm.Navigations.Factories;
using MAUI.Basics.Mvvm.Navigations.Services;
using MAUI.Basics.Mvvm.Views;
using MAUI.Basics.UnitTests.Helpers.Statics;
using Moq;
using Xunit;

namespace MAUI.Basics.UnitTests.Mvvm.Navigations
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
            navigationController.Verify(m => m.SetController(It.IsAny<RootViewController>()), Times.Once);
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
            navigationController.Verify(m => m.SetController(It.IsAny<StackViewController>()), Times.Once);
        }
    }
}