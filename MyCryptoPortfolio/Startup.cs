using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCryptoPortfolio.Repositories;
using MyCryptoPortfolio.Services;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace MyCryptoPortfolio
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddCors();
            var connectionString =
                _config.GetConnectionString("database");

            if (Debugger.IsAttached)
            {
                connectionString = @"Server=(localdb)\mssqllocaldb;Database=MyCryptoPortfolio;Trusted_Connection=true";
            }
            services.AddDbContext<PortfolioContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IHoldingsRepository, HoldingsRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddSingleton<ITickerDataService, TickerDataService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCors(builder =>
            //    builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader());

            app.UseMvc();
        }
    }
}
