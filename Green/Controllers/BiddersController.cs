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
    public class BiddersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bidders
        public ActionResult Index()
        {
            return View(db.Bidders.ToList());
        }

        // GET: Bidders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bidders bidders = db.Bidders.Find(id);
            if (bidders == null)
            {
                return HttpNotFound();
            }
            return View(bidders);
        }

        // GET: Bidders/Create
        public ActionResult Create()
        {
            return View();
        }
        public byte[] ConvertToBytes(HttpPostedFileBase files)
        {

            BinaryReader reader = new BinaryReader(files.InputStream);
            return reader.ReadBytes(files.ContentLength);
        }

        // POST: Bidders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Year,Picture,file,location")] Bidders bidders, HttpPostedFileBase files,HttpPostedFileBase img_upload)
        {
            if (files != null && files.ContentLength > 0)
            {
                bidders.location = files.FileName;
                string[] bits = bidders.location.Split('\\');

                bidders.file = ConvertToBytes(files);

            }
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            bidders.Picture = data;
            if (ModelState.IsValid)
            {
                db.Bidders.Add(bidders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bidders);
        }
        public ActionResult Download(int? id)
        {

            MemoryStream ms = null;

            var item = db.Bidders.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                ms = new MemoryStream(item.file);
            }
            return new FileStreamResult(ms, item.location);
        }

        [HttpPost]
        public ActionResult ViewPDF(int? id)
        {
            var r = db.Bidders.Find(id);
            //PDF pDF = db.PDFs.Find(id);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute(r.location));

            return RedirectToAction("Index");
        }

        // GET: Bidders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bidders bidders = db.Bidders.Find(id);
            if (bidders == null)
            {
                return HttpNotFound();
            }
            return View(bidders);
        }

        // POST: Bidders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Year,file,location")] Bidders bidders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bidders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bidders);
        }

        // GET: Bidders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bidders bidders = db.Bidders.Find(id);
            if (bidders == null)
            {
                return HttpNotFound();
            }
            return View(bidders);
        }

        // POST: Bidders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bidders bidders = db.Bidders.Find(id);
            db.Bidders.Remove(bidders);
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
