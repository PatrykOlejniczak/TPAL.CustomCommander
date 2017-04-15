using System.Globalization;
using System.Threading;
using System.Windows.Input;
using CC.Common.Infrastructure.Events;
using Prism.Events;
using Prism.Commands;

namespace CC.Module.TopOperationPanel.ViewModels
{
    public class LanguagePanelViewModel
    {
        public ICommand ChangeApplicationLanguageCommand { get; }

        private readonly IEventAggregator _eventAggregator;
        private string _actualLangague;

        public LanguagePanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ChangeApplicationLanguageCommand =
                new DelegateCommand<string>(ExecuteChangeApplicationLanguage);
        }

        private void ExecuteChangeApplicationLanguage(string language)
        {
            if (_actualLangague == language)
                return;

            _actualLangague = language;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            _eventAggregator.GetEvent<LanguageChangedEvent>().Publish();
        }
    }
}