using MAUI.Basics.Mvvm.Navigations;
using MAUI.Basics.Mvvm.Navigations.Controllers.Interfaces;
using MAUI.Basics.Mvvm.Navigations.Factories;
using MAUI.Basics.Mvvm.Navigations.Services;
using MAUI.Basics.Mvvm.ViewModels;
using MAUI.Basics.Mvvm.Views;
using MAUI.Basics.UnitTests.Helpers.Statics;
using Moq;
using Xunit;

namespace MAUI.Basics.UnitTests.Mvvm.Navigations
{
    public partial class NavigationServiceTests
    {
        [Fact]
        public void Initializes_ViewModel_On_RootView_Set()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;
            Mock<INavigationController> navigationController = A.NavigationController;

            Mock<IViewModel<object>> viewModel = A.ViewModel;
            Mock<IRootView> rootView = A.RootView
                .Calling(m => m.ViewModel)
                .Returns(viewModel);

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
            viewModel.Verify(m => m.InitializeAsync(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public void Initializes_ViewModel_On_StackRootView_Set()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;
            Mock<INavigationController> navigationController = A.NavigationController;

            Mock<IViewModel<object>> viewModel = A.ViewModel;
            Mock<IStackView> stackView = A.StackView
                .Calling(m => m.ViewModel)
                .Returns(viewModel);

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
            viewModel.Verify(m => m.InitializeAsync(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public void Initializes_ViewModel_On_StackView_Pushed()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;
            Mock<INavigationController> navigationController = A.NavigationController;

            Mock<IViewModel<object>> viewModel = A.ViewModel;
            Mock<IStackView> stackView = A.StackView
                .Calling(m => m.ViewModel)
                .Returns(viewModel);

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
            viewModel.Verify(m => m.InitializeAsync(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public void Initializes_ViewModel_On_ModalView_Pushed()
        {
            // Arrange
            Mock<IAppNavigationService> appNavigationService = An.AppNavigationService;
            Mock<INavigationController> navigationController = A.NavigationController;

            Mock<IViewModel<object>> viewModel = A.ViewModel;
            Mock<IModalView> modalView = A.ModalView
                .Calling(m => m.ViewModel)
                .Returns(viewModel);

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
            viewModel.Verify(m => m.InitializeAsync(It.IsAny<object>()), Times.Once);
        }
    }
}