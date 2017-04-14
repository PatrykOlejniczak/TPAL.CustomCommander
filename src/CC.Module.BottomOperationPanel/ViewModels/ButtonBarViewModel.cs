using System.Globalization;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace CC.Module.BottomOperationPanel.ViewModels
{
    public class ButtonBarViewModel
    {
        public ICommand CopyFileCommand { get; }

        private IRegionManager _regionManager;
        private IUnityContainer _unityContainer;
        private IModuleManager _moduleManager;

        public ButtonBarViewModel(
            IRegionManager regionManager, IModuleManager moduleManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _moduleManager = moduleManager;
            _unityContainer = container;

            CopyFileCommand = new DelegateCommand(ExecuteCopyFileCommand);
        }

        private void ExecuteCopyFileCommand()
        {
            
        }

        public ICommand MoveFileCommand
        {
            get
            {
                return null;
            }
        }

        public ICommand NewFolderCommand
        {
            get
            {
                return null;
            }
        }

        public ICommand DeleteFileCommand
        {
            get
            {
                return null;
            }
        }

        public ICommand ExitProgramCommand
        {
            get
            {
                return null;
            }
        }
    }
}
