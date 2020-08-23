using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace EpidemicManager.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {


            return View(Model);
        }

        public IActionResult Read()
        {


            return View(Model);
        }

        public IActionResult PeopleAdd()
        {


            return View(Model);
        }

        public IActionResult ShowQRcode()
        {


            return View(Model);
        }
    }
}
