using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WEB.Utility
{
    public static class PostedFileExtensions
    {
        public static async Task<byte[]> ToByteArrayAsync(this HttpPostedFileBase file)
        {
            using (var stream = file.InputStream)
            {
                var memoryStream = new MemoryStream();

                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            using (var stream = file.InputStream)
            {
                var memoryStream = new MemoryStream();

                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}