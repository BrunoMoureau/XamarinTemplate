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
        public void Can_Push_StackView()
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
            navigationService.PushAsync<IStackView>();

            // Assert
            viewFactory.Verify(m => m.Create<IStackView>(), Times.Once);
            appNavigationService.Verify(m => m.PushViewAsync(stackView.Object, true), Times.Once);
        }
        
        [Fact]
        public void Can_Push_ModalView()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;
            Mock<INavigationController> navigationController = A.NavigationController;

            Mock<IModalView> modalView = A.ModalView;
            Mock<IViewFactory> viewFactory = A.ViewFactory
                .Calling(m => m.Create<IModalView>())
                .Returns(modalView);

            var navigationService = new NavigationService(
                appNavigationService.Object, 
                navigationController.Object,
                viewFactory.Object);

            // Act
            navigationService.PushModalAsync<IModalView>();

            // Assert
            viewFactory.Verify(m => m.Create<IModalView>(), Times.Once);
            appNavigationService.Verify(m => m.PushModalViewAsync(modalView.Object, true), Times.Once);
        }
    }
}