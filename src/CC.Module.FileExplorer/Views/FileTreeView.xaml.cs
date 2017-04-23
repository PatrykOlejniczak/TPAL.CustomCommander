using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CC.Common.Infrastructure.Events;
using CC.Common.Infrastructure.Models;
using CC.Module.FileExplorer.ViewModels;
using Prism.Events;

namespace CC.Module.FileExplorer.Views
{
    /// <summary>
    /// Interaction logic for FileTreeView.xaml
    /// </summary>
    public partial class FileTreeView : UserControl
    {
        public static readonly DependencyProperty EventAggregatorProperty 
            = DependencyProperty.Register("EventAggregator", typeof(IEventAggregator), typeof(FileTreeView));

        public IEventAggregator EventAggregator
        {
            get
            {
                return (IEventAggregator)GetValue(EventAggregatorProperty);
            }
            set
            {
                SetValue(EventAggregatorProperty, value);
                EventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(UpdateTranslations);
            }
        }

        public FileTreeView()
        {
            InitializeComponent();
        }

        private void UpdateTranslations()
        {
            NameHeader.Content = Properties.Resources.NameHeader;
            ExtensionHeader.Content = Properties.Resources.ExtensionHeader;
            SizeHeader.Content = Properties.Resources.SizeHeader;
            CreatedDateHeader.Content = Properties.Resources.CreatedDateHeader;
        }

        private void RightItemClick(object sender, MouseButtonEventArgs e)
        {
            ((FileTreeViewModel)DataContext).ChangeSelectFile(((FileModel)((ListViewItem)sender).DataContext).Name);
        }

        private void MouseItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((FileTreeViewModel)DataContext).ChangeDirectory(((FileModel)((ListViewItem)sender).DataContext).Name);
        }

        private void LeftItemClick(object sender, MouseButtonEventArgs e)
        {
            ((FileTreeViewModel) DataContext).ActivateControl();
        }
    }
}
