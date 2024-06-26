using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
    public class GuideRepository:IBaseRepository<Guide>
    {
        ApplicationDBContext _db;
        public GuideRepository(ApplicationDBContext db) 
        { 
            _db = db;
        }

        public bool Create(Guide entity)
        {
            if (entity != null)
            {
                _db.Guides.Add(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Guide entity)
        {
            if (entity != null)
            {
                _db.Guides.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Guide> GetAll()
        {
            return _db.Guides;
        }

        public Guide Update(Guide entity)
        {
            _db.Guides.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}

