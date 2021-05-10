using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;

    namespace MetricsManager.DAL
    {
        public interface ICpuMetricsRepository : IRepository<CpuMetric> { }

        public class CpuMetricsRepository : ICpuMetricsRepository
        {
            public IList<CpuMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
            {
                var returnList = new List<CpuMetric>();


                return returnList;
            }

            public IList<CpuMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
            {
                var returnList = new List<CpuMetric>();


                return returnList;
            }
        }
    }
}
