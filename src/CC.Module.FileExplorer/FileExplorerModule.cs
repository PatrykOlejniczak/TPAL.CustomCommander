using CC.Module.FileExplorer.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace CC.Module.FileExplorer
{
    public class FileExplorerModule : IModule
    {
        private const string ModuleRegionName = "FileExplorerRegionLeft";

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public FileExplorerModule(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ModuleRegionName,
                    () => _container.Resolve<FileExplorerView>());
        }
    }
}
