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
    public class HddMetricsControllerUnitTests
        {
            private HddMetricsController controller;
            private Mock<IHddMetricsRepository> mock;

            public HddMetricsControllerUnitTests()
            {
                mock = new Mock<IHddMetricsRepository>();
                controller = new HddMetricsController(mock.Object);
            }

            [Fact]
            public void Create_ShouldCall_Create_From_Repository()
            {
                mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
                var result = controller.Create(new MetricsAgent.DAL.Requests.HddMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
                mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
            }
        }
    }
