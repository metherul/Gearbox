using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public class AsyncDirectory : DirectoryWrapper, IAsyncDirectory
    {
        private readonly IFileStreamFactory _fileStream;
        
        public AsyncDirectory(IFileSystem fileSystem) : base(fileSystem)
        {
            _fileStream = fileSystem.FileStream;
        }
        
        public virtual Task<List<string>> GetDirectoriesAsync(string dir, string filter = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Task.Run(() => GetDirectories(dir, filter, searchOption).ToList());
        }

        public virtual Task<List<string>> GetFilesAsync(string dir, string filter = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Task.Run(() => GetFiles(dir, filter, searchOption).ToList());
        }

        public virtual async Task DeleteAsync(string dir, bool recursive = false)
        {
            var directoryContents = await GetFilesAsync(dir, "*", SearchOption.AllDirectories);

            if (!directoryContents.Any())
            {
                Delete(dir, true);
                return;
            }

            foreach (var file in directoryContents)
            {
                await using var fileStream = _fileStream.Create(file, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Delete, 1,
                                                      FileOptions.DeleteOnClose | FileOptions.Asynchronous);
                await fileStream.FlushAsync();
            }

            Delete(dir, true);
        }
    }
}
