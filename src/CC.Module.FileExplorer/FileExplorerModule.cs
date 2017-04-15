using CC.Module.FileExplorer.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace CC.Module.FileExplorer
{
    public class FileExplorerModule : IModule
    {
        private const string ModuleRegionNameLeft = "FileExplorerRegionLeft";
        private const string ModuleRegionNameRight = "FileExplorerRegionRight";

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public FileExplorerModule(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _container = container;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ModuleRegionNameLeft,
                    () => _container.Resolve<FileExplorerView>());
            _regionManager.RegisterViewWithRegion(ModuleRegionNameRight,
                    () => _container.Resolve<FileExplorerView>());
        }
    }
}
