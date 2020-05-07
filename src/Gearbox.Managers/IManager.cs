using Gearbox.Managers.ModOrganizer;

namespace Gearbox.Managers
{
    public interface IManager
    {
        IManagerReader OpenManager(string exe);
    }
}