using System;
using System.IO;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Autofac;

namespace Gearbox.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IAssetReader _assetReader;
        private readonly IAsyncDirectory _asyncDirectory;

        public string AssetDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets");

        public AssetService(IAssetReader assetReader, IAsyncDirectory asyncDirectory)
        {
            _assetReader = assetReader;
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

            await _asyncDirectory.Delete(AssetDirectory, true);
        }

        public IAssetReader OpenRead()
        {
            return _assetReader;
        }
    }
}
