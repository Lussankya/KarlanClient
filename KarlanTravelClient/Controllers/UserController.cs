using KarlanTravelClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace KarlanTravelClient.Controllers
{
    public class UserController : Controller
    {
        ContextModel db = new ContextModel();
        private SessionCheck SesCheck = new SessionCheck();
        public ActionResult ChangPassword()
        {
            if (!SesCheck.SessionChecking())
            {
                return RedirectToAction("../Home/Login");
            }
            return View();
        }
        public ActionResult UpdateBankAccount()
        {
            if (!SesCheck.SessionChecking())
            {
                return RedirectToAction("../Home/Login");
            }
            TempData["bankAccStatus"] = "";
            int id = Int32.Parse(Session["UserID"].ToString());
            var customer = db.Customers.Where(c => c.CustomerId == id);
            if (customer.FirstOrDefault().BankAccount == null)
            {
                TempData["bankAccStatus"] = "N/a";
            }
            else
            {
                TempData["bankAccStatus"] = "Exist";
            }
            var banks = db.Banks.ToList();
            return View(banks);
        }

        public ActionResult updatePass(String oldPass, String newPass, String rePass)
        {
            int id = Int32.Parse(Session["UserID"].ToString());
            var customer = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            TempData["ErrorPass"] = "";
            if (!customer.UserPassword.Equals(oldPass))
            {
                TempData["ErrorPass"] = "Wrong password!";
                return RedirectToAction("ChangPassword");
            }
            else
            {
                if (!newPass.Equals(rePass))
                {
                    TempData["ErrorPass"] = "Confirm password failed!";
                    return RedirectToAction("ChangPassword");
                }
                else
                {
                    customer.UserPassword = newPass;
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("../Home/Customer");
                }
            }
        }

        public ActionResult addBankAccount(String bankName, String bankAccName, String bankAccNum)
        {
            TempData["ErrorBankAcc"] = "";
            int id = Int32.Parse(Session["UserID"].ToString());
            var customer = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            if (!bankName.Equals("") && !bankAccName.Equals("") && !bankAccNum.Equals(""))
            {
                BankAccount bankAcc = new BankAccount();
                bankAcc.AccountName = bankAccName;
                bankAcc.AccountNumber = bankAccNum;
                bankAcc.BankId = db.Banks.Where(b => b.BankName == bankName).FirstOrDefault().BankId;
                bankAcc.Deleted = false;
                if (db.BankAccounts.Where(b => b.AccountNumber == bankAccNum).FirstOrDefault() != null)
                {
                    TempData["ErrorBankAcc"] = "Bank Account is already used!";
                    return RedirectToAction("UpdateBankAccount");
                }
                db.BankAccounts.Add(bankAcc);
                db.SaveChanges();
                customer.BankAccount = bankAcc;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Home/Customer");
            }
            else
            {
                TempData["ErrorBankAcc"] = "FilL all information please!";
                return RedirectToAction("UpdateBankAccount");
            }
        }
    }
}