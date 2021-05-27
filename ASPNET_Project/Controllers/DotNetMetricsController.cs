﻿using Microsoft.AspNetCore.Http;
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
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgentIdTimeToTime([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/DotnetMetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            IList<DotNetMetric> metrics = _repository.GetMetricsFromAgentIdTimeToTime(agentId, fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new DotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricResponseDto>()
            };

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<DotNetMetricResponseDto>(metric));
                }
                return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClusterTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/DotnetMetrics/cluster/from/{fromTime}/to/{toTime}");
            IList<DotNetMetric> metrics = _repository.GetMetricsFromAllClusterTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new DotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricResponseDto>()
            };

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<DotNetMetricResponseDto>(metric));
                }
                return Ok(response);
        }
    }
}
