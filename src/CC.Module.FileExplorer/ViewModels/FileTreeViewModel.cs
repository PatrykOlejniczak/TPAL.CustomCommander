using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Windows.Input;
using CC.Module.FileExplorer.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace CC.Module.FileExplorer.ViewModels
{
    public class FileTreeViewModel : BindableBase
    {
        public ICommand SortFilesCommand { get; }

        private ObservableFiles _observableFiles;
        public ObservableFiles Files
        {
            private get
            {
                return _observableFiles;
            }
            set
            {
                _observableFiles = value;
                _filesView = new CollectionViewSource();
                _filesView.Source = _observableFiles;
            }
        }

        private CollectionViewSource _filesView;
        public ListCollectionView FilesView => (ListCollectionView)_filesView.View;                      

        public FileTreeViewModel()
        {
            var tempFiles = Directory.GetFiles("c:\\");
            var tempDirs = Directory.GetDirectories("c:\\");

            Files = new ObservableFiles();

            foreach (var t in tempFiles)
            {
                Files.Add(new FileModel() {Name = t.Substring(t.LastIndexOf("\\", StringComparison.Ordinal) + 1), Extension = new FileInfo(t).Extension, Size = new FileInfo(t).Length, LastModyfication = File.GetLastWriteTime(t) });
            }

            foreach (var t in tempDirs)
            {
                Files.Add(new FileModel() { Name = t.Substring(t.LastIndexOf("\\", StringComparison.Ordinal) + 1), Extension = "dir", LastModyfication = Directory.GetLastWriteTime(t) });
            }

            SortFilesCommand = new DelegateCommand<string>(ExecuteSortFiles);
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