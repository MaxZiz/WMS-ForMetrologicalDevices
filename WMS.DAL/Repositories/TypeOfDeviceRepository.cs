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
    public class TypeOfDeviceRepository : IBaseRepository<TypeOfDevice>
    {
        private readonly ApplicationDBContext _db;
        public TypeOfDeviceRepository(ApplicationDBContext db)
        {
            _db = db;
        
        }
        public bool Create(TypeOfDevice entity)
        {
            _db.TypeOfDevices.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(TypeOfDevice entity)
        {
            _db.TypeOfDevices.Remove(entity);
            _db.SaveChanges();
            return true;
        }

        public IQueryable<TypeOfDevice> GetAll()
        {
            return _db.TypeOfDevices;
        }

        public TypeOfDevice Update(TypeOfDevice entity)
        {
            _db.TypeOfDevices.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}
