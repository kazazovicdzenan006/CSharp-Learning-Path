using API_UI.Middleware;
using Data;
using Domain.Identity;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Services.Services;
using Services.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.OpenApi;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MasterContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddIdentity<SystemCityUser, SystemCityRole>()
    .AddEntityFrameworkStores<MasterContext>()
    .AddDefaultTokenProviders();
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


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
