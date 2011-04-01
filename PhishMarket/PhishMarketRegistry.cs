using TheCore.Infrastructure;
using TheCore.Infrastructure.Images;
using StructureMap.Configuration.DSL;
using PhishMarket.Code;

namespace PhishMarket
{
    public class PhishMarketRegistry : Registry
    {
        public PhishMarketRegistry()
        {
            For<IImageMediaFormats>()
                    .Singleton()
                    .Use<ImageMediaFormats>()
                    .EnumerableOf<IImageFormatSpec>().Contains(y =>
                        {
                            y.OfConcreteType<JpegImageFormatSpec>();
                            y.OfConcreteType<GifImageFormatSpec>();
                            y.OfConcreteType<PngImageFormatSpec>();
                        });
        }
    }
}
