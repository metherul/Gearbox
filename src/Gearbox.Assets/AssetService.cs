using System;
using System.IO;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;

namespace Gearbox.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IAsyncDirectory _asyncDirectory;

        public string AssetDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets");

        public AssetService(IAsyncDirectory asyncDirectory)
        {
            _asyncDirectory = asyncDirectory;
        }

        public void CreateAssetDirectory()
        {
            Directory.CreateDirectory(AssetDirectory);
        }

        public async Task RemoveAssetDirectory()
        {
            if (!Directory.Exists(AssetDirectory))
            {
                throw new AssetDirNotFoundException();
            }

            Directory.Delete(AssetDirectory);
        }
    }
}
