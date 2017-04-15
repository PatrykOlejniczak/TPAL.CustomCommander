using CC.Module.BottomOperationPanel.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace CC.Module.BottomOperationPanel
{
    public class BottomOperationPanelModule : IModule
    {
        private const string ModuleRegionName = "BottomOperationPanelRegion";

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public BottomOperationPanelModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ModuleRegionName,
                                           () => _container.Resolve<ButtonBar>());
        }
    }
}
