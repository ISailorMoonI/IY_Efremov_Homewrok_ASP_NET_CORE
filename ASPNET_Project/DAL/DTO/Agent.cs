using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.DTO
{
    public class Agent
    {
        public int AgentId { get; set; }

        [Required]
        [DefaultValue("http://localhost:5070")]
        public string AgentAddress { get; set; }
    }
}
