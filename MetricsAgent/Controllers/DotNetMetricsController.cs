using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repository;
using MetricsAgent.DAL.Requests;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
<<<<<<< HEAD
        private IDotNetMetricsRepository repository;
=======
<<<<<<< HEAD
        private IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;
=======
        private IDotNetMetricsRepository repository;
>>>>>>> master
>>>>>>> Lesson4

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
<<<<<<< HEAD
=======
<<<<<<< HEAD
            _repository = repository;
            _mapper = mapper;
=======
>>>>>>> Lesson4
            this.repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            repository.Create(new DotNetMetric()
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
>>>>>>> master
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/dotnetmetrics/from/{fromTime}/to/{toTime}");

            IList<DotNetMetric> metrics = _repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new DotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricResponseDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<DotNetMetricResponseDto>(metric));
                }
            }
            return Ok(response);
        }
    }
}