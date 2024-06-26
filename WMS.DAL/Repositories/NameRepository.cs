using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
    public class NameRepository : IBaseRepository<NameList>
    {
        ApplicationDBContext _db;
        public NameRepository(ApplicationDBContext dbContext) 
        { 
            _db = dbContext;
        }

        public bool Create(NameList entity)
        {
            if (entity != null)
            {
                _db.NameLists.Add(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(NameList entity)
        {
            if (entity != null)
            {
                _db.NameLists.Remove(entity);
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

        public IQueryable<NameList> GetAll()
        {
            return _db.NameLists;

        }

        /*  public Device GetByName(string name)
          {
              return _db.Device.FirstOrDefault(x => x.Name == name);
          }

          public List<Device> Select()
          {
              return _db.Device.ToList();
          }*/

        public NameList Update(NameList entity)
        {
            _db.NameLists.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}
