using CarControl.Infrastructure;
using CarControl.Infrastructure.Repositories;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service;
using CarControl.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeção de dependência
builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
builder.Services.AddScoped<IOperacaoRepository, OperacaoRepository>();
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IMovimentoService, MovimentoService>();
builder.Services.AddScoped<IOperacaoService, OperacaoService>();
builder.Services.AddScoped<IVagaService, VagaService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();

//Connection strings

//Mysql
//string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<CarControlContext>(options =>  options.UseMySql(mySqlConnection,ServerVersion.AutoDetect(mySqlConnection)));

//Sqlite
var connectionString = builder.Configuration["ConnectionStrings:SqliteConnectionString"];
builder.Services.AddDbContext<CarControlContext>(options => options.UseSqlite(connectionString));

//Ignorando o erro de referência ciclica na serialização (Acontece ao inserir uma propriedade para referênciar uma outra, criando um tipo complexo )
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



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
