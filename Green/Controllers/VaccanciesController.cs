using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Green.Models;
using System.IO;


namespace Green.Controllers
{
    public class VaccanciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vaccancies
        public ActionResult Index()
        {
            return View(db.Vaccancies.ToList());
        }

        // GET: Vaccancies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccancie vaccancie = db.Vaccancies.Find(id);
            if (vaccancie == null)
            {
                return HttpNotFound();
            }
            return View(vaccancie);
        }

        // GET: Vaccancies/Create
        public ActionResult Create()
        {
            return View();
        }

        public byte[] ConvertToBytes(HttpPostedFileBase files)
        {

            BinaryReader reader = new BinaryReader(files.InputStream);
            return reader.ReadBytes(files.ContentLength);
        }
        // POST: Vaccancies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Units,Position,ClosingDate,file,location")] Vaccancie vaccancie, HttpPostedFileBase files)
        {
            if (files != null && files.ContentLength > 0)
            {
                vaccancie.location = files.FileName;
                string[] bits = vaccancie.location.Split('\\');

                vaccancie.file = ConvertToBytes(files);

            }

            if (ModelState.IsValid)
            {
                db.Vaccancies.Add(vaccancie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaccancie);
        }

        public ActionResult Downld(int? id)
        {

            MemoryStream ms = null;

            var item = db.Vaccancies.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                ms = new MemoryStream(item.file);
            }
            return new FileStreamResult(ms, item.location);


            //return RedirectToAction("Download");
        }

        [HttpPost]
        public ActionResult ViewFile(int? id)
        {
            var r = db.Vaccancies.Find(id);
            //PDF pDF = db.PDFs.Find(id);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute(r.location));

            return RedirectToAction("Index");
        }



        // GET: Vaccancies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccancie vaccancie = db.Vaccancies.Find(id);
            if (vaccancie == null)
            {
                return HttpNotFound();
            }
            return View(vaccancie);
        }

        // POST: Vaccancies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Units,Position,ClosingDate,file,location")] Vaccancie vaccancie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccancie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaccancie);
        }

        // GET: Vaccancies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccancie vaccancie = db.Vaccancies.Find(id);
            if (vaccancie == null)
            {
                return HttpNotFound();
            }
            return View(vaccancie);
        }

        // POST: Vaccancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vaccancie vaccancie = db.Vaccancies.Find(id);
            db.Vaccancies.Remove(vaccancie);
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
