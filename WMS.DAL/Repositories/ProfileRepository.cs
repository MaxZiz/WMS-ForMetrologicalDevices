using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly ApplicationDBContext _DBContext;

        public ProfileRepository(ApplicationDBContext DBContext)
        {
            _DBContext = DBContext;
        }


        public IQueryable<Profile> GetAll()
        {
            return _DBContext.Profiles;
        }




        bool IBaseRepository<Profile>.Create(Profile entity)
        {
            _DBContext.Profiles.Add(entity);
            _DBContext.SaveChanges();
            return true;
        }

        bool IBaseRepository<Profile>.Delete(Profile entity)
        {
            _DBContext.Profiles.Remove(entity);
            _DBContext.SaveChanges();
            return true;
        }

        Profile IBaseRepository<Profile>.Update(Profile entity)
        {
            _DBContext.Profiles.Update(entity);
            _DBContext.SaveChanges();

            return entity;
        }
    }
}