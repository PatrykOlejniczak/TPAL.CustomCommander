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
        public List<FileModel> Files { get; private set; }

        public List<FileModel> GetFilesFromLocation(string path)
        {
            var tempFiles = Directory.GetFiles(path);
            var tempDirs = Directory.GetDirectories(path);

            Files = new List<FileModel>();

            foreach (var t in tempFiles)
            {
                var icon = IconReader.GetFileIcon(t, IconReader.IconSize.Small, false).ToBitmap();
                IntPtr hBitmap = icon.GetHbitmap();
                ImageSource wpfBitmap =
     Imaging.CreateBitmapSourceFromHBitmap(
          hBitmap, IntPtr.Zero, Int32Rect.Empty,
          BitmapSizeOptions.FromEmptyOptions());
                Files.Add(new FileModel() { Icon = wpfBitmap, Name = t.Substring(t.LastIndexOf("\\", StringComparison.Ordinal) + 1), Extension = new FileInfo(t).Extension, Size = new FileInfo(t).Length, LastModification = File.GetLastWriteTime(t) });
            }

            foreach (var t in tempDirs)
            {
                var icon = IconReader.GetFolderIcon(t, IconReader.IconSize.Small, IconReader.FolderType.Closed).ToBitmap();
                IntPtr hBitmap = icon.GetHbitmap();
                ImageSource wpfBitmap =
     Imaging.CreateBitmapSourceFromHBitmap(
          hBitmap, IntPtr.Zero, Int32Rect.Empty,
          BitmapSizeOptions.FromEmptyOptions());

                Files.Add(new FileModel() { Icon = wpfBitmap, Name = t.Substring(t.LastIndexOf("\\", StringComparison.Ordinal) + 1), Extension = "dir", LastModification = Directory.GetLastWriteTime(t) });
            }

            return Files;
        }
    }
}
