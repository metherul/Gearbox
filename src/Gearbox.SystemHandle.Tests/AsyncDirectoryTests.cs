using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using FileSystem = Microsoft.VisualBasic.FileIO.FileSystem;

namespace Gearbox.SystemHandle.Tests
{
    public class AsyncDirectoryTests
    {
        [Test]
        public async Task TestRecursiveDeleteDirWithFiles()
        {
            var rootDir = "root/";
            var filesToDelete = new List<string>()
            {
                "root/file1.txt",
                "root/file2.txt",
                "root/dir1/file3.txt",
                "root/dir2/file4.txt",
                "root/dir3/dir4/file5.txt"
            };

            var fileSystem = new Mock<IFileSystem>();
            fileSystem
                .Setup(x => x.FileStream.Create(
                    It.IsAny<string>(),
                    It.IsAny<FileMode>(),
                    It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>(),
                    It.IsAny<int>(), 
                    It.IsAny<FileOptions>()))
                .Returns(new MemoryStream());
            
            var asyncDirectory = new Mock<AsyncDirectory>(fileSystem.Object) {CallBase = true};
            asyncDirectory
                .Setup(x => x.GetFilesAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<SearchOption>()))
                .ReturnsAsync(filesToDelete);
            
            asyncDirectory.Setup(x => x.Delete(
                It.IsAny<string>(), 
                It.IsAny<bool>()));

            await asyncDirectory.Object.DeleteAsync(rootDir, true);

            // Test to ensure that all calls to FileStream.Create() were expected.
            fileSystem
                .Verify(x => x.FileStream.Create(
                    It.Is<string>(a => filesToDelete.Contains(a)),
                    It.IsAny<FileMode>(),
                    It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>(),
                    It.IsAny<int>(), 
                    It.IsAny<FileOptions>()));
            
            // Ensure that the root directory is deleted.
            asyncDirectory.Verify(x => x.Delete(
                It.Is<string>(a => a == rootDir),
                It.IsAny<bool>()));
        }

        [Test]
        public async Task TestRecursiveDeleteEmptyDir()
        {
            var rootDir = "root/";

            var asyncDirectory = new Mock<AsyncDirectory>(new Mock<IFileSystem>().Object) {CallBase = true};
            asyncDirectory.Setup(x => x.GetFilesAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<SearchOption>()))
                .ReturnsAsync(new List<string>());
            asyncDirectory.Setup(x => x.Delete(
                It.IsAny<string>(), 
                It.IsAny<bool>()));

            await asyncDirectory.Object.DeleteAsync(rootDir);
            
            asyncDirectory.Verify(x => x.Delete(
                It.Is<string>(a => a == rootDir),
                It.IsAny<bool>()));
        }
    }
}