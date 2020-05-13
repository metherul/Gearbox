using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Writer;

namespace Gearbox.Sdk.Index.Factories
{
    public class FileEntryFactory : IFileEntryFactory
    {
        private readonly IFileInfoFactory _fileInfoFactory;
        private readonly IPath _path;
        private readonly IAsyncHash _asyncHash;

        public FileEntryFactory(IFileInfoFactory fileInfoFactory, IPath path, IAsyncHash asyncHash)
        {
            _fileInfoFactory = fileInfoFactory;
            _path = path;
            _asyncHash = asyncHash;
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
            var fileInfo = _fileInfoFactory.FromFileName(filePath);
            var fileEntry = new FileEntry()
            {
                Name = fileInfo.Name,
                FilePath = string.IsNullOrWhiteSpace(relativeTo) switch
                {
                    true => fileInfo.FullName,
                    false => _path.GetRelativePath(relativeTo, fileInfo.FullName)
                },
                Hash = fileHashType switch
                {
                    FileHashType.Md5 => await _asyncHash.MakeMd5(fileInfo.FullName),
                    FileHashType.Crc32 => await _asyncHash.MakeCrc32(fileInfo.FullName),
                    _ => await _asyncHash.MakeMd5(fileInfo.FullName)
                },
                LastWriteTimeUtc = fileInfo.LastWriteTimeUtc,
                Length = fileInfo.Length
            };

            return fileEntry;
        }
    }
}