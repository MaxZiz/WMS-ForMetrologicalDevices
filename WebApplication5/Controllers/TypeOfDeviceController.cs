using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.Domain.Entities;
using WMS.Service.Implementations;
using WMS.Service.Interfaces;

namespace WMS.Controllers
{
    public class TypeOfDeviceController : Controller
    {
        ITypeOfDeviceService _typeOfDeviceService;
        public TypeOfDeviceController(ITypeOfDeviceService typeOfDeviceService) 
        { 
            _typeOfDeviceService = typeOfDeviceService;
        }
        // GET: TypeOfDeviceController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllTypes()
        {
            var listOfAllTypes = _typeOfDeviceService.GetAll();
            return View(listOfAllTypes);
        }

        public ActionResult GetTypeOfDevice(int id)
        {
            var type = _typeOfDeviceService.GetTypeOfDevice(id);
            return View(type);
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            var response = _typeOfDeviceService.GetTypeOfDevice(id);
            if (id == 0)
            {
                return View();
            }
            return View(response);
        }

        [HttpPost]
        public ActionResult Save(TypeOfDevice typeOfDevice)
        {
            if (typeOfDevice.Id == 0)
            {
                _typeOfDeviceService.CreateType(typeOfDevice);
            }
            else
            {
                //deviceViewModel.History.UserId = User.Identity.Name;
                var user = User.Identity.Name;
                var id = typeOfDevice.Id;
                _typeOfDeviceService.Edit(id, typeOfDevice);
            }

            /*  else
              {
                  _deviceService.Edit(deviceViewModel.Id, deviceViewModel);
              }*/
            return RedirectToAction("GetAllTypes");
        }

        // GET: TypeOfDeviceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TypeOfDeviceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOfDeviceController/Create
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

        // GET: TypeOfDeviceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TypeOfDeviceController/Edit/5
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



        // GET: TypeOfDeviceController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
           
            _typeOfDeviceService.DeleteType(id);
            return RedirectToAction("GetAllTypes");
        }

        /*// POST: TypeOfDeviceController/Delete/5
        [HttpPost]
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
