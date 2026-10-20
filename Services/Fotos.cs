using System;
using System.IO;
using System.Drawing;


namespace Services
{
    public static class Fotos
    {
        private const string _URI = "http://187.45.181.115/";
        
        public static string SetFoto(string fotoBase64, int _empresaID)
        {
            try
            {
                LogErro.AddErro("Entrou SetFotos");
                var fileName = Guid.NewGuid().ToString("N");
                var path = "imagens/" + _empresaID.ToString().PadLeft(4,'0');

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }
                LogErro.AddErro("Criou Pasta ");
                string imgPath = Path.Combine(path, fileName + ".jpg");

                string base64 = fotoBase64.Substring(fotoBase64.IndexOf(',') + 1);
                
                byte[] bytes = Convert.FromBase64String(base64);
                using (System.IO.FileStream fs = System.IO.File.Create(imgPath))
                {
                    foreach (byte item in bytes)
                    {
                        fs.WriteByte(item);
                    }
                }
                LogErro.AddErro("Salvou arquivo ");
                return string.Format("{0}/{1}",_URI,imgPath);
            }
            catch (Exception ex)
            {
                LogErro.AddErro(ex.InnerException.ToString());
                return ex.InnerException.ToString();
            }
        }

        public static string SetThumbnails(string imgPath)
        {
            const int size = 150;
            imgPath = imgPath.Replace(_URI, "");
            string imgPathThumbnails = imgPath.Replace(_URI, "").Replace(".jpg", "_thumbnails.jpg");
            
            using (var image = Image.FromFile(imgPath))
            {
                int width, height;
                if (image.Width > image.Height)
                {
                    width = 48;
                    height = Convert.ToInt32(image.Height * size / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * size / (double)image.Height);
                    height = size;
                }
                try
                {
                    Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
                    thumb.Save(imgPathThumbnails);
                   
                    return _URI + imgPathThumbnails;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public static string GetFoto(string fileName)
        {
            try
            {

                if (!string.IsNullOrEmpty(fileName))
                {
                    var fotoBase64 = string.Empty;
                    fileName = fileName.Replace(_URI, "./");
                    byte[] imageArray = System.IO.File.ReadAllBytes(fileName);
                    fotoBase64 = Convert.ToBase64String(imageArray);
                    return string.Format("data:image/jpeg;base64,{0}", fotoBase64);
                }
                else
                {
                    return string.Empty;
                }
                
            }
            catch (Exception ex)
            {
                LogErro.AddErro(ex.InnerException.ToString());
                return string.Empty;
            }
        }

        public static void DeleteFotos(string fileName)
        {
            try
            {
                fileName = fileName.Replace(_URI, "");
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
            }
            catch
            {

            }
        }
    }
}
