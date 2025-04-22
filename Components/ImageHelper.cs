using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Components
{
    internal static class ImageHelper
    {
        public static void GenerateThumb(string src, string dest, int w, int h)
        {
            using (var original = Image.FromFile(src))
            using (var thumb = new Bitmap(w, h))
            using (var graphics = Graphics.FromImage(thumb))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(original, 0, 0, w, h);
                
                // Mentsük JPEG formátumban (minőség: 90%)
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                var jpegEncoder = GetEncoder(ImageFormat.Jpeg);
                thumb.Save(dest, jpegEncoder, encoderParams);
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
} 