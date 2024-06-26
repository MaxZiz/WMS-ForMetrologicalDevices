using System.Linq;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{

    public class DeviceRepository : IBaseRepository<Device>
    {

        private readonly ApplicationDBContext _db;

        public DeviceRepository(ApplicationDBContext db)
        {

            _db = db;
        }

        public bool Create(Device entity)
        {
            if (entity != null)
            {
                _db.Device.Add(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Device entity)
        {
            if (entity != null)
            {
                _db.Device.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Device> GetAll()
        {
            return _db.Device;

        }      

        public Device Update(Device entity)
        {
            
            _db.Device.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}
