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
    public class NetworkMetricsControllerUnitTests
        {
            private NetworkMetricsController controller;
            private Mock<INetworkMetricsRepository> mock;

            public NetworkMetricsControllerUnitTests()
            {
                mock = new Mock<INetworkMetricsRepository>();
                controller = new NetworkMetricsController(mock.Object);
            }

            [Fact]
            public void Create_ShouldCall_Create_From_Repository()
            {
                mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();
                var result = controller.Create(new MetricsAgent.DAL.Requests.NetworkMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
                mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
            }
        }
    }
