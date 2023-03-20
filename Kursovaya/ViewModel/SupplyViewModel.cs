﻿using FontAwesome.Sharp;
using Kursovaya.Model;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase
    {
        //Fields
        private ISupplyRepository _supplyRepository;
        private ObservableCollection<SupplyModel>? _supplys;
        private SupplyModel? _selectedSupply;
        private ObservableCollection<WorkerModel> _workerModel;

        //Properties
        public ObservableCollection<SupplyModel>? Supplys
        {
            get => _supplys;
            set
            {
                _supplys = value;
                OnPropertyChanged(nameof(Supplys));
            }
        }
        public SupplyModel? SelectedSupply
        {
            get => _selectedSupply;
            set
            {
                _selectedSupply = value;
                OnPropertyChanged(nameof(SelectedSupply));
            }
        }
        public ObservableCollection<WorkerModel>? WorkerModel
        {
            get => _workerModel;
            set
            {
                _workerModel = value;
                OnPropertyChanged(nameof(WorkerModel));
            }
        }

        //Constructor
        public SupplyViewModel()
        {
            ApplicationContext context = new ApplicationContext();
            _supplyRepository = new SupplyRepository();
            Supplys = _supplyRepository.GetByAll();
            ShowDeleteViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            SelectedSupply = _supplyRepository.GetById(3);

            WorkerModel = (ObservableCollection<WorkerModel>?)context.Workers.Include(w => w.Post);
        }

        public void editSupply()
        {
            //ApplicationContext context = new ApplicationContext();

            //WorkerModel workerModel = context.Workers.Where(w => w.WorkerId == 3).FirstOrDefault();

            //SupplyModel supplyModel = context.Supplies.Where(s => s.SupplyId == SelectedSupply.SupplyId).FirstOrDefault();

            //supplyModel.Workers.Add(workerModel);
            //context.SaveChanges();
            Supplys[0].Workers.Add(WorkerModel[0]);
        }

        public ICommand ShowDeleteViewCommand { get; }
        private void ExecuteShowHomeViewCommand(object? obj)
        {
            editSupply();
        }
    }
}
