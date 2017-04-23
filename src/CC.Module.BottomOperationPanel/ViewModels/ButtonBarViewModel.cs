using System.Windows;
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
        public InteractionRequest<CopyFileNotification> CopyFileNotification { get; }
        public ICommand CopyFileCommand { get; }
        public InteractionRequest<MoveFileNotification> MoveFileNotification { get; }
        public ICommand MoveFileCommand { get; }
        public InteractionRequest<NewFileNotification> NewFileNotification { get; }
        public ICommand NewFolderCommand { get; }
        public InteractionRequest<DeleteFileNotification> DeleteFileNotification { get; }
        public ICommand DeleteFileCommand { get; }

        public ICommand ExitProgramCommand { get; }

        public ButtonBarViewModel()
        {
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
            CopyFileNotification.Raise(new CopyFileNotification { Title = "Copy file" }, r => { });
        }

        private void ExecuteMoveFileCommand()
        {
            MoveFileNotification.Raise(new MoveFileNotification { Title = "Move file" }, r => { });
        }

        private void ExecuteNewFolderCommand()
        {
            NewFileNotification.Raise(new NewFileNotification { Title = "Create folder" }, r => { });
        }

        private void ExecuteDeleteFileCommand()
        {
            DeleteFileNotification.Raise(new DeleteFileNotification { Title = "Delete file" }, r => { });
        }

        private void ExecuteExitProgramCommand()
        {
            Application.Current.Shutdown();
        }
    }
}