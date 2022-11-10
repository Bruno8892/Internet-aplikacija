using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Algebra.Models;

namespace Algebra.Controllers
{
    public class PredbiljezbeController : Controller
    {
        private AlgebraEntities db = new AlgebraEntities();

        // GET: Predbiljezbe
        public ActionResult Index(string searching)
        {
            var predbiljezbas = db.Predbiljezba.Include(p => p.Seminar);
            return View(predbiljezbas.Where(x => x.Ime.Contains(searching) || searching == null).ToList());
        }

        // GET: Predbiljezbe/Create
        public ActionResult Create()
        {
            ViewBag.IdSeminar = new SelectList(db.Seminar, "IdSeminar", "Naziv");
            return View();
        }

        // POST: Predbiljezbe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPredbiljezba,Datum,Ime,Prezime,Adresa,Email,Telefon,IdSeminar,Status")] Predbiljezba predbiljezba)
        {
            if (ModelState.IsValid)
            {
                db.Predbiljezba.Add(predbiljezba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSeminar = new SelectList(db.Seminar, "IdSeminar", "Naziv", predbiljezba.IdSeminar);
            return View(predbiljezba);
        }

        // GET: Predbiljezbe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predbiljezba predbiljezba = db.Predbiljezba.Find(id);
            if (predbiljezba == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSeminar = new SelectList(db.Seminar, "IdSeminar", "Naziv", predbiljezba.IdSeminar);
            return View(predbiljezba);
        }

        // POST: Predbiljezbe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPredbiljezba,Datum,Ime,Prezime,Adresa,Email,Telefon,IdSeminar,Status")] Predbiljezba predbiljezba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predbiljezba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSeminar = new SelectList(db.Seminar, "IdSeminar", "Naziv", predbiljezba.IdSeminar);
            return View(predbiljezba);
        }

        // GET: Predbiljezbe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predbiljezba predbiljezba = db.Predbiljezba.Find(id);
            if (predbiljezba == null)
            {
                return HttpNotFound();
            }
            return View(predbiljezba);
        }

        // POST: Predbiljezbe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Predbiljezba predbiljezba = db.Predbiljezba.Find(id);
            db.Predbiljezba.Remove(predbiljezba);
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
