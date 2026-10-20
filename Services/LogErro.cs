using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class LogErro
    {
 
        public static void AddErro(string conteudo)
        {
            try
            {
                //var dir = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

                //var pathLog = string.Format(@"{0}\log\", dir);
                var pathLog = @".\log\";
                if (!System.IO.Directory.Exists(pathLog))
                {
                    System.IO.Directory.CreateDirectory(pathLog);
                }

                var pathFile = string.Format("{0}Log_{1}.log", pathLog, DateTime.Now.ToString("yyyyMMdd"));

                try
                {
                    var file = new FileInfo(pathFile);
                    if (file.Length > 512000)
                    {
                        file.MoveTo(string.Format("{0}Log_{1}.log", pathLog, DateTime.Now.ToString("ddMMyyyy_HHmmss")));
                    }
                }
                catch (FileNotFoundException)
                {
                }
                catch (Exception)
                {
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



