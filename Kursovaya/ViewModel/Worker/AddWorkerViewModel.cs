using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Worker
{
    public class AddWorkerViewModel : ViewModelBase
    {
        #region Fields
        private WorkerViewModel _currentWorkerViewModel;

        private ApplicationContext _context = new ApplicationContext();

        private WorkerModel _createdWorker = new();

        private List<PostModel> _workerPostList;
        private IPostRepository _postRepository = new PostRepository();
        #endregion Fields

        #region Properties
        public List<PostModel> WorkerPostList
        {
            get => _workerPostList;
            set
            {
                _workerPostList = value;
                OnPropertyChanged(nameof(WorkerPostList));
            }
        }
        #endregion Properties

        //Commands
        public WorkerModel CreatedWorker
        {
            get => _createdWorker;
            set
            {
                _createdWorker = value;
                OnPropertyChanged(nameof(CreatedWorker));
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand SelectPhotoCommand { get; }

        //Commands execution
        public async void ExecuteSaveCommand(object? obj)
        {
            _context.Workers.Add(_createdWorker);
            _context.SaveChanges();
            _currentWorkerViewModel.SaveAndCloseCUView(_createdWorker);
        }
        public void ExecuteSelectPhotoCommand(object? obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Добавление изображения | *.png; *.jpg; *.gif";

            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                byte[] image_bytes = File.ReadAllBytes(fileDialog.FileName);
                _createdWorker.Photo = image_bytes;
                OnPropertyChanged(nameof(CreatedWorker));
            }
        }

        //Constructor
        public AddWorkerViewModel(WorkerViewModel currentWorkerViewModel)
        {
            _currentWorkerViewModel = currentWorkerViewModel;

            WorkerPostList = _postRepository.GetByAll(_context);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }
    }
}