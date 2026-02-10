using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces 
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll(); 
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}
