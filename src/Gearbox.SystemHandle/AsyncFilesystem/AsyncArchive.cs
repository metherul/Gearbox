using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public class AsyncArchive : IAsyncArchive
    {
        private readonly IAsyncDirectory _directory;
        private readonly IProcessService _processService;

        public AsyncArchive(IAsyncDirectory directory, IProcessService processService)
        {
            _directory = directory;
            _processService = processService;
        }
        
        public async Task Extract(string extractDir, string archivePath)
        {
            // A little cheat that lets up enumerate and then dispose.
            await ExtractAndYieldOutput(extractDir, archivePath).GetAsyncEnumerator().DisposeAsync();
        }

        public async IAsyncEnumerable<string> ExtractAndYieldOutput(string extractDir, string archivePath)
        {
            var procArguments = $"x \"{archivePath}\" -o\"{extractDir}\" -y -bsp1 -bb2 -sccUTF-8 -mmt=on";
            var fileName = Path.Combine(_directory.GetCurrentDirectory(), "7z.dll");

            var last = "";
            await foreach (var line in _processService.RunAndYieldOutput(fileName, procArguments))
            {
                // If the line contains no data, ignore it.
                if (string.IsNullOrWhiteSpace(line) || !line.StartsWith("- "))
                {
                    continue;
                }

                // Grab the relative path, removing: '- ' from the line.
                var path = Path.GetFullPath(Path.Combine(extractDir, line[2..]));

                // If the path is actually a directory, ignore it.
                if (_directory.Exists(path))
                {
                    continue;
                }

                // To prevent issues with file ownership timings, we only return the *last* file extracted.
                if (string.IsNullOrEmpty(last))
                {
                    last = path;
                    continue;
                }

                // Pass the path back to the event handler.
                yield return last;
                last = path;
            }
            
            // Take care of the very last path.
            yield return last;
        }
    }
}