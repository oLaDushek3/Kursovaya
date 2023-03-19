using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FontAwesome.Sharp;
using Kursovaya.DialogView;
using Kursovaya.Model.User;
using Kursovaya.Repositories;
using Kursovaya.View;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Kursovaya.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserModel _user;
        private IUserRepository _userRepository;

        private ViewModelBase _currentChildView;
        private ViewModelBase _currentMainView;
        private string _caption;
        private IconChar _icon;

        private Visibility _visibility;
        private Visibility _backVisibility;
        private IDialogShow _dialogShow;
        private ViewModelBase _currentAddView;
        private ViewModelBase _currentEditView;
        private ViewModelBase _currentDeleteDialogView;

        private ICommand _goBackCommand { get; set; }

        //Properties
        public UserModel User 
        {
            get => _user;

            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public ViewModelBase CurrentDeleteDialogView
        {
            get => _currentDeleteDialogView;

            set
            {
                _currentDeleteDialogView = value;
                OnPropertyChanged(nameof(CurrentDeleteDialogView));
            }
        }
        public string Caption
        {
            get => _caption;

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get => _icon;

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public Visibility Visibility
        {
            get => _visibility;

            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
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

        public ICommand GoBackCommand
        {
            get => _goBackCommand;

            set
            {
                _goBackCommand = value;
                OnPropertyChanged(nameof(GoBackCommand));
            }
        }

        //Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowSupplyViewCommand { get; }
        public ICommand ShowFactoryViewCommand { get; }

        public ICommand ShowEditViewCommand { get; }
        public ICommand ShowAddViewCommand { get; }
        public ICommand ShowDeleteViewCommand { get; }


        //Commands execution
        //Navigation button commands execution
        private void ExecuteShowHomeViewCommand(object? obj)
        {
            _currentMainView = new HomeViewModel();
            CurrentChildView = _currentMainView;

            Caption = "Главная";
            Icon = IconChar.Home;
            Visibility = Visibility.Collapsed;
            BackVisibility = Visibility.Collapsed;

            GoBackCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
        }
        private void ExecuteShowSupplyViewCommand(object? obj)
        {
            _currentMainView = new SupplyViewModel();
            CurrentChildView = _currentMainView;

            Caption = "Поставки";
            Icon = IconChar.Truck;
            Visibility = Visibility.Visible;
            BackVisibility = Visibility.Collapsed;

            _currentAddView = new AddSupplyViewModel();
            _currentEditView = new EditSupplyViewModel();
            GoBackCommand = new ViewModelCommand(ExecuteShowSupplyViewCommand);

        }
        private void ExecuteShowFactoryViewCommand(object? obj)
        {
            _currentMainView = new FactoryViewModel();
            CurrentChildView = _currentMainView;

            Caption = "Производства";
            Icon = IconChar.Industry;
            Visibility = Visibility.Visible;
            BackVisibility = Visibility.Collapsed;

            GoBackCommand = new ViewModelCommand(ExecuteShowFactoryViewCommand);
        }

        //Edit Add Delete button commands execution
        private void ExecuteShowEditViewCommand(object? obj)
        {
            CurrentChildView = _currentEditView;
            Caption += " изменение";
            Visibility = Visibility.Collapsed;
            BackVisibility = Visibility.Visible;
        }
        private void ExecuteShowAddViewCommand(object? obj)
        {
            CurrentChildView = _currentAddView;
            Caption += " добавление";
            Visibility = Visibility.Collapsed;
            BackVisibility = Visibility.Visible;
        }
        private void ExecuteShowDeleteViewCommand(object? obj)
        {
            _dialogShow.CreateNewWindow();
        }

        //Constructor
        public MainViewModel()
        {
            _userRepository = new UserRepository();
            User = new UserModel();
            _dialogShow = new DialogShow();

            //Initialize command
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowSupplyViewCommand = new ViewModelCommand(ExecuteShowSupplyViewCommand);
            ShowFactoryViewCommand = new ViewModelCommand(ExecuteShowFactoryViewCommand);
            ShowEditViewCommand = new ViewModelCommand(ExecuteShowEditViewCommand);
            ShowAddViewCommand =  new ViewModelCommand(ExecuteShowAddViewCommand);
            ShowDeleteViewCommand = new ViewModelCommand(ExecuteShowDeleteViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserdata();
        }

        private void LoadCurrentUserdata()
        {
            UserModel user = _userRepository.GetByUsername("admin");
            if (user != null)
            {
                User.Login = user.Login;
            }
            else user.Login = "Invalid user, not logged in";
        }
    }
}
