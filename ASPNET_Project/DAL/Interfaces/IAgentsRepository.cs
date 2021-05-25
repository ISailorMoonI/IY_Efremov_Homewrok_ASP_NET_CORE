using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Repository;
using MetricsManager.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentsRepository
    {
        public void RegisterAgent(Agent newAgent);
        public void RemoveAgent(Agent oldAgent);
        public IList<Agent> GetAllAgentsList();
        public string GetAddressForAgentId(int id);
    }
}
