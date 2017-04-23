using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using CC.Common.Infrastructure.DataProviders;
using CC.Common.Infrastructure.Events;
using CC.Common.Infrastructure.Models;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CC.Module.FileExplorer.ViewModels
{
    public class FileTreeViewModel : BindableBase
    {
        private string _actualPath = "c:";

        public ICommand SortFilesCommand { get; }

        private ObservableCollection<FileModel> _observableFiles;
        public ObservableCollection<FileModel> Files
        {
            get
            {
                return _observableFiles;
            }
            set
            {
                _observableFiles = value;
                _filesView = new CollectionViewSource();
                _filesView.Source = _observableFiles;

                OnPropertyChanged("FilesView");
            }
        }

        private CollectionViewSource _filesView;
        public ListCollectionView FilesView => (ListCollectionView)_filesView.View;

        private readonly IEventAggregator _eventAggregator;
        private readonly IFileProvider _fileProvider;

        public InteractionRequest<INotification> NotificationRequest { get; set; }

        public FileTreeViewModel(IEventAggregator eventAggregator, IFileProvider fileProvider)
        {
            _eventAggregator = eventAggregator;
            _fileProvider = fileProvider;

            SortFilesCommand = new DelegateCommand<string>(ExecuteSortFiles);

            ChangeDirectory(string.Empty);

            NotificationRequest = new InteractionRequest<INotification>();

            _eventAggregator.GetEvent<FileListUpdatedEvent>()
                            .Subscribe(() => ChangeDirectory(""));
            _eventAggregator.GetEvent<LanguageChangedEvent>()
                            .Subscribe(() => ChangeSelectFile(""));
        }

        public void ActivateControl()
        {
            _eventAggregator.GetEvent<DirectoryChangedEvent>()
                            .Publish(_actualPath);
            _eventAggregator.GetEvent<SelectFileChangedEvent>()
                            .Publish(Files.ToList().FindAll(file => file.IsSelected));
        }

        public void ChangeSelectFile(string fileName)
        {
            Files.Where(f => f.Name.Equals(fileName)).ForEach(f => f.IsSelected = !f.IsSelected);
            Files = Files;

            _eventAggregator.GetEvent<SelectFileChangedEvent>()
                            .Publish(Files.ToList().FindAll(file => file.IsSelected));
        }

        public void ChangeDirectory(string path)
        {
            var desitinationPath = Path.GetFullPath(_actualPath + "\\" + path);

            Files = new ObservableCollection<FileModel>();

            try
            {
                Files.AddRange(_fileProvider.GetFilesFromLocation(Path.GetFullPath(desitinationPath)));
                Files.AddRange(_fileProvider.GetDirectoriesFromLocation(Path.GetFullPath(desitinationPath)));

                _actualPath = desitinationPath;
            }
            catch (UnauthorizedAccessException exception)
            {
                NotificationRequest.Raise(new Notification { Content = exception.Message, Title = "Error" });
                ChangeDirectory("");
            }

            _eventAggregator.GetEvent<DirectoryChangedEvent>().Publish(_actualPath);
        }

        private bool _sortAscending = true;
        private void ExecuteSortFiles(string columnName)
        {
            string sortColumn = columnName;
            _filesView.SortDescriptions.Clear();

            if (_sortAscending)
            {
                _filesView.SortDescriptions.Add(new SortDescription(sortColumn, ListSortDirection.Ascending));
                _sortAscending = false;
            }
            else
            {
                _filesView.SortDescriptions.Add(new SortDescription(sortColumn, ListSortDirection.Descending));
                _sortAscending = true;
            }
        }

        public void ChangeDriver(string driver)
        {
            var desitinationPath = Path.GetFullPath(driver);

            Files = new ObservableCollection<FileModel>();

            try
            {
                Files.AddRange(_fileProvider.GetFilesFromLocation(Path.GetFullPath(desitinationPath)));
                Files.AddRange(_fileProvider.GetDirectoriesFromLocation(Path.GetFullPath(desitinationPath)));

                _actualPath = desitinationPath;
            }
            catch (UnauthorizedAccessException exception)
            {
                NotificationRequest.Raise(new Notification { Content = exception.Message, Title = "Error" });
                ChangeDirectory("");
            }
            catch (Exception exception)
            {
                NotificationRequest.Raise(new Notification { Content = exception.Message, Title = "Error" });
            }
            _eventAggregator.GetEvent<DirectoryChangedEvent>()
                .Publish(_actualPath);
        }
    }
}