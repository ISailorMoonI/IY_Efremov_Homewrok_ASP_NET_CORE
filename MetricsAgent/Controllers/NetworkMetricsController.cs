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
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
<<<<<<< HEAD
        private INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;
=======
        private INetworkMetricsRepository repository;
>>>>>>> master

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
<<<<<<< HEAD
            _repository = repository;
            _mapper = mapper;
=======
            this.repository = repository;
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            repository.Create(new NetworkMetric
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetric, NetworkMetricDto>());
            var m = config.CreateMapper();
            IList<NetworkMetric> metrics = _repository.GetAll();
            var response = new NetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricResponseDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<NetworkMetricResponseDto>(metric));
            }
            return Ok(response);
        }

        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/Networkmetrics/from/{fromTime}/to/{toTime}");

            IList<NetworkMetric> metrics = _repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new NetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricResponseDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<NetworkMetricResponseDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}
