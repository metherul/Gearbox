using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Writer;
using Gearbox.Shared.FsExtensions;

namespace Gearbox.Sdk.Index.Factories
{
    public class ArchiveEntryFactory : IArchiveEntryFactory
    {
        private readonly IAsyncArchive _asyncArchive;
        private readonly IAsyncDirectory _asyncDirectory;
        private readonly IFileInfoFactory _fileInfoFactory;
        private readonly IAsyncHash _asyncHash;
        private readonly IFileEntryFactory _fileEntryFactory;

        public ArchiveEntryFactory(IFileEntryFactory fileEntryFactory, IAsyncArchive archiveHandle,
            IAsyncDirectory asyncDirectory, IFileInfoFactory fileInfoFactory, IAsyncHash asyncHash)
        {
            _fileEntryFactory = fileEntryFactory;
            _asyncArchive = archiveHandle;
            _asyncDirectory = asyncDirectory;
            _fileInfoFactory = fileInfoFactory;
            _asyncHash = asyncHash;
        }

        public async Task<IArchiveEntry> Create(string archivePath)
        {
            var archiveInfo = _fileInfoFactory.FromFileName(archivePath);
            var archiveName = archiveInfo.Name;

            // Extract the contents of the archive to a temp directory.
            var extractDir = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(archiveName));

            // Here we begin extracting the archive and process each file as its decompressed.
            var fileEntries = new List<IFileEntry>();
            var entryTasks = new List<Task<IFileEntry>>();
            
            await foreach (var file in _asyncArchive.ExtractAndYieldOutput(archivePath, extractDir))
            {
                var fileEntry = _fileEntryFactory.Create(file, extractDir, FileHashType.Md5);
                entryTasks.Add(fileEntry);
            }
            
            await Task.WhenAll(entryTasks);

            var archiveEntry = new ArchiveEntry()
            {
                Name = archiveName,
                ArchivePath = archivePath,
                LastModified = archiveInfo.LastWriteTimeUtc,
                FilesystemHash = await _asyncHash.MakeFilesystemHash(extractDir),
                Hash = await _asyncHash.MakeMd5(archivePath),
                FileEntries = entryTasks.Select(x => x.Result).ToList(),
                Length = archiveInfo.Length
            };

            // Delete the extraction directory.
            await _asyncDirectory.DeleteAsync(extractDir);

            return archiveEntry;
        }
    }
}