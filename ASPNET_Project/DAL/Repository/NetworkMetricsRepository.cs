using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.DAL.Repository
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly ILogger<NetworkMetricsRepository> _logger;

        public NetworkMetricsRepository(ILogger<NetworkMetricsRepository> logger)
        {
            _logger = logger;
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public void Create(NetworkMetric singleMetric)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    var timeInseconds = singleMetric.Time.ToUniversalTime().ToUnixTimeSeconds();
                    connection.Execute("INSERT INTO NetworkMetrics(AgentId, value, time) VALUES(@agent_id, @value, @time)",
                        new
                        {
                            agent_id = singleMetric.AgentId,
                            value = singleMetric.Value,
                            time = timeInseconds
                        });

                    var getALL = connection.Query<NetworkMetric>("SELECT * FROM NetworkMetrics", null).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public DateTimeOffset GetLastTimeFromAgent(int agent_id)
        {
            DateTimeOffset lastTime;
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    var timeFromAgent = connection.QueryFirstOrDefault<DateTimeOffset>("SELECT time FROM NetworkMetrics WHERE AgentId=@agent_id ORDER BY id DESC",
                        new
                        {
                            agent_id = agent_id
                        });

                    if (timeFromAgent.Year == 1)
                        lastTime = DateTimeOffset.UnixEpoch;
                    else
                    {
                        lastTime = timeFromAgent;
                    }

                    return lastTime;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return DateTimeOffset.UtcNow;
        }

        public IList<NetworkMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<NetworkMetric>("SELECT Id, AgentId, Value, Time FROM NetworkMetrics WHERE (AgentId=@agentId) and ((time>=@fromTime) AND (time<=@toTime))",
                        new
                        {
                            fromTime = fromTime,
                            toTime = toTime,
                            agentId = agentId
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public IList<NetworkMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<NetworkMetric>("SELECT * FROM NetworkMetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                        new
                        {
                            fromTime = fromTime,
                            toTime = toTime,
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
