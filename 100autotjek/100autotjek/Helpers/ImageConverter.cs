using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _100autotjek.Helpers
{
    public static class ImageConverter
    {
        public static async Task<byte[]> ToBytesAsync(Stream imageStream)
        {
            using (var ms = new MemoryStream())
            {
                await imageStream.CopyToAsync(ms);

                return ms.ToArray();
            }
        }
    }
}
