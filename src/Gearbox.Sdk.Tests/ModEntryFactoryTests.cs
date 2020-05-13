using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Factories;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Writer;
using Moq;
using NUnit.Framework;

namespace Gearbox.Sdk.Tests
{
    public class ModEntryFactoryTests
    {
        [Test]
        public async Task TestCreateModEntry()
        {
            var modName = "Test Mod";

            var directoryInfoMock = new Mock<IDirectoryInfo>();
            directoryInfoMock.Setup(x => x.Name).Returns(modName);

            var directoryInfoFactoryMock = new Mock<IDirectoryInfoFactory>();
            directoryInfoFactoryMock.Setup(x => x.FromDirectoryName(It.IsAny<string>())).Returns(directoryInfoMock.Object);

            var modDirContents = new List<string>
            {
                "file.txt",
                "file2.txt",
                "dir1/file.txt",
                "dir2/file.txt",
                "dir3/dir4/file.txt",
                "dir3/dir4/file2.txt"
            };
                
            var asyncDirectoryMock = new Mock<IAsyncDirectory>();
            asyncDirectoryMock
                .Setup(x => x.GetFilesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>()))
                .ReturnsAsync(modDirContents);

            var fileEntryFactoryMock = new Mock<IFileEntryFactory>();
            fileEntryFactoryMock.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IFileEntry>());

            var asyncHashMock = new Mock<IAsyncHash>();
            asyncHashMock.Setup(x => x.MakeFilesystemHash(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<string>());
            
            var modEntryFactory = new ModEntryFactory(fileEntryFactoryMock.Object,
                asyncDirectoryMock.Object,
                directoryInfoFactoryMock.Object,
                asyncHashMock.Object);

            var result = await modEntryFactory.Create(modName);
            
            // Test to verify that each call to IFileEntryFactory was to a file in the mod directory.
            fileEntryFactoryMock.Verify(x => x.Create(It.Is<string>(a => modDirContents.Contains(a)), It.IsAny<string>()));
            
            // Test to verify that the result contains the expected values.
            Assert.AreEqual(result.Name, modName);
            Assert.AreEqual(result.FileEntries.Count, modDirContents.Count);
        }
    }
}