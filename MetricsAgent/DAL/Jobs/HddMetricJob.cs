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
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "0 C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var HddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new HddMetric {Time = time, Value = HddUsageInPercents});

            return Task.CompletedTask;
        }
    }
}
