using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
    public class HistoryRepository : IBaseRepository<History>
    {
        private readonly ApplicationDBContext _db;

        public HistoryRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public bool Create(History entity)
        {
            if (entity != null)
            {
                _db.Histories.Add(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(History entity)
        {
            if (entity != null)
            {
                _db.Histories.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<History> GetAll()
        {
            return _db.Histories;
        }

        public History Update(History entity)
        {
            _db.Histories.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}
