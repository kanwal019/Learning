using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NetCoreApi.Middleware.Extensions;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "files")),
    RequestPath = "/files"
});
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "images")),
    RequestPath = "/images"
});
app.UseLogUrl();
app.MapGet("/", (HttpContext context) => context.Response.WriteAsync("Application Running !!"));
await app.RunAsync();

public partial class Program { }
