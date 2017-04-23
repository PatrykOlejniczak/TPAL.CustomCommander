using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CC.Common.Infrastructure.Models;
using CC.Common.Infrastructure.ShellApi;

namespace CC.Common.Infrastructure.DataProviders.Implementations
{
    public class FileProvider : IFileProvider
    {
        public List<FileModel> GetFilesFromLocation(string path)
        {
            var tempFiles = Directory.GetFiles(path);

            List<FileModel> files = new List<FileModel>();

            foreach (var temp in tempFiles)
            {
                var icon = IconReader.GetFileIcon(
                                        Path.GetFullPath(temp), 
                                        IconReader.IconSize.Small, false).ToBitmap();
                IntPtr hBitmap = icon.GetHbitmap();

                ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                                                    hBitmap, IntPtr.Zero, Int32Rect.Empty,
                                                    BitmapSizeOptions.FromEmptyOptions());

                files.Add(new FileModel()
                {
                    Icon = wpfBitmap,
                    Name = temp.Substring(temp.LastIndexOf("\\", StringComparison.Ordinal) + 1),
                    Extension = new FileInfo(temp).Extension,
                    Size = new FileInfo(temp).Length,
                    CreatedDate = File.GetCreationTime(temp)
                });
            }

            return files;
        }

        public List<FileModel> GetDirectoriesFromLocation(string path)
        {
            List<FileModel> files = new List<FileModel>();

            var backResource = GetReturnPath(path);
            if (backResource != null)
            {
                files.Add(backResource);
            }

            var tempDirs = Directory.GetDirectories(path);

            foreach (var temp in tempDirs)
            {
                var icon = IconReader.GetFolderIcon(
                                        Path.GetFullPath(temp),
                                        IconReader.IconSize.Small,
                                        IconReader.FolderType.Closed).ToBitmap();
                IntPtr hBitmap = icon.GetHbitmap();

                ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                                                    hBitmap, IntPtr.Zero, Int32Rect.Empty,
                                                    BitmapSizeOptions.FromEmptyOptions());

                files.Add(new FileModel()
                {
                    Icon = wpfBitmap,
                    Name = temp.Substring(temp.LastIndexOf("\\", StringComparison.Ordinal) + 1),
                    Size = null,
                    Extension = "dir",
                    CreatedDate = Directory.GetLastWriteTime(temp)
                });
            }

            return files;
        }

        private FileModel GetReturnPath(string path)
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(path);

            if (rootDirectory.Parent != null)
            {
                var returnFile = new FileModel()
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/CC.Common.Infrastructure;Component/Images/arrow_back.png")),
                    Name = "..\\",
                    Extension = null,
                    Size = null,
                    CreatedDate = null
                };
                return returnFile;
            }

            return null;
        }
    }
}
