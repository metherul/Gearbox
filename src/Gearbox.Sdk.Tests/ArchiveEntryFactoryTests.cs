using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Reflection;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Gearbox.Sdk.Index.Factories;
using Gearbox.Sdk.Index.Models;
using Gearbox.Sdk.Index.Writer;
using Moq;
using NUnit.Framework;

namespace Gearbox.Sdk.Tests
{
    public class ArchiveEntryFactoryTests
    {
        public async IAsyncEnumerable<string> GetArchiveContentsTestData()
        {
            var archiveContents = new List<string>
            {
                "file.txt",
                "file2.txt",
                "dir1/file.txt",
                "dir2/file.txt",
                "dir3/dir4/file.txt",
                "dir3/dir4/file2.txt"
            };

            foreach (var file in archiveContents)
            {
                yield return file;
            }
        }
        
        [Test]
        public async Task TestCreateArchiveEntry()
        {
            var archiveContents = new List<string>
            {
                "file.txt",
                "file2.txt",
                "dir1/file.txt",
                "dir2/file.txt",
                "dir3/dir4/file.txt",
                "dir3/dir4/file2.txt"
            };
            
            var archiveName = "TestArchive.7z";
            var lastWriteTime = DateTime.UtcNow;
            
            var fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.Setup(x => x.Name).Returns(archiveName);
            fileInfoMock.Setup(x => x.LastWriteTimeUtc).Returns(lastWriteTime);
            fileInfoMock.Setup(x => x.Length).Returns(1);
                
            var fileInfoFactoryMock = new Mock<IFileInfoFactory>();
            fileInfoFactoryMock
                .Setup(x => x.FromFileName(It.IsAny<string>()))
                .Returns(fileInfoMock.Object);

            var asyncArchiveMock = new Mock<IAsyncArchive>();
            asyncArchiveMock
                .Setup(x => x.ExtractAndYieldOutput(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(GetArchiveContentsTestData);
            
            var fileEntryFactoryMock = new Mock<IFileEntryFactory>();
            fileEntryFactoryMock
                .Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<FileHashType>()))
                .ReturnsAsync(It.IsAny<IFileEntry>());

            var asyncHashMock = new Mock<IAsyncHash>();
            asyncHashMock.Setup(x => x.MakeFilesystemHash(It.IsAny<string>())).ReturnsAsync(It.IsAny<string>());
            asyncHashMock.Setup(x => x.MakeMd5(It.IsAny<string>())).ReturnsAsync(It.IsAny<string>());

            var asyncDirectoryMock = new Mock<IAsyncDirectory>();
            asyncDirectoryMock.Setup(x => x.DeleteAsync(It.IsAny<string>(), It.IsAny<bool>()));

            var archiveEntryFactory = new ArchiveEntryFactory(fileEntryFactoryMock.Object,
                asyncArchiveMock.Object,
                asyncDirectoryMock.Object,
                fileInfoFactoryMock.Object,
                asyncHashMock.Object);

            var result = await archiveEntryFactory.Create(archiveName);
            
            // Test to ensure every call to IFileEntryFactory was to a file in the archive.
            fileEntryFactoryMock.Verify(x => x.Create(It.Is<string>(a => archiveContents.Contains(a)), It.IsAny<string>(), It.IsAny<FileHashType>()));
            
            // Test to ensure that the IAsyncFileHash.MakeMd5() call had archiveName as an argument.
            asyncHashMock.Verify(x => x.MakeMd5(It.Is<string>(a => a == archiveName)));
            
            // Test to ensure that the result contains the correct file entries and archive information.
            Assert.AreEqual(result.Name, archiveName);
            Assert.AreEqual(result.ArchivePath, archiveName);
            Assert.AreEqual(result.Length, fileInfoMock.Object.Length);
            Assert.AreEqual(result.LastModified, lastWriteTime);
            Assert.AreEqual(result.FileEntries.Count, archiveContents.Count);
        }
    }
}