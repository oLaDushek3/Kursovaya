using Kursovaya.DialogView;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Product;
using Kursovaya.Model.Product;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Kursovaya.ViewModel.Buyer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using f = System.Windows.Forms;

namespace Kursovaya.ViewModel.Worker
{
    public class WorkerViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel;
        #region Fields
        public ApplicationContext context = new ApplicationContext();

        private ViewModelBase? _currentChildView;
        private bool _isEnabled = true;
        private Visibility _backVisibility = Visibility.Collapsed;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Worker fields
        private IWorkerRepository _workerRepository = new WorkerRepository();
        private List<WorkerModel>? _allWorkers;
        private List<WorkerModel>? _displayedWorkers;
        private WorkerModel? _selectedWorker;

        private string? _searchString;
        private List<WorkerModel>? _searchWorkers;

        private IPostRepository _postRepository = new PostRepository();
        private List<PostModel> _postsSortList;
        private PostModel? _selectedSortPost;
        private List<WorkerModel>? _sortWorkers;
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
        public ObservableCollection<WorkerModel> DisplayedWorkers
        {
            get => new ObservableCollection<WorkerModel>(_displayedWorkers);
            set
            {
                _displayedWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(DisplayedWorkers));
            }
        }
        public WorkerModel? SelectedWorker
        {
            get => _selectedWorker;
            set
            {
                _selectedWorker = value;
                OnPropertyChanged(nameof(SelectedWorker));
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

        public List<PostModel> PostsSortList
        {
            get => _postsSortList;
            set
            {
                _postsSortList = value;
                OnPropertyChanged(nameof(PostsSortList));
            }
        }
        public List<WorkerModel> SortWorkers
        {
            get => _sortWorkers;
            set
            {
                _sortWorkers = value;
                OnPropertyChanged(nameof(SortWorkers));
            }
        }
        public PostModel SelectedSortPost
        {
            get => _selectedSortPost;
            set
            {
                _selectedSortPost = value;
                OnPropertyChanged(nameof(SelectedSortPost));
                FilterByPost();
            }
        }
        #endregion

        //Commands
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand ClearSortCommand { get; }
        public ICommand ClearSortByDateCommande { get; }

        //Commands execution
        private void ExecuteAddCommand(object? obj)
        {
            CurrentChildView = new AddWorkerViewModel(this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteEditCommand(object? obj)
        {
            CurrentChildView = new EditWorkerViewModel(SelectedWorker, this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private bool CanExecuteEditCommand(object? obj)
        {
            if (SelectedWorker == null) return false;
            return true;
        }
        private async void ExecuteGoBackCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new(MainViewModel);
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
        private async void ExecuteDeleteProductCommand(object obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                _workerRepository.Remove((obj as WorkerModel).WorkerId, context);
                RefreshProductsList(null);
            }
        }
        private void ExecuteClearSearchCommand(object? obj)
        {
            SearchString = "";
            _searchWorkers = null;
            Merger();
        }
        private void ExecuteClearSortCommand(object? obj)
        {
            SelectedSortPost = null;
            _sortWorkers = null;

            Merger();
        }

        //Constructor
        public WorkerViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            AddCommand = new ViewModelCommand(ExecuteAddCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            DeleteProductCommand = new ViewModelCommand(ExecuteDeleteProductCommand);
            ClearSearchCommand = new ViewModelCommand(ExecuteClearSearchCommand);
            ClearSortCommand = new ViewModelCommand(ExecuteClearSortCommand);

            PostsSortList = _postRepository.GetByAll(context);

            RefreshProductsList(null);
        }

        //Methods
        private void RefreshProductsList(int? selectedWorkerId)
        {
            context = new ApplicationContext();

            _allWorkers = _workerRepository.GetByAll(context);
            _displayedWorkers = _allWorkers.ToList();
            OnPropertyChanged(nameof(DisplayedWorkers));

            SelectedWorker = _displayedWorkers.Where(w => w.WorkerId == selectedWorkerId).FirstOrDefault();
            OnPropertyChanged(nameof(SelectedWorker));

            Search();
            FilterByPost();
        }

        private void Search()
        {
            if (SearchString != "" && SearchString != null) 
                _searchWorkers = _allWorkers.Where(w => w.FullName.ToLower().Contains(SearchString.ToLower()) | w.WorkerId.ToString().Contains(SearchString.ToLower()) | w.Post.Title.ToLower().Contains(SearchString.ToLower())).ToList();      
            else
                _searchWorkers = null;
            Merger();
        }
        private void FilterByPost()
        {
            if (_selectedSortPost != null)
                _sortWorkers = _allWorkers.Where(w => w.Post.PostId == SelectedSortPost.PostId).ToList();
            else
                _sortWorkers = null;
            Merger();
        }
        private void Merger()
        {
            _displayedWorkers = _allWorkers;

            if (_sortWorkers != null && _searchWorkers != null)
            {
                _displayedWorkers = _sortWorkers.Intersect(_searchWorkers).ToList();
                OnPropertyChanged(nameof(DisplayedWorkers));
            }

            if (_sortWorkers != null)
            {
                _displayedWorkers = _allWorkers.Intersect(_sortWorkers).ToList();

                if (_searchWorkers != null)
                {
                    _displayedWorkers = _displayedWorkers.Intersect(_searchWorkers).ToList();
                }

                OnPropertyChanged(nameof(DisplayedWorkers));
            }
            else
            {
                if (_searchWorkers != null)
                {
                    _displayedWorkers = _displayedWorkers.Intersect(_searchWorkers).ToList();
                }

                OnPropertyChanged(nameof(DisplayedWorkers));
            }

            if (_sortWorkers == null && _searchWorkers == null)
            {
                _displayedWorkers = _allWorkers;
                OnPropertyChanged(nameof(DisplayedWorkers));
            }
        }

        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
        public void SaveAndCloseCUView(WorkerModel workerModel)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshProductsList(workerModel.WorkerId);
        }
    }
}