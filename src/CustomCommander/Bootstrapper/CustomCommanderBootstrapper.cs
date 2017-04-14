using System.Windows;
using CC.Module.BottomOperationPanel;
using CC.Module.BottomOperationPanel.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace CustomCommander.Bootstrapper
{
    public class CustomCommanderBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(BottomOperationPanelModule));
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<ButtonBarViewModel>();

            base.ConfigureContainer();
        }
    }
}