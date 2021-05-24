using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL.Requests;
using MetricsManager.DAL.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
        {
            CpuMetricsResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);
            DotNetMetricsResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request);
            HddMetricsResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);
            NetworkMetricsResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
            RamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
        }
    }

