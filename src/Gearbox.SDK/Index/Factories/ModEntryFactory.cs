using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Writer;

namespace Gearbox.Sdk.Index.Factories
{
    public class ModEntryFactory : IModEntryFactory
    {
        private readonly IFileEntryFactory _fileEntryFactory;
        private readonly IAsyncDirectory _asyncDirectory;
        private readonly IDirectoryInfoFactory _directoryInfoFactory;
        private readonly IAsyncHash _asyncHash;

        public ModEntryFactory(IFileEntryFactory fileEntryFactory, IAsyncDirectory asyncDirectory, IDirectoryInfoFactory directoryInfoFactory, IAsyncHash asyncHash)
        {
            _fileEntryFactory = fileEntryFactory;
            _asyncDirectory = asyncDirectory;
            _directoryInfoFactory = directoryInfoFactory;
            _asyncHash = asyncHash;
        }

        public async Task<IModEntry> Create(string modDir)
        {
            var dirInfo = _directoryInfoFactory.FromDirectoryName(modDir);

            var contents = await _asyncDirectory.GetFilesAsync(modDir, "*", SearchOption.AllDirectories);
            var entryTasks = new List<Task<IFileEntry>>();

            foreach (var file in contents)
            {
                var fileEntry = _fileEntryFactory.Create(file, modDir);
                entryTasks.Add(fileEntry);
            }
            
            var modEntry = new ModEntry()
            {
                Name = dirInfo.Name,
                Directory = modDir,
                FilesystemHash = await _asyncHash.MakeFilesystemHash(modDir)
            };

            await Task.WhenAll(entryTasks);
            modEntry.FileEntries = entryTasks.Select(x => x.Result).ToList();

            return modEntry;
        }
    }
}