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
            const string Url = "http://stash.gtech.local:7990/projects/SB/repos/dev/commits/";
            const string UniqueSeparator = "unique_separator_1_2343fd";

            if (args.Length > 0)
            {
                StringBuilder commitId = new StringBuilder();
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
                        case 1: commitId.Append(argument + " ");
                            break;
                        case 2: message.Append(argument + " ");
                            break;
                        case 3: authorEmail.Append(argument + " ");
                            break;
                        case 4: author.Append(argument + " ");
                            break;
                        case 5: date.Append(argument + " ");
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

                mm.Body = GenerateResultBody(Url, message, author, authorEmail, date,commitId);

                mm.Subject = String.Format("{0} ({1}) pushed to your repository", authorEmail, author);
                mm.IsBodyHtml = true;

                client.Send(mm);
            }
        }

        private static string GenerateResultBody(string url, StringBuilder message, StringBuilder author, StringBuilder authorEmail, StringBuilder date,StringBuilder commitId)
        {
            StringBuilder resultBody = new StringBuilder();
            resultBody.Append(String.Format("<h1>{0} ({1}) pushed to your repository</h1>", authorEmail, author));
            resultBody.Append(String.Format("<p><b>Commit message :</b>{0}</p><p>CommitId : {2}</p><p>Commit : {3}</p><p>Commited on : {1}</p>", message, !String.IsNullOrEmpty(date.ToString()) ? date.ToString() : DateTime.Now.ToString(),commitId.ToString(),url + commitId));

            return resultBody.ToString();
        }
    }
}
