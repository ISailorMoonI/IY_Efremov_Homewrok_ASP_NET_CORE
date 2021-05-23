using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.DAL;
using MetricsManager.DAL.Repository;

namespace MetricsManagerTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private Mock<ILogger<DotNetMetricsController>> _logger;
        private readonly IMapper _mapper;

        public DotNetControllerUnitTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            _logger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(_logger.Object, mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgentIdTimeToTime_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAgentIdTimeToTime(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllClusterTimeToTime_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAllClusterTimeToTime(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

