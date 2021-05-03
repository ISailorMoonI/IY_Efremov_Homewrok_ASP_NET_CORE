using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homeworkASPNET.Models;

namespace MetricsManager.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public CrudController(ValuesHolder holder)
        {
            this._holder = holder;
        }


        [HttpPost("create")]
        public IActionResult Create([FromQuery] int temperature, DateTime date)
        {
            holder.Add(temperature, date);
            return Ok();
        }
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(holder);
        }
        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] DateTime newDate)
        {
            for (int i = 0; i < holder.Count; i++)
            {
                if (holder[i] == date)
                    holder[i] = newDate;
            }
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime from, DateTime to)
        {
            for (int i = ValuesHolder.Count - 1; i>=0; i--)
            {
                if (ValuesHolder[i].Date >= from && ValuesHolder[i].Date <= to)
                {
                    ValuesHolder.RemoveAt(i);
                }
            }
            return Ok();
        }
    }
}
