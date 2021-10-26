using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Green.Models;

namespace Green.Controllers
{
    public class formsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: forms
        public ActionResult Index()
        {
            return View(db.forms.ToList());
        }

        // GET: forms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forms forms = db.forms.Find(id);
            if (forms == null)
            {
                return HttpNotFound();
            }
            return View(forms);
        }

        // GET: forms/Create
        public ActionResult Create()
        {
            return View();
        }
        public byte[] ConvertToBytes(HttpPostedFileBase files)
        {

            BinaryReader reader = new BinaryReader(files.InputStream);
            return reader.ReadBytes(files.ContentLength);
        }

        // POST: forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,file,FileName,ApplicationForms,location")] forms forms, HttpPostedFileBase files)
        {
            if(files!=null && files.ContentLength>0)
            {
                forms.location = files.FileName;
                string[] bits = forms.location.Split('\\');
              
                forms.file = ConvertToBytes(files);

            }

            if (ModelState.IsValid)
            {
                db.forms.Add(forms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forms);
        }
        public ActionResult Download(int? id)
        {

            MemoryStream ms = null;

            var item = db.forms.FirstOrDefault(x => x.ID == id);
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
            var r = db.forms.Find(id);
            //PDF pDF = db.PDFs.Find(id);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute(r.location));

            return RedirectToAction("Index");
        }

        // GET: forms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forms forms = db.forms.Find(id);
            if (forms == null)
            {
                return HttpNotFound();
            }
            return View(forms);
        }

        // POST: forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,file,FileName,ApplicationForms,location")] forms forms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forms);
        }

        // GET: forms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forms forms = db.forms.Find(id);
            if (forms == null)
            {
                return HttpNotFound();
            }
            return View(forms);
        }

        // POST: forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            forms forms = db.forms.Find(id);
            db.forms.Remove(forms);
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
