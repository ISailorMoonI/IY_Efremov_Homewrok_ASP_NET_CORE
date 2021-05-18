using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.DTO
{
    public class NetworkMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
