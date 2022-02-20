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
        public void Can_Pop_StackView()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;

            Mock<IStackView> stackView = A.StackView;
            Mock<INavigationController> navigationController = A.NavigationController
                .Calling(m => m.GetPoppableView())
                .Returns(stackView);

            Mock<IViewFactory> viewFactory = A.ViewFactory;

            var navigationService = new NavigationService(
                appNavigationService.Object,
                navigationController.Object,
                viewFactory.Object);

            // Act
            navigationService.PopAsync();

            // Assert
            navigationController.Verify(m => m.GetPoppableView(), Times.Once);
            appNavigationService.Verify(m => m.PopViewAsync(stackView.Object, true), Times.Once);
        }

        [Fact]
        public void Can_Pop_ModalView()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;

            Mock<IModalView> modalView = A.ModalView;
            Mock<INavigationController> navigationController = A.NavigationController
                .Calling(m => m.GetPoppableModalView())
                .Returns(modalView);

            Mock<IViewFactory> viewFactory = A.ViewFactory;

            var navigationService = new NavigationService(
                appNavigationService.Object,
                navigationController.Object,
                viewFactory.Object);

            // Act
            navigationService.PopModalAsync();

            // Assert
            navigationController.Verify(m => m.GetPoppableModalView(), Times.Once);
            appNavigationService.Verify(m => m.PopModalViewAsync(modalView.Object, true), Times.Once);
        }
    }
}