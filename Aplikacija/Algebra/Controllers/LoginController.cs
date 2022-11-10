using Algebra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Algebra.Controllers
{
    public class LoginController : Controller
    {
        AlgebraEntities db = new AlgebraEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Zaposlenik objchk)
        {
            if (ModelState.IsValid)
            {
                using(AlgebraEntities db = new AlgebraEntities())
                {
                    var obj = db.Zaposlenik.Where(a => a.Korisnik.Equals(objchk.Korisnik) && a.Lozinka.Equals(objchk.Lozinka)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["Korisnik"] = obj.Korisnik.ToString();
                        Session["Lozinka"] = obj.Korisnik.ToString();
                        return RedirectToAction("Index", "Predbiljezba");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Korisničko ime ili lozinka su netočni");
                    }
                }
            }            

            return View(objchk);
        }

        public ActionResult Odjava()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}