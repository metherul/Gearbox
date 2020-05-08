using System.Threading.Tasks;

namespace Gearbox.Assets
{
    public interface IAssetService
    {
        string AssetDirectory { get; }
        void CreateAssetDirectory();
        Task RemoveAssetDirectory();
    }
}