using Humanizer;
using Kursovaya.DialogView;
using Kursovaya.DialogView.AddSupplyProduct;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class EditSupplyViewModel : ViewModelBase
    {
        public SupplyViewModel currentSupplyViewModel;

        #region Fields
        ApplicationContext context = new ApplicationContext();

        //Supply fields
        private ISupplyRepository _supplyRepository = new SupplyRepository();
        private SupplyModel _selectedSupply;
        private SupplyModel _editableSupply = new SupplyModel();

        //Factory fields
        private IFactoryRepository factoryRepository = new FactoryRepository();
        private List<FactoryModel> _allFactory;

        //Worker fields
        private IWorkerRepository _workerRepository = new WorkerRepository();
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private List<WorkerModel> _editableSupplyWorkers = new List<WorkerModel>();
        private int _supplyWorkersPostId;

        //Product and place fields
        private List<SupplyProductModel> _editableSupplyProduct;
        #endregion Fields

        #region Properties
        //Supply properties
        public SupplyModel EditableSupply
        {
            get => _editableSupply;
            set
            {
                _editableSupply = value;
                OnPropertyChanged(nameof(EditableSupply));
            }
        }

        //Factory properties
        public List<FactoryModel> AllFactory
        {
            get => _allFactory;
            set
            {
                _allFactory = value;
                OnPropertyChanged(nameof(AllFactory));
            }
        }

        //Worker properties
        public ObservableCollection<WorkerModel> AvailableWorker
        {
            get => new ObservableCollection<WorkerModel>(_availableWorker);
            set
            {
                _availableWorker = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(AvailableWorker));
            }
        }
        public WorkerModel? SelectedForAdditionWorker
        {
            get => _selectedForAdditionWorker;
            set
            {
                _selectedForAdditionWorker = value;
                OnPropertyChanged(nameof(SelectedForAdditionWorker));
                if(value != null)
                    AddWorker();
            }
        }
        public ObservableCollection<WorkerModel> EditableSupplyWorkers
        {
            get => new ObservableCollection<WorkerModel>(_editableSupplyWorkers);
            set
            {
                _editableSupplyWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(EditableSupplyWorkers));
            }
        }
        public int SupplyWorkersPostId
        {
            get => _supplyWorkersPostId;
            set
            {
                _supplyWorkersPostId = value;
                OnPropertyChanged(nameof(SupplyWorkersPostId));
                SortSupplyWorkersByPost();
            }
        }

        //Product and place properties
        public ObservableCollection<SupplyProductModel> EditableSupplyProduct
        {
            get => new ObservableCollection<SupplyProductModel>(_editableSupplyProduct);
            set
            {
                _editableSupplyProduct = new List<SupplyProductModel>(value);
                OnPropertyChanged(nameof(EditableSupplyProduct));
            }
        }

        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteWorkerCommand { get; }
        public ICommand DeleteSupplyProductCommand { get; }
        public ICommand AddSupplyProductCommand { get; }

        //Commands execution
        public async void ExecuteSaveCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(currentSupplyViewModel.MainViewModel);
            bool result = await currentSupplyViewModel.MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                List<SupplyProductModel> deleteSupplyProducts;
                List<SupplyProductModel> addSupplyProducts;

                deleteSupplyProducts = _editableSupply.SupplyProducts.Except(_editableSupplyProduct).ToList();
                foreach (SupplyProductModel supplyProduct in deleteSupplyProducts)
                {
                    context.SupplyProducts.Remove(supplyProduct);
                }

                addSupplyProducts = _editableSupplyProduct.Except(_editableSupply.SupplyProducts).ToList();
                foreach (SupplyProductModel supplyProduct in addSupplyProducts)
                {
                    supplyProduct.Supply = _editableSupply;
                    context.SupplyProducts.Add(supplyProduct);
                }

                context.SaveChanges();
                currentSupplyViewModel.SaveModifiedSupply(_editableSupply);
            }
        }
        public void ExecuteDeleteWorkerCommand(object? obj)
        {
            EditableSupply.Workers.Remove(obj as WorkerModel);
            SortSupplyWorkersByPost();
        }
        public void ExecuteDeleteSupplyProductCommand(object obj)
        {
            _editableSupplyProduct.Remove(obj as SupplyProductModel);
            OnPropertyChanged(nameof(EditableSupplyProduct));
        }
        public void ExecuteAddSupplyProductCommand(object? obj)
        {
            AddSupplyProductViewModel addSupplyProductViewModel = new AddSupplyProductViewModel(context, this);
            currentSupplyViewModel.MainViewModel.ShowDialog(addSupplyProductViewModel);
        }

        //Constructor
        public EditSupplyViewModel(SupplyModel selectedSupply, SupplyViewModel supplyViewModel)
        {
            _selectedSupply = selectedSupply;
            _editableSupply = _supplyRepository.GetById(_selectedSupply.SupplyId, context);
            currentSupplyViewModel = supplyViewModel;
            SortSupplyWorkersByPost();

            _allFactory = factoryRepository.GetByAll(context);
            _editableSupplyProduct = _editableSupply.SupplyProducts.ToList();

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            DeleteSupplyProductCommand = new ViewModelCommand(ExecuteDeleteSupplyProductCommand);
            AddSupplyProductCommand = new ViewModelCommand(ExecuteAddSupplyProductCommand);
            DeleteWorkerCommand = new ViewModelCommand(ExecuteDeleteWorkerCommand);
        }

        //Methods
        private void SortSupplyWorkersByPost()
        {
            if (_supplyWorkersPostId != 0)
            {
                _editableSupplyWorkers = _editableSupply.Workers.Where(w => w.PostId == _supplyWorkersPostId).ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_editableSupplyWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).Where(w => w.PostId == _supplyWorkersPostId).ToList();
            }
            else
            {
                _editableSupplyWorkers = _editableSupply.Workers.ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_editableSupplyWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).ToList();
            }

            OnPropertyChanged(nameof(EditableSupplyWorkers));
            OnPropertyChanged(nameof(AvailableWorker));
        }
        private void AddWorker()
        {
            EditableSupply.Workers.Add(SelectedForAdditionWorker);
            SortSupplyWorkersByPost();
            SelectedForAdditionWorker = null;
        }
        public void AddSupplyProduct(SupplyProductModel supplyProductModel)
        {
            _editableSupplyProduct.Add(supplyProductModel);
            OnPropertyChanged(nameof(EditableSupplyProduct));
        }
    }
}