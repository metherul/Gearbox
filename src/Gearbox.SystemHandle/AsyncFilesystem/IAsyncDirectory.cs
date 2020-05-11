using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public interface IAsyncDirectory : IDirectory
    {
        Task<List<string>> GetFilesAsync(string dir, string filter = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly);
        Task<List<string>> GetDirectoriesAsync(string dir, string filter = "*",
           SearchOption searchOption = SearchOption.TopDirectoryOnly);
        Task DeleteAsync(string dir, bool recursive = false);
    }
}