using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.DAL.Requests;
using MetricsManager.DAL.Responses;

namespace MetricsManager.Client
{
    namespace MetricsManager.Client
    {
        namespace MetricsManager.Client
        {
            public class MetricsAgentClient : IMetricsAgentClient
            {
                private readonly HttpClient _httpClient;
                private readonly ILogger<MetricsAgentClient> _logger;

                public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
                {
                    _httpClient = httpClient;
                    _logger = logger;
                }
                public CpuMetricsResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
                {
                    var fromParameter = request.FromTime.UtcDateTime.ToString("O");
                    var toParameter = request.ToTime.UtcDateTime.ToString("O");

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                        $"{request.ClientBaseAddress}/api/cpumetrics/from/{fromParameter}/to/{toParameter}");
                    try
                    {
                        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                        using var responseStream = response.Content.ReadAsStreamAsync().Result;

                        return JsonSerializer.DeserializeAsync<CpuMetricsResponse>(responseStream,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            }).Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    return null;
                }

                public DotNetMetricsResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request)
                {
                    var fromParameter = request.FromTime.UtcDateTime.ToString("O");
                    var toParameter = request.ToTime.UtcDateTime.ToString("O");

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                        $"{request.ClientBaseAddress}/api/dotnetmetrics/from/{fromParameter}/to/{toParameter}");
                    try
                    {
                        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                        using var responseStream = response.Content.ReadAsStreamAsync().Result;

                        return JsonSerializer.DeserializeAsync<DotNetMetricsResponse>(responseStream,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            }).Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    return null;
                }


                public HddMetricsResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
                {
                    var fromParameter = request.FromTime.UtcDateTime.ToString("O");
                    var toParameter = request.ToTime.UtcDateTime.ToString("O");

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                        $"{request.ClientBaseAddress}/api/hddmetrics/from/{fromParameter}/to/{toParameter}");
                    try
                    {
                        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                        using var responseStream = response.Content.ReadAsStreamAsync().Result;

                        return JsonSerializer.DeserializeAsync<HddMetricsResponse>(responseStream,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            }).Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    return null;
                }

                public NetworkMetricsResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
                {
                    var fromParameter = request.FromTime.UtcDateTime.ToString("O");
                    var toParameter = request.ToTime.UtcDateTime.ToString("O");

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                        $"{request.ClientBaseAddress}/api/networkmetrics/from/{fromParameter}/to/{toParameter}");
                    try
                    {
                        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                        using var responseStream = response.Content.ReadAsStreamAsync().Result;

                        return JsonSerializer.DeserializeAsync<NetworkMetricsResponse>(responseStream,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            }).Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    return null;
                }

                public RamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
                {
                    var fromParameter = request.FromTime.UtcDateTime.ToString("O");
                    var toParameter = request.ToTime.UtcDateTime.ToString("O");

                    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                        $"{request.ClientBaseAddress}/api/rammetrics/from/{fromParameter}/to/{toParameter}");
                    try
                    {
                        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                        using var responseStream = response.Content.ReadAsStreamAsync().Result;

                        return JsonSerializer.DeserializeAsync<RamMetricsResponse>(responseStream,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            }).Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    return null;
                }

            }
        }
    }
}
