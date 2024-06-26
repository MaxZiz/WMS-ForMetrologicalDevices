using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Domain.Entities;
using WMS.Domain.Responses;
using WMS.Domain.ViewModels;
using WMS.Service.Interfaces;

namespace WMS.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Save() => PartialView();

        [HttpPost]
        public IActionResult Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.CreateUser(model);
                if (response.StatusCode == WMS.Domain.Enums.StatusCode.OK)
                {
                   return Json(new { description = response.Description });
                    //return View("GetUsers");
                    
                }
                return BadRequest(new { errorMessage = response.Description });
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList();
      
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        public IActionResult DeleteUser(long id)
        {
            var response =  _userService.DeleteUser(id);
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult GetRoles()
        {
            var types = _userService.GetRoles();
            return Json(types.Data);
        }

        public IActionResult GetUsers()
        {
            var response = _userService.GetUsers();
            if (response.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(response.Data);
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
