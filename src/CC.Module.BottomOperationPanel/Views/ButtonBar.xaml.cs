using System.Windows.Controls;
using CC.Common.Infrastructure.Events;
using Prism.Events;

namespace CC.Module.BottomOperationPanel.Views
{
    /// <summary>
    /// Interaction logic for ButtonBar.xaml
    /// </summary>
    public partial class ButtonBar : UserControl
    {
        public ButtonBar(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(UpdateTranslations);

            InitializeComponent();
        }

        private void UpdateTranslations()
        {
            CopyButton.Content = Properties.Resources.Copy;
            MoveButton.Content = Properties.Resources.Move;
            NewFolderButton.Content = Properties.Resources.NewFolder;
            DeleteButton.Content = Properties.Resources.Delete;
            ExitButton.Content = Properties.Resources.Exit;
        }
    }
}
