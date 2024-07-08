using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Ocelot.Provider.Eureka;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Retrieve JWT key, issuer, and audience from configuration
var key = builder.Configuration.GetValue<string>("Jwt:Key");
var issuer = builder.Configuration.GetValue<string>("Jwt:Issuer");
var audience = builder.Configuration.GetValue<string>("Jwt:Audience");

var secret = Encoding.ASCII.GetBytes(key);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
    };
});
// Add Discovery Client services for service discovery
builder.Services.AddDiscoveryClient();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<IHealthCheckHandler, ScopedEurekaHealthCheckHandler>();
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath).AddJsonFile("ocelot.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();

// Add Ocelot services with Eureka integration
builder.Services.AddOcelot(builder.Configuration).AddEureka();

var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to the e-commerce app API");
});

app.UseExceptionHandler("/error");



app.Run();
