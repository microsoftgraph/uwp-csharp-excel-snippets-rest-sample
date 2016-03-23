/*
 *  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
 *  See LICENSE in the source repository root for complete license information.
 */

using System;
using System.Windows.Input;

namespace ExcelServiceExplorer.ViewModel
{
    public class DelegateCommand : ICommand
    {
        Func<object, bool> canExecute;
        Action<object> executeAction;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }

}
