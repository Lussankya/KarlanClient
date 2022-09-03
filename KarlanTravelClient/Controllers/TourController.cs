using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KarlanTravelClient.Models;

namespace KarlanTravelClient.Controllers
{
    public class TourController : Controller
    {
        ContextModel db = new ContextModel();
        private SessionCheck SesCheck = new SessionCheck();
        public ActionResult Index()
        {
            var tour = db.Tours.Include(t => t.Category).Include(t => t.Category1);
            
            return View(tour.ToList());
        }
        public ActionResult Detail(string id)
        {
            var tourDetail = db.TourDetails.Include(t => t.Facility).Include(t => t.TouristSpot).Include(t => t.Tour).Where(t => t.TourId == id);
            
            return View(tourDetail.ToList()) ;
        }
        public ActionResult sendTourID(string tourID)
        {
            if (!SesCheck.SessionChecking())
            {
                return RedirectToAction("../Home/Login");
            }
            else
            {
                int id = Int32.Parse(Session["UserID"].ToString());
                var customer = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
                if (customer.BankAccount == null)
                {
                    return RedirectToAction("../User/UpdateBankAccount");
                }
                if (customer.BlackListed)
                {
                    return RedirectToAction("../Home/Customer");
                }
                var tour = db.Tours.Where(t => t.TourId.Equals(tourID)).FirstOrDefault();
                TempData["tourName"] = tour.TourName;
                TempData["tourStart"] = tour.TourStart;
                TempData["tourID"] = tour.TourId;
                TempData["tourPrice"] = tour.TourPrice;
                return RedirectToAction("../TransactionRecords/TransactionBill");
            }
            
        }
    }
}