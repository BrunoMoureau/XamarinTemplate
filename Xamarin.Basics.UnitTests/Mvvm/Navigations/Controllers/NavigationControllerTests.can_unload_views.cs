using Moq;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.Navigations.Controllers;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Factories;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Tests.Helpers.Statics;
using Xunit;

namespace Xamarin.Basics.Tests.Mvvm.Navigations.Controllers
{
    public partial class NavigationControllerTests
    {
        [Fact]
        public void Can_Unload_Popped_StackView()
        {
            // Arrange
            Mock<IStackView> rootStackView = A.StackView;
            Mock<IStackView> stackView = A.StackView;

            StackViewController stackViewController = new (rootStackView.Object);
            Mock<IViewControllerFactory> viewControllerFactory = A.ViewControllerFactory
                .Calling(m => m.CreateController(It.IsAny<IStackView>()))
                .Returns(stackViewController);

            var controller = new NavigationController(viewControllerFactory.Object);
            controller.UseStackRootViewController(rootStackView.Object);

            // Act
            controller.OnViewPopped(stackView.Object);

            // Assert
            stackView.Verify(m => m.Unload(), Times.Once);
        }
        
        [Fact]
        public void Can_Unload_Popped_ModalView()
        {
            // Arrange
            Mock<IStackView> rootStackView = A.StackView;
            Mock<IModalView> modalView = A.ModalView;

            StackViewController stackViewController = new (rootStackView.Object);
            Mock<IViewControllerFactory> viewControllerFactory = A.ViewControllerFactory
                .Calling(m => m.CreateController(rootStackView.Object))
                .Returns(stackViewController);

            var controller = new NavigationController(viewControllerFactory.Object);
            controller.UseStackRootViewController(rootStackView.Object);

            // Act
            controller.OnModalViewPopped(modalView.Object);

            // Assert
            modalView.Verify(m => m.Unload(), Times.Once);
        }
    }
}