using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.DAL;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repository;
using MetricsManager.Models;

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
            mock.Setup(a => a.GetMetricsFromAgentIdTimeToTime(agentId, 10, 100)).Returns(new List<DotNetMetric>()).Verifiable();
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            _logger.Verify();
        }

        [Fact]
        public void GetMetricsFromAllClusterTimeToTime_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            mock.Setup(a => a.GetMetricsFromAllClusterTimeToTime(10, 100)).Returns(new List<DotNetMetric>()).Verifiable();
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            _logger.Verify();
        }
    }
}

