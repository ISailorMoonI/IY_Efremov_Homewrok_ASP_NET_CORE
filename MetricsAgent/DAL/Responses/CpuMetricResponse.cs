using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricResponse> Metrics { get; set; }
    }

    public class CpuMetricResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
