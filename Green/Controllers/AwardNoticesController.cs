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
    public class AwardNoticesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AwardNotices
        public ActionResult Index()
        {
            return View(db.AwardNotices.ToList());
        }

        // GET: AwardNotices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwardNotice awardNotice = db.AwardNotices.Find(id);
            if (awardNotice == null)
            {
                return HttpNotFound();
            }
            return View(awardNotice);
        }

        // GET: AwardNotices/Create
        public ActionResult Create()
        {
            return View();
        }
        public byte[] ConvertToBytes(HttpPostedFileBase files)
        {

            BinaryReader reader = new BinaryReader(files.InputStream);
            return reader.ReadBytes(files.ContentLength);
        }


        // POST: AwardNotices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Year,file,location")] AwardNotice awardNotice, HttpPostedFileBase files)
        {
            if (files != null && files.ContentLength > 0)
            {
                awardNotice.location = files.FileName;
                string[] bits = awardNotice.location.Split('\\');

                awardNotice.file = ConvertToBytes(files);

            }
            if (ModelState.IsValid)
            {
                db.AwardNotices.Add(awardNotice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(awardNotice);
        }

        public ActionResult Download(int? id)
        {

            MemoryStream ms = null;

            var item = db.AwardNotices.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                ms = new MemoryStream(item.file);
            }
            return new FileStreamResult(ms, item.location);


            //return RedirectToAction("Download");
        }

        [HttpPost]
        public ActionResult ViewPDF(int? id)
        {
            var r = db.AwardNotices.Find(id);
            //PDF pDF = db.PDFs.Find(id);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute(r.location));

            return RedirectToAction("Index");
        }


        // GET: AwardNotices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwardNotice awardNotice = db.AwardNotices.Find(id);
            if (awardNotice == null)
            {
                return HttpNotFound();
            }
            return View(awardNotice);
        }

        // POST: AwardNotices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Year,file,location")] AwardNotice awardNotice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(awardNotice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(awardNotice);
        }

        // GET: AwardNotices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwardNotice awardNotice = db.AwardNotices.Find(id);
            if (awardNotice == null)
            {
                return HttpNotFound();
            }
            return View(awardNotice);
        }

        // POST: AwardNotices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AwardNotice awardNotice = db.AwardNotices.Find(id);
            db.AwardNotices.Remove(awardNotice);
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
