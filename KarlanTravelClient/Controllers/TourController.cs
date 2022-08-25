using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarlanTravelClient.Models;
using System.Data.Entity;

namespace KarlanTravelClient.Controllers
{
    public class TourController : Controller
    {
        // GET: Tour
        ContextModel db = new ContextModel();
        public ActionResult Index()
        {
            var tour = db.Tours.Include(t => t.Category).Include(t => t.Category1);
            return View(tour.ToList());
        }
        public ActionResult Detail()
        {


            return View();
        }
    }
}