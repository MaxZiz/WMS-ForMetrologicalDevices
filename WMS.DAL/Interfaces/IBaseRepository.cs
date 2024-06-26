using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        bool Create(T entity);

        IQueryable<T> GetAll();

       /* T Get(int id);

        List<T> Select();*/

        bool Delete(T entity);

        T Update(T entity);
    }
}
