using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace CC.Module.FileExplorer
{
    public class FileExplorerModule : IModule
    {
        private const string ModuleRegionName = "";

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public FileExplorerModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
