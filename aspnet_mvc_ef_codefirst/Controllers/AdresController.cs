using aspnet_mvc_ef_codefirst.Models;
using aspnet_mvc_ef_codefirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_ef_codefirst.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        public ActionResult YeniAdres()
        {
            DatabaseContext db = new DatabaseContext();

            List<Kisiler> kisiler = db.Kisiler.ToList();

            //LINQ ile
            List<SelectListItem> kisilerList = (from kisi in db.Kisiler.ToList()
                                                select new SelectListItem()
                                                {
                                                    Text = kisi.Ad + " " + kisi.Soyad,
                                                    Value = kisi.ID.ToString()
                                                }).ToList();

            TempData["kisiler"] = kisilerList;
            ViewBag.kisiler = kisilerList;


            return View();
        }

        [HttpPost]
        public ActionResult YeniAdres(Adresler adres)
        {
            DatabaseContext db = new DatabaseContext();
            Kisiler kisi = db.Kisiler.Where(x => x.ID == adres.Kisi.ID).FirstOrDefault(); // LINQ yazdik.

            if (kisi != null)
            {
                adres.Kisi = kisi;
                db.Adresler.Add(adres);
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = " Adres kaydedilmistir.";
                    ViewBag.Status = "success";

                }
                else
                {
                    ViewBag.Result = "Adres kaydedilmemistir !";
                    ViewBag.Status = "danger";
                }
            }
            ViewBag.kisiler = TempData["kisiler"];
            return View();
        }

        public ActionResult Duzenle(int? adres_id)   // homepage uzerinden adres uzerine tiklanildiginda yeni sayfaya secilen adres bilgilerini gonderir.
        {
            Adresler adres = null;


            if (adres_id != null)
            {

                DatabaseContext db = new DatabaseContext();
             

                //LINQ ile
                List<SelectListItem> kisilerList = (from kisi in db.Kisiler.ToList()
                                                    select new SelectListItem()
                                                    {
                                                        Text = kisi.Ad + " " + kisi.Soyad,
                                                        Value = kisi.ID.ToString()
                                                    }).ToList();

                TempData["kisiler"] = kisilerList;
                ViewBag.kisiler = kisilerList;

                adres = db.Adresler.Where(x => x.ID == adres_id).FirstOrDefault();

            }

            return View(adres);
        }


        [HttpPost]
        public ActionResult Duzenle(Adresler model, int? adres_id)
        {
            DatabaseContext db = new DatabaseContext();
            Kisiler kisim = db.Kisiler.Where(x => x.ID == model.Kisi.ID).FirstOrDefault();
            Adresler adresim = db.Adresler.Where(x => x.ID == adres_id).FirstOrDefault();

            if (kisim != null)
            {

                adresim.Kisi = kisim;
  
                adresim.AdresTanim = model.AdresTanim;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = " Adres güncellenmiştir.";
                    ViewBag.Status = "success";

                }
                else
                {
                    ViewBag.Result = "Adres güncellenmemiştir. Hata !";
                    ViewBag.Status = "danger";
                }
            }
            ViewBag.kisiler = TempData["kisiler"];
            return View(adresim);

        }


        public ActionResult Sil(int? adres_id)
        {
            Adresler adres = null;
            if (adres_id != null)
            {

                DatabaseContext db = new DatabaseContext();
                adres = db.Adresler.Where(x => x.ID == adres_id).FirstOrDefault();


            }
            return View(adres);
        }

        //Adres/Sil
        [HttpPost, ActionName("Sil")]
        public ActionResult SilOk(int? adres_id)
        {
           
            if (adres_id != null)
            {

                DatabaseContext db = new DatabaseContext();
                 Adresler adres = db.Adresler.Where(x => x.ID == adres_id).FirstOrDefault();
                db.Adresler.Remove(adres);
                db.SaveChanges();

            }
            //Home/Homapage sayfasina git demek (Yonlendirme)
            return RedirectToAction("Homepage", "Home");
        }
    }
}
