using Kursovaya.ViewModel;
using System.Windows.Input;

namespace Kursovaya.DialogView
{
    public class DialogViewModel : ViewModelBase
    {
        private IDialog _dialogInterface;

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
        public DialogViewModel(ViewModelBase calledViewModel)
        {
            _dialogInterface = calledViewModel;
            ClickYesCommand = new ViewModelCommand(ExecuteClickYesCommand);
            ClickNoCommand = new ViewModelCommand(ExecuteClickNoCommand);
        }
    }
}
