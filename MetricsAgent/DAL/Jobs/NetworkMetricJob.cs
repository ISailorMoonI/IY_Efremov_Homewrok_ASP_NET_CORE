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
    public class NetWorkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _netWorkCounter;

        public NetWorkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            PerformanceCounterCategory netWorkCategory = new PerformanceCounterCategory("Network Interface");
            string[] networkInstNames = netWorkCategory.GetInstanceNames();
            _netWorkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkInstNames[0]);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var NetWorkUsageInPercents = Convert.ToInt32(_netWorkCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new NetworkMetric {Time = time, Value = NetWorkUsageInPercents});

            return Task.CompletedTask;
        }
    }
}
