using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WMS.Domain.Entities;
using WMS.Domain.ViewModels;
using WMS.Service.Implementations;
using WMS.Service.Interfaces;

namespace WMS.Controllers
{
    public class ToDoListController : Controller
    {
        IToDoService _toDoService;
        IBasketService _basketService;
        public ToDoListController(IToDoService toDoService, IBasketService basketService)
        {
            _toDoService = toDoService;
            _basketService = basketService;
        }

        public ActionResult GetLists()
        {
            var list = _toDoService.GetToDoLists();
            return View(list);
        }

        [HttpGet]
        public ActionResult GetList(int id)
        {
            var list = _toDoService.GetToDoList(id);
            return View(list);
        }

        [HttpPost]
        public ActionResult Edit(ToDoList toDoList, int id)
        {
            _toDoService.EditToDoList(id, toDoList);
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ToDoList toDoList)
        {
            if (toDoList.Id == 0)
            {
                var user = User.Identity.Name;
               var listOfDevices = _basketService.GetBasket(user).Devices.ToList();
                toDoList.Devices.AddRange(listOfDevices);
                
                _toDoService.CreateToDoList(toDoList);
            }
            else
            {
                //deviceViewModel.History.UserId = User.Identity.Name;
                var user = User.Identity.Name;
                _toDoService.EditToDoList(toDoList.Id, toDoList);
            }
            return RedirectToAction("GetLists");
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var user = User.Identity.Name;
            ToDoList toDoList = new ToDoList();
            toDoList.Devices = _basketService.GetBasket(user).Devices.ToList();
            return View(toDoList);
        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoListController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _toDoService.DeleteToDoList(id);
            return RedirectToAction("GetLists");
        }

    }
}
