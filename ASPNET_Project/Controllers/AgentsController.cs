﻿using Microsoft.AspNetCore.Mvc;
using System;
using MetricsManager.DAL;
using System.Collections.Generic;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] Agent agent)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/register: AgentAdress={agent.AgentAddress}");
            _repository.RegisterAgent(agent);
            return Ok();
        }


        [HttpPost("delete")]
        public IActionResult DeleteAgent([FromBody] Agent agent)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/delete: AgentAdress={agent.AgentAddress}");
            _repository.RemoveAgent(agent);
            return Ok();
        }

        [HttpGet("getAgentsList")]
        public IActionResult GetAllAgentsList()
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/GetAllAgentsList");
            var agentList = _repository.GetAllAgentsList();
            return Ok(agentList);
        }
    }
}
