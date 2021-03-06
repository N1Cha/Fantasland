﻿using System;
using System.Windows.Input;

namespace Fantasland.Infrastructure
{
    public class Command<T> : ICommand
    {
        readonly Action execute = null;
        readonly Predicate<T> canExecute = null;

        public Command(Action execute) : this(execute, null)
        {
        }

        public Command(Action execute, Predicate<T> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
