using TheCore.Infrastructure;

namespace PhishMarket.Code
{
    public class GifImageFormatSpec : ImageFormatSpec
    {
        public GifImageFormatSpec()
            : base(".gif", "image/gif", System.Drawing.Imaging.ImageFormat.Gif)
        {
        }
    }
}
