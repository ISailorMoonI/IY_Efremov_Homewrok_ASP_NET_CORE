using System;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class UnitTest1
    {
        public class CpuControllerUnitTests
        {
            private CpuMetricsController controller;

            public CpuControllerUnitTests()
            {
                controller = new CpuMetricsController();
            }

            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                //Arrange
                var agentId = 1;
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

                // Assert
                _ = Assert.IsAssignableFrom<IActionResult>(result);
            }
        }

        public class DotNetMetricsControllerTests
        {
            private DotNetMetricsController controller;

            public DotNetMetricsControllerTests()
            {
                controller = new DotNetMetricsController();
            }

            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                //Arrange
                var agentId = 1;
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetErrorsCountFromAgent(agentId, fromTime, toTime);

                // Assert
                _ = Assert.IsAssignableFrom<IActionResult>(result);
            }
        }

        public class NetworkMetricsControllerTests
        {
            private NetworkMetricsController controller;

            public NetworkMetricsControllerTests()
            {
                controller = new NetworkMetricsController();
            }

            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                //Arrange
                var agentId = 1;
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetNetworkMetricsFromAgent(agentId, fromTime, toTime);

                // Assert
                _ = Assert.IsAssignableFrom<IActionResult>(result);
            }
        }
        public class HddMetricsControllerTests
        {
            private HddMetricsController controller;

            public HddMetricsControllerTests()
            {
                controller = new HddMetricsController();
            }

            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                //Arrange
                var agentId = 1;
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetHddSpaceLeft(agentId, fromTime, toTime);

                // Assert
                _ = Assert.IsAssignableFrom<IActionResult>(result);
            }
        }
        public class RamMetricsControllerTests
        {
            private RamMetricsController controller;

            public RamMetricsControllerTests()
            {
                controller = new RamMetricsController();
            }

            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                //Arrange
                var agentId = 1;
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetRamSpaceLeft(agentId, fromTime, toTime);

                // Assert
                _ = Assert.IsAssignableFrom<IActionResult>(result);
            }
        }
    }
}
