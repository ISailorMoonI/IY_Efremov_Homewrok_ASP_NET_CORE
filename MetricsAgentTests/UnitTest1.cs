using MetricsAgent.Controllers;
using MetricsAgent.DAL.Requests;
using MetricsAgent.Models;
using Moq;
using System;
using MetricsAgent.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class UnitTest1
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
                // устанавливаем параметр заглушки
                // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
                mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

                // выполняем действие на контроллере
                var result = controller.Create(new MetricsAgent.DAL.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

                // проверяем заглушку на то, что пока работал контроллер
                // действительно вызвался метод Create репозитория с нужным типом объекта в параметре //////////////
                mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
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
