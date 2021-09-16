using System.Diagnostics;
using Xamarin.Basics.Mvvm.Contracts.Views;

namespace XamarinTemplate.Views.List
{
    public partial class ListView : IStackView
    {
        public bool HasNavigationBar => true;

        public ListView(ListViewModel viewModel)
        {
            var t = new Stopwatch();
            t.Start();

            InitializeComponent();
            BindingContext = viewModel;

            t.Stop();
            Debug.WriteLine("time:" + t.ElapsedMilliseconds / 1000M + "s");
        }
    }
}