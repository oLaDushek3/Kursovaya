using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase
    {
        ApplicationContext context = new ApplicationContext();

        #region Fields
        private ViewModelBase _currentChildView;
        private bool _isEnabled;
        private Visibility _backVisibility;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Supply fields
        private ISupplyRepository _supplyRepository;
        private List<SupplyModel> _supplys;
        private SupplyModel? _selectedSupply;
        #endregion Fields

        #region Properties
        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public bool IsEnabled
        {
            get => _isEnabled;

            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
        public Visibility BackVisibility
        {
            get => _backVisibility;

            set
            {
                _backVisibility = value;
                OnPropertyChanged(nameof(BackVisibility));
            }
        }
        public bool AnimationAction
        {
            get => _animationAction;

            set
            {
                _animationAction = value;
                OnPropertyChanged(nameof(AnimationAction));
            }
        }
        public bool ReverseAnimationAction
        {
            get => _reverseAnimationAction;

            set
            {
                _reverseAnimationAction = value;
                OnPropertyChanged(nameof(ReverseAnimationAction));
            }
        }

        //Supply properties
        public List<SupplyModel> Supplys
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
        #endregion Properties

        //Commands
        public ICommand ShowAddSupplyCommand { get; }
        public ICommand GoBackCommand { get; }

        //Commands execution
        private void ExecuteShowAddSupplyCommand(object? obj)
        {
            CurrentChildView = new EditSupplyViewModel(SelectedSupply);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteGoBackCommand(object? obj)
        {
            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();
        }

        //Constructor
        public SupplyViewModel()
        {
            ShowAddSupplyCommand = new ViewModelCommand(ExecuteShowAddSupplyCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);

            _supplyRepository = new SupplyRepository();
            Supplys = _supplyRepository.GetByAll();

            SelectedSupply = _supplyRepository.GetById(Supplys[0].SupplyId, context);
            IsEnabled = true;
            BackVisibility = Visibility.Collapsed;
        }
        
        //Methods
        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
    }
}