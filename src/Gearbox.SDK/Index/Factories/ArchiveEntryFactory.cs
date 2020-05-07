using System.Collections.Generic;
using System.IO;
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
        private readonly IArchiveHandle _archiveHandle;
        private readonly IAsyncFilesystem _asyncFilesystem;
        private readonly IAsyncHash _hashers;
        private readonly IFileEntryFactory _fileEntryFactory;

        public ArchiveEntryFactory(IFileEntryFactory fileEntryFactory, IArchiveHandle archiveHandle,
            IAsyncFilesystem asyncFilesystem, IAsyncHash hashers)
        {
            _fileEntryFactory = fileEntryFactory;
            _archiveHandle = archiveHandle;
            _asyncFilesystem = asyncFilesystem;
            _hashers = hashers;
        }

        public async Task<IArchiveEntry> Create(string archivePath)
        {
            var archiveInfo = new FileInfo(archivePath);
            var archiveName = archiveInfo.Name;

            // Extract the contents of the archive to a temp directory.
            var extractDir = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(archiveName));

            // Here we begin extracting the archive and process each file as its decompressed.
            var fileEntries = new List<FileEntry>();
            var entryTasks = new List<Task<IFileEntry>>();
            _archiveHandle.FileExtractedEvent += async (sender, path) =>
            {
                var fileEntry = _fileEntryFactory.Create(path, extractDir, FileHashType.Md5);
                entryTasks.Add(fileEntry);
            };
            await _archiveHandle.Extract(extractDir, archivePath);
            await Task.WhenAll(entryTasks);

            var archiveEntry = new ArchiveEntry()
            {
                Name = archiveName,
                ArchivePath = archivePath,
                LastModified = archiveInfo.LastWriteTimeUtc,
                FilesystemHash = await _hashers.MakeFilesystemHash(extractDir),
                Hash = await _hashers.MakeMd5(archivePath),
                FileEntries = entryTasks.Select(x => x.Result).ToList(),
                Length = archiveInfo.Length
            };

            // Delete the extraction directory.
            await _asyncFilesystem.DeleteDirectory(extractDir);

            return archiveEntry;
        }
    }
}