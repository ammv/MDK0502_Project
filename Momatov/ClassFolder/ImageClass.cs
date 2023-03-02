using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Momatov.ClassFolder
{
    class ImageClass
    {
        public static BitmapImage ConvertBase64StringToBitmapImage(string base64string)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64string);
            using (var ms = new MemoryStream(byteBuffer, 0, byteBuffer.Length))
            {
                var image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();

                return image;
            }
        }

        public static string ConvertImageToBase64String(string filePath)
        {
            return Convert.ToBase64String(File.ReadAllBytes(filePath));
        }
    }
}
