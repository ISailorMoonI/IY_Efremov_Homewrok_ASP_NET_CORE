using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricResponseDto> Metrics { get; set; }
    }

    public class CpuMetricResponseDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
