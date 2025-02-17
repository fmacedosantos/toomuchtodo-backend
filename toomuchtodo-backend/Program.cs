using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using toomuchtodo_backend;
using toomuchtodo_backend.Models;
using toomuchtodo_backend.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Carrega as variáveis do arquivo .env
Env.Load();

// Substitui os placeholders na string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("{DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST"))
    .Replace("{DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME"))
    .Replace("{DB_USERNAME}", Environment.GetEnvironmentVariable("DB_USERNAME"))
    .Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

// Adiciona os serviços
builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(connectionString)); // Configura o DbContext com a string de conexão
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddTransient<IUserRepository, UserRepository>(); // Injeta a dependência de usuário
builder.Services.AddTransient<ITaskRepository, TaskRepository>(); // Injeta a dependência de tarefa
builder.Services.AddControllers(); // Registra os controllers

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.Configure<RouteOptions>(options => 
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

// Configura o pipeline HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Mapeia as rotas dos controllers
app.MapControllers();

app.Run();