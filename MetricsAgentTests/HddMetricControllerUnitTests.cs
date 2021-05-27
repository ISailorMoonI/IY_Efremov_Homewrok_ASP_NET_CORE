﻿using System;
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
    public class HddMetricTimeToTimeTests
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsController>> logger;
        private readonly IMapper _mapper;

        public HddMetricTimeToTimeTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            logger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(logger.Object, mock.Object, _mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetFromTimeToTime(5, 10)).Returns(new List<HddMetric>()).Verifiable();
            var result = controller.GetFromTimeToTime(fromTime, toTime);
            mock.Verify(repository => repository.GetFromTimeToTime(5, 10), Times.AtMostOnce());
            logger.Verify();
        }
    }
}
