using AutoMapper.EquivalencyExpression;
using CarControl.Common.AutoMapper;
using CarControl.Domain;
using CarControl.Infrastructure;
using CarControl.Infrastructure.Interface;
using CarControl.Infrastructure.Repositories;
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
            string connectionString = Configuration["ConnectionStrings:SqliteConnectionString"];
            services.AddDbContext<CarControlContext>(options => options.UseSqlite(connectionString));

            //Inje��o de depend�ncia
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<IOperacaoRepository, OperacaoRepository>();
            services.AddScoped<IVagaRepository, VagaRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IMovimentoService, MovimentoService>();
            services.AddScoped<IOperacaoService, OperacaoService>();
            services.AddScoped<IVagaService, VagaService>();
            services.AddScoped<IVeiculoService, VeiculoService>();

            //AutoMapper
            services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, typeof(AutoMapperProfile).Assembly);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //Servi�o de Autentica��o de usu�rio
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequerAdmin", policy =>
                    policy.RequireRole("Admin"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
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
                    pattern: "Home/Veiculo/CadastroDeVeiculo",
                    defaults: new { controller = "Veiculo", action = "CadastroDeVeiculo" });

                endpoints.MapControllerRoute(
                    name: "VeiculosCadastrados",
                    pattern: "Home/Veiculo/VeiculosCadastrados",
                    defaults: new { controller = "Veiculo", action = "VeiculosCadastrados" });

                endpoints.MapControllerRoute(
                    name: "EditarVeiculo",
                    pattern: "Home/Veiculo/EditarVeiculo",
                    defaults: new { controller = "Veiculo", action = "EditarVeiculo" });

                endpoints.MapControllerRoute(
                    name: "Excluir",
                    pattern: "Home/Veiculo/Excluir",
                    defaults: new { controller = "Veiculo", action = "Excluir" });

                endpoints.MapControllerRoute(
                    name: "DetalhesDoVeiculo",
                    pattern: "Home/Veiculo/DetalhesDoVeiculo",
                    defaults: new { controller = "Veiculo", action = "DetalhesDoVeiculo" });

                endpoints.MapControllerRoute(
                    name: "RegistroDeEntrada",
                    pattern: "Home/Movimento/RegistroDeEntrada",
                    defaults: new { controller = "Movimento", action = "RegistroDeEntrada" });

                endpoints.MapControllerRoute(
                    name: "RegistroDeSaida",
                    pattern: "Home/Movimento/RegistroDeSaida",
                    defaults: new { controller = "Movimento", action = "RegistroDeSaida" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=LoginUsuario}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                   name: "ConsultarVagas",
                   pattern: "Home/Vagas/ConsultarVagas",
                   defaults: new { controller = "Vagas", action = "ConsultarVagas" });



            });
        }
    }
}
