using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WMS.Domain.Entities;
using WMS.Domain.ViewModels;

using WMS.Service.Interfaces;

namespace WMS.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        IDeviceService _deviceService;
        private const int PageSize = 7;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public IActionResult GetDevices(string type, int? placeId, int page = 0)
        {
            var response = _deviceService.GetDevices();

            if (response.Data != null)
            {
                var data = response.Data;

                if (placeId != null)
                {
                    HttpContext.Session.SetString("sortOrder", placeId.ToString());
                }
                else
                {
                    if (HttpContext.Session.GetString("sortOrder") != null)
                    {
                        placeId = int.Parse(HttpContext.Session.GetString("sortOrder"));
                    }
                }

                if (placeId != null)
                {
                    if (placeId != 0)
                    {
                        data = data.Where(x => x.PlaceId == placeId).ToList();
                    }
                }

                if (type != null && !string.IsNullOrEmpty(type))
                {
                    switch (type)
                    {
                        case "Мультиметр":
                            data = (List<Device>)data.Where(p => p.TypeDevice == Domain.Enums.TypeDevice.Device).ToList();
                            return View(data.ToList());
                        case "Амперметр":
                            data = (List<Device>)data.Where(p => p.TypeDevice == Domain.Enums.TypeDevice.Cables).ToList();
                            return View(data.ToList());
                        case "Вольтметр":
                            data = (List<Device>)data.Where(p => p.TypeDevice == Domain.Enums.TypeDevice.Other).ToList();
                            return View(data.ToList());
                        default:
                            return View(data.ToList());
                    }
                }

                var count = data.Count();

                data = data.Skip(page * PageSize).Take(PageSize).ToList();

                ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

                ViewBag.Page = page;          
               
                return View(data.ToList());
            }
            else
            {
                return View();
            }
        }

        public IActionResult GetHistories(string type, int? placeId, int page = 0)
        {
            var response = _deviceService.GetDevices();

            if (response.Data != null)
            {
                var data = response.Data;

                //ViewBag.type = type;
                if (placeId != null)
                {
                    HttpContext.Session.SetString("sortOrder", placeId.ToString());
                }
                else
                {
                    if (HttpContext.Session.GetString("sortOrder") != null)
                    {
                        //get sortOrder from session
                        placeId = int.Parse(HttpContext.Session.GetString("sortOrder"));
                    }
                    else
                    {
                        //session expired or is null, use the default sort order.
                    }
                }

                if (placeId != null)
                {
                    if (placeId != 0)
                    {
                        data = data.Where(x => x.PlaceId == placeId).ToList();
                    }

                }

                if (type != null && !string.IsNullOrEmpty(type))
                {
                    switch (type)
                    {
                        case "Приборы":
                            data = (List<Device>)data.Where(p => p.TypeDevice == Domain.Enums.TypeDevice.Device).ToList();
                            return View(data.ToList());
                        case "Кабели":
                            data = (List<Device>)data.Where(p => p.TypeDevice == Domain.Enums.TypeDevice.Cables).ToList();
                            return View(data.ToList());
                        case "Остальное":
                            data = (List<Device>)data.Where(p => p.TypeDevice == Domain.Enums.TypeDevice.Other).ToList();
                            return View(data.ToList());
                        default:
                            return View(data.ToList());
                    }
                }

                var count = data.Count();

                data = data.Skip(page * PageSize).Take(PageSize).ToList();

                ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

                ViewBag.Page = page;

                // data.ToList()[0].TypeDevice.GetDisplayName();
                foreach (var i in data)
                {

                }

                return View(data.ToList());
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult GetDevicesFromSearch(string term)
        {
            var response = _deviceService.GetDevices();

            if (response.Data != null & term != null)
            {
                var data = response.Data;

                //ViewBag.type = type;              
                var t = data.Where(c => c.Name.ToUpper().Contains(term.ToUpper())).ToList();
                var c = data.Where(f => f.Id.ToString().ToUpper().Contains(term.ToUpper())).ToList();
                t.AddRange(c);
                return View(t.ToList());
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult GetDevice(int id)
        {
            var response = _deviceService.GetDevice(id);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return PartialView("GetDevice", response.Data);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var response = _deviceService.DeleteDevice(id);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return RedirectToAction("GetDevices");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        [HttpGet]
        public IActionResult GetDevicesPartial()
        {
            int page = 0;

            var response = _deviceService.GetDevices();
            //var firstFiveItems = response.Data.Take(3);
            const int PageSize = 3;

            var count = response.Data.Count();

            var data = response.Data.Skip(page * PageSize).Take(PageSize).ToList();

            ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            ViewBag.Page = page;
            return PartialView(data.ToList());
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            var response = _deviceService.GetDevice(id);
            if (id == 0)
            {
                return View();
            }

            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                //return RedirectToAction("GetDevices");
                return View(response.Data);
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        [HttpPost]
        public ActionResult Save(DeviceViewModel deviceViewModel, IFormFile upload)
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

            if (deviceViewModel.Id == 0)
            {
                _deviceService.CreateDevice(deviceViewModel, openPath);
            }
            else
            {
                var user = User.Identity.Name;
                _deviceService.Edit(deviceViewModel, user);
            }

            return RedirectToAction("GetDevices");
        }

        [HttpPost]
        public ContentResult Upload(IFormFile upload)
        {
            if (upload != null)
            {
                // путь к папке Files
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgs", imageName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    upload.CopyTo(stream);
                }
                return Content(SavePath);
            }
            return Content("Error");
            //return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetDevice(string term)
        {
            var response = _deviceService.GetDevice(term);
            return Json(response.Data);
        }

        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _deviceService.GetTypes();
            return Json(types.Data);
        }

        [HttpPost]
        public JsonResult GetPlaces()
        {
            var types = _deviceService.GetPlacesDictionary();
            types.Data.Add(0, 0);
            return Json(types.Data);
        }

        [HttpGet]
        public JsonResult GetPlaces(string term)
        {
            var types = _deviceService.GetPlacesDictionary();
            types.Data.Add(0, 0);
            var i = Json(types.Data);
            return Json(types.Data);
        }

        [HttpGet]
        public JsonResult GetPlacesJson(string term)
        {
            var types = _deviceService.GetPlacesDictionary();
            types.Data.Add(0, 0);
            var i = Json(types.Data);
            return Json(types.Data);
        }

        [HttpPost]
        public JsonResult GetTypesDictionary()
        {
            var types = _deviceService.GetTypesDictionary();
            return Json(types);
        }
    }
}
