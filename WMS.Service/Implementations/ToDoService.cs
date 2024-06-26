using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.Domain.Entities;
using WMS.Service.Interfaces;

namespace WMS.Service.Implementations
{
    public class ToDoService:IToDoService
    {
        IBaseRepository<ToDoList> _toDoRepository;
        public ToDoService(IBaseRepository<ToDoList> toDoRepository) 
        { 
            _toDoRepository = toDoRepository;
        }

        public IEnumerable<ToDoList> GetToDoLists() 
        {
            return _toDoRepository.GetAll().ToList();
        }

        public ToDoList GetToDoList(int id)
        {
            var toDoList = _toDoRepository.GetAll().Include(c=>c.Devices).FirstOrDefault(x => x.Id == id);
            return toDoList;
        }

        public bool DeleteToDoList(int id)
        {
            var toDoList = _toDoRepository.GetAll().FirstOrDefault(x => x.Id == id);
            _toDoRepository.Delete(toDoList);
            return true;
        }

        public ToDoList CreateToDoList(ToDoList model)
        {
           // model.Id = 100;
            _toDoRepository.Create(model);
            return model;
        }

        public ToDoList EditToDoList(int id, ToDoList model)
        {
            var toDoList = _toDoRepository.GetAll().Include(c=>c.Devices).FirstOrDefault(x => x.Id == id);
            toDoList.FromPlace = model.FromPlace;
            toDoList.Id = model.Id;
            toDoList.ToPlace = model.ToPlace;
            toDoList.TypeOfOperation = model.TypeOfOperation;
            toDoList.TimeToDo = model.TimeToDo;
            toDoList.Description = model.Description;
            _toDoRepository.Update(toDoList);
            return toDoList;
        }
    }
}
