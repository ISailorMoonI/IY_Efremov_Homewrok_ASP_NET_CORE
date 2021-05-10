using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class NetworkMetricsResponse
    {
        public List<NetworkMetricResponseDto> Metrics { get; set; }
    }

    public class NetworkMetricResponseDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
