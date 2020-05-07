using System.Collections.Generic;
using System.IO;
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
        private readonly IAsyncFilesystem _asyncFilesystem;
        private readonly IAsyncHash _hashers;

        public ModEntryFactory(IFileEntryFactory fileEntryFactory, IAsyncFilesystem asyncFilesystem, IAsyncHash hashers)
        {
            _fileEntryFactory = fileEntryFactory;
            _asyncFilesystem = asyncFilesystem;
            _hashers = hashers;
        }

        public async Task<IModEntry> Create(string modDir)
        {
            var dirInfo = new DirectoryInfo(modDir);

            var contents = await _asyncFilesystem.GetFiles(modDir, "*", SearchOption.AllDirectories);
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
                FilesystemHash = await _hashers.MakeFilesystemHash(modDir)
            };

            await Task.WhenAll(entryTasks);
            modEntry.FileEntries = entryTasks.Select(x => x.Result).ToList();

            return modEntry;
        }
    }
}