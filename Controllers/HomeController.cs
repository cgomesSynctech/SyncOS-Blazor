using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Services;
using System.Linq;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly APPDbContext _context;
        private readonly INotyfService _notyf;
        private Usuario _usuarioLogado;

        private void CreateCookie(Usuario usr)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true,
                HttpOnly = false
            };
            Response.Cookies.Append("UsuarioLogado", JsonConvert.SerializeObject(usr), options);
        }


        public HomeController(APPDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }


        // GET: HomeController
        public ActionResult Index(string msg)
        {
            _usuarioLogado = new Usuario();
            return RedirectToAction("Login", "Home");
        }



        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword([Bind("Email,PassWord")] Login login)
        {

            //var usuarios = new List<Usuario>();
            //usuarios.Add(new Usuario() { Id = 01, LoginName = "Admin", LoginPass = "123", Email = "marcos@synctech.com.br", PerfilId = 1 });
            //usuarios.Add(new Usuario() { Id = 02, LoginName = "Marcos", LoginPass = "123", Email = "marcosantunes@gmail.com", PerfilId = 1 });
            //usuarios.Add(new Usuario() { Id = 03, LoginName = "Jorge", LoginPass = "123", Email = "jorge@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 04, LoginName = "Alan", LoginPass = "123", Email = "alan@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 05, LoginName = "Alisson", LoginPass = "123", Email = "alisson@synctech.com.br", PerfilId = 1 });
            //usuarios.Add(new Usuario() { Id = 06, LoginName = "Leonardo", LoginPass = "123", Email = "leonardo@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 07, LoginName = "PedroJunior", LoginPass = "123", Email = "pedrojunior@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 08, LoginName = "Chiquinho", LoginPass = "123", Email = "chiquinho@synctech.com.br", PerfilId = 1 });
            //usuarios.Add(new Usuario() { Id = 09, LoginName = "Didi", LoginPass = "123", Email = "didi@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 10, LoginName = "Suan", LoginPass = "123", Email = "suan@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 11, LoginName = "Cesar", LoginPass = "123", Email = "cesar@synctech.com.br", PerfilId = 1 });
            //usuarios.Add(new Usuario() { Id = 12, LoginName = "Secretaria", LoginPass = "123", Email = "secretaria@synctech.com.br", PerfilId = 1 });
            //usuarios.Add(new Usuario() { Id = 13, LoginName = "Joanderson", LoginPass = "123", Email = "joanderson@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 14, LoginName = "Jeferson", LoginPass = "123", Email = "jeferson@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 15, LoginName = "leonardoalves", LoginPass = "123", Email = "leonardoalves@synctech.com.br", PerfilId = 3 });
            //usuarios.Add(new Usuario() { Id = 16, LoginName = "Bianca", LoginPass = "123", Email = "bianca@synctech.com.br", PerfilId = 2 });
            //usuarios.Add(new Usuario() { Id = 17, LoginName = "Ivalmir", LoginPass = "123", Email = "ivalmir@synctech.com.br", PerfilId = 3 });

            var user = _context.Usuarios.FirstOrDefault(u => u.Email == login.Email);
            if (user != null)
            {
                var novaSenha = GerarSenha.SetSenha(6);
                user.LoginPass = novaSenha;
                user.LoginPass = ToPassWord.Get(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var emailEnviado = Email.NovaSenha(user, novaSenha);
                if (emailEnviado)
                {
                    var msg = string.Format("<p>Email enviado com sucesso!<Br><b><i> <p>Acesse seu email {0} e verifique sua nova senha.</p> </i></b></p><br>", login.Email);
                    _notyf.Information(msg, 5);
                }
                else
                {
                    var msg = "<p>Erro ao Enviar o Email!<Br><b><i> <p>Verifique o Log do Sistema.</p> </i></b></p><br>";
                    _notyf.Error(msg, 5);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var msg = string.Format("<p>O E-Mail {0}!<Br><b><i> <p>Não existe em nossa base de dados.</p> </i></b></p><br>", login.Email);
                _notyf.Error(msg, 5);
                return View();
            }
        }

        private object List<T>()
        {
            throw new NotImplementedException();
        }

        // GET: HomeController/Details/5
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,PassWord")] Login login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.PassWord))
                {
                    _notyf.Warning("<p>O E-Mail ou a Senha, não informado!<Br><b><i> <p>Favor informar um email e senha válidos!!!.</p> </i></b></p><br>", 5);
                    return RedirectToAction("Login", "Home");
                }

                var usuario = new Usuario()
                {
                    Email = login.Email,
                    LoginPass = login.PassWord
                };

                _usuarioLogado = new Usuario();

                var senha = ToPassWord.Get(usuario);
                var user = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == login.Email && u.LoginPass == senha);
                var perfil = await _context.Perfis.FindAsync(user.PerfilId);
                var msg = "";
                if (user == null)
                {
                    msg = string.Format("<p>O E-Mail ou a Senha, inválido!<Br><b><i> <p>Favor informar um usuário válido!!!.</p> </i></b></p><br>");
                    _notyf.Error(msg, 5);
                    return RedirectToAction("Login", "Home");
                }
                user.Role = perfil.Descricao;
                user.Token = TokenService.GenereteToken(user);
                _usuarioLogado = user;

                CreateCookie(user);

                ViewData["Perfil"] = _usuarioLogado.PerfilId;
                return RedirectToAction("Index", "HomeAdmin");
            }
            catch (Exception)
            {
                _notyf.Error("<p>O E-Mail ou a Senha, inválido!<Br><b><i> <p>Favor informar um usuário válido!!!.</p> </i></b></p><br>", 5);
                return RedirectToAction("Login", "Home");
            }
        }




        // GET: HomeController/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
