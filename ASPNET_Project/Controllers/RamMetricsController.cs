using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repository;
using MetricsManager.DAL.Responses;
using MetricsManager.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgentIdTimeToTime([FromRoute] int agentId,
            [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation(
                $"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/RamMetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            IList<RamMetric> metrics = _repository.GetMetricsFromAgentIdTimeToTime(agentId,
                fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<RamMetricResponseDto>()
            };

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<RamMetricResponseDto>(metric));
                }

                return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClusterTimeToTime([FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation(
                $"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/RamMetrics/cluster/from/{fromTime}/to/{toTime}");
            IList<RamMetric> metrics =
                _repository.GetMetricsFromAllClusterTimeToTime(fromTime.ToUnixTimeSeconds(),
                    toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<RamMetricResponseDto>()
            };

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<RamMetricResponseDto>(metric));
                }
                return Ok(response);
        }
    }
}