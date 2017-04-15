using CC.Module.FileExplorer.Views;
using Microsoft.Practices.Unity;
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

        public FileExplorerModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
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
