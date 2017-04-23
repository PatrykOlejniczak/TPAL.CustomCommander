using System;
using System.IO;
using CC.Common.Infrastructure.Events;
using CC.Common.Popup.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CC.Common.Popup.ViewModels
{
    public class NewFileViewModel : BindableBase, IInteractionRequestAware
    {
        public Action FinishInteraction { get; set; }

        public DelegateCommand AcceptCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private NewFileNotification _notification;
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (NewFileNotification)value); }
        }

        private string _directoryPath;
        public string DirectoryPath
        {
            get { return _directoryPath; }
            set
            {
                SetProperty(ref _directoryPath, value);
                RaisePropertyChanged();
            }
        }

        private string _directoryName;
        public string DirectoryName
        {
            get { return _directoryName; }
            set
            {
                SetProperty(ref _directoryName, value);
                RaisePropertyChanged();
            }
        }

        private readonly IEventAggregator _eventAggregator;

        public NewFileViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            AcceptCommand = new DelegateCommand(AcceptInteraction);
            CancelCommand = new DelegateCommand(CancelInteraction);

            _eventAggregator.GetEvent<DirectoryChangedEvent>().Subscribe(directory => DirectoryPath = directory);
        }

        private void CancelInteraction()
        {
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private void AcceptInteraction()
        {
            var newDirectoryPath = DirectoryPath + "\\" + DirectoryName;

            if (!Directory.Exists(newDirectoryPath))
            {
                Directory.CreateDirectory(newDirectoryPath);

                _eventAggregator.GetEvent<FileListUpdatedEvent>().Publish();

                _notification.Confirmed = true;
                FinishInteraction?.Invoke();
            }
            else
            {
                //TODO directory exists pop-up error
            }
        }
    }
}