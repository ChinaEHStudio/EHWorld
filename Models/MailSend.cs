using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace EHWorld.Models
{
    /// <summary>
    /// 改class将提供mail发送功能,全局可用
    /// </summary>
    public class MailSend
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        public bool send = false;
        public string random_str()
        {
            const int totalRolls = Config.totalRoll;
            int[] results = new int[6];

            // Roll the dice 25000 times and display
            // the results to the console.
            for (int x = 0; x < totalRolls; x++)
            {
                byte roll = RollDice((byte)results.Length);
                results[roll - 1]++;
            }
       //     for (int i = 0; i < results.Length; ++i)
     //       {
       //         Console.WriteLine("{0}: {1} ({2:p1})", i + 1, results[i], (double)results[i] / (double)totalRolls);
      //      }
            
            rngCsp.Dispose();
            return results[0].ToString();
        }
        public static byte RollDice(byte numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % numberSides) + 1);
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }
        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            string token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                send = true;
            }

        }

        public void sendmail(string msg, string receive_email)
        {
            // Command-line argument must be the SMTP host.
            SmtpClient client = new SmtpClient("smtp.qq.com")
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Config.email,
                Config.passwor)
            };
            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress(Config.email,
            "EHStudio",
            System.Text.Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress(receive_email);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to)
            {
                Body = msg,
                // Include some non-ASCII characters in body and subject.
                //    string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
                //  message.Body += Environment.NewLine + someArrows;
                BodyEncoding = System.Text.Encoding.UTF8,
                Subject = "test",
                SubjectEncoding = System.Text.Encoding.UTF8
            };
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            //    string userState = "test message1";
            //   client.SendAsync(message, userState);
            client.Send(message);
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            // Clean up.
            message.Dispose();

        }
    }
}
