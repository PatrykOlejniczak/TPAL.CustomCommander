using System;

namespace CC.Module.FileExplorer.Models
{
    public class FileModel
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public DateTime LastModyfication { get; set; }
    }
}