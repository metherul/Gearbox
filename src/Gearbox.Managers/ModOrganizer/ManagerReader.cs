using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;

namespace Gearbox.Managers.ModOrganizer
{
    public class ManagerReader : IManagerReader
    {
        private readonly IAsyncDirectory _asyncDirectory;
        public string ManagerDir { get; set; }

        public ManagerReader(IAsyncDirectory asyncDirectory)
        {
            _asyncDirectory = asyncDirectory;
        }
        
        public async Task<List<string>> GetMods()
        {
            return (await GetModDirs()).Select(x => new DirectoryInfo(x).Name).ToList();
        }

        public async Task<List<string>> GetModDirs()
        {
            var modDir = Path.Combine(ManagerDir, "mods");
            var modDirs = await _asyncDirectory.GetDirectoriesAsync(modDir);

            return modDirs;
        }
    }
}