using Application.DTO.Mapper;
using Application.Middleware;
using Domain.Configuration.ASP.Configuration;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.Validation;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Security.Interfaces;
using Security.Services;

var builder = WebApplication.CreateBuilder(args);
// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOriginsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", true, true)
    .Build();

builder.Services.AddSingleton(configuration);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(dbOptions =>
{
    if (builder.Environment.IsDevelopment())
    {
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }
    else if (builder.Environment.IsProduction())
    {
        dbOptions.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors();
    }
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers(r => r.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Consultant API",
        Description = "Consultant Rest API Documentation",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Consultant",
            Url = new Uri("https://github.com/LordMathi2741")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
        
    });
});


builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository,VehicleRepository>();
builder.Services.AddScoped<ICylinderRepository,CylinderRepository>();
builder.Services.AddScoped<IObservationRepository, ObservationRepository>();
builder.Services.AddScoped<ICertifierRepository, CertifierRepository>();
builder.Services.AddScoped<IOperationCenterRepository,OperationCenterRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IValveRepository, ValveRepository>();

builder.Services.AddScoped<IWorkShopRepository, WorkShopRepository>();
builder.Services.AddScoped<IWorkShopCompanyRepository,WorkShopCompanyRepository>();
builder.Services.AddScoped<IWorkShopCylinderRepository, WorkShopCylinderRepository>();
builder.Services.AddScoped<IProviderCompanyRepository, ProviderCompanyRepository>();
builder.Services.AddScoped<IInstallerCompanyRepository, InstallerCompanyRepository>();
builder.Services.AddScoped<ICylinderProviderRepository, CylinderProviderRepository>();
    

builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddScoped<IBusinessRulesValidator, BusinessRulesValidator>();
builder.Services.AddAutoMapper(typeof(ModelToResponse), typeof(RequestToModel));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000").AllowCredentials());

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<AuthenticacionMiddleware>();


app.UseCors("AllowSpecificOriginsPolicy");

app.MapControllers();
app.UseAuthentication();

app.UseHttpsRedirection();


app.Run();
