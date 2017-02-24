using aspnet_mvc_ef_codefirst.Models;
using aspnet_mvc_ef_codefirst.Models.Managers;
using aspnet_mvc_ef_codefirst.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_ef_codefirst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Homepage()
        {
            DatabaseContext db = new DatabaseContext();

            //List<Kisiler> kisiler = db.Kisiler.ToList(); //Select * from Kisiler


            HomePageViewModel model = new HomePageViewModel();
            model.kisiler = db.Kisiler.ToList();
            model.adresler = db.Adresler.ToList();
           
            return View(model);

        }


    }
}