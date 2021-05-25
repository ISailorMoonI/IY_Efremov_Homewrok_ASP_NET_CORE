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

        // добавляем парсинг для типа данных DateTimeOffset в возвращаемые значения
        public NetworkMetricsRepository(ILogger<NetworkMetricsRepository> logger)
        {
            _logger = logger;
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public void Create(NetworkMetric singleMetric)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    var timeInseconds = singleMetric.Time.ToUniversalTime().ToUnixTimeSeconds();
                    connection.Execute("INSERT INTO networkmetrics(AgentId, value, time) VALUES(@agent_id, @value, @time)",
                        new
                        {
                            agent_id = singleMetric.AgentId,
                            value = singleMetric.Value,
                            time = timeInseconds
                        });

                    var getALL = connection.Query<NetworkMetric>("SELECT * FROM networkmetrics", null).ToList();
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
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    var timeFromAgent = connection.QueryFirstOrDefault<DateTimeOffset>("SELECT time FROM networkmetrics WHERE AgentId=@agent_id ORDER BY id DESC",
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
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<NetworkMetric>("SELECT Id, AgentId, Value, Time FROM networkmetrics WHERE (AgentId=@agentId) and ((time>=@fromTime) AND (time<=@toTime))",
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
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
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
