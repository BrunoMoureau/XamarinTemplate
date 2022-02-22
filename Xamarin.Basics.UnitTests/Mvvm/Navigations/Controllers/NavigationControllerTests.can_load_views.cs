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
        public void Can_Load_RootView()
        {
            // Arrange
            Mock<IRootView> rootView = A.RootView;
            Mock<IViewControllerFactory> viewControllerFactory = A.ViewControllerFactory;

            var controller = new NavigationController(viewControllerFactory.Object);

            // Act
            controller.UseRootViewController(rootView.Object);

            // Assert
            rootView.Verify(m => m.Load(), Times.Once);
        }
        
        [Fact]
        public void Can_Load_StackRootView()
        {
            // Arrange
            Mock<IStackView> rootStackView = A.StackView;
            Mock<IViewControllerFactory> viewControllerFactory = A.ViewControllerFactory;
            
            var controller = new NavigationController(viewControllerFactory.Object);

            // Act
            controller.UseStackRootViewController(rootStackView.Object);

            // Assert
            rootStackView.Verify(m => m.Load(), Times.Once);
        }
        
        [Fact]
        public void Can_Load_Pushed_StackView()
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
            controller.OnViewPushed(stackView.Object);

            // Assert
            stackView.Verify(m => m.Load(), Times.Once);
        }
        
        [Fact]
        public void Can_Load_Pushed_ModalView()
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
            controller.OnModalViewPushed(modalView.Object);

            // Assert
            modalView.Verify(m => m.Load(), Times.Once);
        }
        
        [Fact]
        public void Uses_StackRootController()
        {
            // Arrange
            Mock<IStackView> rootStackView = A.StackView;

            Mock<IViewControllerFactory> viewControllerFactory = A.ViewControllerFactory;

            var controller = new NavigationController(viewControllerFactory.Object);

            // Act
            controller.UseStackRootViewController(rootStackView.Object);

            // Assert
            viewControllerFactory.Verify(m => m.CreateController(rootStackView.Object), Times.Once);
        }
    }
}