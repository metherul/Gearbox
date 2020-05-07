using System.IO;
using Gearbox.Managers.ModOrganizer;

namespace Gearbox.Managers
{
    public class Manager : IManager
    {
        private readonly IManagerReader _managerReader;
        
        public Manager(IManagerReader managerReader)
        {
            _managerReader = managerReader;
        }

        public IManagerReader OpenManager(string exe)
        {
            _managerReader.ManagerDir = Path.GetDirectoryName(exe);
            return _managerReader;
        }
    }
}