using System.Windows;
using CustomCommander.Bootstrapper;

namespace CustomCommander
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new CustomCommanderBootstrapper();
            bootstrapper.Run();
        }
    }
}
