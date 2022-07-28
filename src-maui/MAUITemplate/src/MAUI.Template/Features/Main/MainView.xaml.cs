using MAUI.Basics.Mvvm.Views;

namespace MAUI.Template.Features.Main
{
    public partial class MainView : IStackView
    {
        public bool HasNavigationBar => true;

        public MainView(MainViewModel viewModel)
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