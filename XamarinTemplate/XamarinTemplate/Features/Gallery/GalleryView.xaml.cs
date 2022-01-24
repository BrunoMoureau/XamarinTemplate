using Xamarin.Basics.Mvvm.Views;

namespace XamarinTemplate.Features.Gallery
{
    public partial class GalleryView : IStackView
    {
        public bool HasNavigationBar => true;

        public GalleryView(GalleryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        
        public void Load()
        {
        }

        public void Unload()
        {
        }
    }
}