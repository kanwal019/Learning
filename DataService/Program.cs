using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin", policy => policy
    .WithOrigins("http://localhost:8080")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()));

var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                if (context.SecurityToken is JwtSecurityToken token &&
                    context.Principal?.Identity is ClaimsIdentity identity)
                {
                    identity.AddClaim(new Claim("access_token", token.RawData));
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSingleton(_ => new AuthenticationApiClient(new Uri(domain)));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DataService", Version = "v1" }));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "DataService v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();

public partial class Program { }
