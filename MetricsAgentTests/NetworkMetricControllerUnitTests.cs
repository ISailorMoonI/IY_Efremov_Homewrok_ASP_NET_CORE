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

namespace MetricsAgentTests
{
    public class NetworkMetricTimeToTimeTests
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsController>> logger;
        private readonly IMapper _mapper;

        public NetworkMetricTimeToTimeTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            logger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(logger.Object, mock.Object, _mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            var returnList = new List<NetworkMetric>();
            mock.Setup(repository => repository.GetFromTimeToTime(It.IsAny<DateTimeOffset>().ToUnixTimeSeconds(), It.IsAny<DateTimeOffset>().ToUnixTimeSeconds())).Returns(returnList);
            IActionResult result = controller.GetFromTimeToTime(DateTimeOffset.FromUnixTimeSeconds(10), DateTimeOffset.FromUnixTimeSeconds(20));
            mock.Verify(repository => repository.GetFromTimeToTime(10, 20), Times.AtLeastOnce());
        }
    }
}

