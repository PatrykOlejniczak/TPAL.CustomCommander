using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace CC.Module.BottomOperationPanel.ViewModels
{
    public class ButtonBarViewModel : BindableBase
    {
        public ICommand CopyFileCommand { get; }
        public ICommand MoveFileCommand { get; }
        public ICommand NewFolderCommand { get; }
        public ICommand DeleteFileCommand { get; }
        public ICommand ExitProgramCommand { get; }

        private readonly IUnityContainer _unityContainer;
        private readonly IEventAggregator _eventAggregator;

        public ButtonBarViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _unityContainer = container;
            _eventAggregator = eventAggregator;

            CopyFileCommand = new DelegateCommand(ExecuteCopyFileCommand);
            MoveFileCommand = new DelegateCommand(ExecuteMoveFileCommand);
            NewFolderCommand = new DelegateCommand(ExecuteNewFolderCommand);
            DeleteFileCommand = new DelegateCommand(ExecuteDeleteFileCommand);
            ExitProgramCommand = new DelegateCommand(ExecuteExitProgramCommand);

        }

        private void ExecuteCopyFileCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteMoveFileCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteNewFolderCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteDeleteFileCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteExitProgramCommand()
        {
            throw new NotImplementedException();
        }
    }
}
