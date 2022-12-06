
using Application;
using Infrastructure;
using Infrastructure.Configuration;  
using Infrastructure.Persistence.Extention;
using Serilog;
using WebUI;
using WebUI.Configuration;

SerilogConfig.CreateLogger();

try
{
    var application     = typeof(Application.ConfigureServices).Assembly;
    var Infrastructure  = typeof(Infrastructure.ConfigureServices).Assembly;
    var builder         = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    

    builder.Services.AddWebUIServices           (builder);
    builder.Services.AddInfrastructureServices  (builder);
    builder.Services.AddApplicationServices     ();

     
    builder.Host.ConfigureAutofact(application, Infrastructure);


    var app = builder.Build();

    

    if (!app.Environment.IsDevelopment())
    {
        app.UseCustomExceptionHandler();
        app.UseExceptionHandler("/Error/Index");
        app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?code={0}");

    }
    else
    {
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?code={0}");
       
    }

    app.UseHsts();

    app.UseStaticFilesCaching(60 * 60 * 24 * 7);

    app.UseRouting();

    app.UseWebMarkUpMini();

    app.MapRazorPages();
    
    app.MapControllers();

    await app.ApplyMigration();
   
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

