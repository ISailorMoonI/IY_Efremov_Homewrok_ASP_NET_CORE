using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class RamMetricsResponse
    {
        public List<RamMetricResponseDto> Metrics { get; set; }
    }

    public class RamMetricResponseDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
