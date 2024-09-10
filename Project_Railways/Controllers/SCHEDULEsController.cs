using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Railways.Models;

namespace Project_Railways.Controllers
{
    
    public class SCHEDULEsController : Controller
    {
        private RailwayInfoEntities db = new RailwayInfoEntities();

        // GET: SCHEDULEs
        [Authorize]
        public ActionResult Index()
        {
            var sCHEDULEs = db.SCHEDULEs.Include(s => s.Train);
            return View(sCHEDULEs.ToList());
        }

        // GET: SCHEDULEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id);
            if (sCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(sCHEDULE);
        }

        // GET: SCHEDULEs/Create
        public ActionResult Create()
        {
            ViewBag.train_id = new SelectList(db.Trains, "train_id", "train_name");
            return View();
        }

        // POST: SCHEDULEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "start_location,destination_location,price,start_date_time,destination_date_time,train_id,Schedule_id")] SCHEDULE sCHEDULE)
        {
            if (ModelState.IsValid)
            {
                db.SCHEDULEs.Add(sCHEDULE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.train_id = new SelectList(db.Trains, "train_id", "train_name", sCHEDULE.train_id);
            return View(sCHEDULE);
        }

        // GET: SCHEDULEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id);
            if (sCHEDULE == null)
            {
                return HttpNotFound();
            }
            ViewBag.train_id = new SelectList(db.Trains, "train_id", "train_name", sCHEDULE.train_id);
            return View(sCHEDULE);
        }

        // POST: SCHEDULEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "start_location,destination_location,price,start_date_time,destination_date_time,train_id,Schedule_id")] SCHEDULE sCHEDULE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sCHEDULE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.train_id = new SelectList(db.Trains, "train_id", "train_name", sCHEDULE.train_id);
            return View(sCHEDULE);
        }

        // GET: SCHEDULEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id);
            if (sCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(sCHEDULE);
        }

        // POST: SCHEDULEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id);
            db.SCHEDULEs.Remove(sCHEDULE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        
        
        public ActionResult Train_details()
        {
            var sCHEDULEs = db.SCHEDULEs.Include(s => s.Train).AsQueryable();

            
           

            //return View(sCHEDULEs.ToList());
            //var sCHEDULEs = db.SCHEDULEs.Include(s => s.Train);
            return View(sCHEDULEs.ToList());
            //return View();
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult search_details(DateTime? start_date_time, string start_location, string destination_location)
        {
            // Initialize the query
            var query = db.SCHEDULEs.Include(s => s.Train).AsQueryable();
            bool allCriteriaProvided = start_date_time.HasValue &&
                               !string.IsNullOrEmpty(start_location) &&
                               !string.IsNullOrEmpty(destination_location);
            if (allCriteriaProvided)
            {
                // Apply filters based on provided search criteria
                query = query.Where(s => s.start_date_time >= start_date_time.Value &&
                                         s.start_location == start_location &&
                                         s.destination_location == destination_location);

                // Execute the query and get the results
                var results = query.ToList();

                // If no results, set a flag for the view
                ViewBag.NoResultsFound = !results.Any();

                // Return the view with results
                return View(results);
            }
            else
            {
                // If not all criteria are provided, show a message or return empty results
                ViewBag.NoResultsFound = true;
                return View(new List<SCHEDULE>()); // or return a view with a specific message
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

