using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskMgr.Data;
using TaskMgr.Models;

namespace TaskMgr.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Task> objList = _db.Tasks;

            TwoData twoData = new TwoData();

            int howMuchDone = 0;
            int howMuchNotDone = 0;

            foreach (var x in objList)
            {
                if (x.Done == false)
                {
                    howMuchNotDone++;
                }
                else
                {
                    howMuchDone++;
                }
            }

            twoData.NumberDone = howMuchDone;
            twoData.NumberNotDone = howMuchNotDone;

            return View(twoData);
        }

        public IActionResult Info()
        {
            return View();
        }

        public IActionResult Reset()
        {
            foreach (var x in _db.Tasks)
            {
                _db.Tasks.Remove(x);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
