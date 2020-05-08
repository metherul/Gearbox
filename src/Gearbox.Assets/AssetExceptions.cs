using System;

namespace Gearbox.Assets
{
    public class AssetDirNotFoundException : Exception
    {
        public AssetDirNotFoundException() : base()
        {
        }

        public AssetDirNotFoundException(string message) : base(message)
        {
        }

        public AssetDirNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
