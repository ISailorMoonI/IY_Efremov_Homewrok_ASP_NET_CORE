using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repository
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric> { }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        public IList<DotNetMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            var returnList = new List<DotNetMetric>();


            return returnList;
        }

        public IList<DotNetMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            var returnList = new List<DotNetMetric>();


            return returnList;
        }
    }
}
