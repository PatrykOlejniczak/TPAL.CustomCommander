using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using CC.Common.Infrastructure.DataProviders;
using CC.Common.Infrastructure.Events;
using CC.Common.Infrastructure.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Prism.Events;

namespace CC.Module.FileExplorer.ViewModels
{
    public class FileTreeViewModel : BindableBase
    {
        public ICommand SortFilesCommand { get; }

        private ObservableCollection<FileModel> _observableFiles;
        private ObservableCollection<FileModel> Files
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

        public FileTreeViewModel(IEventAggregator eventAggregator, IFileProvider fileProvider)
        {
            _eventAggregator = eventAggregator;
            _fileProvider = fileProvider;

            SortFilesCommand = new DelegateCommand<string>(ExecuteSortFiles);

            ChangeDirectory(string.Empty);            

            _eventAggregator.GetEvent<DirectoryChangedEvent>().Subscribe(ChangeDirectory);
            _eventAggregator.GetEvent<SelectFileEvent>().Subscribe(SelectFile);
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(() => ChangeDirectory(""));
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly IFileProvider _fileProvider;

        private string _actualPath = "c:";
        private void SelectFile(string fileName)
        {
            var file = Files.Single(f => f.Name.Equals(fileName));
            file.IsSelected = !file.IsSelected;
        }

        private void ChangeDirectory(string path)
        {
            _actualPath = Path.GetFullPath(_actualPath + "\\" + path);

            Files = new ObservableCollection<FileModel>();

            Files.AddRange(_fileProvider.GetFilesFromLocation(Path.GetFullPath(_actualPath)));
            Files.AddRange(_fileProvider.GetDirectoriesFromLocation(Path.GetFullPath(_actualPath)));
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
    }
}