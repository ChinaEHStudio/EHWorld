using EHWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EHWorld.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using Microsoft.EntityFrameworkCore;

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
        public string reg()
        {
                StreamReader sr = new StreamReader(Request.Body);

                string body = sr.ReadToEndAsync().Result;
                JObject job = JObject.Parse(body);
                var account = new Account() { username = job["username"].ToString(), email = job["email"].ToString(), password = job["pass"].ToString() };
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return "successlly";

        }
       
        [HttpPost]
        public async Task<string> logAsync()
        {
            StreamReader sr = new StreamReader(Request.Body);

            string body = sr.ReadToEndAsync().Result;
            JObject job = JObject.Parse(body);

            var account =  await _context.Accounts.FirstOrDefaultAsync(s => s.email == job["email"].ToString());
            if (account == null) return "NotFound Account";

            if (account.password == job["pass"].ToString())
            {
                account.uuiduser=Guid.NewGuid().ToString();
                _context.Accounts.Update(account);

                _context.SaveChanges();

                var accoun = _context.Accounts.Find(account.Id);

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
