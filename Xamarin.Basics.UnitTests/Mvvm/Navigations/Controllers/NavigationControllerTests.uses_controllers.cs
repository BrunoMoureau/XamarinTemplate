using Moq;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.Navigations.Controllers.Factories;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Tests.Helpers.Statics;
using Xunit;

namespace Xamarin.Basics.Tests.Mvvm.Navigations.Controllers
{
    public partial class NavigationControllerTests
    {
        [Fact]
        public void Uses_RootController_With_RootView()
        {
            // Arrange
            Mock<IRootView> rootView = A.RootView;

            Mock<IViewControllerFactory> viewControllerFactory = A.ViewControllerFactory;

            var controller = new NavigationController(viewControllerFactory.Object);

            // Act
            controller.UseRootViewController(rootView.Object);

            // Assert
            viewControllerFactory.Verify(m => m.CreateController(rootView.Object), Times.Once);
        }
        
        [Fact]
        public void Uses_StackRootController_With_StackView()
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