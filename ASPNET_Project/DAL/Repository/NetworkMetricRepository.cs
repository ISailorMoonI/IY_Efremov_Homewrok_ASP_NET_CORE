using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repository
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public IList<NetworkMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            var returnList = new List<NetworkMetric>();


            return returnList;
        }

        public IList<NetworkMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            var returnList = new List<NetworkMetric>();


            return returnList;
        }
    }
}
