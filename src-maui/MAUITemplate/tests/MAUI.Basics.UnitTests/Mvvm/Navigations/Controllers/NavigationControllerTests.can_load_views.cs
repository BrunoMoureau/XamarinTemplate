using MAUI.Basics.Mvvm.Navigations;
using MAUI.Basics.Mvvm.Navigations.Controllers;
using MAUI.Basics.Mvvm.Views;
using MAUI.Basics.UnitTests.Helpers.Statics;
using Moq;
using Xunit;

namespace MAUI.Basics.UnitTests.Mvvm.Navigations.Controllers
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
            controller.SetController(new RootViewController(rootView.Object));

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
            controller.SetController(new StackViewController(rootStackView.Object));

            // Assert
            rootStackView.Verify(m => m.Load(), Times.Once);
        }
        
        [Fact]
        public void Can_Load_Pushed_StackView()
        {
            // Arrange
            Mock<IStackView> rootStackView = A.StackView;
            Mock<IStackView> stackView = A.StackView;

            var controller = new NavigationController();
            controller.SetController(new StackViewController(rootStackView.Object));

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

            var controller = new NavigationController();
            controller.SetController(new StackViewController(rootStackView.Object));

            // Act
            controller.OnModalViewPushed(modalView.Object);

            // Assert
            modalView.Verify(m => m.Load(), Times.Once);
        }
    }
}