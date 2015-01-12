using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            const string UniqueSeparator = "unique_separator_1_2343fd";

            if (args.Length > 0)
            {
                StringBuilder message = new StringBuilder();
                StringBuilder author = new StringBuilder();
                StringBuilder authorEmail = new StringBuilder();
                StringBuilder date = new StringBuilder();
                int argumentIndex = 1;

                foreach (var argument in args)
                {
                    if (argument == UniqueSeparator)
                    {
                        argumentIndex++;
                        continue;
                    }
                    switch (argumentIndex)
                    {
                        case 1: message.Append(argument + " ");
                            break;
                        case 2: authorEmail.Append(argument + " ");
                            break;
                        case 3: author.Append(argument + " ");
                            break;
                        case 4: date.Append(argument + " ");
                            break;
                        default:
                            break;
                    }
                }

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("viktordakov97@gmail.com", "Qaz97wsx");

                MailMessage mm = new MailMessage("viktordakov97@gmail.com", "tdd@dekom.bg");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.Body = GenerateResultBody(message, author, authorEmail, date);
                mm.Subject = String.Format("{0} ({1}) pushed to your repository", authorEmail, author);
                mm.IsBodyHtml = true;

                client.Send(mm);
            }
        }

        private static string GenerateResultBody(StringBuilder message, StringBuilder author, StringBuilder authorEmail, StringBuilder date)
        {
            StringBuilder resultBody = new StringBuilder();
            resultBody.Append(String.Format("<h1>{0} ({1}) pushed to your repository</h1>", authorEmail, author));
            resultBody.Append(String.Format("<h3>Commit message :</h3><p>{0}</p><p>Commited on : {1}</p>", message, date != null ? date.ToString() : DateTime.Now.ToString()));

            return resultBody.ToString();
        }
    }
}
