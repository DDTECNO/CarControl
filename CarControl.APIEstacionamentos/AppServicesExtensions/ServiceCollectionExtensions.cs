using AutoMapper.EquivalencyExpression;
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

namespace CarControl.APIEstacionamentos.AppServicesExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddApiSwagger(this WebApplicationBuilder builder)
        {

            builder.Services.AddSwagger();

            return builder;

        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
  
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "APIEstecionamento",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Hearder de autenticação JWT usando o esquema Bearer.\r\n\r\nInforme 'Bearer' [espaço] e o seu token.\n\r\n\rExemplo: \'Bearer 12345abcdef\'",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;

        }

        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {

            //Connection strings

            //Mysql
            //string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            //builder.Services.AddDbContext<CarControlContext>(options =>  options.UseMySql(mySqlConnection,ServerVersion.AutoDetect(mySqlConnection)));

            //Sqlite
            string? connectionString = builder.Configuration["ConnectionStrings:SqliteConnectionString"];
            builder.Services.AddDbContext<CarControlContext>(options => options.UseSqlite(connectionString));

            //Ignorando o erro de referência ciclica na serialização (Acontece ao inserir uma propriedade para referênciar uma outra, criando um tipo complexo )
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //AutoMapper
            builder.Services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, typeof(AutoMapperProfile).Assembly);


            return builder;

        }

        public static WebApplicationBuilder AddDependencyInjection(this WebApplicationBuilder builder)
        {
            //Injeção de dependência
            builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            builder.Services.AddScoped<IOperacaoRepository, OperacaoRepository>();
            builder.Services.AddScoped<IVagaRepository, VagaRepository>();
            builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            builder.Services.AddScoped<IMovimentoService, MovimentoService>();
            builder.Services.AddScoped<IOperacaoService, OperacaoService>();
            builder.Services.AddScoped<IVagaService, VagaService>();
            builder.Services.AddScoped<IVeiculoService, VeiculoService>();
            builder.Services.AddSingleton<IAutenticacao, Autenticacao>();
            builder.Services.AddScoped<UserManager<IdentityUser>>();
            builder.Services.AddScoped<SignInManager<IdentityUser>>();

            return builder;
        }

        public static WebApplicationBuilder AddAutenticationJwt(this WebApplicationBuilder builder)
        {
            //Serviço de Autenticação de usuário
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequerAdmin", policy =>
                    policy.RequireRole("Admin"));
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
             .AddEntityFrameworkStores<CarControlContext>()
             .AddDefaultTokenProviders();

            //JWT
            //Adiciona o manipulador de autenticação e define o esquema
            //de autenticação utilizado (bearer). Valida o emissor,
            //audiencia e chave e utilzando a chave secreta definida no 
            //appsettings.json, é validada a assinatura.

            builder.Services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
                    ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });

            return builder;
        }

    }
}
