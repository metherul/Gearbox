using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public class AsyncFilesystem : IAsyncFilesystem
    {
        public Task<List<string>> GetFiles(string dir, string filter = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Task.Run(() => Directory.GetFiles(dir, filter, searchOption).ToList());
        }

        public Task<List<string>> GetDirectories(string dir, string filter = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Task.Run(() => Directory.GetDirectories(dir, filter, searchOption).ToList());
        }

        public async Task DeleteDirectory(string dir)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<string> MakeCrc32(string file)
        {
            throw new System.NotImplementedException();
        }
    }
}