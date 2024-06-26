using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly ApplicationDBContext _db;

        public BasketRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public bool Create(Basket entity)
        {
            if (entity != null)
            {
                _db.Basket.Add(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Basket entity)
        {
            if (entity != null)
            {
                _db.Basket.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Basket> GetAll()
        {
            return _db.Basket;
        }

        public Basket Update(Basket entity)
        {
            _db.Basket.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}
