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
        public ActionResult Detail(string id)
        {
            var tourDetail = db.TourDetails.Include(t => t.Facility).Include(t => t.TouristSpot).Include(t => t.Tour).Where(t => t.TourId == id && t.Deleted==false).OrderBy(t => t.ActivityTimeStart);
            List<Decimal> map = new List<Decimal>();
            List<string> spotName = new List<string>();
            List<TourDetail> temp = tourDetail.ToList();
            for(int i = 0; i < temp.Count; i++)
            {
                if(temp[i].TouristSpotId != "none")
                {
                    string tempId = temp[i].TouristSpotId;
                    TouristSpot ts = db.TouristSpots.Where(t => t.Deleted == false && t.TouristSpotId == tempId).First();
                    map.Add((ts.Cord_Lat ?? 0));
                    map.Add((ts.Cord_Long ?? 0));
                    spotName.Add(ts.TouristSpotName);
                }
            }
            ViewBag.Map = map;
            ViewBag.SpotName = spotName;
            return View(tourDetail.ToList());
        }
        // GET: Locations
        [HttpGet]
        public ActionResult map()
        {
             
            var q = (from a in db.TouristSpots
                     select new { a.Cord_Lat, a.Cord_Long, a.TouristSpotName} );
            // return PartialView("_map", q.ToList()); 
            return Json(q, JsonRequestBehavior.AllowGet);
        }
        
            
        
            
        
    }
}