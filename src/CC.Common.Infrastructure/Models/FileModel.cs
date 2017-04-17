using System;
using System.Windows.Media;

namespace CC.Common.Infrastructure.Models
{
    public class FileModel
    {
        public ImageSource Icon { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long? Size { get; set; }
        public DateTime? LastModification { get; set; }

        public bool IsSelected { get; set; }
    }
}