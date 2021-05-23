using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.DTO;
using MetricsAgent.DAL.Interfaces;
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
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
<<<<<<< HEAD
        private IHddMetricsRepository _repository;
        private readonly IMapper _mapper;
=======
        private IHddMetricsRepository repository;
>>>>>>> master

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
<<<<<<< HEAD
            _repository = repository;
            _mapper = mapper;
=======
            this.repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            repository.Create(new HddMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
>>>>>>> master
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetric, HddMetricDto>());
            var m = config.CreateMapper();
            IList<HddMetric> metrics = _repository.GetAll();
            var response = new HddMetricsResponse()
            {
                Metrics = new List<HddMetricResponseDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<HddMetricResponseDto>(metric));
            }
            return Ok(response);
        }

        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/dotnetmetrics/from/{fromTime}/to/{toTime}");

            IList<HddMetric> metrics = _repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new HddMetricsResponse()
            {
                Metrics = new List<HddMetricResponseDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<HddMetricResponseDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}