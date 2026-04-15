using Microsoft.EntityFrameworkCore;
using test_site.Data;
using test_site.Utilities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddHostedService<LiveDataTicker>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

WebApplication app = builder.Build();

app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        IHeaderDictionary headers = ctx.Context.Response.Headers;

        if (ctx.Context.Request.Path.StartsWithSegments("/assets"))
        {
            headers.CacheControl = "public, max-age=31536000, immutable";
        }
        else
        {
            headers.CacheControl = "public, max-age=86400";
        }
    }
});

app.Use(async (context, next) =>
{
    context.Response.Headers.XContentTypeOptions = "nosniff";
    context.Response.Headers["Cross-Origin-Resource-Policy"] = "same-origin";
    context.Response.Headers["Cross-Origin-Embedder-Policy"] = "require-corp";
    context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";

    context.Response.OnStarting(() =>
    {
        string contentType = context.Response.ContentType ?? "";

        if (contentType.Contains("html") || contentType.Contains("svg") || contentType.Contains("xml"))
        {
            context.Response.Headers.ContentSecurityPolicy =
                "default-src 'self'; base-uri 'self'; frame-ancestors 'none'; object-src 'none'; form-action 'self';";

            context.Response.Headers.CacheControl = "public, max-age=86400";
        }
        else if (contentType.Contains("css") || contentType.Contains("javascript"))
        {
            context.Response.Headers.CacheControl = "public, max-age=31536000, immutable";
        }

        return Task.CompletedTask;
    });

    await next();
});


app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapHub<LiveHub>("/LiveHub");

app.Run();
