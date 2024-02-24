using DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Entities.Models;
using Service.Impl;
using Service.Services;
using SendGrid;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// crear variable para la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("Connection");
//registrar servicio para la conexion
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString)
);
IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

// Retrieve SendGrid options
var sendGridOptions = new SendGridOptions();
configuration.GetSection("SendGrid").Bind(sendGridOptions);

// Now you can use sendGridOptions to access SendGrid settings
builder.Services.AddTransient<ISendGridClient>(provider =>
{
    var apiKey = configuration["SendGrid:ApiKey"];
    return new SendGridClient(apiKey);
});


// Example usage:
Console.WriteLine($"SendGrid API Key: {sendGridOptions.ApiKey}");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tomas Web API", Version = "v1" });

});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "yourdomain.com",
                     ValidAudience = "yourdomain.com",
                     IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Llave_super_secreta"])),
                     ClockSkew = TimeSpan.Zero
                 });
//ending...
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();