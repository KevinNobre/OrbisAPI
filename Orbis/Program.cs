using Microsoft.EntityFrameworkCore;
using Orbis.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Orbis.Domain.Repositories;
using Orbis.Infrastructure.Repositories;


namespace Orbis { 

    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do banco de dados Oracle
            builder.Services.AddDbContext<OrbisDbContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

            // Configuração do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Orbis API",
                    Version = "v1",
                    Description = "O Orbis é um sistema de solicitação de ajudas em tempo real.",
                    Contact = new OpenApiContact
                    {
                        Name = "ByteBloom Tech",
                        Url = new Uri("https://github.com/KevinNobre/OrbisAPI.git")
                    }
                });

                var xmlFile = "Orbis.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            //Registro de Repositório
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPedidoAjudaRepository, PedidoAjudaRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Middleware do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Orbis API v1");
                options.RoutePrefix = "swagger";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

























        }
























    }


























}


