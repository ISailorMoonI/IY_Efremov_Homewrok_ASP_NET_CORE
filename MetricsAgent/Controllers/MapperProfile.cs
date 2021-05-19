using AutoMapper;
using MetricsAgent.DAL.Responses;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
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

