using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Green.Models;

namespace Green.Controllers
{
    public class TelbooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Telbooks
        public ActionResult Index()
        {
            return View(db.Telbooks.ToList());
        }

        // GET: Telbooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telbook telbook = db.Telbooks.Find(id);
            if (telbook == null)
            {
                return HttpNotFound();
            }
            return View(telbook);
        }

        // GET: Telbooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Telbooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Site,Units,cellphoneNum,Name,Code")] Telbook telbook)
        {
            if (ModelState.IsValid)
            {
                db.Telbooks.Add(telbook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(telbook);
        }

        // GET: Telbooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telbook telbook = db.Telbooks.Find(id);
            if (telbook == null)
            {
                return HttpNotFound();
            }
            return View(telbook);
        }

        // POST: Telbooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Site,Units,cellphoneNum,Name,Code")] Telbook telbook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telbook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(telbook);
        }

        // GET: Telbooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telbook telbook = db.Telbooks.Find(id);
            if (telbook == null)
            {
                return HttpNotFound();
            }
            return View(telbook);
        }

        // POST: Telbooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telbook telbook = db.Telbooks.Find(id);
            db.Telbooks.Remove(telbook);
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
