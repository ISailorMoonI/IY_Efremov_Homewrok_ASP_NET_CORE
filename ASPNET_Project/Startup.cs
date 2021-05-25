using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL;
using System.Data.SQLite;
using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repository;

namespace MetricsManager
{
    public class Startup
    {//
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSqlLiteConnection(services);

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetWorkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            var connection = new SQLiteConnection(DataBaseConnection.DataBaseConnectionSettings.ConnectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                string[] tableNames = new string[]
                {
                    "cpumetrics",
                    "dotnetmetrics",
                    "hddmetrics",
                    "networkmetrics",
                    "rammetrics"
                };

                foreach (string name in tableNames)
                {
                    command.CommandText = $"DROP TABLE IF EXISTS {name};";
                    command.ExecuteNonQuery();
                }

                foreach (string name in tableNames)
                {
                    command.CommandText = $"CREATE TABLE {name}(id INTEGER PRIMARY KEY, value INT, time BIGINT);";
                    command.ExecuteNonQuery();
                }

                byte valueShifter = 0;
                foreach (string name in tableNames)
                {
                    for (int i = 0; i < 60; i += 5)
                    {
                        long time = System.DateTimeOffset.Parse("2021-05-01 00:" + i + ":00-00:00").ToUnixTimeSeconds();

                        command.CommandText = $"INSERT INTO {name}(value, time) VALUES({i + valueShifter},{time});";
                        command.ExecuteNonQuery();
                    }

                    valueShifter++;
                }
            }
        }
    }
}
