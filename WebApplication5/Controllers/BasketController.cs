using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WMS.Service.Interfaces;

namespace WMS.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public ActionResult Index()
        {
           return View();
        }

        [HttpPost]
        public ActionResult MoveDevices(int placeId)
        {
            var user = User.Identity.Name;
            _basketService.MoveDevices(user, placeId);
            return RedirectToAction("GetDevices", "Device");
        }
      
        [HttpPost]
        public ActionResult ClearBasket()
        {
            var user = User.Identity.Name;
            _basketService.ClearBasket(user);
            return RedirectToAction("GetBasket");
        }

        [HttpPost]
        public ActionResult DeleteFromBasket(long id)
        {
            var user = User.Identity.Name;
            var basket = _basketService.DeleteFromBasket(user, id);
            return RedirectToAction("GetBasket");   
        }

        [HttpPost]
        public ActionResult AddToBasket(long id)
        {
            //HttpContext.User.
            //var basket = _basketService.GetBasket();
            var user = User.Identity.Name;
            var basket = _basketService.AddToBasket(user, id);
            return RedirectToAction("GetBasket");
        }

        public ActionResult GetBasket()
        {
            //HttpContext.User.
            //var basket = _basketService.GetBasket();
            var user = User.Identity.Name;
            var basket = _basketService.GetBasket(user);
            return View(basket);
        }

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

        public ActionResult Edit(int id)
        {
            return View();
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

        public ActionResult Delete(int id)
        {
            return View();
        }


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
