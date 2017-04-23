using System;
using System.IO;
using System.Linq;
using System.Windows.Data;
using Prism.Mvvm;

namespace CC.Module.FileExplorer.ViewModels
{
    public class DriverManagerViewModel : BindableBase
    {
        public event Action<string> DriverChangedEvent;

        public DriverManagerViewModel()
        {
            var drivers = DriveInfo.GetDrives();

            _driverListView = new CollectionView(drivers.Select(d => d.Name).ToList());
        }

        private readonly CollectionView _driverListView;
        private string _selectedDriver;

        public CollectionView DriverListView
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