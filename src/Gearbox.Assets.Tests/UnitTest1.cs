using NUnit.Framework;
using System.IO;

namespace Gearbox.Assets.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAssetDirectory()
        {
            var leger = new AssetLeger();

            Assert.IsTrue(Directory.Exists(leger.AssetDirectory));
        }

        [Test]
        public void TestCreateAssetDirectory()
        {
            var leger = new AssetLeger();
            leger.CreateAssetDirectory();

            Assert.IsTrue(Directory.Exists(leger.AssetDirectory));
        }

        [Test]
        public void TestRemoveAssetDirectory()
        {
            var leger = new AssetLeger();
            leger.CreateAssetDirectory();
            leger.RemoveAssetDirectory();

            Assert.IsFalse(Directory.Exists(leger.AssetDirectory));
            Assert.Throws<AssetDirNotFoundException>(() => leger.RemoveAssetDirectory());
        }
    }
}