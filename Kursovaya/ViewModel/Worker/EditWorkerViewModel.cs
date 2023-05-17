using Kursovaya.DialogView;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Kursovaya.ViewModel.Worker
{
    public class EditWorkerViewModel : ViewModelBase
    {
        #region Fields
        private WorkerViewModel _currentWorkerViewModel;

        private ApplicationContext _context = new ApplicationContext();

        private IWorkerRepository _workerRepository = new WorkerRepository();
        private WorkerModel _selectedWorker;
        private WorkerModel _editableWorker = new();

        private List<PostModel> _workerPostList;
        private IPostRepository _postRepository = new PostRepository();
        #endregion Fields

        #region Properties
        public WorkerModel EditableWorker
        {
            get => _editableWorker;
            set
            {
                _editableWorker = value;
                OnPropertyChanged(nameof(EditableWorker));
            }
        }
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
        public ICommand SaveCommand { get; }
        public ICommand SelectPhotoCommand { get; }

        //Commands execution
        public async void ExecuteSaveCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(_currentWorkerViewModel.MainViewModel);
            bool result = await _currentWorkerViewModel.MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                _context.SaveChanges();
                _currentWorkerViewModel.SaveAndCloseCUView(_editableWorker);
            }
        }
        public void ExecuteSelectPhotoCommand(object? obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Добавление изображения | *.png; *.jpg; *.gif";

            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                byte[] image_bytes = File.ReadAllBytes(fileDialog.FileName);
                _editableWorker.Photo = image_bytes;
                OnPropertyChanged(nameof(EditableWorker));
            }
        }

        //Constructor
        public EditWorkerViewModel(WorkerModel selectedWorker, WorkerViewModel currentWorkerViewModel)
        {
            _currentWorkerViewModel = currentWorkerViewModel;
            _selectedWorker = selectedWorker;
            _editableWorker = _workerRepository.GetById(selectedWorker.WorkerId, _context);

            WorkerPostList = _postRepository.GetByAll(_context);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            SelectPhotoCommand = new ViewModelCommand(ExecuteSelectPhotoCommand);
        }
    }
}