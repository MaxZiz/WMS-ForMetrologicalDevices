using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {

        private readonly ApplicationDBContext _db;

        public UserRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public bool Delete(User entity)
        {
            _db.Users.Remove(entity);
             _db.SaveChanges();
            return true;
        }

        public bool Create(User entity)
        {
             _db.Users.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public User Update(User entity)
        {
            _db.Users.Update(entity);
             _db.SaveChanges();

            return entity;
        }
    }
}
