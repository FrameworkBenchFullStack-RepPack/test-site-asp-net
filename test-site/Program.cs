using System.Text.RegularExpressions;
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

Regex fingerprintedUrl = new(@"\.[a-z0-9]{8,}\.(css|js|svg|png|ico|woff2?)$", RegexOptions.Compiled);

app.Use(async (context, next) =>
{
    context.Response.OnStarting(() =>
    {
        string path = context.Request.Path.Value ?? "";
        if (fingerprintedUrl.IsMatch(path))
        {
            context.Response.Headers.CacheControl = "public, max-age=31536000, immutable";
        }
        else if (context.Response.ContentType?.Contains("text/html") == true)
        {
            context.Response.Headers.CacheControl = "public, max-age=3600";
        }
        else if (context.Response.ContentType?.Contains("text/event-stream") != true)
        {
            context.Response.Headers.CacheControl = "public, max-age=86400";
        }
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        context.Response.Headers["Cross-Origin-Resource-Policy"] = "same-origin";
        context.Response.Headers["Cross-Origin-Embedder-Policy"] = "require-corp";
        context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";
        string contentType = context.Response.ContentType ?? "";
        if (contentType.Contains("html") || contentType.Contains("svg") || contentType.Contains("xml"))
        {
            context.Response.Headers["Content-Security-Policy"] =
                "default-src 'self'; base-uri 'self'; frame-ancestors 'none'; object-src 'none'; form-action 'self';";
        }
        return Task.CompletedTask;
    });
    await next();
});

app.UseRouting();

app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapHub<LiveHub>("/LiveHub");

app.Run();
