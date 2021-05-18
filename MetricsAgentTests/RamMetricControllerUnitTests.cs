using System;
using System.Collections.Generic;
using System.Text;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repository;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> logger;
        private Mock<IRamMetricsRepository> mock;

        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            logger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(logger.Object, mock.Object); ;
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.DAL.Requests.RamMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }

    public class RamMetricTimeToTimeTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> logger;
        private Mock<IRamMetricsRepository> mock;

        public RamMetricTimeToTimeTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            logger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(logger.Object, mock.Object);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            var returnList = new List<RamMetric>();
            mock.Setup(repository => repository.GetFromTimeToTime(It.IsAny<DateTimeOffset>().ToUnixTimeSeconds(), It.IsAny<DateTimeOffset>().ToUnixTimeSeconds())).Returns(returnList);
            var result = controller.GetFromTimeToTime(DateTimeOffset.FromUnixTimeSeconds(10), DateTimeOffset.FromUnixTimeSeconds(20));
            mock.Verify(repository => repository.GetFromTimeToTime(10, 20), Times.AtLeastOnce());
        }
    }
}
