using System.Threading.Tasks;

namespace Xamarin.Basics.Mvvm.ViewModels
{
    public interface IViewModel : IViewModel<object>
    {
    }

    public interface IViewModel<in TParams>
    {
        public Task InitializeAsync(TParams @params);

        public void Load()
        {
        }

        public void Unload()
        {
        }
    }
}