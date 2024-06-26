using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.Domain.Entities;
using WMS.Domain.ViewModels;
using WMS.Service.Implementations;
using WMS.Service.Interfaces;

namespace WMS.Controllers
{
    public class ListNamesController : Controller
    {
        INameListService _nameListService;
        public ListNamesController(INameListService nameListService)
        {
            _nameListService = nameListService;
        }
        // GET: ListNamesController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNames()
        {
           var listOfAllNames = _nameListService.GetListNames();
            return View(listOfAllNames);
        }

        public ActionResult GetName(int id)
        {
            var name = _nameListService.GetName(id);
            return View(name);
        }


        // GET: ListNamesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListNamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListNamesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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


        [HttpGet]
        public ActionResult Save(int id)
        {
            var response = _nameListService.GetName(id);
            if (id == 0)
            {
                return View();
            }
            return View(response);
        }

        [HttpPost]
        public ActionResult Save(NameList nameList)
        {
            if (nameList.Id == 0)
            {
                _nameListService.CreateName(nameList);
            }
            else
            {
                //deviceViewModel.History.UserId = User.Identity.Name;
                var user = User.Identity.Name;
                var id = nameList.Id;
                _nameListService.Edit(id, nameList);
            }

            /*  else
              {
                  _deviceService.Edit(deviceViewModel.Id, deviceViewModel);
              }*/
            return RedirectToAction("GetNames");
        }

        // GET: ListNamesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListNamesController/Edit/5
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

        // GET: ListNamesController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _nameListService.DeleteName(id);
            return RedirectToAction("GetNames");
        }

        // POST: ListNamesController/Delete/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
