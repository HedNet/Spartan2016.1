using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Ria.FxonlineInstaller
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FxInstaller());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = ((Exception)e.ExceptionObject);
            MessageBox.Show(ex.Message, "Installation Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
            /*
            string body = ((Exception)e.ExceptionObject).ToString();

            var fromAddress = new System.Net.Mail.MailAddress("no-reply-exception-reports@ria.webege.com", "System message");
            var toAddress = new System.Net.Mail.MailAddress("hanseliseofk@gmail.com", "Developer");
            string fromPassword = "deNirvana3105";
            string subject = ex.GetType().ToString();

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "mail.000webhost.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);                
            }
            */
        }
    }
}
