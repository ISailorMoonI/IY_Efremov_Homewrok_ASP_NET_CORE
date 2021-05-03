using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace homeworkASPNET.Models
{
    public class ValuesHolder
        {
            public List<DataAndTemperature> Values { get; set; }
            public ValuesHolder()
            {
                Values = new List<DataAndTemperature>();
            }
        }
}
    
