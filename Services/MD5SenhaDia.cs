namespace SyncOSCore.Services
{
    public class MD5SenhaDia
    {
        public static string Get(string source)
        {
            using (System.Security.Cryptography.MD5CryptoServiceProvider sKey = new())
            {
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(source);
                bs = sKey.ComputeHash(bs);
                System.Text.StringBuilder sString = new System.Text.StringBuilder();
                foreach (byte b in bs)
                {
                    sString.Append(b.ToString("x2").ToLower());
                }

                return sString.ToString();
            }
        }
    }
}
