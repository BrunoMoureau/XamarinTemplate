using Xamarin.Basics.Mvvm.Views;

namespace XamarinTemplate.Features.Language
{
    public partial class LanguageView : IStackView
    {
        public bool HasNavigationBar => true;

        public LanguageView(LanguageViewModel viewModel)
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