using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Navigations.Interfaces;

namespace Xamarin.Basics.Mvvm.ViewModels
{
    public interface IViewModel : IViewModel<object>
    {
    }

    public interface IViewModel<in TParams> : ILoadable
    {
        public Task InitializeAsync(TParams @params);
    }
}