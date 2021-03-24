using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using tel_api.Configurations.Factories;
using tel_api.Configurations.Middlewares;
using tel_api.Domain.Models;
using tel_api.Domain.Models.Options;
using tel_api.Domain.Repositories;
using tel_api.Domain.Services;
using tel_api.Domain.Validators;

namespace tel_api
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
            services.AddControllers();

            services.AddOptions();
            services.Configure<Database>(Configuration.GetSection("Database"));

            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddTransient<ISqlService, SqlService>();            
            services.AddTransient<ITarifaFixaRepository, TarifaFixaRepository>();            

            services.AddTransient<ISimuladorTarifaService, SimuladorTarifaService>();
            services.AddTransient<IPlanoFaleMais30Factory, PlanoFaleMais30Factory>();
            services.AddTransient<IPlanoFaleMais60Factory, PlanoFaleMais60Factory>();
            services.AddTransient<IPlanoFaleMais120Factory, PlanoFaleMais120Factory>();
            services.AddTransient<ILigacaoBuilder, LigacaoBuilder>();            
            services.AddTransient<IPlanoTarifaFixaFactory, PlanoTarifaFixaService>();
            services.AddTransient<IPlanoFaleMaisBuilder, PlanoFaleMaisBuilder>();
            services.AddTransient<IPlanoComercializadoService, PlanoComercializadoService>();
            
            services.AddSingleton<IValidator<SimulacaoCustoLigacao>, SimulacaoCustoLigacaoValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Tel API", 
                    Version = "v1",
                    Description ="Description for the API goes here.",                   
                });
                c.EnableAnnotations();
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

             app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });           
        }
    }
}
