using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetFromTimeToTime(long fromTime, long toTime);

        IList<T> GetAll();

        void Create(T item);
    }
}
