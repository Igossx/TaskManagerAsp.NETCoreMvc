using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskMgr.Data;
using TaskMgr.Models;

namespace TaskMgr.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            IEnumerable<Task> objList = _db.Tasks;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "time_desc" : "Time";

            var tasks = from t in objList
                        select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    tasks = tasks.OrderByDescending(t => t.Name);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(t => t.Date);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(t => t.Date);
                    break;
                case "time_desc":
                    tasks = tasks.OrderByDescending(t => t.Time);
                    break;
                case "Time":
                    tasks = tasks.OrderBy(t => t.Time);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Name);
                    break;
            }

            return View(tasks.ToList());
        }

        // GET: Task/Details/5
        public ActionResult Details(int? id)
        {
            var obj = _db.Tasks.Find(id);
            return View(obj);

        }

        // GET - Create
        public IActionResult Create()
        {
            return View();
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task obj)
        {
            if (ModelState.IsValid)
            {
                SetStartDate(obj);
                _db.Tasks.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Tasks.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Task obj)
        {
            if (ModelState.IsValid)
            {
                SetStartDate(obj);
                _db.Tasks.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Tasks.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Tasks.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Tasks.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET: Task/Done/5
        public IActionResult Done(int id)
        {
            var obj = _db.Tasks.Find(id);
            obj.Done = true;

            _db.Tasks.Update(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public void SetStartDate(Task obj)
        {
            obj.StartDate = DateTime.Today;
        }
    }
}
