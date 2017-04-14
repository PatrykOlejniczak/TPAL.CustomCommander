using System.Windows;
using Microsoft.Practices.Unity;
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
    }
}