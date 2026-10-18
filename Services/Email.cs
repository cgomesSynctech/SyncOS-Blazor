using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Modelos;

namespace Services
{
    public class Email
    {
        private const string EMAIL = "synctech4@gmail.com";
        private const string PASSWORD = "St*+8802";
        private static bool mailSent = false;

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                mailSent = false;
            }
            if (e.Error != null)
            {
                mailSent = false;
            }
            else
            {
                mailSent = true;
            }
        }


        public static async Task<bool> EnviarRelatorio(string email, string filePath)
        {
            try
            {
                    using (SmtpClient client = new SmtpClient())
                    {
                        MailAddress from = new MailAddress(EMAIL, "IBestPlay!", Encoding.UTF8);
                        MailAddress to = new MailAddress(email, "IBestPlay", Encoding.UTF8);
                        MailMessage mail = new MailMessage(from, to);
                        mail.Subject = "Relatório do IBestPlay";
                        mail.IsBodyHtml = true;
                        mail.Body = "<head><meta charset = 'UTF-8'></head><div style = 'background: #2E2EFE; color: #fff; width: 100%; text-align: center; font-size: 20px; padding: 10px 0px;'> SyncOs, Controle de Chamados!</div><div style = 'padding: 10px 0; font-size: 15px; width: 70%; margin: 0 auto;'><center><h1>Segue relatório em anexo!</h1></center></div>";
                        mail.Attachments.Add(new Attachment(filePath));
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.EnableSsl = false;
                        client.Credentials = new NetworkCredential(EMAIL, PASSWORD);
                        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                        await client.SendMailAsync(mail);
                        if (mailSent == false)
                        {
                            client.SendAsyncCancel();
                        }
                        mail.Dispose();
                    }
                return mailSent;
            }
            catch (Exception e)
            {
                Services.LogErro.AddErro(e.Message);
                return false;
            }
        }

        public static bool NovaSenha(Usuario usr, string novaSenha)
        {
            if (usr.Email == null)
            {
                return false;
            }
            try
            {
                var corpoEmail = System.IO.File.ReadAllText("docHtml/emailNovaSenha.html");
                corpoEmail = corpoEmail.Replace("[usuario]", usr.LoginName);
                corpoEmail = corpoEmail.Replace("[senha]", novaSenha);
                MailAddress from = new MailAddress(EMAIL, "SyncOS!", Encoding.UTF8);
                MailAddress to = new MailAddress(usr.Email, usr.LoginName, Encoding.UTF8);
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Nova Senha.";
                mail.IsBodyHtml = true;
                mail.Body = corpoEmail;
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(EMAIL, PASSWORD)
                };
                client.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                Services.LogErro.AddErro(e.Message);
                return false;
            }
        }

        public static bool BoasVindas(Usuario usr)
        {
            LogErro.AddErro(string.Format("Novo Cadastro para {0}", usr.Email));
            if (usr.Email == null)
            {
                return false;
            }
            try
            {

                var corpoEmail = System.IO.File.ReadAllText("docHtml/emailBoasVindas.html");
                corpoEmail = corpoEmail.Replace("[usuario]", usr.LoginName);
                corpoEmail = corpoEmail.Replace("[Email]", usr.Email);
                corpoEmail = corpoEmail.Replace("[Senha]", usr.LoginPass);
                MailAddress from = new(EMAIL, "SyncOS!", Encoding.UTF8);
                MailAddress to = new(usr.Email, usr.LoginName, Encoding.UTF8);
                MailMessage mail = new(from, to);
                mail.Subject = "SyncOS - Seja Bem Vindo.";
                mail.IsBodyHtml = true;
                mail.Body = corpoEmail;
                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential(EMAIL, PASSWORD);
                    client.Send(mail);
                }

                return true;
            }
            catch (Exception e)
            {
                Services.LogErro.AddErro(e.Message);
                return false;
            }
        }


        public static bool ValidaEmail(string email)
        {
            var rg = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+).(\.[a-z]{2,3})$");
            return (!string.IsNullOrEmpty(email)) && (rg.IsMatch(email));
        }



        public static bool EnviarEmail(string emailEmitente, string senhaEmitente, string smtpClient, int porta, bool isSSL, List<string> Destinatarios, string titulo, string body, ref string msgErro, List<string> diretorioAnexos = null, int timeOutEnvio = 0)
        {
            var mail = new MailMessage();

            try
            {
                var smtp = new SmtpClient
                {
                    Host = smtpClient,
                    EnableSsl = isSSL,
                    Port = porta,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailEmitente, senhaEmitente),
                    Timeout = timeOutEnvio == 0 ? 3000 : timeOutEnvio
                };

                mail.Sender = new MailAddress(emailEmitente);
                mail.From = new MailAddress(emailEmitente);
                foreach (var item in Destinatarios)
                {
                    mail.To.Add(new MailAddress(item));
                }

                mail.Subject = titulo;

                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                if (diretorioAnexos != null)
                {
                    foreach (var item in diretorioAnexos)
                    {
                        mail.Attachments.Add(new Attachment(item));
                    }
                }

                smtp.Send(mail);
                mail.Dispose();

                return true;
            }
            catch (Exception e)
            {
                Services.LogErro.AddErro(e.Message);
                return false;
            }
        }



    }
}
