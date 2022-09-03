using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KarlanTravelClient.Controllers
{
    public class SessionCheck
    {
        public string HashPW(string pass)
        {
            SHA1Managed sha1 = new SHA1Managed();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));
            var pw = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                pw.Append(b.ToString("x2"));
            }

            return pw.ToString();
        }

        public bool SessionChecking()
        {
            return (
                HttpContext.Current != null &&
                HttpContext.Current.Session != null &&
                HttpContext.Current.Session["UserId"] != null
                );
        }
    }
}