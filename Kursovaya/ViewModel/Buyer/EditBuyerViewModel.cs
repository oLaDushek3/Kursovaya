using Kursovaya.DialogView;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Kursovaya.ViewModel.Shipping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Buyer
{
    public class EditBuyerViewModel : ViewModelBase
    {
        private BuyerViewModel _currentBuyerViewModel;

        #region Fields
        ApplicationContext context = new ApplicationContext();

        private IBuyerRepository _buyerRepository = new BuyerRepository();
        private BuyerModel _selectedBuyer;
        private BuyerModel _editableBuyer = new();

        private string _visibleIndividualPanel;
        private string _visibleLeaglEntityPanel;
        #endregion Fields

        #region Properties
        public BuyerModel EditableBuyer
        {
            get => _editableBuyer;
            set
            {
                _editableBuyer = value;
                OnPropertyChanged(nameof(EditableBuyer));
            }
        }
        public string VisibleIndividualPanel
        {
            get => _visibleIndividualPanel;
            set
            {
                _visibleIndividualPanel = value;
                OnPropertyChanged(nameof(VisibleIndividualPanel));
            }
        }
        public string VisibleLeaglEntityPanel
        {
            get => _visibleLeaglEntityPanel;
            set
            {
                _visibleLeaglEntityPanel = value;
                OnPropertyChanged(nameof(VisibleLeaglEntityPanel));
            }
        }
        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }

        //Commands execution
        public async void ExecuteSaveCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(_currentBuyerViewModel.MainViewModel);
            bool result = await _currentBuyerViewModel.MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                context.SaveChanges();
                _currentBuyerViewModel.SaveAndCloseCUView(_editableBuyer);
            }
        }

        //Constructor
        public EditBuyerViewModel(BuyerModel selectedBuyer, BuyerViewModel currentBuyerViewModel)
        {
            _currentBuyerViewModel = currentBuyerViewModel;
            _selectedBuyer = selectedBuyer;
            _editableBuyer = _buyerRepository.GetById(selectedBuyer.Buyer1, context);

            if (_editableBuyer.Individual != null)
            {
                VisibleIndividualPanel = "Visible";
                VisibleLeaglEntityPanel = "Collapsed";
            }
            else
            {
                VisibleIndividualPanel = "Collapsed";
                VisibleLeaglEntityPanel = "Visible";
            }
            OnPropertyChanged(nameof(VisibleIndividualPanel));
            OnPropertyChanged(nameof(VisibleLeaglEntityPanel));

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }
    }
}
