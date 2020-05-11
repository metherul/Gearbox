using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Tests.Shared;
using Moq;

namespace Gearbox.Assets.Tests
{
    public class AssetServiceTests : IocEnabledTest<AssetsModule>
    {
        private IAssetService _assetService;

        [SetUp]
        public void Setup()
        {
            var mock = AutoMock.GetLoose();
        }

        [Test]
        public void TestAssetDirectory()
        {
            var assetService = new AssetService(new Mock<IAssetReader>().Object, new Mock<IAsyncDirectory>().Object);
            Assert.AreEqual(assetService.AssetDirectory, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets"));
        }

        [Test]
        public void TestCreateAssetDirectory()
        {
            var dirMock = new Mock<IAsyncDirectory>();
            var assetService = new AssetService(new Mock<IAssetReader>().Object, dirMock.Object);
            _assetService.CreateAssetDirectory();

            Assert.IsTrue(Directory.Exists(_assetService.AssetDirectory));
        }

        [Test]
        public async Task TestRemoveAssetDirectory()
        {
            _assetService.CreateAssetDirectory();
            await _assetService.RemoveAssetDirectory();

            Assert.IsFalse(Directory.Exists(_assetService.AssetDirectory));
            Assert.ThrowsAsync<AssetDirNotFoundException>(async () => await _assetService.RemoveAssetDirectory());
        }
    }
}