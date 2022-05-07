using EHWorld.Data;
using EHWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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


            StreamReader sr = new StreamReader(Request.Body);
            string body = sr.ReadToEndAsync().Result;
            JObject job = JObject.Parse(body);
            Account? account = new Account() { username = job["username"].ToString(), email = job["email"].ToString(), password = job["pass"].ToString() };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return "successlly";

        }

        [HttpPost]
        public string log()
        {
            StreamReader sr = new StreamReader(Request.Body);

            string body = sr.ReadToEndAsync().Result;
            JObject job = JObject.Parse(body);







            Account? account = _context.Accounts.Single(s => s.email == job["email"].ToString());
            if (account == null)
            {
                return "NotFound Account";
            }

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
