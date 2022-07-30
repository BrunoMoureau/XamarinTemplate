using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace MAUI.Basics.Mvvm.Commands
{
    public class BasicCommand : BasicCommand<object, object>
    {
        public BasicCommand(Action execute) : base(_ => execute())
        {
        }

        public BasicCommand(Action execute, Func<bool> canExecute) : base(_ => execute(), _ => canExecute())
        {
        }
    }
    
    public class BasicCommand<TExecuteParams, TCanExecuteParams> : ICommand
    {
        private readonly Action<TExecuteParams> _execute;
        private readonly Func<TCanExecuteParams, bool> _canExecute;
        
        public BasicCommand([NotNull] Action<TExecuteParams> execute) : this(execute, canExecute: _ => true)
        {
        }

        public BasicCommand([NotNull] Action<TExecuteParams> execute, [NotNull] Func<TCanExecuteParams, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if(parameter is TCanExecuteParams @params)
                return _canExecute(@params);
            
            return _canExecute(default);
        }

        public void Execute(object parameter)
        {
            if(parameter is TExecuteParams @params)
                _execute(@params);
            else
                _execute(default);
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
