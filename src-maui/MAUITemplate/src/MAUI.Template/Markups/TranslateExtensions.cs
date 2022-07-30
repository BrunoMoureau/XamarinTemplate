using System.Globalization;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUI.Template.Markups
{
    [ContentProperty(nameof(Key))]
    public class TranslateExtension : IMarkupExtension
    {
        public string Key { get; set; } = string.Empty;
        public string StringFormat { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Key}]",
                Source = LocalizationResourceManager.Current,
                StringFormat = StringFormat
            };

            return binding;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
    
    public class LocalizationResourceManager : ObservableObject
    {
        private static readonly Lazy<LocalizationResourceManager> CurrentHolder = new(() => new LocalizationResourceManager());
        public static LocalizationResourceManager Current => CurrentHolder.Value;

        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture = Thread.CurrentThread.CurrentUICulture;
        
        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set => SetProperty(ref _currentCulture, value, null);
        }

        public void Init(ResourceManager resource) => _resourceManager = resource;

        public void Init(ResourceManager resource, CultureInfo initialCulture)
        {
            CurrentCulture = initialCulture;
            Init(resource);
        }

        private string GetValue(string text)
        {
            if (_resourceManager == null)
                throw new InvalidOperationException($"Must call {nameof(LocalizationResourceManager)}.{nameof(Init)} first");

            return _resourceManager.GetString(text, CurrentCulture);
        }

        public string this[string text] => GetValue(text);
    }
}
