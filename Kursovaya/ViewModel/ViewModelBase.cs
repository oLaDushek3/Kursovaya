using System.ComponentModel;
using Kursovaya.DialogView;

namespace Kursovaya.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IConfirmationDialog
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void ClickNo()
        {
            throw new System.NotImplementedException();
        }

        public void ClickYes()
        {
            throw new System.NotImplementedException();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
