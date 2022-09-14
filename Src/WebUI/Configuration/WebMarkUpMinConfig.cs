using System.IO.Compression;
using WebMarkupMin.AspNet.Brotli;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNetCore6;
using WebMarkupMin.Core;
using WebMarkupMin.NUglify;

namespace WebUI.Configuration
{
    public static class WebMarkUpMinConfig
    {
        public static void AddWebMarkUpMin(this IServiceCollection services)
        {
            services
                .AddWebMarkupMin(options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.AllowCompressionInDevelopmentEnvironment = true;
                    options.DisablePoweredByHttpHeaders = true;
                })
                .AddHtmlMinification(options =>
                {
                    var settings = options.MinificationSettings;
                    settings.RemoveRedundantAttributes = true;
                    settings.RemoveHttpProtocolFromAttributes = true;
                    settings.RemoveHttpsProtocolFromAttributes = true;

                    options.CssMinifierFactory = new NUglifyCssMinifierFactory();
                    options.JsMinifierFactory = new NUglifyJsMinifierFactory();
                })
                .AddXhtmlMinification(options =>
                {
                    XhtmlMinificationSettings settings = options.MinificationSettings;
                    settings.RemoveRedundantAttributes = true;
                    settings.RemoveHttpProtocolFromAttributes = true;
                    settings.RemoveHttpsProtocolFromAttributes = true;

                    options.CssMinifierFactory = new KristensenCssMinifierFactory();
                    options.JsMinifierFactory = new CrockfordJsMinifierFactory();
                })
                .AddXmlMinification(options =>
                {
                    XmlMinificationSettings settings = options.MinificationSettings;
                    settings.CollapseTagsWithoutContent = true;

                })
                .AddHttpCompression(options =>
                {
                    options.CompressorFactories = new List<ICompressorFactory>
                    {
                        new BrotliCompressorFactory(new BrotliCompressionSettings
                        {
                            Level = 1
                        }),
                        new DeflateCompressorFactory(new DeflateCompressionSettings
                        {
                            Level = CompressionLevel.Fastest
                        }),
                        new GZipCompressorFactory(new GZipCompressionSettings
                        {
                            Level = CompressionLevel.Fastest
                        })
                    };
                });
        }

        public static void UseWebMarkUpMini(this IApplicationBuilder app)
                            => app.UseWebMarkupMin();
    }
}
