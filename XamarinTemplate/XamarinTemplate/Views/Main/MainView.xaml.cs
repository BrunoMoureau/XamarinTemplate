using Xamarin.Basics.Mvvm.Contracts.Views;

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