using Autofac;
using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Gearbox.Assets.Tests
{
    public class AssetServiceTests
    {
        private IAssetService _assetService;

        [SetUp]
        public void Setup()
        {
            using var mock = AutoMock.GetLoose();
            _assetService = mock.Create<AssetService>();
        }

        [Test]
        public void TestAssetDirectory()
        {
            Assert.AreEqual(_assetService.AssetDirectory, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets"));
        }

        [Test]
        public void TestCreateAssetDirectory()
        {
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