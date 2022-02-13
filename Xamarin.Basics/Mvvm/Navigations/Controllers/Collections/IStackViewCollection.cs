using System.Collections.Generic;
using Xamarin.Basics.Mvvm.Views;

namespace Xamarin.Basics.Mvvm.Navigations.Controllers.Collections
{
    public interface IStackViewCollection
    {
        List<IStackView> NavigationStack { get; }
    }
}