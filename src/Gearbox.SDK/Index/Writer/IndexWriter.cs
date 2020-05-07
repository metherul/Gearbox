using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gearbox.Sdk.Index.Models;
using Gearbox.Shared.JsonExt;

namespace Gearbox.Sdk.Index.Writer
{
    public class IndexWriter : IIndexWriter
    {
        private readonly string _modIndex;
        private readonly string _archiveIndex;

        private Dictionary<string, IModEntry> _modEntries = new Dictionary<string, IModEntry>(StringComparer.InvariantCultureIgnoreCase);
        private Dictionary<string, IArchiveEntry> _archiveEntries = new Dictionary<string, IArchiveEntry>(StringComparer.InvariantCultureIgnoreCase);

        public async Task Load(string indexDir)
        {
            return;
            
            var modIndexTask = JsonExt.ReadJson<Dictionary<string, IModEntry>>(_modIndex);
            var archiveIndexTask = JsonExt.ReadJson<Dictionary<string, IArchiveEntry>>(_archiveIndex);

            await Task.WhenAll(modIndexTask, archiveIndexTask);

            _modEntries = modIndexTask.Result;
            _archiveEntries = archiveIndexTask.Result;
        }

        public void Push(IModEntry indexedMod)
        {
            if (_modEntries.ContainsKey(indexedMod.Name))
            {
                _modEntries[indexedMod.Name] = indexedMod;

                return;
            }

            _modEntries.Add(indexedMod.Name, indexedMod);        }

        public void Push(IArchiveEntry indexedArchive)
        {
            if (_archiveEntries.ContainsKey(indexedArchive.Name))
            {
                _archiveEntries[indexedArchive.Name] = indexedArchive;

                return;
            }

            _archiveEntries.Add(indexedArchive.Name, indexedArchive);
        }

        public async Task Flush()
        {
            var modTask = JsonExt.WriteJson(_modEntries, _modIndex);
            var archiveTask = JsonExt.WriteJson(_archiveEntries, _archiveIndex);

            await Task.WhenAll(modTask, archiveTask);
        }
    }
}