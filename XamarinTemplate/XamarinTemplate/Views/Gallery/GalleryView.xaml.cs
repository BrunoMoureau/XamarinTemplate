using Xamarin.Basics.Mvvm.Views;

namespace XamarinTemplate.Views.Gallery
{
    public partial class GalleryView : IStackView
    {
        public bool HasNavigationBar => true;

        public GalleryView(GalleryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}