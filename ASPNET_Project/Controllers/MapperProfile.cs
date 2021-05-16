using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL.Responses;
using MetricsManager.Models;
using AutoMapper;

namespace MetricsManager.Controllers
{
    public class MapperProfile : Profile
    {
            public MapperProfile()
            {
                CreateMap<CpuMetric, CpuMetricResponseDto>();
                CreateMap<DotNetMetric, DotNetMetricResponseDto>();
                CreateMap<HddMetric, HddMetricResponseDto>();
                CreateMap<NetworkMetric, NetworkMetricResponseDto>();
                CreateMap<RamMetric, RamMetricResponseDto>();
            }
        }
    }
