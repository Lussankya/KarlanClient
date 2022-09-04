using KarlanTravelClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace KarlanTravelClient.Controllers
{
    public class HomeController : Controller
    {
        ContextModel db = new ContextModel();
        private SessionCheck SesCheck = new SessionCheck();
        public ActionResult Index()
        {
            int showTour = 4;
            var tour = db.Tours.Include(t => t.Category).Include(t => t.Category1).OrderByDescending(t => t.TourRating).ToList();
            List<Tour> temp = new List<Tour>();
            for(int i = 0; i < showTour; i++)
            {
                temp.Add(tour[i]);
            }
            return View(temp);
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }
        public ActionResult Customer()
        {
            if (!SesCheck.SessionChecking())
            {
                return RedirectToAction("../Home/Login");
            }
            int id = Int32.Parse(Session["UserID"].ToString());
            var customer = db.Customers.Include(c => c.BankAccount).Where(c => c.CustomerId == id).ToList();
            return View(customer);
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            var bank = db.Banks.ToList();
            var city = db.Cities.ToList();
            ListData list = new ListData();
            list.banks = bank;
            list.cities = city;
            List<ListData> bagList = new List<ListData>();
            bagList.Add(list);
            return View(bagList);
        }
        public ActionResult checkLogin(String userName, String pass)
        {
            var customer = db.Customers.Include(c => c.BankAccount).Include(c => c.City);
            TempData["LoginResult"] = "";
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(pass))
            {
                TempData["LoginResult"] = "Fill all information please!";
                return RedirectToAction("Login");
            }
            else
            {
                customer = customer.Where(c => c.Username == userName).Where(c => c.UserPassword == pass);
                if (customer.FirstOrDefault() != null)
                {
                    Session.Timeout = 100;
                    Session["UserID"] = customer.FirstOrDefault().CustomerId;
                    Session["UserName"] = customer.FirstOrDefault().Username;
                    return RedirectToAction("../TransactionRecords/Index");
                }
                else
                {
                    TempData["LoginResult"] = "Invalid username or password";
                    return RedirectToAction("Login");
                }
            }
        }
        public ActionResult createCustomer(String username, String pass, String repass, String email, String phone, String city, String bankName, String accName, String accNum)
        {
            TempData["error"] = "";
            var checkcstm = db.Customers.Where(c => c.Username == username);
            if (username.Equals("") || pass.Equals("") || repass.Equals("") || email.Equals("") || phone.Equals("") || city.Equals(""))
            {
                TempData["error"] = "Fill all information please!";
                return RedirectToAction("Register");
            }
            else
            {
                if (!pass.Equals(repass))
                {
                    TempData["error"] = "Confirm password fail!";
                    return RedirectToAction("Register");
                }
                if (checkcstm.FirstOrDefault() != null)
                {
                    TempData["error"] = "Username is already used!";
                    return RedirectToAction("Register");
                }

                Customer customer = new Customer();
                if (!bankName.Equals("") && !accName.Equals("") && !accNum.Equals(""))
                {
                    BankAccount bankAcc = new BankAccount();
                    bankAcc.AccountName = accName;
                    bankAcc.AccountNumber = accNum;
                    bankAcc.BankId = db.Banks.Where(b => b.BankName == bankName).FirstOrDefault().BankId;
                    bankAcc.Deleted = false;
                    if (db.BankAccounts.Where(b => b.AccountNumber == accNum).FirstOrDefault() != null)
                    {
                        TempData["error"] = "Bank Account is already used!";
                        return RedirectToAction("Register");
                    }
                    db.BankAccounts.Add(bankAcc);
                    db.SaveChanges();
                    customer.BankAccountId = db.BankAccounts.Where(b => b.AccountNumber == accNum).FirstOrDefault().BankAccountId;
                }
                customer.Username = username;
                customer.UserPassword = pass;
                if (db.Customers.Where(c => c.Email == email).FirstOrDefault() != null)
                {
                    TempData["error"] = "Email is already used!";
                    return RedirectToAction("Register");
                }
                customer.Email = email;
                customer.Phone = phone;
                customer.CityId = db.Cities.Where(c => c.CityName == city).FirstOrDefault().CityId;
                customer.CustomerNote = "";
                customer.Deleted = false;
                customer.AmountToPay = 0;
                customer.AmountToRefund = 0;
                customer.BlackListed = false;
                customer.Violations = 0;
                db.Customers.Add(customer);
                db.SaveChanges();
                TempData["error"] = "";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Login");
        }

    }
}