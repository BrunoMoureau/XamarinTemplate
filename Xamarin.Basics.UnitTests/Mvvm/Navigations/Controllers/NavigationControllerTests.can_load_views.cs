using Moq;
using Xamarin.Basics.Mvvm.Navigations;
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

            var controller = new NavigationController();

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

            var controller = new NavigationController();

            // Act
            controller.UseStackRootViewController(rootStackView.Object);

            // Assert
            rootStackView.Verify(m => m.Load(), Times.Once);
        }
    }
}