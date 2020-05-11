using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public class ProcessService : IProcessService
    {
        public async IAsyncEnumerable<string> RunAndYieldOutput(string fileName, string args)
        {
            using var process = new Process();
            var procArguments = args;
            var processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = fileName,
                Arguments = procArguments,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8
            };

            process.StartInfo = processStartInfo;
            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                var line = await process.StandardOutput.ReadLineAsync();
                yield return line;
            }

            await Task.Run(() => process.WaitForExit());
            process.Close();
        }
    }
}