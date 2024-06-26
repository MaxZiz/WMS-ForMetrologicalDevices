using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Domain.Entities;
using WMS.Service.Interfaces;

namespace WMS.Controllers
{
    [Authorize]
    public class PlaceController : Controller
    {
        IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public IActionResult GetPlaces()
        {
            var response = _placeService.GetPlaces();
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(response.Data);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult GetPlace(int id)
        {
            var response = _placeService.GetPlace(id);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return PartialView("GetPlace", response.Data);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Delete(int id)
        {
            var response = _placeService.DeletePlace(id);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return RedirectToAction("GetPlaces");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var response = _placeService.GetPlace(id);
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
        public ActionResult Save(Place Model)
        {
            _placeService.CreatePlace(Model);
            /*  else
              {
                  _deviceService.Edit(deviceViewModel.Id, deviceViewModel);
              }*/
            return RedirectToAction("GetPlaces");
        }

        /*[HttpPost]
        public JsonResult GetTypes()
        {
            var types = _deviceService.GetTypes();
            return Json(types.Data);
        }*/
    }
}
