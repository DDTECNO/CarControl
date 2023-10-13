using AutoMapper.EquivalencyExpression;
using CarControl.APIVeiculos.AppServicesExtensions;
using CarControl.Common.AutoMapper;
using CarControl.Infrastructure;
using CarControl.Infrastructure.Interface;
using CarControl.Infrastructure.Repositories;
using CarControl.Service;
using CarControl.Service.Autenticacao;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddApiSwagger();
builder.AddDependencyInjection();
builder.AddPersistence();
builder.AddAutenticationJwt();



//O cors permite o acesso a API de um dominio diferente (ignora a política de mesma origem dos navegadores)
builder.Services.AddCors();

var app = builder.Build();

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
app.MapControllers();
app.Run();
