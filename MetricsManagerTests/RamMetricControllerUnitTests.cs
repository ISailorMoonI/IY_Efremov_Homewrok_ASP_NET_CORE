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
            mock.Setup(a => a.GetMetricsFromAgentIdTimeToTime(agentId, 10, 100)).Returns(new List<RamMetric>()).Verifiable();
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            _logger.Verify();
        }

        [Fact]
        public void GetMetricsFromAllClusterTimeToTime_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            mock.Setup(a => a.GetMetricsFromAllClusterTimeToTime(10, 100)).Returns(new List<RamMetric>()).Verifiable();
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            _logger.Verify();
        }
    }
}
