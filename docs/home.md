

# Welcome to Xamarin.Template!
Hi ! 
I wanted to share some of my knowledge about Xamarin.Forms.

The goal of this repository is to give some best practices for your next Xamarin.Forms project.
I hope you will find some parts useful and learn something from them !

## Architecture
Besides Xamarin.Forms projects, the solution contains some other projects :
 -  **Xamarin.Basics** : provides a set of interfaces and tools to use inside your app.
 -  **Xamarin.Abstractions** : contains some business interfaces and domain objects.
 -  **Xamarin.Api** : contains HTTP logics to communicate with a backend server.

Let's dive into the features!

## Features
- API Calls & Retry Policies
- AppSettings & Settings objects
- Background Service
- Dependency Injection
- Languages
- Merged dictionaries
- Messaging
- MVVM & Navigation

## API Calls & Retry Policies
I recommend using [Refit](https://github.com/reactiveui/refit) and [Polly](https://github.com/App-vNext/Polly) packages to manage API calls.

Every HTTP call is located in a service that acts as an abstraction layer between your ViewModels and your API.
In my opinion, ViewModels should not be aware how your data are retrieved or sent.

![alt text](https://github.com/BrunoMoureau/XamarinTemplate/blob/master/docs/images/service_graph.png?raw=true)

## AppSettings & Settings objects


### AppSettings

There are 3 appsettings files inside the Xamarin.Template project.
Each one contains some properties used by different runtime environments.

You should notice changes when running your app in Local, Debug or Release mode. 
This code written inside the Xamarin.Template.csproj file tells your app to use one particular appsettings file depending on your build configuration. 
```
<ItemGroup>  
 <EmbeddedResource Include="appsettings.json" Condition="!Exists('appsettings.json')" />  
 <EmbeddedResource Include="appsettings.$(Configuration).json" Link="appsettings.json" Condition="Exists('appsettings.$(Configuration).json')" />  
 ...
</ItemGroup>
```

Keep in mind that **you should never put sensitive information inside appsettings files!**

### Settings objects

You can retrieve appsettings properties using **AppSettings** class.
See how **EnvironmentSettings** is retrieved and injected in the app container.

```
var appSettings = new AppSettings(_assembly);  
_container.RegisterInstance(appSettings.Get<EnvironmentSettings>("Environment"));
```

## Background service

In a Xamarin app, almost every line of code is executed by the **main thread**. This thread is the only thread responsible to render visual elements on your pages. You don't want it to call services, execute API calls and deserialize responses...

You can use the **BackgroundTask** class to execute background tasks and also cancel them.

```
public class GalleryViewModel : ObservableObject, IViewModel
{
	private readonly BackgroundTask _getPhotosTask = new();
	
	// ...
	
	private async Task LoadGalleryAsync()
	{
		try  
		{
			// 'c' is a CancellationToken here
			var photos = await _getPhotosTask.RunAsync(c => _photoService.GetPhotosAsync(c));  
			Photos.ReplaceRange(photos);  
		}  
		catch (OperationCanceledException)  
		{  
		}
		
		// ...
	}

	public void Unload()  
	{  
		_getPhotosTask.Cancel();  
	}
}
```

## Dependency Injection

I tried to find the fastest and most lightweight **dependency injection container** and end up using [DryIoc](https://github.com/dadhi/DryIoc).

The **DI container** is really important in my opinion to write **clean code**. You should define the way to create types inside your container and it will resolve object dependencies automatically for you.

See how to **initialize** our app container inside **App.xaml.cs** : 
```
var appContainer = new AppContainer();  
appContainer.Initialize();
```

## Languages

I have never found a good way to swap language in app easily until now.
[Xamarin.CommunityToolkit](https://github.com/xamarin/XamarinCommunityToolkit) provides a set of super useful features you need in most apps.

It provides a way to swap language of the whole app (even the pages currently loaded in the navigation !).

Let's see how it is done :

**Use Xamarin.CommunityToolkit.Extensions.TranslateExtension (xct:Translate)**

```
<?xml version="1.0" encoding="utf-8"?>  
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"  
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"  
	xmlns:language="clr-namespace:XamarinTemplate.Features.Language;assembly=XamarinTemplate"  
	xmlns:xct="http://xamarin.com/schemas/2020/toolkit"  
	x:Class="XamarinTemplate.Features.Language.LanguageView"  
	x:DataType="{x:Type language:LanguageViewModel}">  
	
	<StackLayout>  
		<Button Text="{xct:Translate Lang_EN}"  
				Command="{Binding SetCultureCommand}"  
				CommandParameter="EN"/>  

		<Button Text="{xct:Translate Lang_FR}"  
				Command="{Binding SetCultureCommand}"  
				CommandParameter="FR" />  
	</StackLayout>  
</ContentPage>
```

**Set selected culture using ILanguageService.SetCulture(string)**
```
public class LanguageViewModel : ObservableObject, IViewModel  
{  
	private readonly ILanguageService _languageService;
	
	public ICommand SetCultureCommand { get; }  
	
	public LanguageViewModel(ILanguageService languageService) 
	{  
		_languageService = languageService;  
		
		SetCultureCommand = new Command<string>(SetCulture);  
	}

	// ... 
		
	private void SetCulture(string culture) => _languageService.SetCulture(culture);  
}
```
```
public class LanguageService : ILanguageService  
{  
	public CultureInfo CultureInfo => LocalizationResourceManager.Current.CurrentCulture;  
	
	public void Initialize()  
	{  
		LocalizationResourceManager.Current.PropertyChanged += (_, _) =>  
	        AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;  
	        
		LocalizationResourceManager.Current.Init(AppResources.ResourceManager);  
	}  
	
	public void SetCulture(string culture)  
	{  
		LocalizationResourceManager.Current.CurrentCulture = new CultureInfo(culture);  
	} 
}
```

## Merged dictionaries

Have you ever seen an endless **App.xaml with thousand of lines** ? It is almost impossible to not be lost when searching for a specific line or style.

**Merged dictionaries** can be used to join some resource dictionaries together and avoid this nightmare.

```
<?xml version="1.0" encoding="utf-8" ?>  
<Application xmlns="http://xamarin.com/schemas/2014/forms"  
			 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"  
			 x:Class="XamarinTemplate.App">  
      
    <Application.Resources>  
        <!-- Merged dictionaries -->  
		<!-- Be aware, when some files depends on others, they must be declared after them -->  
        <ResourceDictionary>  
            <ResourceDictionary.MergedDictionaries>  
                <ResourceDictionary Source="Resources/AppColors.xaml" />  
                <ResourceDictionary Source="Resources/AppBrushes.xaml" />  
                <ResourceDictionary Source="Resources/AppConverters.xaml" />  
                <ResourceDictionary Source="Resources/AppDimensions.xaml" />  
                <ResourceDictionary Source="Resources/AppFonts.xaml" />  
            </ResourceDictionary.MergedDictionaries>  
        </ResourceDictionary>  
    </Application.Resources>  
</Application>
```

## Messaging

You certainly already heard about **MessagingCenter** from Xamarin.Forms. It is usually used to communicate with two ViewModels or pages by messages.

I built a simple interface around **MessagingCenter** system to make some abstraction with it and also to be more consistant when subscribing and sending messages. 

Notes :

With **MessagingCenter**, you can **subscribe message from object** instead of specific type **to received messages from any source**.

**Define a message with optional properties**
```
public class HelloMessage : IMessage  
{  
}
```

**Subscribe to and unsubscribe from the message**
```
public partial class MessagingView : IStackView, IMessageSubscriber<HelloMessage>  
{  
	private readonly IMessageService _messageService;
	private readonly IAlertService _alertService;
	
	public bool HasNavigationBar => true;  
	
	public MessagingView(MessagingViewModel viewModel, IMessageService messageService, IAlertService alertService)  
	{  
		_messageService = messageService;  
		_alertService = alertService;  

		InitializeComponent();  
		BindingContext = viewModel;  
	}  
	
	public void Load()  
	{  
		_messageService.Subscribe<HelloMessage>(this);  
	}  
	
	public void Unload()  
	{  
		_messageService.Unsubscribe<HelloMessage>(this);  
	} 
	 
	public void OnMessageReceived(object sender, HelloMessage message)  
	{  
		_alertService.Show(AppResources.Alert_Hello, "(☞ﾟヮﾟ)☞");  
	}  
}
```

**Send a message using IMessageService**
```
public class MessagingViewModel : ObservableObject, IViewModel  
{  
	private readonly IMessageService _messageService;  
	
	public ICommand SendMessageCommand { get; } 
	 
	public MessagingViewModel(IMessageService messageService)   
	{  
		_messageService = messageService;  
		
		SendMessageCommand = new Command(SendMessage);  
	}  

	// ...
	 
	private void SendMessage() => _messageService.Send(new HelloMessage());  
}
```

## MVVM & Navigation

### MVVM

There are some interface to define how each page should be handled by the `NavigationService`.

- **IRootView** : 
`NavigationService` will replace the current `Applicaton.Current.MainPage` by this page.

- **IStackView** 
`NavigationService` will replaces current `Applicaton.Current.MainPage` by a `NavigationPage` with this page as the first one in the stack.

- **IModalView**
`NavigationService` will push this page as modal in the current `NavigationPage`.

They all inherit from an `IView` :
```
public interface IView : ILoadable  
{  
	object BindingContext { get; }  
	IViewModel<object> ViewModel => BindingContext as IViewModel<object>;  
}
```

A `ViewModel` has also its own dedicated interface.
```
public interface IViewModel : IViewModel<object>  
{  
}
  
public interface IViewModel<in TParams> : ILoadable  
{  
	public Task InitializeAsync(TParams @params);  
}
```

`IView` and `IViewModel` both inherit from `ILoadable`, an interface used by the `NavigationService` each time a navigation occurs.

```
public interface ILoadable  
{  
	public void Load();  
	public void Unload();  
}
```

It is a safe place to register to and unregister from events. It is also useful to prepare a singleton ViewModel and clean its previous data.

### Navigation

//todo




