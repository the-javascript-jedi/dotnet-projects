using System.Text;
using API;
using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// when we want something from our db, we need access to that dbcontext class,so we add it as a service to our program class
builder.Services.AddDbContext<DataContext>(opt =>
{
    // if intellisense is not present try ctrl + '.'
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// add authentication - jwt token options after installig nuget package
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// add CORS Support
builder.Services.AddCors();
// add a service using the scoped method
// we add the interface along with the scoped service
builder.Services.AddScoped<ITokenService, TokenService>();
var app = builder.Build();
// add CORS Middleware to allow website name
// app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

//The authentication part just asks, Do you have a valid token
app.UseAuthentication();
//the authorization part says, okay, you have a valid token.Now what are you allowed to do
app.UseAuthorization();

app.MapControllers();

app.Run();
