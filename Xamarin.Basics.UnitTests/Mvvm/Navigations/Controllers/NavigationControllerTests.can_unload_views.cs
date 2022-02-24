using Moq;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.Navigations.Controllers;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Basics.Tests.Helpers.Statics;
using Xunit;

namespace Xamarin.Basics.Tests.Mvvm.Navigations.Controllers
{
    public partial class NavigationControllerTests
    {
        [Fact]
        public void Unload_RootViewController_Views_When_Replaced()
        {
            // Arrange
            Mock<IRootView> rootView = A.RootView;
            Mock<IModalView> modalView1 = A.ModalView;
            Mock<IModalView> modalView2 = A.ModalView;
            
            var navigationController = new NavigationController();
            navigationController.SetController(new RootViewController(rootView.Object));
            navigationController.OnModalViewPushed(modalView1.Object);
            navigationController.OnModalViewPushed(modalView2.Object);

            // Act
            navigationController.SetController(new RootViewController(rootView.Object));

            // Assert
            rootView.Verify(m => m.Unload(), Times.Once);
            modalView1.Verify(m => m.Unload(), Times.Once);
            modalView2.Verify(m => m.Unload(), Times.Once);
        }
        
        [Fact]
        public void Unload_StackViewController_Views_When_Replaced()
        {
            // Arrange
            Mock<IStackView> rootView = A.StackView;
            Mock<IStackView> stackView1 = A.StackView;
            Mock<IStackView> stackView2 = A.StackView;
            Mock<IModalView> modalView1 = A.ModalView;
            Mock<IModalView> modalView2 = A.ModalView;
            
            var navigationController = new NavigationController();
            navigationController.SetController(new StackViewController(rootView.Object));
            navigationController.OnViewPushed(stackView1.Object);
            navigationController.OnViewPushed(stackView2.Object);
            navigationController.OnModalViewPushed(modalView1.Object);
            navigationController.OnModalViewPushed(modalView2.Object);

            // Act
            navigationController.SetController(new RootViewController(rootView.Object));

            // Assert
            rootView.Verify(m => m.Unload(), Times.Once);
            stackView1.Verify(m => m.Unload(), Times.Once);
            stackView2.Verify(m => m.Unload(), Times.Once);
            modalView1.Verify(m => m.Unload(), Times.Once);
            modalView2.Verify(m => m.Unload(), Times.Once);
        }
        
        [Fact]
        public void Can_Unload_Popped_StackView()
        {
            // Arrange
            Mock<IStackView> rootStackView = A.StackView;
            Mock<IStackView> stackView = A.StackView;

            var controller = new NavigationController();
            controller.SetController(new RootViewController(rootStackView.Object));

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

            var controller = new NavigationController();
            controller.SetController(new RootViewController(rootStackView.Object));

            // Act
            controller.OnModalViewPopped(modalView.Object);

            // Assert
            modalView.Verify(m => m.Unload(), Times.Once);
        }
    }
}