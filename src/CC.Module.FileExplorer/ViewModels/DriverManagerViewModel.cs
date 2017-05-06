using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using CC.Common.Infrastructure.Events;
using Prism.Events;
using Prism.Mvvm;

namespace CC.Module.FileExplorer.ViewModels
{
    public class DriverManagerViewModel : BindableBase
    {
        public event Action<string> DriverChangedEvent;

        private readonly IEventAggregator _eventAggregator;

        public DriverManagerViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            var drivers = DriveInfo.GetDrives();

            _eventAggregator.GetEvent<DriverListChangedEvent>().Subscribe(UpdateDrivers);

            _driverListView = new ObservableCollection<string>(drivers.Select(d => d.Name).ToList());
            SelectedDriver = "C:\\";
        }

        private void UpdateDrivers()
        {
            var drivers = DriveInfo.GetDrives();

            _driverListView = new ObservableCollection<string>(drivers.Select(d => d.Name).ToList());
            RaisePropertyChanged("DriverListView");
            SelectedDriver = "C:\\";
        }

        private ObservableCollection<string> _driverListView;
        private string _selectedDriver;

        public ObservableCollection<string> DriverListView
        {
            get { return _driverListView; }
        }

        public string SelectedDriver
        {
            get { return _selectedDriver; }
            set
            {
                if (_selectedDriver == value) return;
                _selectedDriver = value;
                RaisePropertyChanged();
                DriverChangedEvent?.Invoke(SelectedDriver);
            }
        }
    }
}