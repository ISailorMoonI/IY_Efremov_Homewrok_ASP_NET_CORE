using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsManager.DAL;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Interfaces;

namespace MetricsManagerTests
{
    public class AgentControllerUnitTests
    {
        private AgentsController _controller;
        private Mock<ILogger<AgentsController>> _logger;
        private Mock<IAgentsRepository> _repository;

        public AgentControllerUnitTests()
        {
            _logger = new Mock<ILogger<AgentsController>>();
            _repository = new Mock<IAgentsRepository>();
            _controller = new AgentsController(_logger.Object, _repository.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            _repository.Setup(repo => repo.RegisterAgent(new Agent())).Verifiable();
            var result = _controller.RegisterAgent(new Agent());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void RemoveAgent_ReturnsOk()
        {
            _repository.Setup(repo => repo.RemoveAgent(new Agent())).Verifiable();
            var result = _controller.DeleteAgent(new Agent());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetAllAgentsList_ReturnsOk()
        {
            _repository.Setup(repo => repo.GetAllAgentsList()).Returns(new List<Agent>()).Verifiable();
            var result = _controller.GetAllAgentsList();
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
