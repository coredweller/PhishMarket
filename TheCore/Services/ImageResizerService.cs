using System;
using System.IO;
using TheCore.Infrastructure;


namespace TheCore.Services
{
    public class ImageResizerService : IImageResizerService
    {
        public ImageResizerService()
        {

        }


        /// <summary>
        /// Resizes image
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="NewWidth"></param>
        /// <param name="MaxHeight"></param>
        /// <param name="OnlyResizeIfWider"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public byte[] ResizeImage(byte[] imageBytes, int NewWidth, int MaxHeight, bool OnlyResizeIfWider, IImageFormatSpec imageFormatSpec)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                System.Drawing.Image FullsizeImage = System.Drawing.Image.FromStream(ms);

                // Prevent using images internal thumbnail
                FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

                if (OnlyResizeIfWider)
                {
                    if (FullsizeImage.Width <= NewWidth)
                    {
                        NewWidth = FullsizeImage.Width;
                    }
                }

                int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
                if (NewHeight > MaxHeight)
                {
                    // Resize with height instead
                    NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                    NewHeight = MaxHeight;
                }

                System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

                // Clear handle to original file so that we can overwrite it if necessary
                FullsizeImage.Dispose();

                // Save resized picture
                using (var msOut = new MemoryStream())
                {

                    NewImage.Save(msOut, imageFormatSpec.Format);
                    return msOut.GetBuffer();
                }

            }
        }


        /// <summary>
        /// Resizes image
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="imageSize"></param>
        /// <param name="imageFormatSpec"></param>
        /// <returns></returns>
        public byte[] ResizeImage(byte[] imageBytes, IImageSizes imageSize, IImageFormatSpec imageFormatSpec)
        {
            return this.ResizeImage(imageBytes, imageSize.Width, imageSize.MaxHeight, imageSize.OnlyResizeIfWider, imageFormatSpec);
        }

    }
}
