using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KarlanTravelClient.Models;

namespace KarlanTravelClient.Controllers
{
    public class TourDetailsController : Controller
    {
        ContextModel db = new ContextModel();
        // GET: TourDetails
        public ActionResult Index(int id)
        {
            var tourDetail = db.TourDetails.Include(t => t.Facility).Include(t => t.TouristSpot).Include(t => t.Tour).Where(t => t.TourDetailId == id);
            return View(tourDetail.ToList());
        }
        
    }
}