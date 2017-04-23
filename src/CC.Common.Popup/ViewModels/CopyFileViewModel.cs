﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using CC.Common.Infrastructure.Events;
using CC.Common.Infrastructure.Models;
using CC.Common.Popup.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace CC.Common.Popup.ViewModels
{
    public class CopyFileViewModel : BindableBase, IInteractionRequestAware
    {
        public Action FinishInteraction { get; set; }

        public DelegateCommand AcceptCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private ObservableCollection<FileModel> _selectedFiles;
        public ObservableCollection<FileModel> SelectedFiles
        {
            get
            {
                return _selectedFiles;
            }
            private set
            {
                _selectedFiles = value;
                RaisePropertyChanged();
            }
        }

        private CopyFileNotification _notification;
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (CopyFileNotification)value); }
        }

        private string _destinationDir;
        public string DestinationDir
        {
            get { return _destinationDir; }
            set
            {
                SetProperty(ref _destinationDir, value);
                RaisePropertyChanged();
            }
        }

        private readonly IEventAggregator _eventAggregator;
        private BackgroundWorker _backgroundWorker;

        public CopyFileViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            AcceptCommand = new DelegateCommand(AcceptInteraction);
            CancelCommand = new DelegateCommand(CancelInteraction);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerCopy);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerComplete);
            _backgroundWorker.WorkerSupportsCancellation = true;

            _eventAggregator.GetEvent<SelectFileChangedEvent>().Subscribe(files => SelectedFiles = new ObservableCollection<FileModel>(files));
            _eventAggregator.GetEvent<DirectoryChangedEvent>().Subscribe(directory => DestinationDir = directory);
        }

        private void CancelInteraction()
        {
            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }

            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private void AcceptInteraction()
        {
            if (Directory.Exists(DestinationDir))
            {
                _backgroundWorker.RunWorkerAsync();
            }
            else
            {
                //TODO directory exists pop-up error
            }
        }

        private void BackgroundWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            _eventAggregator.GetEvent<FileListUpdatedEvent>().Publish();

            _notification.Confirmed = true;
            FinishInteraction?.Invoke();
        }

        private void BackgroundWorkerCopy(object sender, DoWorkEventArgs e)
        {
            foreach (var selectedFile in SelectedFiles)
            {
                if (selectedFile.Extension != "dir")
                {
                    File.Copy(selectedFile.Path, DestinationDir + "\\" + selectedFile.Name, true);
                }
                else
                {
                    DirectoryCopy(selectedFile.Path, DestinationDir, true);
                }
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.

            Directory.CreateDirectory(destDirName);


            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}