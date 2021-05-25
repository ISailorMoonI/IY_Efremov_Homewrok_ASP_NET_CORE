using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MetricsManager.DAL;
using System.Data.SQLite;
using AutoMapper;
using FluentMigrator.Runner;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using MetricsManager.Client;
using System;
using MetricsManager.Client.MetricsManager.Client.MetricsManager.Client;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Jobs;
using MetricsManager.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Polly;


namespace MetricsManager
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IAgentsRepository, AgentsRepository>();
            services.AddSingleton<IMetricsAgentClient, MetricsAgentClient>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

            services.AddFluentMigratorCore().ConfigureRunner
                (
                    rb => rb
                        .AddSQLite()
                        .WithGlobalConnectionString(DataBaseConnection.DataBaseManagerConnectionSettings
                            .ConnectionString)
                        .ScanIn(typeof(Startup).Assembly).For.Migrations()
                )
                .AddLogging
                (
                    lb => lb
                        .AddFluentMigratorConsole()
                );

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();

            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricJob),
                cronExpression: "0/5 * * * * ?")); // http://www.quartz-scheduler.org/api/2.1.7/org/quartz/CronExpression.html
            services.AddSingleton(new JobSchedule(jobType: typeof(NetworkMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(DotNetMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(HddMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob), cronExpression: "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                {
                    endpoints.MapControllers();
                })

                ;

                // çàïóñêàåì ìèãðàöèè
                migrationRunner.MigrateUp();
            }
        }
    }
}
        
