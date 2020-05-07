using Gearbox.Shared.ModOrganizer;

namespace Gearbox.Sdk.Compiling
{
    public class CompilerOptions
    {
        public bool EnableFomodFiltering = false;
        public bool RaiseErrorNoConcreteChoice = false;
        public ProfileReader Profile;
        public string PackName;
        public string Author;
        public string Version;
    }
}