using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Kursovaya.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    internal class DeleteSupply : ViewModelBase
    {
        ApplicationContext context = new ApplicationContext();

        private ISupplyRepository _supplyRepository;
        private SupplyModel _selectedSupply;
        private SupplyModel? _editableSelectedSupply;

        public SupplyModel SelectedSupply
        {
            get => _selectedSupply;
            set
            {
                _selectedSupply = value;
                OnPropertyChanged(nameof(SelectedSupply));
            }
        }
        public SupplyModel? EditableSelectedSupply
        {
            get => _editableSelectedSupply;
            set
            {
                _editableSelectedSupply = value;
                OnPropertyChanged(nameof(EditableSelectedSupply));
            }
        }
        public void ExecuteSaveCommand(object? obj)
        {
            SupplyModel? supplyModel = _supplyRepository.GetById(SelectedSupply.SupplyId, context);
            EditableSelectedSupply = _supplyRepository.GetById(SelectedSupply.SupplyId, context);
            foreach (SupplyProductModel supplyProduct in supplyModel.SupplyProducts)
            {
                foreach (SupplyProductPlaceModel supplyProductPlace in supplyProduct.SupplyProductPlaces)
                {
                    context.SupplyProductPlaces.Remove(supplyProductPlace);
                }
                context.SupplyProducts.Remove(supplyProduct);
            }

            foreach (WorkerModel workerModel in supplyModel.Workers)
            {
                workerModel.Supplies.Remove(supplyModel);
            }

            context.Supplies.Remove(supplyModel);
            context.SaveChanges();
        }
    }
}
