using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
   public class PlaceRepository: IBaseRepository<Place>
    {
        private readonly ApplicationDBContext _db;

        public PlaceRepository(ApplicationDBContext db)
        {

            _db = db;
        }

        public bool Create(Place entity)
        {
            if (entity != null)
            {
                _db.Places.Add(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Delete(Place entity)
        {
            if (entity != null)
            {
                _db.Places.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*public Device Get(int id)
         {
             return _db.Device.FirstOrDefault(x => x.Id == id);
         }
         */

        public IQueryable<Place> GetAll()
        {
            return _db.Places.Include(u => u.Devices);
        }

        /*  public Device GetByName(string name)
          {
              return _db.Device.FirstOrDefault(x => x.Name == name);
          }

          public List<Device> Select()
          {
              return _db.Device.ToList();
          }*/

        public Place Update(Place entity)
        {
            _db.Places.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}
