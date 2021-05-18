using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using MetricsManager.Controllers;
using MetricsManager.DAL;
using MetricsManager.DAL.Repository;

namespace MetricsManagerTests
{
    public class HddMetricControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsController>> logger;

        public HddMetricControllerUnitTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            logger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(logger.Object, mock.Object);
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
