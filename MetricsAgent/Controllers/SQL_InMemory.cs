using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using MetricsAgent.DAL.Responses;
using Microsoft.Extensions.Logging;
using CpuMetricDto = MetricsAgent.DAL.DTO.CpuMetricDto;

namespace MetricsAgent.Controllers
{
    [Route("api/sql")]
    [ApiController]
    public class SQLController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        public SQLController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в SQLController");
        }

        [HttpGet("version")]
        public IActionResult TryToSqlLite()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();

                _logger.LogInformation($"{System.DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent / TryToSqlLite " +
                    $"api/sql/version {version}");

                return Ok(version);
            }
        }

        [HttpGet("read-write-test")]
        public IActionResult TryToInsertAndRead()
        {
            string connectionString = "Data Source=:memory:";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(11,222)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(55,4444)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(66,6666)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(77,7777)";
                    command.ExecuteNonQuery();

                    string readQuery = "SELECT * FROM cpumetrics";

                    var returnArray = new CpuMetricDto[4];
                    command.CommandText = readQuery;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var counter = 0;
                        while (reader.Read())
                        {
                            returnArray[counter] = new CpuMetricDto()
                            {
                                Id = reader.GetInt32(0), 
                                Value = reader.GetInt32(1), 
                                Time = (int)reader.GetInt64(2)
                            };
                            counter++;
                        }
                    }

                    string resultStr = "";
                    foreach (var item in returnArray)
                    {
                        resultStr = resultStr + item.Time + " " + item.Id + " " + item.Value + " \r\n";
                    }
                    _logger.LogInformation($"{System.DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent / TryToInsertAndRead " +
                        $"api/sql/read-write-test result: {resultStr}");
                    return Ok(returnArray);
                }
            }
        }
    }
}
