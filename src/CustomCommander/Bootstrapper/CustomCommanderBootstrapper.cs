using System.Windows;
using CC.Common.Infrastructure.DataProviders;
using CC.Common.Infrastructure.DataProviders.Implementations;
using CC.Common.Popup.ViewModels;
using CC.Module.BottomOperationPanel;
using CC.Module.BottomOperationPanel.ViewModels;
using CC.Module.FileExplorer;
using CC.Module.FileExplorer.ViewModels;
using CC.Module.TopOperationPanel;
using CC.Module.TopOperationPanel.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;
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

            moduleCatalog.AddModule(typeof(TopOperationPanelModule));
            moduleCatalog.AddModule(typeof(FileExplorerModule));
            moduleCatalog.AddModule(typeof(BottomOperationPanelModule));
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IEventAggregator, EventAggregator>();

            Container.RegisterType<IFileProvider, FileProvider>();

            Container.RegisterType<CopyFileViewModel>();
            Container.RegisterType<DeleteFileViewModel>();
            Container.RegisterType<MoveFileViewModel>();
            Container.RegisterType<NewFileViewModel>();

            Container.RegisterType<ButtonBarViewModel>();
            Container.RegisterType<LanguagePanelViewModel>();
            Container.RegisterType<FileTreeViewModel>(new PerResolveLifetimeManager());

            base.ConfigureContainer();
        }
    }
}