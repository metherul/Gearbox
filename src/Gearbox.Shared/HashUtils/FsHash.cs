﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gearbox.Shared.HashUtils
{
    public class FsHash
    {
        public async Task<string> MakeFilesystemHash(string directory)
        {
            var contents = await DirectoryExt.GetFilesAsync(directory, "*", SearchOption.AllDirectories);
            return await MakeFilesystemHash(directory, contents.ToList());
        }

        public async Task<string> MakeFilesystemHash(string directory, List<string> contents)
        {
            var hashBuilder = new StringBuilder();

            foreach (var file in contents)
            {
                var offsetPath = Path.GetRelativePath(directory, file);
                var fileInfo = new FileInfo(file);

                hashBuilder.Append($"{offsetPath}{fileInfo.Name}{fileInfo.Length}");
            }

            var hashBytes = Encoding.ASCII.GetBytes(hashBuilder.ToString());
            return await GetMd5Async(new MemoryStream(hashBytes));
        }

        public async Task<string> MakeFilesystemHash(List<Entry> archiveEntries)
        {
            var hashBuilder = new StringBuilder();

            foreach (var entry in archiveEntries)
            {
                var fileName = Path.GetFileName(entry.FileName);

                hashBuilder.Append($"{entry.FileName}{fileName}{entry.Size}");
            }

            var hashBytes = Encoding.ASCII.GetBytes(hashBuilder.ToString());
            return await GetMd5Async(new MemoryStream(hashBytes));
        }

        public string GetMd5(Stream stream)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(stream);

            stream.Close();

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public Task<string> GetMd5Async(Stream stream)
        {
            return Task.Run(() => GetMd5(stream));
        }
    
        public uint GetCrc32(Stream stream)
        {
            using var crc = new Crc32Algorithm();
            var crcBytes = crc.ComputeHash(stream);
            var crcInt = BitConverter.ToUInt32(crcBytes);

            return crcInt;
        }

        public async Task<uint> GetCrc32Async(Stream stream)
        {
            return await Task.Run(() => GetCrc32(stream));
        }
    }
}
