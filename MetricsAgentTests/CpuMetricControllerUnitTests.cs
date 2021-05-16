using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Moq;
using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricTimeToTimeTests
    {
        private CpuMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuMetricsController>> logger;
        private readonly IMapper _mapper;

        public CpuMetricTimeToTimeTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            logger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(logger.Object, mock.Object, _mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            var returnList = new List<CpuMetric>();
            mock.Setup(repository => repository.GetFromTimeToTime(It.IsAny<DateTimeOffset>().ToUnixTimeSeconds(), It.IsAny<DateTimeOffset>().ToUnixTimeSeconds())).Returns(returnList);
            IActionResult result = controller.GetFromTimeToTime(DateTimeOffset.FromUnixTimeSeconds(10), DateTimeOffset.FromUnixTimeSeconds(20));
            mock.Verify(repository => repository.GetFromTimeToTime(10, 20), Times.AtLeastOnce());
        }
    }
}
