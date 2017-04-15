﻿using System.Windows;
using System.Windows.Controls;
using CC.Common.Infrastructure.Events;
using Prism.Events;

namespace CC.Module.FileExplorer.Views
{
    /// <summary>
    /// Interaction logic for FileTreeView.xaml
    /// </summary>
    public partial class FileTreeView : UserControl
    {
        public static readonly DependencyProperty EventAggregatorProperty = DependencyProperty.Register("Song", typeof(IEventAggregator), typeof(IEventAggregator));

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
            LastModificationHeader.Content = Properties.Resources.LastModificationHeader;
        }
    }
}
