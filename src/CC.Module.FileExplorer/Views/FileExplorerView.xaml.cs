using System.Windows.Controls;
using Prism.Events;


namespace CC.Module.FileExplorer.Views
{
    /// <summary>
    /// Interaction logic for FileExplorerView.xaml
    /// </summary>
    public partial class FileExplorerView : UserControl
    {
        public FileExplorerView(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            FileTreeViewLeft.EventAggregator = eventAggregator;
            FileTreeViewRight.EventAggregator = eventAggregator;
        }
    }
}
