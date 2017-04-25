using System.Windows;
using System.Windows.Controls;
using CC.Common.Infrastructure.Events;
using Prism.Events;

namespace CC.Common.Popup.Views
{
    /// <summary>
    /// Interaction logic for NewFileView.xaml
    /// </summary>
    public partial class NewFileView : UserControl
    {
        public static readonly DependencyProperty EventAggregatorProperty
= DependencyProperty.Register("EventAggregator", typeof(IEventAggregator), typeof(NewFileView));

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
        public NewFileView()
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