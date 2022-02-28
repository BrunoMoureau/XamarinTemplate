# Welcome to Xamarin.Template
I wanted to share some of my knowledge about Xamarin.Forms.

The goal of this repository is to give some best practices for your next Xamarin.Forms project.
I hope you will find some parts useful and learn something from them.

## Project structure
Besides Xamarin.Forms projects, the solution contains some other ones:
 -  **Xamarin.Basics** : provides a set of interfaces and tools to use inside your app.
 -  **Xamarin.Abstractions** : contains some interfaces and objects.
 -  **Xamarin.Api** : contains HTTP calls to communicate with a backend server.

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
I recommend using [Refit](https://github.com/reactiveui/refit) combined with [Polly](https://github.com/App-vNext/Polly) to manage API calls.

Every HTTP call is located in a service. It acts as an abstraction layer between your ViewModels and your API.
ViewModels should not be aware how your data are retrieved or sent.

![alt text](https://github.com/BrunoMoureau/XamarinTemplate/blob/master/docs/images/service_graph.png?raw=true)

## AppSettings & Settings objects


### AppSettings

There are **appsettings files** inside the Xamarin.Template project.
Each file contains a set of **JSON properties** used by a specific runtime environment.

This code written inside the **Xamarin.Template.csproj** file tells your project to use one particular appsettings file depending on the selected **build configuration** (Local, Debug or Release). 
```
<ItemGroup>  
 <EmbeddedResource Include="appsettings.json" Condition="!Exists('appsettings.json')" />  
 <EmbeddedResource Include="appsettings.$(Configuration).json" Link="appsettings.json" Condition="Exists('appsettings.$(Configuration).json')" />  
 ...
</ItemGroup>
```

Keep in mind that **you should never put sensitive information inside appsettings files!**

### Settings objects

You can retrieve properties from appsettings file using **AppSettings** class.
They could be single properties or objects.
```
var appSettings = new AppSettings(_assembly);  
appSettings.Get<EnvironmentSettings>("Environment");
```

## Background service

In ViewModels, almost everything is executed by the **main thread**. But this thread is also the **only thread responsible to render visual elements** on your pages. Therefore, you should prevent it from executing extra works like calling services or executing API calls...

You can use the **BackgroundTask** class to execute background tasks.
By keeping a reference to the instance, it can also be used to cancel running task (e.g when leaving the page).

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

I tried to find the fastest and most lightweight **dependency injection container** and ended up using [DryIoc](https://github.com/dadhi/DryIoc).

The **DI container** is really important to write **clean code**. You should define the way types are created and reused inside your app. When a class is **registered**, its dependencies will be resolved at runtime.

See how the app container is initialized inside **App.xaml.cs** : 
```
var appContainer = new AppContainer();  
appContainer.Initialize();
```

## Languages

I have never found a good way to **swap language in app** easily until now.
[Xamarin.CommunityToolkit](https://github.com/xamarin/XamarinCommunityToolkit) provides a set of super useful features you need in most apps.

It provides a way to swap language of the whole app (even the pages currently loaded in the navigation).

## Merged dictionaries

Have you ever seen an endless **App.xaml with thousand of lines** ? 
**Merged dictionaries** can be used to join some resource dictionaries together and avoid this.

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

When you want to perform a navigation, you can use the dedicated **NavigationService**.

You can inject the **INavigationService** interface in any **IView** or any **IViewModel** and make use of it.


### Notes
There is a bug with current Xamarin.Forms version using Xcode 13.2.1
https://github.com/xamarin/Xamarin.Forms/issues/15104






