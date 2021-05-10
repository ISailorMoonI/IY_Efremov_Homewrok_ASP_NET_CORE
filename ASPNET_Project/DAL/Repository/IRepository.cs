using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime);
        IList<T> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime);
    }
}
