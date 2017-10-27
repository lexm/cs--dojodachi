using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace dojodachi.Controllers
{
    public class DojodachiController : Controller
    {
        Random randy = new Random();
        [HttpGet]
        [Route("")]
        public IActionResult Dojodachi()
        {
            if(HttpContext.Session.GetInt32("message") == null)
            {
                HttpContext.Session.SetInt32("fullness", 20);
                HttpContext.Session.SetInt32("happiness", 20);
                HttpContext.Session.SetInt32("meals", 3);
                HttpContext.Session.SetInt32("energy", 50);
                HttpContext.Session.SetString("message", "Welcome to DojoDachi!");
                HttpContext.Session.SetInt32("playing", 1);
            }
                ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
                ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
                ViewBag.meals = HttpContext.Session.GetInt32("meals");
                ViewBag.energy = HttpContext.Session.GetInt32("energy");
                ViewBag.playing = HttpContext.Session.GetInt32("playing");
                ViewBag.message = HttpContext.Session.GetString("message");
            return View("dojodachi", ViewBag);
        }

        [HttpPost]
        [Route("move")]
        public IActionResult Move(string result)
        {
            int? fullInt = HttpContext.Session.GetInt32("fullness");
            int? happInt = HttpContext.Session.GetInt32("happiness");
            int? mealInt = HttpContext.Session.GetInt32("meals");
            int? enerInt = HttpContext.Session.GetInt32("energy");
            int fuller = 0;
            int happier = 0;
            int mealier = 0;

            string message = "Error: inform coder ASAP!";
            System.Console.WriteLine("{0} {1} {2} {3}", fullInt, happInt, mealInt, enerInt);
            if(result == "Feed")
            {
                if(mealInt > 0)
                {
                    bool like = randy.Next(0, 4) != 0;
                    if(like)
                    {
                        fuller = randy.Next(5, 11);
                        message = "You fed your DojoDachi! Fullness +" + fuller + ", Meals -1";
                    }
                    else
                    {
                        message = "You tried to feed your Dojodachi, but it didn't like it. Meals -1";
                    }
                    fullInt += fuller;
                    mealInt--;
                }
                else
                {
                    message = "No meals left. Work for more...";
                }
            }
            else if(result == "Play")
            {
                if(enerInt > 4)
                {
                    bool like = randy.Next(0, 4) != 0;
                    if(like)
                    {
                        happier = randy.Next(5, 11);
                        message = "You played with your Dojodachi! Happiness +" + happier + ", Energy -5";
                    } else 
                    {
                        message = "You tried to play with your Dojodachi, but it didn't like it. Energy -5";
                    }
                    happInt += happier;
                    enerInt -= 5;
                }
                else{
                    message = "Out of energy. Time to sleep?";
                }
            }
            else if(result == "Work")
            {
                if(enerInt > 4)
                {
                    mealier = randy.Next(1, 4);
                    mealInt += mealier;
                    enerInt -= 5;
                    message = "Dojodachi worked. Meals +" + mealier + ", Energy -5";
                }
                else{
                    message = "Out of energy. Time to sleep?";
                }
            }
            else if(result == "Sleep") 
            {
                enerInt += 15;
                fullInt -= 5;
                happInt -= 5;
                message = "Dojodachi slept. Energy +15, Fullness -5, Happiness -5";
            }
            else if(result == "Restart?") 
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Dojodachi");
            }

            if(enerInt > 100 && fullInt > 100 && happInt > 100)
            {
                HttpContext.Session.SetInt32("playing", 0);
                message = "Congratulations! You won!";
            }
            else if(fullInt <= 0 || happInt <= 0)
            {
                HttpContext.Session.SetInt32("playing", 0);
                message = "Your Dojodachi has passed away.";
            }
            else 
            {
            }
                System.Console.WriteLine("** {0} {1} {2} {3} **", fullInt, happInt, mealInt, enerInt);
                HttpContext.Session.SetInt32("fullness", (int)fullInt);
                HttpContext.Session.SetInt32("happiness", (int)happInt);
                HttpContext.Session.SetInt32("meals", (int)mealInt);
                HttpContext.Session.SetInt32("energy", (int)enerInt);
                HttpContext.Session.SetString("message", message);
            return RedirectToAction("Dojodachi");
        }
    }
}