using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;

namespace WMS.Service.Interfaces
{
    public interface IToDoService
    {
        public ToDoList EditToDoList(int id, ToDoList model);
        public ToDoList CreateToDoList(ToDoList model);
        public bool DeleteToDoList(int id);
        public ToDoList GetToDoList(int id);
        public IEnumerable<ToDoList> GetToDoLists();
    }
}
