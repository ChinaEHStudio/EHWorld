using EHWorld.Data;
using EHWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
<<<<<<< HEAD
=======
using Newtonsoft.Json;
using System;
using Microsoft.EntityFrameworkCore;
>>>>>>> 2b12a359d33c47877d9b5d9ed1342de1ac65e614

namespace EHWorld.Controllers
{
    public class accountController : Controller
    {
        private readonly EhWorldContext _context;



        public accountController(EhWorldContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<string> reg()
        {
<<<<<<< HEAD
=======
                StreamReader sr = new StreamReader(Request.Body);
>>>>>>> 2b12a359d33c47877d9b5d9ed1342de1ac65e614


            StreamReader sr = new StreamReader(Request.Body);
            string body = sr.ReadToEndAsync().Result;
            JObject job = JObject.Parse(body);
            Account? account = new Account() { username = job["username"].ToString(), email = job["email"].ToString(), password = job["pass"].ToString() };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return "successlly";

        }

        [HttpPost]
        public async Task<string> logAsync()
        {
            StreamReader sr = new StreamReader(Request.Body);

            string body = sr.ReadToEndAsync().Result;
            JObject job = JObject.Parse(body);

<<<<<<< HEAD






            Account? account = _context.Accounts.Single(s => s.email == job["email"].ToString());
            if (account == null)
            {
                return "NotFound Account";
            }
=======
            var account =  await _context.Accounts.FirstOrDefaultAsync(s => s.email == job["email"].ToString());
            if (account == null) return "NotFound Account";
>>>>>>> 2b12a359d33c47877d9b5d9ed1342de1ac65e614

            if (account.password == job["pass"].ToString())
            {
                account.uuiduser = Guid.NewGuid().ToString();
                _context.Accounts.Update(account);

                _context.SaveChanges();

                Account? accoun = _context.Accounts.Find(account.Id);

                return accoun.uuiduser;
            }
            return "faild to login";
        }
        public string test()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
