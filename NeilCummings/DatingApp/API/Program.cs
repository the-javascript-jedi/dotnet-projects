// to run > dotnet watch --no-hot-reload
using API;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// we are extending the the Service for making db calls in a separate extension, instead of specifying here itself
// we will extend the services by this IServiceCollection services
builder.Services.AddApplicationServices(builder.Configuration);
// JWT Authentication service extension
builder.Services.AddIdentityServices(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline
// add the exception middleware
app.UseMiddleware<ExceptionMiddleware>();

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
