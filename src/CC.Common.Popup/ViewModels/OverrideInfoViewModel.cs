using System;
using CC.Common.Popup.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CC.Common.Popup.ViewModels
{
    public class OverrideInfoViewModel : BindableBase, IInteractionRequestAware
    {
        private OverrideInfoNotification _notification;
        public INotification Notification
        {
            get { return _notification; }
            set
            {
                SetProperty(ref _notification, (OverrideInfoNotification)value);
                FileName = _notification.Content.ToString();
                RememberChoice = false;
            }
        }

        public Action FinishInteraction { get; set; }

        public DelegateCommand AcceptCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                SetProperty(ref _fileName, value);
                RaisePropertyChanged();
            }
        }

        private bool _rememberChoice;
        public bool RememberChoice
        {
            get { return _rememberChoice; }
            set
            {
                SetProperty(ref _rememberChoice, value);
                RaisePropertyChanged();
            }
        }

        public OverrideInfoViewModel()
        {
            AcceptCommand = new DelegateCommand(AcceptInteraction);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        private void AcceptInteraction()
        {
            _notification.RememberChoice = RememberChoice;
            _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }

        private void CancelInteraction()
        {
            _notification.RememberChoice = RememberChoice;
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }
    }
}