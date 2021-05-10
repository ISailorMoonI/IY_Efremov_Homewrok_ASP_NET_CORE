using AutoMapper;
using MetricsAgent.DAL.DTO;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // добавлять сопоставления в таком стиле нужно для всех объектов 
            CreateMap<CpuMetric, CpuMetricDto>();
        }
    }
}

