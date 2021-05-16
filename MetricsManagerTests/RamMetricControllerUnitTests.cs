using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL;
using MetricsManager.DAL.Repository;

namespace MetricsManagerTests
{
    public class RamMetricControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> _logger;
        private readonly IMapper _mapper;

        public RamMetricControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            _logger = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_logger.Object, mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgentIdTimeToTime_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = _controller.GetMetricsFromAgentIdTimeToTime(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllClusterTimeToTime_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = _controller.GetMetricsFromAllClusterTimeToTime(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
