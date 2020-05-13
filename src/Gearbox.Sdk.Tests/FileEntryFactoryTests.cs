using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Factories;
using Moq;
using NUnit.Framework;

namespace Gearbox.Sdk.Tests
{
    public class FileEntryFactoryTests
    {
        [Test]
        public async Task TestCreateFileEntry_WithRelativePath_WithMd5Hash()
        {
            var fileName = "TestFile.txt";
            var filePath = "C:\\Dir1\\Dir2\\Dir3\\TestFile.txt";
            
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.Name).Returns(fileName);
            fileInfoMock.Setup(x => x.Length).Returns(100);
            fileInfoMock.Setup(x => x.LastWriteTimeUtc).Returns(DateTime.UtcNow);
            fileInfoMock.Setup(x => x.FullName).Returns(filePath);

            var fileInfoFactoryMock = new Mock<IFileInfoFactory>();
            fileInfoFactoryMock.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(fileInfoMock.Object);

            var pathMock = new Mock<IPath>();
            pathMock
                .Setup(x => x.GetRelativePath(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string x, string y) => Path.GetRelativePath(x, y));

            var asyncHashMock = new Mock<IAsyncHash>();
            asyncHashMock.Setup(x => x.MakeMd5(It.IsAny<string>())).ReturnsAsync("Md5Hash");
            
            var fileEntryFactory = new FileEntryFactory(fileInfoFactoryMock.Object, pathMock.Object, asyncHashMock.Object);

            var relativeTo = "C:\\Dir1\\Dir2\\";
            
            var result = await fileEntryFactory.Create(filePath, relativeTo, FileHashType.Md5);
            
            // Test to ensure that the result has expected values.
            Assert.AreEqual(result.FilePath, "Dir3\\TestFile.txt");
            Assert.AreEqual(result.Hash, "Md5Hash");
            Assert.AreEqual(result.LastWriteTimeUtc, fileInfoMock.Object.LastWriteTimeUtc);
            Assert.AreEqual(result.Name, fileName);
        }

        [Test]
        public async Task TestCreateFileEntry_WithRelativePath_WithCrc32Hash()
        {
            var fileName = "TestFile.txt";
            var filePath = "C:\\Dir1\\Dir2\\Dir3\\TestFile.txt";
            
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.Name).Returns(fileName);
            fileInfoMock.Setup(x => x.Length).Returns(100);
            fileInfoMock.Setup(x => x.LastWriteTimeUtc).Returns(DateTime.UtcNow);
            fileInfoMock.Setup(x => x.FullName).Returns(filePath);

            var fileInfoFactoryMock = new Mock<IFileInfoFactory>();
            fileInfoFactoryMock.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(fileInfoMock.Object);

            var pathMock = new Mock<IPath>();
            pathMock
                .Setup(x => x.GetRelativePath(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string x, string y) => Path.GetRelativePath(x, y));

            var asyncHashMock = new Mock<IAsyncHash>();
            asyncHashMock.Setup(x => x.MakeCrc32(It.IsAny<string>())).ReturnsAsync("Crc32Hash");
            
            var fileEntryFactory = new FileEntryFactory(fileInfoFactoryMock.Object, pathMock.Object, asyncHashMock.Object);

            var relativeTo = "C:\\Dir1\\Dir2\\";
            
            var result = await fileEntryFactory.Create(filePath, relativeTo, FileHashType.Crc32);
            
            // Test to ensure that the result has expected values.
            Assert.AreEqual(result.FilePath, "Dir3\\TestFile.txt");
            Assert.AreEqual(result.Hash, "Crc32Hash");
            Assert.AreEqual(result.LastWriteTimeUtc, fileInfoMock.Object.LastWriteTimeUtc);
            Assert.AreEqual(result.Name, fileName);
        }

        [Test]
        public async Task TestCreateFileEntry_NoRelativePath()
        {
            var fileName = "TestFile.txt";
            var filePath = "C:\\Dir1\\Dir2\\Dir3\\TestFile.txt";
            
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.Name).Returns(fileName);
            fileInfoMock.Setup(x => x.Length).Returns(100);
            fileInfoMock.Setup(x => x.LastWriteTimeUtc).Returns(DateTime.UtcNow);
            fileInfoMock.Setup(x => x.FullName).Returns(filePath);

            var fileInfoFactoryMock = new Mock<IFileInfoFactory>();
            fileInfoFactoryMock.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(fileInfoMock.Object);

            var pathMock = new Mock<IPath>();
            pathMock
                .Setup(x => x.GetRelativePath(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string x, string y) => Path.GetRelativePath(x, y));

            var asyncHashMock = new Mock<IAsyncHash>();
            asyncHashMock.Setup(x => x.MakeMd5(It.IsAny<string>())).ReturnsAsync("Md5Hash");
            
            var fileEntryFactory = new FileEntryFactory(fileInfoFactoryMock.Object, pathMock.Object, asyncHashMock.Object);
            
            var result = await fileEntryFactory.Create(filePath, "");
            
            // Test to ensure that the result has expected values.
            Assert.AreEqual(result.FilePath, filePath);
            Assert.AreEqual(result.Hash, "Md5Hash");
            Assert.AreEqual(result.LastWriteTimeUtc, fileInfoMock.Object.LastWriteTimeUtc);
            Assert.AreEqual(result.Name, fileName);
        }
    }
}