using System.IO;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Autofac.Extras.Moq;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;

namespace Gearbox.SystemHandle.Tests
{
    public class AsyncDirectoryTests
    {
        private AsyncDirectory _asyncDirectory;

        [SetUp]
        public void Setup()
        {
            var mock = AutoMock.GetLoose();
            _asyncDirectory = mock.Create<AsyncDirectory>();
            
            // Setup our test directory.
            FileSystem.CopyDirectory(AssetsResolver.ResolveAssetsDir(), Pat);
        }

        [Test]
        public async Task TestDelete()
        {
            
        }
    }
}