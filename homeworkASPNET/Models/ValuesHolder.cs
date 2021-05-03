using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace homeworkASPNET.Models
{
    public class ValuesHolder
    {
        public List<ValuesHolder> Holder { get; set; } = new List<ValuesHolder>();

        public void Add(ValuesHolder values)
        {
            Holder.Add(values);
        }
        public int temperature { get; set; }
        public DateTime Date { get; set; }

    }
}
    
