using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homeworkASPNET.Models;

namespace homeworkASPNET.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder holder;

        public CrudController(ValuesHolder holder)
        {
            this.holder = holder;
        }


        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime? date, [FromQuery] int? temperature)
        {
            DataAndTemperature dt = new DataAndTemperature();

            if (date != null)
            {
                dt.Date = (DateTime)date;
            }
            else
            {
                dt.Date = DateTime.Now;
            }

            if (temperature != null)
            {
                dt.Temperature = (int)temperature;
                holder.Values.Add(dt); 
            }

            return Ok();
        }
        [HttpGet("read")]

        public IActionResult Read()
        {
            return Ok(holder);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime? date, [FromQuery] int? newValue)
        {
            if (date != null && newValue != null)
            {
                bool founded = false;

                foreach (DataAndTemperature data in holder.Values)
                {
                    if (data.Date == date)
                    {
                        data.Temperature = (int)newValue;
                        founded = true;
                    }
                }

                if (!founded)
                    return BadRequest();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime? dateStart, [FromQuery] DateTime? dateEnd)
        {
            if (dateEnd == null)
            {
                dateEnd = DateTime.MaxValue;
            }

            if (dateStart == null)
            {
                dateStart = DateTime.MinValue;
            }

            bool founded = false;
            // !!! backward direction only!
            for (int i = holder.Values.Count - 1; i >= 0; i--)
            {
                if (holder.Values[i].Date >= dateStart && holder.Values[i].Date <= dateEnd)
                {
                    holder.Values.RemoveAt(i);
                    founded = true;
                }
            }

            if (!founded)
                return BadRequest();

            return Ok();
        }
    }
}
