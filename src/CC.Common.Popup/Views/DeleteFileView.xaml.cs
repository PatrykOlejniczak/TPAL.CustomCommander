using System.Windows;
using System.Windows.Controls;
using CC.Common.Infrastructure.Events;
using Prism.Events;

namespace CC.Common.Popup.Views
{
    /// <summary>
    /// Interaction logic for DeleteFileView.xaml
    /// </summary>
    public partial class DeleteFileView : UserControl
    {
        public static readonly DependencyProperty EventAggregatorProperty
                = DependencyProperty.Register("EventAggregator", typeof(IEventAggregator), typeof(DeleteFileView));

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

        public DeleteFileView()
        {
            InitializeComponent();
        }


        private void UpdateTranslations()
        {
            AcceptButton.Content = Properties.Resources.Accept;
            CancelButton.Content = Properties.Resources.Cancel;
            QuoteTextBlock.Text = Properties.Resources.DeleteQuote;
        }
    }
}
