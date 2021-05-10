using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repository
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        public IList<RamMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            var returnList = new List<RamMetric>();


            return returnList;
        }

        public IList<RamMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            var returnList = new List<RamMetric>();


            return returnList;
        }
    }
}
