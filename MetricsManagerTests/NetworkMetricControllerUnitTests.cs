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
    public class NetworkMetricControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsController>> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            _logger = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(_logger.Object, mock.Object, _mapper);
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
