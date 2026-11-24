using API.Gateway.Handlers;
using Library.Common.Events;
using Library.Common.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
builder.Services.AddEventSubscription<ActivityCreated>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
