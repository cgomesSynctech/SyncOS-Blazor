using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Modelos;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public static class ToPassWord
    {
        public static string Get(Usuario u)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                IConfiguration configuration = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appSettings.json")
                                   .Build();

                var secret = string.Format("{0}{1}{2}", ChaveSecret.Get(), u.Email.ToLower(), u.LoginPass);
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(secret));

                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

    }
}
