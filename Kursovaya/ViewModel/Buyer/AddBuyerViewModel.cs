using Kursovaya.Converters;
using Kursovaya.DialogView;
using Kursovaya.Model.Buyer;
using Kursovaya.Repositories;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Buyer
{
    public class AddBuyerViewModel : ViewModelBase
    {
        private BuyerViewModel _currentBuyerViewModel;

        #region Fields
        ApplicationContext context = new ApplicationContext();

        private BuyerModel _createdBuyer = new();
        private IndividualModel _createdIndividual = new();
        private LegalEntityModel _createdLegalEntity = new();
        private BuyerAddressModel _createdBuyerAddress = new();

        private string _selectedBuyerType;

        private string _visibleIndividualPanel = "Collapsed";
        private string _visibleLeaglEntityPanel = "Collapsed";
        #endregion Fields

        #region Properties
        public IndividualModel CreatedIndividual
        {
            get => _createdIndividual;
            set
            {
                _createdIndividual = value;
                OnPropertyChanged(nameof(CreatedIndividual));
            }
        }
        public LegalEntityModel CreatedLegalEntity
        {
            get => _createdLegalEntity;
            set
            {
                _createdLegalEntity = value;
                OnPropertyChanged(nameof(CreatedLegalEntity));
            }
        }
        public BuyerAddressModel CreatedBuyerAddress
        {
            get => _createdBuyerAddress;
            set
            {
                _createdBuyerAddress = value;
                OnPropertyChanged(nameof(CreatedBuyerAddress));
            }
        }

        public List<string> BuyerTypeList
        {
            get
            {
                return new List<string>() { "Физ. лицо", "Юр. Лицо" };
            }
        }
        public string SelectedBuyerType
        {
            get => _selectedBuyerType;
            set
            {
                _selectedBuyerType = value;
                OnPropertyChanged(nameof(SelectedBuyerType));
                BuyerTypeChange();
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
            if (_selectedBuyerType == "Физ. лицо")
            {
                _createdIndividual.BuyerAddresses.Add(_createdBuyerAddress);
                _createdBuyer.Individual = _createdIndividual;

                context.BuyerAddresses.Add(_createdBuyerAddress);
                context.Individuals.Add(_createdIndividual);
                context.Buyers.Add(_createdBuyer);
            }
            else
            {
                _createdLegalEntity.BuyerAddresses.Add(_createdBuyerAddress);
                _createdBuyer.LegalEntity = _createdLegalEntity;

                context.BuyerAddresses.Add(_createdBuyerAddress);
                context.LegalEntities.Add(_createdLegalEntity);
                context.Buyers.Add(_createdBuyer);
            }

            context.SaveChanges();
            _currentBuyerViewModel.SaveAndCloseCUView(_createdBuyer);
        }


        //Constructor
        public AddBuyerViewModel(BuyerViewModel currentBuyerViewModel)
        {
            _currentBuyerViewModel = currentBuyerViewModel;

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }

        //Methods
        private void BuyerTypeChange()
        {
            if (_selectedBuyerType == "Физ. лицо")
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
        }
    }
}
