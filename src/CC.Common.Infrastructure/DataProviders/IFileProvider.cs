using System.Collections.Generic;
using CC.Common.Infrastructure.Models;

namespace CC.Common.Infrastructure.DataProviders
{
    public interface IFileProvider
    {
        List<FileModel> GetFilesFromLocation(string path);
        List<FileModel> GetDirectoriesFromLocation(string path);
    }
}
