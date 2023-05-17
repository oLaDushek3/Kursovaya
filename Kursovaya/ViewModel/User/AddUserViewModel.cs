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
    public class AddUserViewModel : ViewModelBase
    {
        #region Fields
        private UserViewModel _currentUserViewModel;

        private ApplicationContext _context = new ApplicationContext();

        private IUserRepository _userRepository = new UserRepository();
        private UserModel _createdUser = new();
        private string _newUserPassword;
        #endregion Fields

        #region Properties
        public UserModel CreatedUser
        {
            get => _createdUser;
            set
            {
                _createdUser = value;
                OnPropertyChanged(nameof(CreatedUser));
            }
        }

        public List<string> RoleList
        {
            get
            {
                return new List<string>() { "Admin", "User" };
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
                _context.Users.Add(_createdUser);
                _context.SaveChanges();
                _currentUserViewModel.SaveAndCloseCUView(_createdUser);
            }
        }

        //Constructor
        public AddUserViewModel(UserViewModel currentUserViewModel)
        {
            _currentUserViewModel = currentUserViewModel;

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }
    }
}