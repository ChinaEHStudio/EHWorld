using EHWorld.Data;
using EHWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
            {

                StreamReader sr = new StreamReader(Request.Body);

                string body = sr.ReadToEndAsync().Result;
                JObject job = JObject.Parse(body);
                MailSend? email = new MailSend();
                try
                {
                    Random random = new Random();
                    Byte[] b = new Byte[13];
                    email.sendmail("验证码:", job["username"].ToString());
                    return "successlly to send email";
                }
                catch (Exception) { return "faild to send"; }
                Account? account = new Account() { username = job["username"].ToString(), email = job["email"].ToString(), password = job["pass"].ToString(), uuiduser = Guid.NewGuid().ToString() };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return account.uuiduser;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpPost]
        public async Task<string> logAsync()
        {
            try
            {


                StreamReader sr = new StreamReader(Request.Body);
                string body = sr.ReadToEndAsync().Result;
                JObject job = JObject.Parse(body);
                //  Account? account = _context.Accounts.Single(s => s.email == job["email"].ToString());
                //   if (account == null)
                //  {
                //     return "NotFound Account";
                // }
                Account? account = await _context.Accounts.FirstOrDefaultAsync(s => s.email == job["email"].ToString());
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string test()
        {
            MailSend mailSend = new MailSend();
            return mailSend.random_str();
        }

    }
}
