using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gearbox.Managers.ModOrganizer
{
    public interface IManagerReader
    {
        string ManagerDir { get; set; }
        Task<List<string>> GetMods();
        Task<List<string>> GetModDirs();
    }
}