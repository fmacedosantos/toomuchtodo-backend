using DotNetEnv;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepository, UserRepository>(); // Injeta a dependência de usuário
builder.Services.AddControllers(); // Registra os controllers

var app = builder.Build();

// Configura o pipeline HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Mapeia as rotas dos controllers
app.MapControllers();

app.Run();