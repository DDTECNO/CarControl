using AutoMapper.EquivalencyExpression;
using CarControl.Domain;
using CarControl.Infrastructure;
using CarControl.Infrastructure.Repositories;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service;
using CarControl.Service.DTO;
using CarControl.Service.External;
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

//AutoMapper
builder.Services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, typeof(AutoMapperProfile).Assembly);

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



app.MapPost("/Veiculos", async (VeiculoDTO veiculo, IVeiculoService vs) =>
{

    await vs.InserirVeiculo(veiculo);

    return Results.Created($"/Veiculos/{veiculo.IdVeiculo}", veiculo);
});

app.MapGet("/Veiculos", async (IVeiculoService vs) => await vs.ListarVeiculos());

app.MapGet("/Veiculos/{cpf}", async (string cpf, IVeiculoService vs) => await vs.ObterVeiculoPorCPF(cpf));

app.MapPut("/Veiculos/{Id}", async (int id, VeiculoDTO veiculoDTO, IVeiculoService vs) =>
{

    if (veiculoDTO.IdVeiculo != id)
    {
        return Results.BadRequest();
    }

    VeiculoDTO veiculoAtualizado = await vs.EditarVeiculo(veiculoDTO);

    return veiculoAtualizado is null ? Results.NotFound() : Results.Ok(veiculoAtualizado);

});

app.MapDelete("/Veiculos/{id}", async (int id, IVeiculoService vs, IMovimentoService ms) =>
{
    bool movimentacaoVeiculo = await ms.ConsultaSeTemMovimento(id);
    if (movimentacaoVeiculo)
    {
        return Results.BadRequest("O veículo possuí movimentações");
    }

    VeiculoDTO veiculoExcluido = await vs.ExcluirVeiculo(id);

    if (veiculoExcluido == null)
    {
        return Results.NotFound("O veículo não foi encontrado");
    }

    return Results.NoContent();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

