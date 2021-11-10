using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using smartmat.Models;

namespace smartmat.Controllers
{
    public class MealsController : Controller
    {
        private readonly ILogger<MealsController> _logger;

        public MealsController(ILogger<MealsController> logger)
        {
            _logger = logger;
        }

        public IActionResult FastFood()
        {
            return View();
        }        
        
        public IActionResult Breakfast()
        {
            return View();
        }        
        
        public IActionResult Lunch()
        {
            return View();
        }        
        
        public IActionResult Dinner()
        {
            return View();
        }        
        
        public IActionResult Dessert()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}