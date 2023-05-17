using Kursovaya.DialogView;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Product;
using Kursovaya.Model.Supply;
using Kursovaya.Model.User;
using Kursovaya.Model.User;
using Kursovaya.Repositories;
using Kursovaya.ViewModel.Buyer;
using Kursovaya.ViewModel.Product;
using Kursovaya.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel.User
{
    public class EditUserViewModel : ViewModelBase
    {
        #region Fields
        private UserViewModel _currentUserViewModel;

        private ApplicationContext _context = new ApplicationContext();

        private IUserRepository _userRepository = new UserRepository();
        private UserModel _selectedUser;
        private UserModel _editableUser = new();
        private string _newUserPassword;
        #endregion Fields

        #region Properties
        public UserModel EditableUser
        {
            get => _editableUser;
            set
            {
                _editableUser = value;
                OnPropertyChanged(nameof(EditableUser));
            }
        }

        public List<string> RoleList
        {
            get
            {
                return new List<string>() { "Admin", "User" };
            }
        }
        public string NewUserPassword
        {
            get => _newUserPassword;
            set
            {
                _newUserPassword = value;
                OnPropertyChanged(nameof(NewUserPassword));
            }
        }
        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }

        //Commands execution
        public async void ExecuteSaveCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(_currentUserViewModel.MainViewModel);
            bool result = await _currentUserViewModel.MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                if(NewUserPassword != null)
                    _editableUser.Password = NewUserPassword;
                _editableUser.Login = "gg";
                _context.SaveChanges();
                _currentUserViewModel.SaveAndCloseCUView(_editableUser);
            }
        }

        //Constructor
        public EditUserViewModel(UserModel selectedUser, UserViewModel currentUserViewModel)
        {
            _currentUserViewModel = currentUserViewModel;
            _selectedUser = selectedUser;
            _editableUser = _userRepository.GetById(selectedUser.UserId, _context);

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }
    }
}