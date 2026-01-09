using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using RondaSegurancaBack.Data;
using RondaSegurancaBack.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// Conexão MySQL
// ------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ------------------------
// Identity
// ------------------------
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ------------------------
// JWT
// ------------------------
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

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
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,      // opcional: ajuste se tiver issuer
        ValidateAudience = false,    // opcional: ajuste se tiver audience
        ClockSkew = TimeSpan.Zero
    };
});

// ------------------------
// CORS
// ------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllDevAndProd", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173", // Frontend dev
                "https://6193d72eea78.ngrok-free.app" // Frontend produção
            )
            .AllowAnyHeader()       // permite todos headers
            .AllowAnyMethod()       // permite GET, POST, PUT, DELETE...
            .AllowCredentials();    // permite enviar cookies ou auth header
    });
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()   // libera qualquer origem
            .AllowAnyHeader()   // libera qualquer header
            .AllowAnyMethod();  // libera qualquer método HTTP
    });
    // Política aberta (apenas para testes, NÃO usar em produção)
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ------------------------
// Controllers e Swagger
// ------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ------------------------
// Aplicar migrations e criar admin
// ------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    // Cria admin se necessário
    await SeedAdmin.CreateAdmin(scope.ServiceProvider);
}

// ------------------------
// Middleware
// ------------------------
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// ------------------------
// Static Files (Uploads)
// ------------------------
var uploadsPath = "/root/ronda/Uploads";

if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

app.UseCors("AllowAll"); // CORS precisa estar antes de Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
