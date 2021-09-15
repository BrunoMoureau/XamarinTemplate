using System;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;

namespace Xamarin.Basics.Mvvm.Models
{
    public abstract class ViewModel : ViewModel<ViewModelParams>
    {
    }
    
    //Todo observable object
    public abstract class ViewModel<TParams> : IDisposable, IViewModel where TParams : ViewModelParams
    {
        protected TParams Params { get; private set; }

        public Task InitializeAsync(TParams @params)
        {
            Params = @params;
            return InitializeAsync();
        }

        protected abstract Task InitializeAsync();
        public abstract void Dispose();
    }
}