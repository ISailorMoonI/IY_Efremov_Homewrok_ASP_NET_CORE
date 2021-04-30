using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeworkASPNET
{
    public class ValuesHolder
    {
        private List<ValuesHolder> _holder = new List<ValuesHolder>();

        public int temperature { get; set; }
        public DateTime Date { get; set; }

    }
}


