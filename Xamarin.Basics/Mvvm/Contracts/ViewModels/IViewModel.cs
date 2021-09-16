using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.OpenClose;

namespace Xamarin.Basics.Mvvm.Contracts.ViewModels
{
    public interface IViewModel : IViewModel<object>
    {
    }
    
    public interface IViewModel<in TParams> : IOpenClose
    {
        public Task InitializeAsync(TParams @params);
    }
}