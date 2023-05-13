using Kursovaya.ViewModel;
using System;
using System.Windows.Input;

namespace Kursovaya.DialogView
{
    public class ConfirmationDialogViewModel : ViewModelBase
    {
        private MainViewModel _currentMainViewModel;

        //Commands
        public ICommand ClickYesCommand { get; }
        public ICommand ClickNoCommand { get; }

        //Commands execution
        private void ExecuteClickYesCommand(object? obj)
        {
            _currentMainViewModel.DialogResult = true;
            _currentMainViewModel.CloseDialog();
        }
        private void ExecuteClickNoCommand(object? obj)
        {
            _currentMainViewModel.DialogResult = false;
        }

        //Constructor
        public ConfirmationDialogViewModel(MainViewModel currentMainViewModel)
        {
            _currentMainViewModel = currentMainViewModel;
            ClickYesCommand = new ViewModelCommand(ExecuteClickYesCommand);
            ClickNoCommand = new ViewModelCommand(ExecuteClickNoCommand);
        }
    }
}
