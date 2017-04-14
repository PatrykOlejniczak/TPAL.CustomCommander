using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace CC.Module.Navigation
{
    public class NavigationModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public NavigationModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            /*
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");

            _eventAggregator.GetEvent<LanguageChangedEvent>().Publish();
            */
        }
    }
}
