using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MlModelForDevices_App;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using WMS.DAL.Repositories;
using WMS.Domain.Entities;
using WMS.Domain.ViewModels.Index;
using WMS.Service.Interfaces;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDeviceService _historyService;
        private IToDoService _todoService;

        public HomeController(ILogger<HomeController> logger, IDeviceService historyService, IToDoService todoService)
        {
            _logger = logger;
            _historyService = historyService;
            _todoService = todoService;
        }

        public IActionResult GetHistories()
        {
            var history = _historyService.GetDevices().Data;
            var histories = new List<History>();
            try
            {
                foreach (var historyItem in history)
                {
                    for (int i = 0; i < historyItem.Histories.Count; i++)
                    {
                        histories.Add(historyItem.Histories[i]);
                    }

                }
            }
            catch { }

            histories = histories.OrderByDescending(x => x.Id).ToList();
            var firstThree = histories.ToList();
            List<IndexViewModel> list = new List<IndexViewModel>();

            for (int i = 0; i < firstThree.Count; i++)
            {
                IndexViewModel indexViewModel = new IndexViewModel();
                indexViewModel.FromPlace = firstThree[i].FromPlace;
                indexViewModel.ToPlace = firstThree[i].ToPlace;
                indexViewModel.UserId = firstThree[i].UserId;
                indexViewModel.Device.Name = firstThree[i].Device.Name;
                indexViewModel.DateTime = firstThree[i].DateTime;
                list.Add(indexViewModel);

            }

            return View(list);
        }

        public IActionResult Index()
        {
            var toDoList = _todoService.GetToDoLists();
            var history = _historyService.GetDevices().Data;
            var histories = new List<History>();
            try
            { 
               
                foreach (var historyItem in history)
                {
                    for (int i = 0; i < historyItem.Histories.Count; i++)
                    {
                        histories.Add(historyItem.Histories[i]);
                    }

                }
            }
            catch  { }
           
                var orderedList = toDoList.OrderByDescending(d => d.Id);
                var firstThreeLists = orderedList.Take(3).ToList();

          
            while(firstThreeLists.Count<3)
            {
                firstThreeLists.Add(firstThreeLists[0]);

            }
            histories = histories.OrderByDescending(x=>x.Id).ToList();
            var firstThree = histories.Take(3).ToList();
            List<IndexViewModel> list = new List<IndexViewModel>();
            for(int i = 0; i<3;i++)
            {
                IndexViewModel indexViewModel = new IndexViewModel();
                indexViewModel.Id = firstThreeLists[i].Id;
                indexViewModel.TimeToDo = firstThreeLists[i].TimeToDo;
                indexViewModel.TypeOfOperation = firstThreeLists[i].TypeOfOperation;
                indexViewModel.FromPlace = firstThree[i].FromPlace;
                indexViewModel.ToPlace = firstThree[i].ToPlace;
                indexViewModel.DateTime = firstThree[i].DateTime;
                indexViewModel.UserId = "Зизевских М.А.";
                list.Add(indexViewModel);

            }
            return View(list);
        }

        public IActionResult Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
