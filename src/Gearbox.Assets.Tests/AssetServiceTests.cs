using NUnit.Framework;
using System.IO.Abstractions;
using SystemHandle.AsyncFilesystem;
using Gearbox.Tests.Shared;
using Moq;

namespace Gearbox.Assets.Tests
{
    public class AssetServiceTests : IocEnabledTest<AssetsModule>
    {
        [Test]
        public void TestCreateAssetDirectory()
        {
            var asyncDirectoryMock = new Mock<IAsyncDirectory>();
            asyncDirectoryMock.Setup(x => x.CreateDirectory(It.IsAny<string>()));

            var path = "Dir1\\Dir2\\assets";
            var pathMock = new Mock<IPath>();
            pathMock.Setup(x => x.Combine(It.IsAny<string>(), It.IsAny<string>())).Returns(path);
            
            
            var assetService = new AssetService(new Mock<IAssetReader>().Object,
                asyncDirectoryMock.Object,
                pathMock.Object);
            
            assetService.CreateAssetDirectory();
            
            asyncDirectoryMock.Verify(x => x.CreateDirectory(It.Is<string>(a => a == path)));
        }
    }
}