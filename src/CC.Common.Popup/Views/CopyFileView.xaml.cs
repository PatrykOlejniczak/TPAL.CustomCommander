using System.Windows;
using System.Windows.Controls;
using CC.Common.Infrastructure.Events;
using Prism.Events;

namespace CC.Common.Popup.Views
{
    /// <summary>
    /// Interaction logic for CopyFileView.xaml
    /// </summary>
    public partial class CopyFileView : UserControl
    {
        public static readonly DependencyProperty EventAggregatorProperty
        = DependencyProperty.Register("EventAggregator", typeof(IEventAggregator), typeof(CopyFileView));

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

        public CopyFileView()
        {
            InitializeComponent();
        }

        private void UpdateTranslations()
        {
            AcceptButton.Content = Properties.Resources.Accept;
            CancelButton.Content = Properties.Resources.Cancel;
        }
    }
}
