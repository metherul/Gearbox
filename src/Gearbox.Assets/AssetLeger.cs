using System;
using System.IO;

namespace Gearbox.Assets
{
    public class AssetLeger
    {
        public string AssetDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets");

        public void CreateAssetDirectory()
        {
            Directory.CreateDirectory(AssetDirectory);
        }

        public void RemoveAssetDirectory()
        {
            if (!Directory.Exists(AssetDirectory))
            {
                throw new AssetDirNotFoundException();
            }

            Directory.Delete(AssetDirectory);
        }
    }
}
