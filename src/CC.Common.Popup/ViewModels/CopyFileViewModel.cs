using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using CC.Common.Infrastructure.Events;
using CC.Common.Infrastructure.Models;
using CC.Common.Popup.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CC.Common.Popup.ViewModels
{
    public class CopyFileViewModel : BindableBase, IInteractionRequestAware
    {
        public Action FinishInteraction { get; set; }

        public DelegateCommand AcceptCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private ObservableCollection<FileModel> _selectedFiles;
        public ObservableCollection<FileModel> SelectedFiles
        {
            get
            {
                return _selectedFiles;
            }
            private set
            {
                _selectedFiles = value;
                RaisePropertyChanged();
            }
        }

        private CopyFileNotification _notification;
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (CopyFileNotification)value); }
        }

        private string _destinationDir;
        public string DestinationDir
        {
            get { return _destinationDir; }
            set
            {
                SetProperty(ref _destinationDir, value);
                RaisePropertyChanged();
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                SetProperty(ref _isEnabled, value);
                RaisePropertyChanged();
            }
        }

        private readonly IEventAggregator _eventAggregator;
        private BackgroundWorker _backgroundWorker;

        public InteractionRequest<INotification> NotificationRequest { get; }

        public CopyFileViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            AcceptCommand = new DelegateCommand(AcceptInteraction);
            CancelCommand = new DelegateCommand(CancelInteraction);

            NotificationRequest = new InteractionRequest<INotification>();

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerCopy);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerComplete);
            _backgroundWorker.WorkerSupportsCancellation = true;

            _eventAggregator.GetEvent<SelectFileChangedEvent>().Subscribe(files => SelectedFiles = new ObservableCollection<FileModel>(files));
            _eventAggregator.GetEvent<DirectoryChangedEvent>().Subscribe(directory => DestinationDir = directory);

            IsEnabled = true;
        }

        private void CancelInteraction()
        {
            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }

            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private void AcceptInteraction()
        {
            IsEnabled = false;
            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                NotificationRequest.Raise(new Notification {Content = e.Error.Message, Title = "Error"});
            else
            {
                _eventAggregator.GetEvent<FileListUpdatedEvent>().Publish();

                _notification.Confirmed = true;
                FinishInteraction?.Invoke();
            }
        }

        private void BackgroundWorkerCopy(object sender, DoWorkEventArgs e)
        {
            foreach (var selectedFile in SelectedFiles)
            {
                if (selectedFile.Extension != "dir")
                {
                    File.Copy(selectedFile.Path, DestinationDir + "\\" + selectedFile.Name, true);
                }
                else
                {
                    DirectoryCopy(selectedFile.Path, DestinationDir, true);
                }
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(destDirName);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}