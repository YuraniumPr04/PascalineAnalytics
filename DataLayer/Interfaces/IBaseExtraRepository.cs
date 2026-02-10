using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IBaseExtraRepository<T> : IBaseRepository<T> where T : class
    {
        public void CreateAsync(T item, int Id);
    }

}
