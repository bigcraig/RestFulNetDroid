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
    public class TreasuresMVCController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TreasuresMVC
        public ActionResult Index()
        {
            return View(db.Treasures.ToList());
        }

        // GET: TreasuresMVC/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasure treasure = db.Treasures.Find(id);
            if (treasure == null)
            {
                return HttpNotFound();
            }
            return View(treasure);
        }

        // GET: TreasuresMVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TreasuresMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,Name,Url")] Treasure treasure)
        {
            if (ModelState.IsValid)
            {
                db.Treasures.Add(treasure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(treasure);
        }

        // GET: TreasuresMVC/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasure treasure = db.Treasures.Find(id);
            if (treasure == null)
            {
                return HttpNotFound();
            }
            return View(treasure);
        }

        // POST: TreasuresMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,Name,Url")] Treasure treasure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treasure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(treasure);
        }

        // GET: TreasuresMVC/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasure treasure = db.Treasures.Find(id);
            if (treasure == null)
            {
                return HttpNotFound();
            }
            return View(treasure);
        }

        // POST: TreasuresMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Treasure treasure = db.Treasures.Find(id);
            db.Treasures.Remove(treasure);
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
