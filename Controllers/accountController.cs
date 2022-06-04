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


        public  string getvn(string email)
        {
            MailSend send = new MailSend();
            string n=send.random_str();











            Vnu checkac = _context.Vnumbers.FirstOrDefault(s => s.email == email);
            if (checkac != null)
            {
                checkac.vnumb = hash(n);
                _context.Vnumbers.Update(checkac);
                _context.SaveChanges();
           //     return hash(n);
            }
            else
            {
                var vn = new Vnu() { email = email, vnumb = hash(n) };
                _context.Vnumbers.Add(vn);
















                _context.SaveChanges();
            }
           










            send.sendmail(n,email);
            return hash(n);

        }
        /*public string getnum(string email)
        {
            MailSend send = new MailSend();
            string n = send.random_str();
            send.sendmail(n, email);
            Vnumber vnumber = new Vnumber()
            {
                email = email,
                dateTime = DateTime.Now,
                vnumber = hash(n)
            };
            return hash(n);

        }*/

        [HttpPost]
        public async Task<string> reg()
        {
         //   try
         //   {

                StreamReader sr = new StreamReader(Request.Body);

                string body = sr.ReadToEndAsync().Result;
                JObject job = JObject.Parse(body);

















































                Vnu checkac = _context.Vnumbers.FirstOrDefault(s => s.email == job["email"].ToString());
                if (checkac == null) return "not found vn";































































































































































            Console.WriteLine("successlly");
                if (checkac.vnumb != hash(job["vernumber"].ToString())) return "vnumber error"; 
                /*
                MailSend? email = new MailSend();
                try
                {
                    MailSend mailSend = new MailSend();
                    string n = mailSend.random_str();

                    email.sendmail(n, job["email"].ToString());
                    return hash(n);
                }
                catch (Exception) { return "faild to send"; } */
              //  Account account_check=_context

                Account? account = new Account() { username = job["username"].ToString(), email = job["email"].ToString(), password = job["pass"].ToString(), uuiduser = Guid.NewGuid().ToString() };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return account.uuiduser;
      //      }
      //      catch (Exception ex)
         //   {
          //      return ex.Message;
         //   }

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

        public string hash(string word)
        {
            var h = new Hash("salt");
            return h.ApplyHash(word);
        }

    }
}
