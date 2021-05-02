using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace homeworkASPNET.Models
{
    public class ValuesHolder
    {
        public int temperature { get; set; }
        public DateTime date { get; set; }

        private List<ValuesHolder> _holder = new List<ValuesHolder>();
    }
    
    }
