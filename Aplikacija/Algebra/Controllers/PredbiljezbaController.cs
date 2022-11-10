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
    public class PredbiljezbaController : Controller
    {
        private AlgebraEntities db = new AlgebraEntities();

        // GET: Predbiljezba
        public ActionResult Index(string searching)
        {
            return View(db.Seminar.Where(x => x.Naziv.Contains(searching) || searching == null).ToList());
        }

        // GET: Predbiljezba/Create
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
                predbiljezba.Datum = DateTime.Now;
                db.Predbiljezba.Add(predbiljezba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSeminar = new SelectList(db.Seminar, "IdSeminar", "Naziv", predbiljezba.IdSeminar);
            return View(predbiljezba);
        }
    }
}
