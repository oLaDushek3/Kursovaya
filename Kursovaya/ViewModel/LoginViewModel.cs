using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Windows.Input;
using Kursovaya.Model.User;
using Kursovaya.Repositories;

namespace Kursovaya.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisidible = true;

        private IUserRepository userRepository;

        //Properties
        public string Username { get => _username;
            set 
            { 
                _username = value;
                OnPropertyChanged(nameof(Username));
            } 
        }
        public string Password { get => _password; 
            set
            {
                _password = value; 
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisidible
        {
            get => _isViewVisidible;
            set
            {
                _isViewVisidible= value;
                OnPropertyChanged(nameof(IsViewVisidible));
            }
        }

        //Commands
        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RecoverPasswordCommand { get; }

        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);

            //ApplicationContext context = new ApplicationContext();
            //List<Factory> factorys = context.Factory.ToList();
            //List<SupplyModel> supplys = context.Supply.ToList();
            //MessageBox.Show(supplys[1].Factory.Address);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Password == null)
                validData = false;
            else
                validData = true;

            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if(isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsViewVisidible = false;
            }
            else
                ErrorMessage = "* Не верный логин или пароль";
        }
    }
}
