using Kursovaya.ViewModel;

namespace Kursovaya.DialogView.BaseDialog
{
    public class BaseDialogViewModel : ViewModelBase
    {
        private ViewModelBase _currentDialogView { get; set; }

        public ViewModelBase CurrentDialogView
        {
            get => _currentDialogView;

            set
            {
                _currentDialogView = value;
                OnPropertyChanged(nameof(CurrentDialogView));
            }
        }

        public BaseDialogViewModel(ViewModelBase currentDialogView)
        {
            _currentDialogView = currentDialogView;
        }
    }
}
