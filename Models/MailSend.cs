using System.ComponentModel;
using System.Net;
using System.Net.Mail;
namespace EHWorld.Models
{
    /// <summary>
    /// 改class将提供mail发送功能,全局可用
    /// </summary>
    public class MailSend
    {
        public bool send = false;
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
