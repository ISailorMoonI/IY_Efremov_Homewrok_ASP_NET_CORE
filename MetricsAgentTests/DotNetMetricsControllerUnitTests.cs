using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repository;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using DateTimeOffset = System.DateTimeOffset;

namespace MetricsAgentTests
{
    public class DotNetMetricTimeToTimeTests
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private Mock<ILogger<DotNetMetricsController>> logger;
        private readonly IMapper _mapper;

        public DotNetMetricTimeToTimeTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            logger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(logger.Object, mock.Object, _mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetFromTimeToTime(5, 10)).Returns(new List<DotNetMetric>()).Verifiable();
            var result = controller.GetFromTimeToTime(fromTime, toTime);
            mock.Verify(repository => repository.GetFromTimeToTime(5, 10), Times.AtMostOnce());
            logger.Verify();
        }
    }
}
