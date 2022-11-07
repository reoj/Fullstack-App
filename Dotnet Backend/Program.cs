//using exam_webapi.Repositories;
using exam_webapi.Services.Inventory;
using exam_webapi.Services.UserServices;
using users_items_backend.Context;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Introduces SQLite data context
builder.Services.AddDbContext<DataContext>(op =>
{
    op.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( // Enabling the authentication for Swagger UI
    cnfg =>
    {
        cnfg.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "RPG Test API",
            Description = "Evidence Fullstack project for the .Net Accademy",
            Contact = new OpenApiContact
            {
                Name = "Jaciel Israel Res�ndiz Ochoa",
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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInventoryService,ItemService>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
