using System.Threading.Tasks;
using Gearbox.Sdk.Index.Factories;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Reader;
using Gearbox.Sdk.Index.Writer;

namespace Gearbox.Sdk.Index
{
    public class Indexer : IIndexer
    {
        private readonly IArchiveEntryFactory _archiveEntryFactory;
        private readonly IModEntryFactory _modEntryFactory;
        private readonly IIndexReader _indexReader;
        private readonly IIndexWriter _indexWriter;

        public Indexer(IArchiveEntryFactory archiveEntryFactory, IModEntryFactory modEntryFactory,
            IIndexReader indexReader, IIndexWriter indexWriter)
        {
            _archiveEntryFactory = archiveEntryFactory;
            _modEntryFactory = modEntryFactory;
            _indexReader = indexReader;
            _indexWriter = indexWriter;
        }
        
        public async Task<IIndexReader> OpenRead(string indexDir)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IIndexWriter> OpenWrite(string indexDir)
        {
            await _indexWriter.Load(indexDir);
            return _indexWriter;
        }

        public Task<IArchiveEntry> IndexArchive(string archivePath)
        {
            return _archiveEntryFactory.Create(archivePath);
        }

        public Task<IModEntry> IndexMod(string modDir)
        {
            return _modEntryFactory.Create(modDir);
        }
    }
}
