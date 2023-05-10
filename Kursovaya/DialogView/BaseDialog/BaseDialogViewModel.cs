using Kursovaya.Model.User;
using Kursovaya.ViewModel;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
