using System;
using System.IO;

namespace Gearbox.SystemHandle.Tests
{
    public static class AssetsResolver
    {
        public static string ResolveAssetsDir()
        {
            var envDir = Environment.CurrentDirectory;
            return Path.GetFullPath(Path.Combine(envDir, "../../../Assets"));
        }
    }
}
