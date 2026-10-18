using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Newtonsoft.Json;
using SyncOSCore.Services;
using System;
using System.Linq;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class HomeAdminController : Controller
    {

        private readonly APPDbContext _ctx;
        private readonly INotyfService _notyf;
        private Usuario _usuarioLogado;

        private Usuario SetUserLogado()
        {
            try
            {
                var cookieValue = Request.Cookies["UsuarioLogado"];
                var userLogado = JsonConvert.DeserializeObject<Usuario>(cookieValue);
                ViewData["Perfil"] = userLogado.PerfilId;
                ViewData["NomeUsuario"] = userLogado.LoginName;
                return userLogado;
            }
            catch (Exception)
            {
                return new Usuario();
            }
        }

        public HomeAdminController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }


        [Authorize]
        public IActionResult Index()
        {

            _usuarioLogado = SetUserLogado();

            var dataInicial = DateTime.Now.AddMonths(-5);
            string[] meses = new string[6];
            int[] qtdMeses = new int[] { 0, 0, 0, 0, 0, 0 };
            int mesInicio = dataInicial.Month;
            for (int i = 0; i < 6; i++)
            {
                meses[i] = dataInicial.AddMonths(i).ToString("MMM/yyyy").ToUpper();
                qtdMeses[i] = _ctx.Chamados
                 .Count(d => (d.DataAbertura.Year == dataInicial.AddMonths(i).Year) && (d.DataAbertura.Month == dataInicial.AddMonths(i).Month));
            }
            ViewBag.Titulos = meses;
            ViewBag.Valores = qtdMeses;




            return View();
        }

        [HttpPost]
        public IActionResult SenhaDia()
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId < 1 && _usuarioLogado.PerfilId > 2)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var senhaDia = string.Empty;
            try
            {
                using (System.Security.Cryptography.MD5CryptoServiceProvider sKey = new())
                {
                    var ano = DateTime.Now.Year;
                    var mes = DateTime.Now.Month;
                    var dia = DateTime.Now.Day;
                    var diaSemana = ((int)DateTime.Now.DayOfWeek) + 1;
                    var senhaTmp = ((diaSemana + dia + mes + ano) * mes).ToString();
                    senhaTmp = MD5SenhaDia.Get(senhaTmp + "{CHAVE PARA 30 DIAS}").ToUpper();
                    senhaDia = senhaTmp.Substring(3, 1);
                    senhaDia = senhaDia + senhaTmp.Substring(7, 1);
                    senhaDia = senhaDia + senhaTmp.Substring(11, 1);
                    senhaDia = senhaDia + senhaTmp.Substring(15, 1);
                    senhaDia = senhaDia + senhaTmp.Substring(19, 1);
                    senhaDia = senhaDia + senhaTmp.Substring(23, 1);
                }
                _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Senha do dia: {0} gerada com sucesso.</p>", senhaDia), 5);
                return Json(new { success = senhaDia });
            }
            catch (Exception e)
            {
                return Json(new { success = e.Message });
            }
        }

        [HttpPost]
        public IActionResult SenhaDiaXML()
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            try
            {
                var ano = DateTime.Now.Year;
                var mes = DateTime.Now.Month;
                var dia = DateTime.Now.Day;
                var diaSemana = ((int)DateTime.Now.DayOfWeek) + 1;
                var senhaTmp = ((diaSemana + dia + mes + ano) * mes).ToString();
                senhaTmp = MD5SenhaDia.Get(senhaTmp + "{SyncTech Info}").ToUpper();
                var senhaDia = senhaTmp.Substring(29, 1);
                senhaDia = senhaDia + senhaTmp.Substring(16, 1);
                senhaDia = senhaDia + senhaTmp.Substring(7, 1);
                senhaDia = senhaDia + senhaTmp.Substring(20, 1);
                senhaDia = senhaDia + senhaTmp.Substring(24, 1);
                senhaDia = senhaDia + senhaTmp.Substring(31, 1);
                _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Senha do dia: {0} gerada com sucesso.</p>", senhaDia), 5);
                return Json(new { success = senhaDia });
            }
            catch (Exception e)
            {
                return Json(new { success = e.Message });
            }
        }
    }

}
