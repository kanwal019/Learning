using Library.Common.Commands;
using Library.Common.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Activity.Handlers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();
builder.Services.AddCommandSubscription<CreateActivity>();

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
