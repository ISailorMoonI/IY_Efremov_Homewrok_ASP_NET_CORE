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

namespace MetricsManagerTests
{
    public class AgentControllerUnitTests
    {
        private AgentsController controller;
        private List<Agent> _registeredAgents = new List<Agent>();
        private Mock<ILogger<AgentsController>> logger;

        public AgentControllerUnitTests()
        {
            _registeredAgents.Add(new Agent());
            logger = new Mock<ILogger<AgentsController>>();
            controller = new AgentsController(_registeredAgents, logger.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var result = controller.RegisterAgent(_registeredAgents[0]);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var result = controller.EnableAgentById(_registeredAgents[0].AgentId);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var result = controller.DisableAgentById(_registeredAgents[0].AgentId);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
