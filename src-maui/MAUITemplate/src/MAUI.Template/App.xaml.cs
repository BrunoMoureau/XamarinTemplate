using MAUI.Basics.Mvvm.Navigations.Interfaces;
using MAUI.Template.Features.Main;

namespace MAUI.Template;

public partial class App
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();
		navigationService.SetStackRootAsync<MainView>();
    }
}
