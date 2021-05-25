using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Requests;
using MetricsManager.Models;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MetricsManager.DAL.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly ILogger<DotNetMetricJob> _logger;

        public DotNetMetricJob(
            IDotNetMetricsRepository repository,
            IAgentsRepository agentsRepository,
            IMetricsAgentClient metricsAgentClient,
            ILogger<DotNetMetricJob> logger)
        {
            _repository = repository;
            _agentsRepository = agentsRepository;
            _metricsAgentClient = metricsAgentClient;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: DotNetMetricsJob started");
            var allAgentsList = _agentsRepository.GetAllAgentsList();

            foreach (var agent in allAgentsList)
            {
                var fromTime = _repository.GetLastTimeFromAgent(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                try
                {
                    _logger.LogInformation($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: DotNetMetricJob try GetDotNetMetrics " +
                        $"from {fromTime} to {toTime}, agentAddr {agent.AgentAddress}");

                    var outerMetrics = _metricsAgentClient.GetAllDotNetMetrics(new GetAllDotNetMetricsApiRequest
                    {
                        ClientBaseAddress = agent.AgentAddress,
                        FromTime = fromTime,
                        ToTime = toTime
                    });

                    if (outerMetrics != null)
                    {
                        foreach (var singleMetric in outerMetrics.Metrics)
                        {
                            _logger.LogInformation($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: DotNetMetricJob write DotNet metric to DB from agentId {agent.AgentId}," +
                                $" {singleMetric.Time} value:{singleMetric.Value}");

                            _repository.Create(new DotNetMetric
                            {
                                AgentId = agent.AgentId,
                                Value = singleMetric.Value,
                                Time = singleMetric.Time
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("DotNetMetricJob " + ex.Message);
                }
            }

            return Task.CompletedTask;
        }
    }
}
