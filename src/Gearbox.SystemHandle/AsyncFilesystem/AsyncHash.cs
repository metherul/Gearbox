using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public class AsyncHash : IAsyncHash
    {
        private readonly IAsyncFilesystem _asyncFilesystem;

        public AsyncHash(IAsyncFilesystem asyncFilesystem)
        {
            _asyncFilesystem = asyncFilesystem;
        }
        
        public async Task<string> MakeFilesystemHash(string dir)
        {
            var contents = await  _asyncFilesystem.GetFiles(dir, "*", SearchOption.AllDirectories);
            return await MakeFilesystemHash(dir, contents.ToList()); 
        }

        public async Task<string> MakeFilesystemHash(string dir, List<string> contents)
        {
            var hashBuilder = new StringBuilder();

            foreach (var file in contents)
            {
                var offsetPath = Path.GetRelativePath(dir, file);
                var fileInfo = new FileInfo(file);

                hashBuilder.Append($"{offsetPath}{fileInfo.Name}{fileInfo.Length}");
            }

            return await MakeMd5(hashBuilder.ToString());
        }

        public Task<string> MakeMd5(string file)
        {
            return MakeMd5(File.OpenRead(file));
        }

        public async Task<string> MakeMd5(Stream stream)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(stream);

            stream.Close();

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public async Task<string> MakeCrc32(string file)
        {
            throw new System.NotImplementedException();
        }
    }
}