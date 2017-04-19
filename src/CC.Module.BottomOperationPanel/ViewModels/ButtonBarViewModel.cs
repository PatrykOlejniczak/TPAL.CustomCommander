using System;
using System.Windows.Input;
using CC.Common.Popup.Notifications;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace CC.Module.BottomOperationPanel.ViewModels
{
    public class ButtonBarViewModel : BindableBase
    {
        public InteractionRequest<CopyFileNotification> CopyFileNotification { get; set; }
        public ICommand CopyFileCommand { get; }
        public InteractionRequest<MoveFileNotification> MoveFileNotification { get; set; }
        public ICommand MoveFileCommand { get; }
        public InteractionRequest<NewFileNotification> NewFileNotification { get; set; }
        public ICommand NewFolderCommand { get; }
        public InteractionRequest<DeleteFileNotification> DeleteFileNotification { get; set; }
        public ICommand DeleteFileCommand { get; }

        public ICommand ExitProgramCommand { get; }

        private readonly IUnityContainer _unityContainer;
        private readonly IEventAggregator _eventAggregator;

        public ButtonBarViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _unityContainer = container;
            _eventAggregator = eventAggregator;

            CopyFileNotification = new InteractionRequest<CopyFileNotification>();
            CopyFileCommand = new DelegateCommand(ExecuteCopyFileCommand);

            MoveFileNotification = new InteractionRequest<MoveFileNotification>();
            MoveFileCommand = new DelegateCommand(ExecuteMoveFileCommand);

            NewFileNotification = new InteractionRequest<NewFileNotification>();
            NewFolderCommand = new DelegateCommand(ExecuteNewFolderCommand);

            DeleteFileNotification = new InteractionRequest<DeleteFileNotification>();
            DeleteFileCommand = new DelegateCommand(ExecuteDeleteFileCommand);

            ExitProgramCommand = new DelegateCommand(ExecuteExitProgramCommand);
        }

        private void ExecuteCopyFileCommand()
        {
            CopyFileNotification.Raise(new CopyFileNotification { Title = "Custom Notification" }, r =>
            {

            });
        }

        private void ExecuteMoveFileCommand()
        {
            MoveFileNotification.Raise(new MoveFileNotification { Title = "Custom Notification" }, r =>
            {

            });
        }

        private void ExecuteNewFolderCommand()
        {
            NewFileNotification.Raise(new NewFileNotification { Title = "Custom Notification" }, r =>
            {

            });
        }

        private void ExecuteDeleteFileCommand()
        {
            DeleteFileNotification.Raise(new DeleteFileNotification { Title = "Custom Notification" }, r =>
            {

            });
        }

        private void ExecuteExitProgramCommand()
        {
            throw new NotImplementedException();
        }
    }
}
