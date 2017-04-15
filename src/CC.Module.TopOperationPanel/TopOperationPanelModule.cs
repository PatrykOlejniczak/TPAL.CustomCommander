using CC.Module.TopOperationPanel.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace CC.Module.TopOperationPanel
{
    public class TopOperationPanelModule : IModule
    {
        private const string ModuleRegionName = "TopOperationPanelRegion";

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public TopOperationPanelModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ModuleRegionName,
                                () => _container.Resolve<TopOperationPanelView>());
        }
    }
}
