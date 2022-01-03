using HotelListing;
using HotelListing.Config;
using HotelListing.Data;
using HotelListing.Repository;
using HotelListing.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

var connectionString = builder.Configuration.GetConnectionString("sqlConnection");

builder.Services.AddDbContext<DatabaseContext>(options => 
options.UseSqlServer(connectionString));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(x =>
{
    x.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAutoMapper(typeof(MapperInit));
builder.Services.AddRouting(o => o.LowercaseUrls = true);

builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore );

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(configuration);
builder.Services.ConfigureVersioning();

builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console()
.WriteTo.File(
    path: "logs\\log-.txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{level:u3}] {Message:lj}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: LogEventLevel.Information));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.ConfigureExceptionHandler();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
