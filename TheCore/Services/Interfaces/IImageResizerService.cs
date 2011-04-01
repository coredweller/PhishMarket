using TheCore.Infrastructure;

namespace TheCore.Services
{
    public interface IImageResizerService
    {
        byte[] ResizeImage(byte[] imageBytes, IImageSizes imageSize, IImageFormatSpec imageFormatSpec);
        byte[] ResizeImage(byte[] imageBytes, int NewWidth, int MaxHeight, bool OnlyResizeIfWider, IImageFormatSpec imageFormatSpec);
    }
}
