using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;

namespace Gearbox.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IAssetReader _assetReader;
        private readonly IAsyncDirectory _asyncDirectory;
        private readonly IPath _path;

        public string AssetDirectory => _path.Combine(Environment.CurrentDirectory, "assets");

        public AssetService(IAssetReader assetReader, IAsyncDirectory asyncDirectory, IPath path)
        {
            _assetReader = assetReader;
            _asyncDirectory = asyncDirectory;
            _path = path;
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
