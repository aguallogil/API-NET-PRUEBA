using PruebaAPI.Services;
using DAL.Helpers;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//SERVICIOS
builder.Services.AddScoped<IUsuarioSVC, UsuarioSVC>();
builder.Services.AddScoped<IClienteSvc,ClienteSVC>();
builder.Services.AddScoped<ITipoClienteSvc, TipoClienteSVC>();
builder.Services.AddScoped<IProductoSvc, ProductoSVC>();
builder.Services.AddScoped<IFacturaSvc, FacturaSVC>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    x.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
                context.Response.StatusCode = 408; // request timeout
            }
            else if (context.Exception.GetType() == typeof(SecurityTokenSignatureKeyNotFoundException))
            {
                context.Response.Headers.Add("Token-Invalid", "true");
                context.Response.StatusCode = 401; // security token signature is invalid
            }
            else if (context.Response.GetType() == typeof(SecurityTokenInvalidSignatureException))
            {
                context.Response.StatusCode = 408;
            }
            // if no token is set 
            context.Response.StatusCode = 402; // check if bearer has   
            Console.WriteLine($"No se ha establecido ningún token {context.Response.StatusCode = 408}");
            return Task.FromResult(0);
        }
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//se agrego
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin 
    .AllowCredentials());

app.MapControllers();

app.Run();
