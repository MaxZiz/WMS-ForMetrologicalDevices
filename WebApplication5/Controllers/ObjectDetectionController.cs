using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using WMS.Domain.ViewModels;
using MlModelForDevices_App;
using WMS.Service.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace WMS.Controllers
{
    public class ObjectDetectionController : Controller
    {
        IDeviceService _deviceService;
        IBasketService _basketService;
        public ObjectDetectionController(IDeviceService deviceService, IBasketService basketService)
        {
            _deviceService = deviceService;
            _basketService = basketService;

        }
        // GET: ObjectDetectionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ObjectDetectionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int placeId ,IFormFile upload)
        {
            string SavePath = "";
            string openPath = "";
            //byte[] imageData;
            if (upload != null)
            {
                // путь к папке Files
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                openPath = Path.Combine("\\imgs\\", imageName);
                SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs\\", imageName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    upload.CopyTo(stream);
                }

            }
            var list = NamesFromDetect.GetNamesFromDetect(SavePath);
            for(int i =0; i<list.Count;i++)
            {
                if (list[i]=="multimeter")
                {
                    list[i] = "Appa";
                }
                else
                {
                    list[i] = "123";
                }
            }
            var listOfDevices = _deviceService.GetDevices();

            //listOfDevices.Data = listOfDevices.Data.Where(f=>list.Any(y=>y.));
            var filteredByPlaces = listOfDevices.Data.Where(x => x.PlaceId == placeId).ToList();
            var filtered = new List<WMS.Domain.Entities.Device>();
            for (int i=0; i<list.Count; i++)
            {
                for (int j = 0; j < filteredByPlaces.Count; j++)
                {
                    if (filteredByPlaces[j].Name.ToUpper().Contains(list[i].ToUpper())|| filteredByPlaces[j].Name.ToUpper()==list[i].ToUpper())
                    {
                        filtered.Add(filteredByPlaces[j]);
                    }
                }
            }
     

            var user = User.Identity.Name;
                _basketService.AddToBasket(user, filtered[0].Id);

            /*  else
              {
                  _deviceService.Edit(deviceViewModel.Id, deviceViewModel);
              }*/
            return RedirectToAction("GetBasket","Basket");
        }

        // GET: ObjectDetectionController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       /* // POST: ObjectDetectionController/Create
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
        }*/

        // GET: ObjectDetectionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ObjectDetectionController/Edit/5
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

        // GET: ObjectDetectionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ObjectDetectionController/Delete/5
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
        }
    }
}
