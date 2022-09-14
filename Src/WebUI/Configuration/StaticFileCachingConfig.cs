using Microsoft.Net.Http.Headers;

namespace WebUI.Configuration
{
    public static class StaticFileCachingConfig
    {
        public static void UseStaticFilesCaching(this IApplicationBuilder app, int DurationInSecond)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
        }
    }
}
