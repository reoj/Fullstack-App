//using exam_webapi.Repositories;
using users_items_backend.Context;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DotnetBackend.Services.UserServices;
using DotnetBackend.Services.Inventory;
using DotnetBackend.Services.ExchangeServices;
using DotnetBackend.Filters;

var builder = WebApplication.CreateBuilder(args);

// Introduces SQLite data context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddControllers( options => options.Filters.Add<ErrorHanldingFilterAttribute>() );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( // Enabling the authentication for Swagger UI
    cnfg =>
    {
        cnfg.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Fullstack Integration Project",
            Description = "Evidence Fullstack project for the .Net Accademy",
            Contact = new OpenApiContact
            {
                Name = "Jaciel Israel Reséndiz Ochoa",
                Email = "jresendizochoa@deloitte.com",
                Url = new Uri("https://github.com/reoj")
            },
            Version = "v1"
        }
        );
    }
);

//builder.Services.AddScoped<StaticData>();
// Add services to the container.

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInventoryService, ItemService>();
builder.Services.AddScoped<IExchangeService, ExchangeService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
     //.AllowCredentials()
     );

app.UseAuthorization();

app.MapControllers();

app.Run();
