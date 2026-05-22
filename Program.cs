using Microsoft.EntityFrameworkCore;
using W5iAtendimentoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o Contexto com o SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Inicialização Automatizada e Elegante via Script SQL
// Inicialização Automatizada, Limpa e Nativa
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Cria o banco e as tabelas automaticamente se eles não existirem,
    // usando apenas o mapeamento das suas classes C#
    db.Database.EnsureCreated(); 
    
    Console.WriteLine("Banco de dados verificado/criado com sucesso!");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();