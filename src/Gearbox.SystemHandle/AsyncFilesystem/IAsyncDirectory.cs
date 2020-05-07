using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public interface IAsyncDirectory
    {

        Task<List<string>> GetFiles(string dir, string filter = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly);
        Task<List<string>> GetDirectories(string dir, string filter = "*",
           SearchOption searchOption = SearchOption.TopDirectoryOnly);
        
        // Ignore this lmao
    }
}