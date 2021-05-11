using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Requests;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/cpu")]
    [ApiController]

    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            this.repository = repository;
        }

        private ICpuMetricsRepository repository;

        public CpuMetricsController(ICpuMetricsRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            repository.Create(new CpuMetric()
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repository.GetAll();

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<CpuMetricResponseDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricResponseDto {Time = metric.Time, Value = metric.Value, Id = metric.Id});
            }

            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/cpumetrics/from/{fromTime}/to/{toTime}");

            IList<CpuMetric> metrics = repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<CpuMetricResponseDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new CpuMetricResponseDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
                }
            }
            return Ok(response);
        }

    }
}

