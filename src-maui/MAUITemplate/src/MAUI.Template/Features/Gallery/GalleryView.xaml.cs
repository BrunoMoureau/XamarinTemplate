using MAUI.Basics.Mvvm.Views;

namespace MAUI.Template.Features.Gallery
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