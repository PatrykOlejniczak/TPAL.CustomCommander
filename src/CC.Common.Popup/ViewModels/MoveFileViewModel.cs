using System;
using CC.Common.Popup.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CC.Common.Popup.ViewModels
{
    public class MoveFileViewModel : BindableBase, IInteractionRequestAware
    {
        private MoveFileNotification _notification;

        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (MoveFileNotification)value); }
        }

        public DelegateCommand SelectItemCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public MoveFileViewModel()
        {
            SelectItemCommand = new DelegateCommand(AcceptSelectedItem);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        private void CancelInteraction()
        {
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private void AcceptSelectedItem()
        {
            _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }

        public Action FinishInteraction { get; set; }
    }
}