using Kursovaya.DialogView;
using Kursovaya.Model.User;
using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel.User
{
    public class UserViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel;
        #region Fields
        public ApplicationContext _context = new ApplicationContext();

        private ViewModelBase? _currentChildView;
        private bool _isEnabled = true;
        private Visibility _backVisibility = Visibility.Collapsed;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Worker fields
        private IUserRepository _userRepository = new UserRepository();
        private List<UserModel>? _allUsers;
        private List<UserModel>? _displayedUsers;
        private UserModel? _selectedUser;

        private string? _searchString;
        private List<UserModel>? _searchUsers;

        private string? _selectedSortRole;
        private List<UserModel>? _sortUsers;
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

        //Product properties
        public ObservableCollection<UserModel> DisplayedUsers
        {
            get => new ObservableCollection<UserModel>(_displayedUsers);
            set
            {
                _displayedUsers = new List<UserModel>(value);
                OnPropertyChanged(nameof(DisplayedUsers));
            }
        }
        public UserModel? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                Search();
            }
        }

        public List<string> RoleSortList
        {
            get
            {
                return new List<string>() { "Администратор", "Пользователь" };
            }
        }
        public List<UserModel> SortUsers
        {
            get => _sortUsers;
            set
            {
                _sortUsers = value;
                OnPropertyChanged(nameof(SortUsers));
            }
        }
        public string SelectedSortRole
        {
            get => _selectedSortRole;
            set
            {
                _selectedSortRole = value;
                OnPropertyChanged(nameof(SelectedSortRole));
                FilterByRole();
            }
        }
        #endregion Properties

        //Commands
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteBuyerCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand ClearSortCommand { get; }
        public ICommand ClearSortByDateCommande { get; }

        //Commands execution
        private void ExecuteAddCommand(object? obj)
        {
            CurrentChildView = new AddUserViewModel(this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteEditCommand(object? obj)
        {
            CurrentChildView = new EditUserViewModel(SelectedUser, this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private bool CanExecuteEditCommand(object? obj)
        {
            if (SelectedUser == null) return false;
            return true;
        }
        private async void ExecuteGoBackCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                MainViewModel.CloseDialog();

                AnimationAction = false;
                ReverseAnimationAction = true;
                BackVisibility = Visibility.Collapsed;
                IsEnabled = true;

                Thread myThread = new(ClearCurrentChildView);
                myThread.Start();
            }
        }
        private async void ExecuteDeleteBuyerCommand(object obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                _userRepository.Remove((obj as UserModel).UserId, _context);
                RefreshBuyersList(null);
            }
        }
        private void ExecuteClearSearchCommand(object? obj)
        {
            SearchString = "";
            _searchUsers = null;
            Merger();
        }
        private void ExecuteClearSortCommand(object? obj)
        {
            SelectedSortRole = null;
            _sortUsers = null;

            Merger();
        }

        //Constructor
        public UserViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            AddCommand = new ViewModelCommand(ExecuteAddCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            DeleteBuyerCommand = new ViewModelCommand(ExecuteDeleteBuyerCommand);
            ClearSearchCommand = new ViewModelCommand(ExecuteClearSearchCommand);
            ClearSortCommand = new ViewModelCommand(ExecuteClearSortCommand);

            RefreshBuyersList(null);
        }

        //Methods
        private void RefreshBuyersList(int? SelectedUserId)
        {
            _context = new ApplicationContext();

            _allUsers = _userRepository.GetByAll(_context);
            _displayedUsers = _allUsers.ToList();
            OnPropertyChanged(nameof(DisplayedUsers));

            SelectedUser = _displayedUsers.Where(u => u .UserId== SelectedUserId).FirstOrDefault();
            OnPropertyChanged(nameof(SelectedUser));

            Search();
            FilterByRole();
        }

        private void Search()
        {
            if (SearchString != "" && SearchString != null)
                _searchUsers = _allUsers.Where(u => u.Login.ToLower().Contains(SearchString.ToLower())).ToList();
            else
                _searchUsers = null;
            Merger();
        }
        private void FilterByRole()
        {
            if (_selectedSortRole != null)
            {
                if (_selectedSortRole == "Администратор")
                    _sortUsers = _allUsers.Where(u => u.Role == "Admin").ToList();
                else
                    _sortUsers = _allUsers.Where(s => s.Role == "User").ToList();
            }
            else
                _sortUsers = null;
            Merger();
        }
        private void Merger()
        {
            _displayedUsers = _allUsers;

            if (_sortUsers != null && _searchUsers != null)
            {
                _displayedUsers = _sortUsers.Intersect(_searchUsers).ToList();
                OnPropertyChanged(nameof(DisplayedUsers));
            }

            if (_sortUsers != null)
            {
                _displayedUsers = _allUsers.Intersect(_sortUsers).ToList();

                if (_searchUsers != null)
                {
                    _displayedUsers = _displayedUsers.Intersect(_searchUsers).ToList();
                }

                OnPropertyChanged(nameof(DisplayedUsers));
            }
            else
            {
                if (_searchUsers != null)
                {
                    _displayedUsers = _displayedUsers.Intersect(_searchUsers).ToList();
                }

                OnPropertyChanged(nameof(DisplayedUsers));
            }

            if (_sortUsers == null && _searchUsers == null)
            {
                _displayedUsers = _allUsers;
                OnPropertyChanged(nameof(DisplayedUsers));
            }
        }

        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
        public void SaveAndCloseCUView(UserModel userModel)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshBuyersList(userModel.UserId);
        }
    }
}
