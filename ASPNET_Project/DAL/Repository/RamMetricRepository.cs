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
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly ILogger<RamMetricsRepository> _logger;
        public RamMetricsRepository(ILogger<RamMetricsRepository> logger)
        {
            _logger = logger;
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public void Create(RamMetric singleMetric)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    var timeInseconds = singleMetric.Time.ToUniversalTime().ToUnixTimeSeconds();
                    connection.Execute("INSERT INTO rammetrics(AgentId, value, time) VALUES(@agent_id, @value, @time)",
                        new
                        {
                            agent_id = singleMetric.AgentId,
                            value = singleMetric.Value,
                            time = timeInseconds
                        });

                    var getALL = connection.Query<RamMetric>("SELECT * FROM rammetrics", null).ToList();
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
                    var timeFromAgent = connection.QueryFirstOrDefault<DateTimeOffset>("SELECT time FROM rammetrics WHERE AgentId=@agent_id ORDER BY id DESC",
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

        public IList<RamMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<RamMetric>("SELECT Id, AgentId, Value, Time FROM rammetrics WHERE (AgentId=@agentId) and ((time>=@fromTime) AND (time<=@toTime))",
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

        public IList<RamMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
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
