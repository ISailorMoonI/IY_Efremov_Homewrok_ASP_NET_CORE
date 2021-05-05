using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
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
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetMetricsFromAgent(fromTime, toTime);

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
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetErrorsCountFromAgent(fromTime, toTime);

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
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetNetworkMetricsFromAgent(fromTime, toTime);

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
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);


                //Act
                var result = controller.GetHddSpaceLeft(fromTime, toTime);

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
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);

                //Act
                var result = controller.GetRamSpaceLeft(fromTime, toTime);

                // Assert
                _ = Assert.IsAssignableFrom<IActionResult>(result);
            }
        }
    }
}
