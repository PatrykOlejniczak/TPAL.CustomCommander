using System;
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
    public class MoveFileViewModel : BindableBase, IInteractionRequestAware
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

        private MoveFileNotification _notification;
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (MoveFileNotification)value); }
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

        private readonly IEventAggregator _eventAggregator;
        private BackgroundWorker _backgroundWorker;

        public InteractionRequest<INotification> NotificationRequest { get; }

        public MoveFileViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            AcceptCommand = new DelegateCommand(AcceptSelectedItem);
            CancelCommand = new DelegateCommand(CancelInteraction);

            NotificationRequest = new InteractionRequest<INotification>();

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerMove);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerComplete);
            _backgroundWorker.WorkerSupportsCancellation = true;

            _eventAggregator.GetEvent<SelectFileChangedEvent>().Subscribe(files => SelectedFiles = new ObservableCollection<FileModel>(files));
            _eventAggregator.GetEvent<DirectoryChangedEvent>().Subscribe(directory => DestinationDir = directory);
        }

        private void BackgroundWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                NotificationRequest.Raise(new Notification { Content = e.Error.Message, Title = "Error" });
            else
            {
                _eventAggregator.GetEvent<FileListUpdatedEvent>().Publish();

                _notification.Confirmed = true;
                FinishInteraction?.Invoke();
            }
        }

        private void BackgroundWorkerMove(object sender, DoWorkEventArgs e)
        {
            if (Directory.Exists(DestinationDir))
            {
                foreach (var selectedFile in SelectedFiles)
                {
                    if (selectedFile.Extension != "dir")
                    {
                        File.Move(selectedFile.Path, DestinationDir + "\\" + selectedFile.Name);
                    }
                    else
                    {
                        Directory.Move(selectedFile.Path, DestinationDir + "\\" + selectedFile.Name);
                    }
                }
            }
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

        private void AcceptSelectedItem()
        {
            _backgroundWorker.RunWorkerAsync();
        }
    }
}