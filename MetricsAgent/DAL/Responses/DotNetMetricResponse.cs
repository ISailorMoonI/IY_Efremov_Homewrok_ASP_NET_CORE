using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricResponseDto> Metrics { get; set; }
    }

    public class DotNetMetricResponseDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
