using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private DishContext dbContext;

        public HomeController(DishContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes
                .OrderByDescending(newest => newest.CreatedAt)
                .ToList();
            return View(AllDishes);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("addDish")]
        public IActionResult AddDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"***********************{newDish.Chef} | {newDish.Name} | {newDish.Calories} | {newDish.Tastiness} | {newDish.Description}***********************");
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("new");
            }
        }

        [HttpGet("{DishId}")]
        public IActionResult DishDisplay(int DishId)
        {
            Dish DishInfo = dbContext.Dishes.FirstOrDefault(info => info.DishId == DishId);
            ViewBag.DishInfo = DishInfo;
            return View();
        }

        [HttpGet("edit/{DishId}")]
        public IActionResult EditDish(int DishId)
        {
            Dish DishInfo = dbContext.Dishes.FirstOrDefault(info => info.DishId == DishId);
            ViewBag.EditDish = DishInfo;
            return View();
        }

        [HttpPost("process2/{DishId}")]
        public IActionResult EditedDish(int DishId, Dish updatedDish)
        {
            Dish DishUpdate = dbContext.Dishes.FirstOrDefault(info => info.DishId == DishId);
            if (ModelState.IsValid)
                {
                    DishUpdate.Chef=updatedDish.Chef;
                    DishUpdate.Name= updatedDish.Name;
                    DishUpdate.Calories= updatedDish.Calories;
                    DishUpdate.Tastiness= updatedDish.Tastiness;
                    DishUpdate.Description= updatedDish.Description;
                    DishUpdate.UpdatedAt = DateTime.Now;
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            else
            {
                ViewBag.EditDish = updatedDish;
                return View("EditDish", updatedDish);
            }
        }

        [HttpGet("delete/{DishId}")]
        public IActionResult DeleteDish(int DishId)
        {
            Dish DishDelete = dbContext.Dishes.FirstOrDefault(info => info.DishId == DishId);
            dbContext.Dishes.Remove(DishDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
