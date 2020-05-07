using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SystemHandle.AsyncFilesystem
{
    public interface IAsyncHash
    {
        Task<string> MakeFilesystemHash(string dir);
        Task<string> MakeFilesystemHash(string dir, List<string> contents);
        Task<string> MakeMd5(string file);
        Task<string> MakeMd5(Stream stream);
        Task<string> MakeCrc32(string file); 
    }
}