using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public static class Logger
    {
        public static void Erro(string conteudo)
        {
            try
            {
                var pathFile = string.Format(@".\Log\Log_Erro_{0}.log", DateTime.Now.ToString("yyyyMMdd"));

                try
                {
                    var file = new FileInfo(pathFile);
                    if (file.Length > 512000)
                    {
                        file.MoveTo(string.Format(@".\Log\Log_{0}.log", DateTime.Now.ToString("ddMMyyyy_HHmmss")));
                    }
                }
                catch (FileNotFoundException)
                {
                }
                catch (Exception)
                {
                    // ignored
                }

                if (!System.IO.File.Exists(pathFile))
                {
                    System.IO.File.Create(pathFile).Close();
                }

                using (var sw = new System.IO.StreamWriter(pathFile, true))
                {
                    sw.WriteLine(string.Format("{0}\n{3} Log => Data: {1}\n{2}\n", "".PadLeft(100, '-'), DateTime.Now, conteudo, Environment.MachineName));
                }
            }
            catch (Exception)
            {
            }

        }
    }
}
