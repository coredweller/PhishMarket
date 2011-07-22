using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brettle.Web.NeatUpload;
using TheCore.Services;
using System.IO;
using System.Web.UI;

namespace TheCore.Infrastructure.Images
{
    [ValidationProperty("ValidationFileName")]
    public class PhishMarketInputFile : InputFile
    {
        public PhishMarketInputFile(InputFile inputFile)
        {
            /// LEFT OFF HERE  TRYING TO TAKE in all the fields
            //ContentLength = inputFile.ContentLength;
            //ContentType = inputFile.ContentType;
        }

        public override void MoveTo(string path, MoveToOptions opts)
        {
            var imageResizerService = new ImageResizerService();

            int intContentLength;
            if (!int.TryParse(ContentLength.ToString(), out intContentLength))
            {
                return;
            }

            var imageMediaFormats = Ioc.GetInstance<IImageMediaFormats>();

            var fileExt = Path.GetExtension(FileName.ToLower());
            var mediaFormat = imageMediaFormats.GetSpecByExtension(fileExt);

            //try to resize the image
            var tmpResizeBuffer = new byte[ContentLength];
            FileContent.Read(tmpResizeBuffer, 0, intContentLength);
            var fullResizedBuffer = imageResizerService.ResizeImage(tmpResizeBuffer, new FullImageSize(), mediaFormat);

            FileContent.Flush();
            FileContent.SetLength(fullResizedBuffer.Length);
            FileContent.Write(fullResizedBuffer, 0, fullResizedBuffer.Length);

            base.MoveTo(path, opts);
        }
    }
}
