using System;
using CC.Common.Popup.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CC.Common.Popup.ViewModels
{
    public class DeleteFileViewModel : BindableBase, IInteractionRequestAware
    {
        private DeleteFileNotification _notification;

        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (DeleteFileNotification)value); }
        }

        public DelegateCommand SelectItemCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public DeleteFileViewModel()
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