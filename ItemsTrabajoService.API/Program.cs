using ItemsTrabajoService.API.Data;
using ItemsTrabajoService.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuramos ÚNICAMENTE SQLite como el proveedor oficial de base de datos
builder.Services.AddDbContext<ItemsDbContext>(options =>
    options.UseSqlite("Data Source=items.db"));

// 2. Registrar el Servicio de Distribución (Algoritmo de balanceo)
builder.Services.AddScoped<IDistribucionService, DistribucionService>();

// 3. Registrar el cliente HTTP para conectar dinámicamente con la API de Usuarios
builder.Services.AddHttpClient("UsuariosClient", client =>
{
    var urlUsuarios = builder.Configuration["UrlsServicios:UsuariosApi"];
    client.BaseAddress = new Uri(urlUsuarios ?? "https://localhost:7018");
});

// Agregar servicios estándar de la API de Web
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();