using Kursovaya.DialogView.BaseDialog;
using Kursovaya.Model.User;
using Kursovaya.Repositories;
using Kursovaya.ViewModel.Buyer;
using Kursovaya.ViewModel.Product;
using Kursovaya.ViewModel.Shipping;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        private string _icon;

        private ViewModelBase _dialogView;
        private bool _mainEnable;
        private double _blurEffectRadius;
        private Visibility _dimmingEffectEnable;

        private ViewModelBase _currentDeleteDialogView;

        private bool _isViewVisidible = true;
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

        public ViewModelBase? DialogView
        {
            get => _dialogView;
            set
            {
                _dialogView = value;
                OnPropertyChanged(nameof(DialogView));
            }
        }
        public bool MainEnable
        {
            get => _mainEnable;
            set
            {
                _mainEnable = value;
                OnPropertyChanged(nameof(MainEnable));
            }
        }
        public double BlurEffectRadius
        {
            get => _blurEffectRadius;
            set
            {
                _blurEffectRadius = value;
                OnPropertyChanged(nameof(BlurEffectRadius));
            }
        }
        public Visibility DimmingEffectEnable
        {
            get => _dimmingEffectEnable;
            set
            {
                _dimmingEffectEnable = value;
                OnPropertyChanged(nameof(DimmingEffectEnable));
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
        public string Icon
        {
            get => _icon;

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public bool IsViewVisidible
        {
            get => _isViewVisidible;
            set
            {
                _isViewVisidible = value;
                OnPropertyChanged(nameof(IsViewVisidible));
            }
        }
        //Commands
        public ICommand LogoutCommand { get; }
        public ICommand ShowSupplyViewCommand { get; }
        public ICommand ShowShippingViewCommand { get; }
        public ICommand ShowBuyerViewCommand { get; }
        public ICommand ShowProductViewCommand { get; }

        //Commands execution
        private void ExecuteLogoutCommand(object? obj)
        {
            IsViewVisidible = false;
        }
        private void ExecuteShowSupplyViewCommand(object? obj)
        {
            _currentMainView = new SupplyViewModel(this);
            CurrentChildView = _currentMainView;

            Caption = "Поставки";
            Icon = "Truck";

        }
        private void ExecuteShowShippingViewCommand(object? obj)
        {
            _currentMainView = new ShippingViewModel(this);
            CurrentChildView = _currentMainView;

            Caption = "Заказы";
            Icon = "Box";
        }
        private void ExecuteShowBuyerViewCommand (object? obj)
        {
            _currentMainView = new BuyerViewModel(this);
            CurrentChildView = _currentMainView;

            Caption = "Покуапетли";
            Icon = "UserGroup";
        }
        private void ExecuteShowProductViewCommand (object? obj)
        {
            _currentMainView = new ProductViewModel(this);
            CurrentChildView = _currentMainView;

            Caption = "Продукция";
            Icon = "Boxes";
        }

        //Constructor
        public MainViewModel()
        {
            _userRepository = new UserRepository();
            User = new UserModel();

            //Initialize command
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            ShowSupplyViewCommand = new ViewModelCommand(ExecuteShowSupplyViewCommand);
            ShowShippingViewCommand = new ViewModelCommand(ExecuteShowShippingViewCommand);
            ShowBuyerViewCommand = new ViewModelCommand(ExecuteShowBuyerViewCommand);
            ShowProductViewCommand = new ViewModelCommand(ExecuteShowProductViewCommand);

            LoadCurrentUserdata();

            MainEnable = true;
            BlurEffectRadius = 0;
            DimmingEffectEnable = Visibility.Collapsed;
        }

        #region Methods
        private void LoadCurrentUserdata()
        {
            //User = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            UserModel user = _userRepository.GetByUsername("admin");
            if (user != null)
            {
                User.Login = user.Login;
            }
            else user.Login = "Invalid user, not logged in";
        }

        //Dialog
        public delegate void CloseDialogDelegate();
        public event CloseDialogDelegate CloseDialogEvent;
        public bool DialogResult;

        public Task<bool> ShowDialog(ViewModelBase currentDialogView)
        {
            BaseDialogViewModel baseDialogViewModel = new(currentDialogView);
            DialogView = baseDialogViewModel;

            MainEnable = false;
            BlurEffectRadius = 3;
            DimmingEffectEnable = Visibility.Visible;

            var completion = new TaskCompletionSource<bool>();

            CloseDialogEvent += () => completion.TrySetResult(DialogResult);
            return completion.Task;
        }
        public void CloseDialog()
        {
            DialogView = null;
            MainEnable = true;
            BlurEffectRadius = 0;
            DimmingEffectEnable = Visibility.Collapsed;

            CloseDialogEvent?.Invoke();
        }
        #endregion Methods
    }
}
