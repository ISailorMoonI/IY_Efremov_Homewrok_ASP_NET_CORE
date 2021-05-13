using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Requests;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository repository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
            this.repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            repository.Create(new RamMetric()
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

            var response = new RamMetricsResponse()
            {
                Metrics = new List<RamMetricResponseDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricResponseDto() { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/cpumetrics/from/{fromTime}/to/{toTime}");

            IList<RamMetric> metrics = repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<RamMetricResponseDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new RamMetricResponseDto() { Time = metric.Time, Value = metric.Value, Id = metric.Id });
                }
            }
            return Ok(response);
        }
    }
}
