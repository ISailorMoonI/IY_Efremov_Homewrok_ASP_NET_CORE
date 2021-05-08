using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Moq;
using System;
using MetricsAgent.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
        {
            private CpuMetricsController controller;
            private Mock<ICpuMetricsRepository> mock;

            public CpuMetricsControllerUnitTests()
            {
                mock = new Mock<ICpuMetricsRepository>();
                controller = new CpuMetricsController(mock.Object);
            }

            [Fact]
            public void Create_ShouldCall_Create_From_Repository()
            {
                mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
                var result = controller.Create(new MetricsAgent.DAL.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
                mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
            }
        }
    }

