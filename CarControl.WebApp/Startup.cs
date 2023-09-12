using CarControl.Domain;
using CarControl.Infrastructure;
using CarControl.Infrastructure.Repositories;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace CarControl.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews();
            services.AddApplicationInsightsTelemetry();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddControllersWithViews();

            //Connection string
            var connectionString = Configuration["ConnectionStrings:SqliteConnectionString"];
            services.AddDbContext<CarControlContext>(options => options.UseSqlite(connectionString));

            //Injeção de dependência
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<IOperacaoRepository, OperacaoRepository>();
            services.AddScoped<IVagaRepository, VagaRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IMovimentoService, MovimentoService>();
            services.AddScoped<IOperacaoService, OperacaoService>();
            services.AddScoped<IVagaService, VagaService>();
            services.AddScoped<IVeiculoService, VeiculoService>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

         
            //Serviço de Autenticação de usuário
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequerAdmin", policy =>
                    policy.RequireRole("Admin"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
             .AddEntityFrameworkStores<CarControlContext>()
             .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            


            //Verificando outro meio de disponibilizar as rotas 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "CadastroDeVeiculo",
                    pattern: "Home/CadastroDeVeiculo/CadastroDeVeiculo",
                    defaults: new { controller = "CadastroDeVeiculo", action = "CadastroDeVeiculo" });

                endpoints.MapControllerRoute(
                    name: "VeiculosCadastrados",
                    pattern: "Home/CadastroDeVeiculo/VeiculosCadastrados",
                    defaults: new { controller = "CadastroDeVeiculo", action = "VeiculosCadastrados" });
               
                endpoints.MapControllerRoute(
                    name: "EditarVeiculo",
                    pattern: "Home/CadastroDeVeiculo/EditarVeiculo",
                    defaults: new { controller = "CadastroDeVeiculo", action = "EditarVeiculo" });

                endpoints.MapControllerRoute(
                    name: "Excluir",
                    pattern: "Home/CadastroDeVeiculo/Excluir",
                    defaults: new { controller = "CadastroDeVeiculo", action = "Excluir" });

                endpoints.MapControllerRoute(
                    name: "DetalhesDoVeiculo",
                    pattern: "Home/CadastroDeVeiculo/DetalhesDoVeiculo",
                    defaults: new { controller = "CadastroDeVeiculo", action = "DetalhesDoVeiculo" });

                endpoints.MapControllerRoute(
                    name: "RegistroDeEntrada",
                    pattern: "Home/RegistroDeMovimento/RegistroDeEntrada",
                    defaults: new { controller = "RegistroDeMovimento", action = "RegistroDeEntrada" });

                endpoints.MapControllerRoute(
                    name: "RegistroDeSaida",
                    pattern: "Home/RegistroDeMovimento/RegistroDeSaida",
                    defaults: new { controller = "RegistroDeMovimento", action = "RegistroDeSaida" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=LoginUsuario}/{id?}");
                    endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                   name: "ConsultarVagas",
                   pattern: "Home/ConsultarVagas/ConsultarVagas",
                   defaults: new { controller = "ConsultarVagas", action = "ConsultarVagas" });



            });
        }
    }
}
