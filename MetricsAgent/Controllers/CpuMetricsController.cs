using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.DTO;
using MetricsAgent.DAL.Interfaces;
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
        private ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>());
            var m = config.CreateMapper();
            IList<CpuMetric> metrics = _repository.GetAll();
            var response = new CpuMetricsResponse()
            {
                Metrics = new List<CpuMetricResponseDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<CpuMetricResponseDto>(metric));
            }
            return Ok(response);
        }

        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/cpumetrics/from/{fromTime}/to/{toTime}");

            IList<CpuMetric> metrics = _repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<CpuMetricResponseDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<CpuMetricResponseDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}


