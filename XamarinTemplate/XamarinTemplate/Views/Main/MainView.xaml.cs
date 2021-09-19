using Xamarin.Basics.Mvvm.Views;

namespace XamarinTemplate.Views.Main
{
    public partial class MainView : IStackView
    {
        public bool HasNavigationBar => true;

        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}