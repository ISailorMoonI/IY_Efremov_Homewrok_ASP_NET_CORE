﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Responses
{
    public class RamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }

    public class RamMetricDto
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}