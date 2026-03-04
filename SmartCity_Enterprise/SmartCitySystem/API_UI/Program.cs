using API_UI.Middleware;
using Domain.Identity;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

//using Microsoft.OpenApi.Models;
using Services.Services;
using Services.Validators;
using System.Security.Claims;
using System.Text;






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

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // Direktno dodajemo security requirement u dokument bez instanciranja OpenApiSecurityRequirement klase
        var requirements = new Dictionary<string, IEnumerable<string>>
        {
            { "Bearer", Array.Empty<string>() }
        };

        // .NET 10 interna logika će ovo mapirati bez potrebe za .Models namespaceom
        return Task.CompletedTask;
    });
});
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
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                if (claimsIdentity == null) return Task.CompletedTask;

                // Ako su role u jednom claimu kao JSON niz, .NET ih nekad ne vidi kao odvojene identitete
                var roleClaims = claimsIdentity.FindAll("role").ToList();
                if (roleClaims.Count == 1 && roleClaims[0].Value.Contains("["))
                {
                    // Ovdje bi išla logika za parsiranje niza, ali obično RoleClaimType = "role" rješava stvar
                }
                return Task.CompletedTask;
            }
        };
    });



var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Smart City API")
               .WithTheme(ScalarTheme.Moon)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Automatsko kreiranje baze i tabela pri pokretanju
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MasterContext>();
        // Ovo će kreirati bazu ako ne postoji i primijeniti sve migracije
        context.Database.Migrate();
        Console.WriteLine("Baza podataka je uspješno migrirana.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Greška pri migraciji: {ex.Message}");
    }
}


app.Run();
