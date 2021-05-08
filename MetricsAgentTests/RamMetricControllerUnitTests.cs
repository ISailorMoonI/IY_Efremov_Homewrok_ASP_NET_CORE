using System;
using System.Collections.Generic;
using System.Text;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repository;
using MetricsAgent.Models;
using Moq;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
        {
            private RamMetricsController controller;
            private Mock<IRamMetricsRepository> mock;

            public RamMetricsControllerUnitTests()
            {
                mock = new Mock<IRamMetricsRepository>();
                controller = new RamMetricsController(mock.Object);
            }

            [Fact]
            public void Create_ShouldCall_Create_From_Repository()
            {
                mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();
                var result = controller.Create(new MetricsAgent.DAL.Requests.RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
                mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
            }
        }
    }
