using CarControl.Domain;
using CarControl.Infrastructure;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Infrastructure.Repositories;
using CarControl.Service;
using CarControl.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Connection strings

//Mysql
//string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<CarControlContext>(options =>  options.UseMySql(mySqlConnection,ServerVersion.AutoDetect(mySqlConnection)));

//Sqlite
string? connectionString = builder.Configuration["ConnectionStrings:SqliteConnectionString"];
builder.Services.AddDbContext<CarControlContext>(options => options.UseSqlite(connectionString));




// Add services to the container.
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


var app = builder.Build();



app.MapPost("/Veiculos", async (Veiculo veiculo, IVeiculoService vs) =>
{

    await vs.Create(veiculo);

    return Results.Created($"/Veiculos/{veiculo.IdVeiculo}", veiculo);
});

app.MapGet("/Veiculos", async (IVeiculoService vs) => await vs.ListaVeiculos());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

