using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Helpers
{
    public static class FileHelpers
    {
        public static class FileHelper
        {
            public static byte[] ReadFully(Stream input)
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }

            public static string CleanInvalidChars(string fn)
            {
                foreach (var c in System.IO.Path.GetInvalidFileNameChars())
                {
                    fn.Replace(c, '_');
                }
                return fn;
            }
        }
    }
}
