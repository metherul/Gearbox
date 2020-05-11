using System;
using System.IO;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;

namespace Gearbox.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IAssetReader _assetReader;
        private readonly IAsyncDirectory _asyncDirectory;

        public string AssetDirectory => Path.Combine(Environment.CurrentDirectory, "assets");

        public AssetService(IAssetReader assetReader, IAsyncDirectory asyncDirectory)
        {
            _assetReader = assetReader;
            _asyncDirectory = asyncDirectory;
        }

        public void CreateAssetDirectory()
        {
            _asyncDirectory.CreateDirectory(AssetDirectory);
        }

        public async Task RemoveAssetDirectory()
        {
            if (!_asyncDirectory.Exists(AssetDirectory))
            {
                throw new AssetDirNotFoundException();
            }

            await _asyncDirectory.DeleteAsync(AssetDirectory, true);
        }

        public IAssetReader OpenRead()
        {
            return _assetReader;
        }
    }
}
