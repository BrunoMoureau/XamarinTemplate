using MAUI.Basics.Mvvm.Navigations;
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
        public void Can_Set_Root_As_RootView()
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
            viewFactory.Verify(m => m.Create<IRootView>(), Times.Once);
            appNavigationService.Verify(m => m.SetRootView(rootView.Object), Times.Once);
        }
        
        [Fact]
        public void Can_Set_Root_As_StackRootView()
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
            viewFactory.Verify(m => m.Create<IStackView>(), Times.Once);
            appNavigationService.Verify(m => m.SetStackRootView(stackView.Object), Times.Once);
        }
    }
}