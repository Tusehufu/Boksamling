using Microsoft.EntityFrameworkCore;
//using InternJohan.Dev.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// Konfigurera anslutningssträngar och andra inställningar
//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// Lägg till Entity Framework Core-tjänsten och använd din ApplicationDbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    // Använd den anslutningssträng du definierade i appsettings.json
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});




// Lägg till controllers
builder.Services.AddControllers();

// Lägg till Swagger/OpenAPI om du använder det
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lägg till CORS-policy om du har en
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsRule", builder =>
    {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Lägg till `IHttpContextAccessor`-tjänsten
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Lägg till autentisering och auktoriseringstjänster
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Lägg till JWT-autentiseringstjänster
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Använd din JWT-utfärdare (issuer)
            ValidAudience = builder.Configuration["Jwt:Audience"], // Använd din JWT-publik (audience)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Använd din JWT-signeringsnyckel
        };
    });



var app = builder.Build();

// Använd CORS-policy om du har definierat en
app.UseCors("CorsRule");

// Om applikationen är i utvecklingsläge, använd Swagger och SwaggerUI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Använd HTTPS om det behövs
app.UseHttpsRedirection();

// Använd autentisering och auktorisering
app.UseAuthentication(); // Lägg till autentiseringstjänster
app.UseAuthorization();

// Mappa controllers
app.MapControllers();

// Konfigurera SPA om du har en client app
app.MapWhen(x => !x.Request.Path.StartsWithSegments("/api"), x =>
{
    app.UseSpa(spa =>
    {
        if (!builder.Environment.IsDevelopment())
            spa.Options.SourcePath = "wwwroot/clientapp";
        else
            spa.Options.SourcePath = @"C:\Data\Repos\KEYnet\InternJohan.Dev.App\wwwroot\clientapp";
    });
});

// Kör applikationen
app.Run();
