using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repository;
using MetricsAgent.Models;
using Quartz;

namespace MetricsAgent.DAL.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var RamUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new RamMetric {Time = time, Value = RamUsageInPercents});

            return Task.CompletedTask;
        }
    }
}
