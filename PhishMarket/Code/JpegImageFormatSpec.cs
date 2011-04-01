using TheCore.Infrastructure;

namespace PhishMarket.Code
{
    public class JpegImageFormatSpec : ImageFormatSpec
    {
        public JpegImageFormatSpec()
            : base(".jpg", "image/jpeg", System.Drawing.Imaging.ImageFormat.Jpeg)
        {
        }
    }
}
