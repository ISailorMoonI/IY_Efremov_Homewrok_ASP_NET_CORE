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
            var returnList = new List<DotNetMetric>();
            mock.Setup(repository => repository.GetFromTimeToTime(It.IsAny<DateTimeOffset>().ToUnixTimeSeconds(), It.IsAny<DateTimeOffset>().ToUnixTimeSeconds())).Returns(returnList);
            IActionResult result = controller.GetFromTimeToTime(DateTimeOffset.FromUnixTimeSeconds(10), DateTimeOffset.FromUnixTimeSeconds(20));
            mock.Verify(repository => repository.GetFromTimeToTime(10, 20), Times.AtLeastOnce());
        }
    }
}
