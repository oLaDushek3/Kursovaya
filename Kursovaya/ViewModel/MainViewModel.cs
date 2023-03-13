using System.Windows.Input;
using FontAwesome.Sharp;
using Kursovaya.Model.User;
using Kursovaya.Repositories;

namespace Kursovaya.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserModel _user;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository _userRepository;

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

        //Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowSupplyViewCommand { get; }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            User = new UserModel();

            //Initialize command
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowSupplyViewCommand = new ViewModelCommand(ExecuteShowSupplyViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserdata();
        }

        private void ExecuteShowHomeViewCommand(object? obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Главная";
            Icon = IconChar.Home;

        }

        private void ExecuteShowSupplyViewCommand(object? obj)
        {
            CurrentChildView = new SupplyViewModel();
            Caption = "Поставки";
            Icon = IconChar.Truck;

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
