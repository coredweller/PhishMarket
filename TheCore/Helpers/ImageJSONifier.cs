using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheCore.Helpers
{
    public class ImageJSONifier : IJSONifier
    {
        public ImageJSONifier(string collectionName)
        {
            CollectionName = collectionName;

            BuildTemplate();
        }

        public ImageJSONifier(string collectionName, ImageItem image)
        {
            CollectionName = collectionName;

            BuildTemplate();
            Add(image);
        }

        protected new void BuildTemplate()
        {
            var sb = new StringBuilder();

            sb.Append("{");
            sb.Append("\"image\":\"{0}\",");
            sb.Append("\"thumb\":\"{1}\"");
            sb.Append("\"title\":\"{2}\"");
            sb.Append("\"description\":\"{3}\"");
            sb.Append("\"link\":\"{4}\"");
            sb.Append("},");

            Template = sb.ToString();

            base.BuildTemplate();
        }

        public void Add(ImageItem image)
        {
            var s = string.Format(Template, image.Image, image.Thumb, image.Title, image.Description, image.Link);

            Builder.Append(s);
        }
    }
}
