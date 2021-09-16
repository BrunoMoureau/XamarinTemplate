using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace XamarinTemplate.Views.List
{
    public class Foo
    {
        public string Test { get; }

        public Foo()
        {
            Test = Guid.NewGuid().ToString();
        }
    }

    public class ListViewModel : ObservableObject, IViewModel
    {
        private List<Foo> _foes;

        public List<Foo> Foes
        {
            get => _foes;
            set => SetProperty(ref _foes, value);
        }

        public void Open()
        {
        }

        public async Task InitializeAsync(object @params)
        {
            var foes = await Task.Run(GetFoes);

            Foes = foes;
        }

        private List<Foo> GetFoes()
        {
            var foes = new List<Foo>();
            for (int i = 0; i < 1000000; i++)
            {
                foes.Add(new());
            }

            return foes;
        }

        public void Close()
        {
            //todo cancellationtoken utils   
        }
    }
}