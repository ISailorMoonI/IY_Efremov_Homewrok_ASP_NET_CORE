using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repository
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        public IList<HddMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            var returnList = new List<HddMetric>();


            return returnList;
        }

        public IList<HddMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            var returnList = new List<HddMetric>();


            return returnList;
        }
    }
}
