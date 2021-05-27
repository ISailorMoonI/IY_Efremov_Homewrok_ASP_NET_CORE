using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repository;
using MetricsManager.Models;

namespace MetricsManagerTests
{
    public class HddMetricControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsController>> _logger;
        private readonly IMapper _mapper;

        public HddMetricControllerUnitTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            _logger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(_logger.Object, mock.Object, _mapper);
        }

        [Fact]
        public void GetMetricsFromAgentIdTimeToTime_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            mock.Setup(a => a.GetMetricsFromAgentIdTimeToTime(agentId, 10, 100)).Returns(new List<HddMetric>()).Verifiable();
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            _logger.Verify();
        }

        [Fact]
        public void GetMetricsFromAllClusterTimeToTime_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            mock.Setup(a => a.GetMetricsFromAllClusterTimeToTime(10, 100)).Returns(new List<HddMetric>()).Verifiable();
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            _logger.Verify();
        }
    }
}
