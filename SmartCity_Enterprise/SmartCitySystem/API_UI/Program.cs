using API_UI.Middleware;
using Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Services.Validators;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MasterContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();  

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BookStoreService>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<SenzorService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<Services.Profiles.BookProfile>();
    cfg.AddProfile<Services.Profiles.FilmProfile>();
    cfg.AddProfile<Services.Profiles.BookStoreItemsProfile>();
    cfg.AddProfile<Services.Profiles.SenzorProfile>();
    cfg.AddProfile<Services.Profiles.ControllerProfile>();
    cfg.AddProfile<Services.Profiles.DevicesProfile>();
    cfg.AddProfile<Services.Profiles.CrossRoadProfile>();
    cfg.AddProfile<Services.Profiles.ParkingLotProfile>();
    cfg.AddProfile<Services.Profiles.CityNodeProfile>();
    cfg.AddProfile<Services.Profiles.GradProfile>();
});



var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();    
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
