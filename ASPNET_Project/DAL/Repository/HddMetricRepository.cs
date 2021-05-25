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
    

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly ILogger<HddMetricsRepository> _logger;
        public HddMetricsRepository(ILogger<HddMetricsRepository> logger)
        {
            _logger = logger;
            SqlMapper.AddTypeHandler(new DapperDateTimeOffsetHandler());
        }

        public void Create(HddMetric singleMetric)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    var timeInseconds = singleMetric.Time.ToUniversalTime().ToUnixTimeSeconds();
                    connection.Execute("INSERT INTO hddmetrics(AgentId, value, time) VALUES(@agent_id, @value, @time)",
                        new
                        {
                            agent_id = singleMetric.AgentId,
                            value = singleMetric.Value,
                            time = timeInseconds
                        });

                    var getALL = connection.Query<HddMetric>("SELECT * FROM hddmetrics", null).ToList();
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
                    var timeFromAgent = connection.QueryFirstOrDefault<DateTimeOffset>("SELECT time FROM hddmetrics WHERE AgentId=@agent_id ORDER BY id DESC",
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

        public IList<HddMetric> GetMetricsFromAgentIdTimeToTime(int agentId, long fromTime, long toTime)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<HddMetric>("SELECT Id, AgentId, Value, Time FROM hddmetrics WHERE (AgentId=@agentId) and ((time>=@fromTime) AND (time<=@toTime))",
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

        public IList<HddMetric> GetMetricsFromAllClusterTimeToTime(long fromTime, long toTime)
        {
            try
            {
                using (var connection = new SQLiteConnection(DataBaseConnection.DataBaseManagerConnectionSettings.ConnectionString))
                {
                    return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
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