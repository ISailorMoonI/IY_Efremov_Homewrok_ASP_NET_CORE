using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricResponse> Metrics { get; set; }
    }

    public class DotNetMetricResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
