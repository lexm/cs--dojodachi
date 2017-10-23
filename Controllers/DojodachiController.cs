using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dojodachi.Controllers
{
    public class DojodachiController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Dojodachi()
        {
            return View("dojodachi");
        }
    }
}