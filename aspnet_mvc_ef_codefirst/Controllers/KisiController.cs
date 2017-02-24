using aspnet_mvc_ef_codefirst.Models;
using aspnet_mvc_ef_codefirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_ef_codefirst.Controllers
{
    public class KisiController : Controller
    {
        // GET: Kisi
        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Kisiler kisi)
        {
            DatabaseContext db = new DatabaseContext();
            db.Kisiler.Add(kisi);

            int sonuc = db.SaveChanges();

            if (sonuc > 0)
            {
                ViewBag.Result = " Kisi kaydedilmistir.";
                ViewBag.Status = "success";

            }
            else
            {
                ViewBag.Result = "Kisi kaydedilmemistir !";
                ViewBag.Status = "danger";
            }

            return View();
        }

        public ActionResult Duzenle(int? kisi_id)  //int?= null alabilen demek.
        {
            Kisiler kisi = null;

            if (kisi_id != null)
            {
                DatabaseContext db = new DatabaseContext();
                kisi = db.Kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();
            }
            return View(kisi);
        }

        [HttpPost]
        public ActionResult Duzenle(Kisiler model, int? kisi_id)  //int?= null alabilen demek.
        {
            DatabaseContext db = new DatabaseContext();
            Kisiler kisi = db.Kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();

            if (kisi != null)
            {
                kisi.Ad = model.Ad;
                kisi.Soyad = model.Soyad;
                kisi.Yas = model.Yas;
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = " Kisi güncellenmiştir.";
                    ViewBag.Status = "success";

                }
                else
                {
                    ViewBag.Result = "Kişi güncellenmemiştir. Hata !";
                    ViewBag.Status = "danger";
                }
            }
            return View(kisi);
        }

        [HttpGet]
        public ActionResult Sil(int? kisi_id)
        {
            Kisiler kisi = null;
            if (kisi_id != null)
            {

                DatabaseContext db = new DatabaseContext();
                kisi = db.Kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();


            }
            return View(kisi);
        }


        //Kisi/Sil
        [HttpPost, ActionName("Sil")]
        public ActionResult SilOk(int? kisi_id)
        {
            DatabaseContext db = new DatabaseContext();
            if (kisi_id != null)
            {

                
             Kisiler kisi = db.Kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();
                List<Adresler> adres = db.Adresler.Where(x => x.Kisi.ID == kisi_id).ToList();
                foreach (Adresler _adres in adres)
                {
                    db.Adresler.Remove(_adres);
                }

                db.Kisiler.Remove(kisi);
                db.SaveChanges();

                
            }
           
            //Home/Homapage sayfasina git demek (Yonlendirme)
            return RedirectToAction("Homepage", "Home");
        }

    }

 

}
