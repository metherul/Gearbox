using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemHandle.AsyncFilesystem;
using Moq;
using NUnit.Framework;

namespace Gearbox.SystemHandle.Tests
{
    public class AsyncArchiveTests
    {
        private async IAsyncEnumerable<string> GetTestValuesForArchiveExtractCallback()
        {
            var procOut = await File.ReadAllLinesAsync(Path.Combine(AssetsResolver.ResolveAssetsDir(), "procOut.txt"));

            foreach (var line in procOut)
            {
                yield return line;
            }
        }

        [Test]
        public async Task TestArchiveExtractCallback()
        {
            var extractDir = "C:/Test1/Test2/Test3/extract";
            var expectedOutput =
                await File.ReadAllLinesAsync(Path.Combine(AssetsResolver.ResolveAssetsDir(), "expectedProcOut.txt"));
            var expectedDirs =
                await File.ReadAllLinesAsync(Path.Combine(AssetsResolver.ResolveAssetsDir(),
                    "expectedProcOutDirs.txt"));

            var processService = new Mock<IProcessService>();
            processService.Setup(x => x.RunAndYieldOutput(
                It.IsAny<string>(), 
                It.IsAny<string>()))
                .Returns(GetTestValuesForArchiveExtractCallback());

            var directory = new Mock<IAsyncDirectory>();
            directory
                .Setup(x => x.Exists(It.IsAny<string>()))
                .Returns((string x) => expectedDirs.Contains(x));
            
            directory.Setup(x => x.GetCurrentDirectory()).Returns("");

            var asyncArchive = new AsyncArchive(directory.Object, processService.Object);

            await foreach (var line in asyncArchive.ExtractAndYieldOutput(extractDir, It.IsAny<string>()))
            {
                Assert.True(expectedOutput.Contains(line));
            }
        }
    }
}