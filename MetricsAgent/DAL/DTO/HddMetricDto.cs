using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.DTO
{
    public class HddMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
