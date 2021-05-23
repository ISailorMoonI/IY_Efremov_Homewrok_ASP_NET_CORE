using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class HddMetricsResponse
    {
        public List<HddMetricResponseDto> Metrics { get; set; }
    }

    public class HddMetricResponseDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
