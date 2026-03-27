using System;
using System.Windows.Input;

namespace ToolModiSAO.informazioneControl
{
    /// <summary>
    /// Command base senza parametro
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        /// <summary>
        /// Forza il refresh dello stato del comando
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Command generico con parametro tipizzato
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            // Caso null per tipi valore (int, bool, ecc.)
            if (parameter == null && typeof(T).IsValueType)
                return _canExecute == null;

            if (parameter == null || parameter is T)
                return _canExecute?.Invoke((T)parameter) ?? true;

            return false;
        }

        public void Execute(object parameter)
        {
            if (parameter == null && typeof(T).IsValueType)
                throw new InvalidOperationException($"Parametro richiesto di tipo {typeof(T).Name}");

            if (parameter == null || parameter is T)
                _execute((T)parameter);
        }

        /// <summary>
        /// Forza il refresh dello stato del comando
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}