using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;

namespace WMS.DAL.Repositories
{
    public class ToDoRepository : IBaseRepository<ToDoList>
    {
        private readonly ApplicationDBContext _db;

        public ToDoRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public bool Create(ToDoList entity)
        {
            if (entity != null)
            {
                _db.ToDoList.Add(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(ToDoList entity)
        {
            if (entity != null)
            {
                _db.ToDoList.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<ToDoList> GetAll()
        {
            return _db.ToDoList;
        }

        public ToDoList Update(ToDoList entity)
        {
            _db.ToDoList.Update(entity);
            _db.SaveChanges();
            return entity;
        }
    }
}
