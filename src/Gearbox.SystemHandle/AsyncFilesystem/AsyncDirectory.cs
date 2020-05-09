using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public class AsyncDirectory : IAsyncDirectory
    {
        public Task<List<string>> GetDirectories(string dir, string filter = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Task.Run(() => Directory.GetDirectories(dir, filter, searchOption).ToList());
        }

        public Task<List<string>> GetFiles(string dir, string filter = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Task.Run(() => Directory.GetFiles(dir, filter, searchOption).ToList());
        }

        public async Task Delete(string dir, bool recursive = false)
        {
            var directoryContents = await GetFiles(dir, "*", SearchOption.AllDirectories);

            if (!directoryContents.Any())
            {
                Directory.Delete(dir);
                return;
            }

            foreach (var file in directoryContents)
            {
                await using var fileStream = new FileStream(file, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Delete, 1,
                                                      FileOptions.DeleteOnClose | FileOptions.Asynchronous);
                await fileStream.FlushAsync();
            }

            Directory.Delete(dir, true);
        }
    }
}
