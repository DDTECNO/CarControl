using CarControl.APIEstacionamentos.ApiEndpoints;
using CarControl.APIEstacionamentos.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddApiSwagger();
builder.AddDependencyInjection();
builder.AddPersistence();
builder.AddAutenticationJwt();



//O cors permite o acesso a API de um dominio diferente (ignora a política de mesma origem dos navegadores)
builder.Services.AddCors();

var app = builder.Build();

app.MapLoginEndpoints();
app.MapVeiculosEndpoint();

// Configure the HTTP request pipeline.
var environment = app.Environment;

app.UseExceptionHandling(environment)
   .UseSwaggerMiddleware()
   .UseAppCors();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//Definindo a política do CORS (permitindo qualquer origem)
app.UseCors(options => options.AllowAnyOrigin());
app.Run();

