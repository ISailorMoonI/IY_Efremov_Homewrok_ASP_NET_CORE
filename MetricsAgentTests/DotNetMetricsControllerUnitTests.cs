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
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> logger;
        private Mock<IDotNetMetricsRepository> mock;

        public DotNetMetricsControllerUnitTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            logger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(logger.Object, mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.DAL.Requests.DotNetMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
    }
    public class DotNetMetricTimeToTimeTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> logger;
        private Mock<IDotNetMetricsRepository> mock;

        public DotNetMetricTimeToTimeTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            logger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(logger.Object, mock.Object);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            var returnList = new List<DotNetMetric>();
            mock.Setup(repository => repository.GetFromTimeToTime(It.IsAny<DateTimeOffset>().ToUnixTimeSeconds(), It.IsAny<DateTimeOffset>().ToUnixTimeSeconds())).Returns(returnList);
            var result = controller.GetFromTimeToTime(DateTimeOffset.FromUnixTimeSeconds(10), DateTimeOffset.FromUnixTimeSeconds(20));
            mock.Verify(repository => repository.GetFromTimeToTime(10, 20), Times.AtLeastOnce());
        }
    }
}
