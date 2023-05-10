using Kursovaya.ViewModel;
using System.Windows.Input;

namespace Kursovaya.DialogView
{
    public class ConfirmationDialogViewModel : ViewModelBase
    {
        private IConfirmationDialog _dialogInterface;

        //Commands
        public ICommand ClickYesCommand { get; }
        public ICommand ClickNoCommand { get; }

        //Commands execution
        private void ExecuteClickYesCommand(object? obj)
        {
            _dialogInterface.ClickYes();
        }
        private void ExecuteClickNoCommand(object? obj)
        {
            _dialogInterface.ClickNo();
        }

        //Constructor
        public ConfirmationDialogViewModel(ViewModelBase calledViewModel)
        {
            _dialogInterface = calledViewModel;
            ClickYesCommand = new ViewModelCommand(ExecuteClickYesCommand);
            ClickNoCommand = new ViewModelCommand(ExecuteClickNoCommand);
        }
    }
}
