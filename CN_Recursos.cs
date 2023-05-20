using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using System.Net.Mail;
using System.Net;
using System.IO;

namespace CapaNegocio
{
    public class CN_Recursos
    {

        //Auto generacion de clave con 16 digitos alfanumericos
        public static string GenerarClave()
        {
            string pass = Guid.NewGuid().ToString("N").Substring(0, 16);
            return pass;
        }


        //Encriptacion text to SHA256

        public static string ConvertToSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            //Usando la reference "System.Security.Cryptography"
            using(SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }


        public static bool SendMail(string correo, string asunto, string mensaje)
        {

            bool result = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("carloscerquinoropeza@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("carloscerquinoropeza@gmail.com", "ofriebxgcjxgwjdf"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail);
                result = true;

            }
            catch(Exception ex)
            {
            
                result = true;
            }

            return result;
        }


        //Convert strin to Base64

        public static string ConvertBase64(string rute, out bool conversion)
        {
            string textBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(rute);
                textBase64 = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }
            return textBase64;
        }

    }
}
