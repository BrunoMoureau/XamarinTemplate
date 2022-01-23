using System;
using Xamarin.Basics.Mvvm.Navigations;
using Xamarin.Basics.Mvvm.Views;
using Xamarin.Forms;

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
    }
}