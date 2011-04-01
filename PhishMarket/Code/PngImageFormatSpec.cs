using TheCore.Infrastructure;

namespace PhishMarket.Code
{
    public class PngImageFormatSpec : ImageFormatSpec
    {
        public PngImageFormatSpec()
            : base(".png", "image/png", System.Drawing.Imaging.ImageFormat.Png)
        {
        }
    }
}
