using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeworkASPNET.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly List<WeatherInfo> _info = new List<WeatherInfo>();

        public CrudController(WeatherInfo info) => this._info = info;


        [HttpPost("create")]
        public IActionResult Create([FromQuery] int Temperature)
        {
            _info.Add(Temperature);
            return Ok();
        }
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_info);
        }
        [HttpPut("update")]
        public IActionResult Update([FromQuery] int Temperature, DateTime Date, [FromQuery] int newTemperature)
        {
            for (int i = 0; i < _info.Count; i++)
            {
                if (_info[i] == Temperature)
                    _info[i] = newTemperature;
            }
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            holder.Values = holder.Values.Where(w => w != stringsToDelete).ToList();
            return Ok();
        }
    }
}
