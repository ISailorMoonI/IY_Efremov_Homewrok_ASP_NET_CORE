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
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly ILogger<NetworkMetricJob> _logger;

        public NetworkMetricJob(
            INetworkMetricsRepository repository,
            IAgentsRepository agentsRepository,
            IMetricsAgentClient metricsAgentClient,
            ILogger<NetworkMetricJob> logger)
        {
            _repository = repository;
            _agentsRepository = agentsRepository;
            _metricsAgentClient = metricsAgentClient;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: NetWorkMetricsJob started");
            var allAgentsList = _agentsRepository.GetAllAgentsList();

            foreach (var agent in allAgentsList)
            {
                var fromTime = _repository.GetLastTimeFromAgent(agent.AgentId);
                var toTime = DateTimeOffset.UtcNow;

                try
                {
                    _logger.LogInformation(
                        $"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: NetWorkMetricJob try GetNetWorkMetrics " +
                        $"from {fromTime} to {toTime}, agentAddr {agent.AgentAddress}");

                    var outerMetrics = _metricsAgentClient.GetAllNetworkMetrics(new GetAllNetworkMetricsApiRequest
                    {
                        ClientBaseAddress = agent.AgentAddress,
                        FromTime = fromTime,
                        ToTime = toTime
                    });

                    if (outerMetrics != null)
                    {
                        foreach (var singleMetric in outerMetrics.Metrics)
                        {
                            _logger.LogInformation(
                                $"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: NetWorkMetricJob write NetWork metric to DB from agentId {agent.AgentId}," +
                                $" {singleMetric.Time} value:{singleMetric.Value}");

                            _repository.Create(new NetworkMetric
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
                    _logger.LogError("NetWorkMetricJob " + ex.Message);
                }
            }

            return Task.CompletedTask;
        }
    }
}
