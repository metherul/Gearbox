using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;

namespace Gearbox.Managers.ModOrganizer
{
    public class ManagerReader : IManagerReader
    {
        private readonly IAsyncFilesystem _asyncFilesystem;
        public string ManagerDir { get; set; }

        public ManagerReader(IAsyncFilesystem asyncFilesystem)
        {
            _asyncFilesystem = asyncFilesystem;
        }
        
        public async Task<List<string>> GetMods()
        {
            return (await GetModDirs()).Select(x => new DirectoryInfo(x).Name).ToList();
        }

        public async Task<List<string>> GetModDirs()
        {
            var modDir = Path.Combine(ManagerDir, "mods");
            var modDirs = await _asyncFilesystem.GetDirectories(modDir);

            return modDirs;
        }
    }
}