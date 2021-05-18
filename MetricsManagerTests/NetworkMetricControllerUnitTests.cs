using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using MetricsManager.DAL;
using MetricsManager.DAL.Repository;

namespace MetricsManagerTests
{
    public class NetworkMetricControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsController>> logger;

        public NetworkMetricControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            logger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(logger.Object, mock.Object);
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
    }
}
