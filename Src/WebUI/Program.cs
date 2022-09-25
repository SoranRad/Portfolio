
using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Serilog;
using WebUI;
using WebUI.Configuration;

Log.Logger = SerilogConfig.CreateLoggerConfiguration();

try
{
    var application     = typeof(Application.ConfigureServices).Assembly;
    var Infrastructure  = typeof(Infrastructure.ConfigureServices).Assembly;
    var builder         = WebApplication.CreateBuilder(args);


    builder.Services.AddWebUIServices           (builder);
    builder.Services.AddInfrastructureServices  (builder.Configuration);
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

    // apply migration
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<DbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }

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

