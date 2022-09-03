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
    public class TransactionRecordsController : Controller
    {
        private ContextModel db = new ContextModel();
        private SessionCheck SesCheck = new SessionCheck();
        public ActionResult Index()
        {
            if (!SesCheck.SessionChecking())
            {
                return RedirectToAction("../Home/Login");
            }
            else
            {
                int id = Int32.Parse(Session["UserID"].ToString());
                if (db.Customers.Where(c => c.CustomerId == id).FirstOrDefault().BankAccount == null)
                {
                    return RedirectToAction("../User/UpdateBankAccount");
                }
                //Customer customer = db.Customers.Find(Session["UserID"]);
                var transRecord = db.TransactionRecords.Include(t => t.Tour).Include(t => t.TransactionType).Include(t => t.Admin).Include(t => t.Customer).Where(t => t.CustomerID == id).Where(t => t.Deleted == false).ToList();
                foreach (var trans in transRecord)
                {
                    if (DateTime.Compare(trans.DueDate, DateTime.Now) <= 0 && !trans.Canceled)
                    {
                        if (!trans.Paid)
                        {
                            TransactionRecord newPurTrans = new TransactionRecord();
                            newPurTrans.Tour = trans.Tour;
                            newPurTrans.Admin = trans.Admin;
                            newPurTrans.RecordedTime = trans.DueDate;
                            newPurTrans.Deleted = false;
                            newPurTrans.Paid = false;
                            newPurTrans.DueDate = trans.DueDate;
                            newPurTrans.Canceled = true;
                            newPurTrans.TransactionTypeId = "CANCL_LATE";
                            newPurTrans.TransactionFee = 0;
                            newPurTrans.TransactionNote = "Non refund cause of canceling late";
                            trans.Customer.AmountToPay -= trans.Tour.TourPrice * 0.7M;
                            int i = ++trans.Customer.Violations;
                            if (i >= 5)
                            {
                                trans.Customer.BlackListed = true;
                            }
                            db.Entry(trans.Customer).State = EntityState.Modified;
                            db.SaveChanges();
                            newPurTrans.Customer = trans.Customer;
                            db.TransactionRecords.Add(newPurTrans);
                            db.SaveChanges();
                            trans.Canceled = true;
                            db.Entry(trans).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                var newTransRecord = db.TransactionRecords.Include(t => t.Tour).Include(t => t.TransactionType).Include(t => t.Admin).Include(t => t.Customer).Where(t => t.CustomerID == id).Where(t => t.Deleted == false).OrderByDescending(t => t.RecordedTime).ToList();
                return View(newTransRecord);
            }
        }
        public ActionResult TransactionBill()
        {
            if (!SesCheck.SessionChecking())
            {
                return RedirectToAction("../Home/Login");
            }
            else
            {
                int id = Int32.Parse(Session["UserID"].ToString());
                if (db.Customers.Where(c => c.CustomerId == id).FirstOrDefault().BankAccount == null)
                {
                    return RedirectToAction("../User/UpdateBankAccount");
                }
                var customer = db.Customers.Include(c => c.BankAccount).Where(c => c.CustomerId == id).ToList();
                return View(customer);
            }
            
        }
        public ActionResult canceledTour(String transID)
        {
            int id = Int32.Parse(transID);
            var transRecord = db.TransactionRecords.Where(t => t.TransactionRecordId == id).FirstOrDefault();
            TransactionRecord newCancelTrans = new TransactionRecord();
            newCancelTrans.Admin = transRecord.Admin;
            newCancelTrans.RecordedTime = DateTime.Now;
            newCancelTrans.Deleted = false;
            newCancelTrans.Paid = false;
            newCancelTrans.Canceled = true;
            newCancelTrans.DueDate = DateTime.Now;
            if (DateTime.Compare(transRecord.DueDate, DateTime.Now) <= 0)
            {
                newCancelTrans.TransactionTypeId = "CANCL_LATE";
                newCancelTrans.TransactionFee = 0;
                newCancelTrans.TransactionNote = "Non refund cause of canceling late";
                if (!transRecord.Paid)
                {
                    transRecord.Customer.AmountToPay -= transRecord.Tour.TourPrice * 0.7M;
                }
                int i = ++transRecord.Customer.Violations;
                if (i >= 5)
                {
                    transRecord.Customer.BlackListed = true;
                }
                db.Entry(transRecord.Customer).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                newCancelTrans.TransactionTypeId = "CANCL_EARL";
                if (!transRecord.Paid)
                {
                    newCancelTrans.TransactionFee = transRecord.Tour.TourPrice * (decimal)transRecord.TransactionType.TransactionPriceRate;
                    newCancelTrans.TransactionNote = "Refund deposit (30% price of tour)";
                    transRecord.Customer.AmountToPay -= transRecord.Tour.TourPrice * 0.7M;
                    transRecord.Customer.AmountToRefund += transRecord.Tour.TourPrice * 0.3M;
                    db.Entry(transRecord.Customer).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    newCancelTrans.TransactionFee = transRecord.Tour.TourPrice;
                    newCancelTrans.TransactionNote = "Refund 100% price of tour";
                    transRecord.Customer.AmountToRefund += transRecord.Tour.TourPrice;
                    db.Entry(transRecord.Customer).State = EntityState.Modified;
                    db.SaveChanges();
                }
                transRecord.Tour.MaxBooking++;
                if (!transRecord.Tour.TourAvailability)
                {
                    transRecord.Tour.TourAvailability = true;
                }
                db.Entry(transRecord.Tour).State = EntityState.Modified;
                db.SaveChanges();
            }
            newCancelTrans.Customer = transRecord.Customer;
            newCancelTrans.Tour = transRecord.Tour;
            db.TransactionRecords.Add(newCancelTrans);
            db.SaveChanges();
            transRecord.Canceled = true;
            db.Entry(transRecord).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult purchaseFullTour(String transID)
        {
            int id = Int32.Parse(transID);
            var transRecord = db.TransactionRecords.Where(t => t.TransactionRecordId == id).FirstOrDefault();
            transRecord.Customer.AmountToPay -= transRecord.Tour.TourPrice * 0.7M;
            db.Entry(transRecord.Customer).State = EntityState.Modified;
            db.SaveChanges();
            transRecord.Paid = true;
            transRecord.RecordedTime = DateTime.Now;
            transRecord.TransactionFee = transRecord.Tour.TourPrice;
            transRecord.TransactionNote = "Purchase full tour";
            transRecord.TransactionTypeId = "PURCHSE";
            db.Entry(transRecord).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult depositTour(String tourID, String transOpt)
        {
            int id = Int32.Parse(Session["UserID"].ToString());
            int count = db.TransactionRecords.Where(t => t.CustomerID == id).Where(t => t.TourId.Equals(tourID)).Where(t => t.TransactionTypeId.Equals("DEPOSIT") || t.TransactionTypeId.Equals("PURCHSE")).Count();
            var tour = db.Tours.Where(t => t.TourId.Equals(tourID)).FirstOrDefault();
            if (count >= tour.BookTimeLimit)
            {
                TempData["TransError"] = "You have purchased maximum quantity for this tour";
                return RedirectToAction("TransactionBill");
            }
            tour.MaxBooking--;
            if(tour.MaxBooking == 0)
            {
                tour.TourAvailability = false;
            }
            db.Entry(tour).State = EntityState.Modified;
            db.SaveChanges();
            TransactionRecord newDepositTrans = new TransactionRecord();
            newDepositTrans.CustomerID = id;
            newDepositTrans.Admin = null;
            newDepositTrans.RecordedTime = DateTime.Now;
            newDepositTrans.TourId = tourID;
            newDepositTrans.Deleted = false;
            newDepositTrans.Canceled = false;
            newDepositTrans.DueDate = db.Tours.Where(t => t.TourId.Equals(tourID)).FirstOrDefault().TourStart.AddDays(-1*tour.CancelDueDate);
            if (transOpt.Equals("30%"))
            {
                newDepositTrans.TransactionTypeId = "DEPOSIT";
                newDepositTrans.TransactionFee = db.Tours.Where(t => t.TourId.Equals(tourID)).FirstOrDefault().TourPrice * 0.3M;
                newDepositTrans.TransactionNote = "Put deposit for tour (30% price)";
                newDepositTrans.Paid = false;
                var updateCustomer = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
                updateCustomer.AmountToPay += db.Tours.Where(t => t.TourId.Equals(tourID)).FirstOrDefault().TourPrice * 0.7M;
                db.Entry(updateCustomer).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                newDepositTrans.TransactionTypeId = "PURCHSE";
                newDepositTrans.TransactionFee = db.Tours.Where(t => t.TourId.Equals(tourID)).FirstOrDefault().TourPrice;
                newDepositTrans.TransactionNote = "Purchase full tour";
                newDepositTrans.Paid = true;
            }
            db.TransactionRecords.Add(newDepositTrans);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
