using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using CC.Common.Infrastructure.Events;
using CC.Common.Infrastructure.Models;
using CC.Common.Infrastructure.ShellApi;
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

            ((DriverManagerViewModel)DriverManagerView.DataContext).DriverChangedEvent += ((FileTreeViewModel)DataContext).ChangeDriver;

            Window parentWindow = Application.Current.MainWindow;

            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(parentWindow).Handle);
            if (source != null)
            {
                source.AddHook(HwndHandler);
                UsbNotification.RegisterUsbDeviceNotification(source.Handle);
            }
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

        private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == UsbNotification.WmDevicechange)
            {
                switch ((int)wparam)
                {
                    case UsbNotification.DbtDeviceremovecomplete:
                        EventAggregator.GetEvent<DriverListChangedEvent>().Publish();
                        break;
                    case UsbNotification.DbtDevicearrival:
                        EventAggregator.GetEvent<DriverListChangedEvent>().Publish();
                        break;
                }
            }

            handled = false;
            return IntPtr.Zero;
        }
    }
}