using System.IO;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Writer;

namespace Gearbox.Sdk.Index
{
    public class FileEntryFactory : IFileEntryFactory
    {
        private readonly IAsyncFilesystem _asyncFilesystem;
        private readonly IAsyncHash _hashers;

        public FileEntryFactory(IAsyncFilesystem asyncFilesystem, IAsyncHash hashers)
        {
            _asyncFilesystem = asyncFilesystem;
            _hashers = hashers;
        }

        public Task<IFileEntry> Create(string filePath)
        {
            return Create(filePath, string.Empty, FileHashType.Md5);
        }

        public Task<IFileEntry> Create(string filePath, string relativeTo)
        {
            return Create(filePath, relativeTo, FileHashType.Md5);
        }

        public async Task<IFileEntry> Create(string filePath, string relativeTo, FileHashType fileHashType)
        {
            var fileInfo = new FileInfo(filePath);
            var fileEntry = new FileEntry()
            {
                Name = fileInfo.Name,
                FilePath = string.IsNullOrWhiteSpace(relativeTo) switch
                {
                    true => fileInfo.FullName,
                    false => Path.GetRelativePath(relativeTo, fileInfo.FullName)
                },
                Hash = fileHashType switch
                {
                    FileHashType.Md5 => await _hashers.MakeMd5(fileInfo.FullName),
                    FileHashType.Crc32 => await _hashers.MakeCrc32(fileInfo.FullName),
                    _ => await _hashers.MakeMd5(fileInfo.FullName)
                },
                LastWriteTimeUtc = fileInfo.LastWriteTimeUtc,
                Length = fileInfo.Length
            };

            return fileEntry;
        }
    }
}