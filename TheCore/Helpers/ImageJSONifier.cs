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


            //sb.Append("{");
            //sb.Append("\"image\":'{0}',");
            //sb.Append("\"thumb\":'{1}',");
            //sb.Append("\"title\":'{2}',");
            //sb.Append("\"description\":'{3}',");
            //sb.Append("\"link\":'{4}'");
            //sb.Append("},");

            sb.Append("{");
            sb.Append("\"image\":\"{0}\",");
            sb.Append("\"thumb\":\"{1}\",");
            sb.Append("\"title\":\"{2}\",");
            sb.Append("\"description\":\"{3}\",");
            sb.Append("\"link\":\"{4}\"");
            sb.Append("},");

            Template = sb.ToString();

            base.BuildTemplate();
        }

        public void Add(ImageItem image)
        {
            var temp = Template.Replace("{0}", image.Image);
            var temp2 = temp.Replace("{1}", image.Thumb);
            var temp3 = temp2.Replace("{2}", image.Title);
            var temp4 = temp3.Replace("{3}", image.Description);
            var temp5 = temp4.Replace("{4}", image.Link);

            Builder.Append(temp5);
        }
    }
}
