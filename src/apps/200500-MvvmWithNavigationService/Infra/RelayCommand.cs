﻿using System;
using System.Windows.Input;

namespace MvvmWithNavigationService.Infra
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }

        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked 
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;

            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
