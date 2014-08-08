using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestFulNetDroid.Models;

namespace RestFulNetDroid.Controllers
{
    public class CoordinatesMVCController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CoordinatesMVC
        public ActionResult Index()
        {
            var coordinates = db.Coordinates.Include(c => c.Treasure);
            return View(coordinates.ToList());
        }

        // GET: CoordinatesMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinate coordinate = db.Coordinates.Find(id);
            if (coordinate == null)
            {
                return HttpNotFound();
            }
            return View(coordinate);
        }

        // GET: CoordinatesMVC/Create
        public ActionResult Create()
        {
            ViewBag.TreasureId = new SelectList(db.Treasures, "Id", "Address");
            return View();
        }

        // POST: CoordinatesMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Data,TreasureId")] Coordinate coordinate)
        {
            if (ModelState.IsValid)
            {
                db.Coordinates.Add(coordinate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TreasureId = new SelectList(db.Treasures, "Id", "Address", coordinate.TreasureId);
            return View(coordinate);
        }

        // GET: CoordinatesMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinate coordinate = db.Coordinates.Find(id);
            if (coordinate == null)
            {
                return HttpNotFound();
            }
            ViewBag.TreasureId = new SelectList(db.Treasures, "Id", "Address", coordinate.TreasureId);
            return View(coordinate);
        }

        // POST: CoordinatesMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,TreasureId")] Coordinate coordinate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordinate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TreasureId = new SelectList(db.Treasures, "Id", "Address", coordinate.TreasureId);
            return View(coordinate);
        }

        // GET: CoordinatesMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinate coordinate = db.Coordinates.Find(id);
            if (coordinate == null)
            {
                return HttpNotFound();
            }
            return View(coordinate);
        }

        // POST: CoordinatesMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coordinate coordinate = db.Coordinates.Find(id);
            db.Coordinates.Remove(coordinate);
            db.SaveChanges();
            return RedirectToAction("Index");
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
