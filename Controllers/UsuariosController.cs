
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using info;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using X.PagedList;
using Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class UsuariosController : Controller
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


        public UsuariosController(APPDbContext context, INotyfService notyf)
        {
            _notyf = notyf;
            _ctx = context;

        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AlterarSenha()
        {
            _usuarioLogado = SetUserLogado();   
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var usuario = await _ctx.Usuarios.FindAsync(_usuarioLogado.Id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["PerfilId"] = new SelectList(_ctx.Perfis.Where(w => w.Id == _usuarioLogado.PerfilId), "Id", "Descricao", usuario.PerfilId);
            return View(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha(int id, [Bind("Id,LoginName,LoginPass,ConfirmePass,PerfilId,Email,Ativo")] Usuario usuario)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            
            if (!usuario.ConfirmePass.Equals(usuario.LoginPass))
            {
                _notyf.Warning("<p>Erro<Br><b><i> <p>Senhas não conferem</p> </i></b></p><br>", 5);
                return View();
            }

            usuario.LoginPass = ToPassWord.Get(usuario);
            _ctx.Entry(usuario).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            _notyf.Information("<p>Sucesso<Br><b><i> <p>Senha Modificado com sucesso!</p> </i></b></p><br>", 5);
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(int pagina)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var usr = _ctx.Usuarios;

            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await usr.ToPagedListAsync(numeroPagina, 20));
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _ctx.Usuarios

                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var user = new Usuario
            {
                LoginName = "",
                LoginPass = "123",
                ConfirmePass = "123",
                Role = "",

            };
            ViewData["PerfilId"] = new SelectList(_ctx.Perfis, "Id", "Descricao");
            return View(user);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoginName,LoginPass,PerfilId,Email,Ativo,Role")] Usuario usuario)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var perfil = _ctx.Perfis.Find(usuario.PerfilId);
            //usuario.Id = await NextID.GetNextId("Usuario", _ctx);
            usuario.Ativo = true;
            usuario.Role = perfil.Descricao;
            usuario.Token = TokenService.GenereteToken(usuario);
            usuario.LoginPass = "123";
            usuario.ConfirmePass= "123";    
            if (ModelState.IsValid)
            {
                usuario.LoginPass = ToPassWord.Get(usuario);
                usuario.Role = perfil.Descricao;
                _ctx.Add(usuario);
                await _ctx.SaveChangesAsync();

                var emailEnviado = Email.BoasVindas(usuario);
                if (emailEnviado)
                {
                    var msg = "<p>Email enviado com sucesso!<Br><b><i> <p>Acesse seu email e verifique sua nova senha.</p> </i></b></p><br>";
                    _notyf.Information(msg, 5);
                }
                else
                {
                    var msg = "<p>Erro ao Enviar o Email!<Br><b><i> <p>Verifique o Log do Sistema.</p> </i></b></p><br>";
                    _notyf.Error(msg, 5);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var msg = "<p>Erro<Br><b><i> <p>Erro ao tentar criar um usuário, verifique o Log do Sistema.</p> </i></b></p><br>";
                _notyf.Error(msg, 5);
                ViewData["PerfilId"] = new SelectList(_ctx.Perfis, "Id", "Descricao", usuario.PerfilId);
                return View(usuario);
            }

        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _ctx.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["PerfilId"] = new SelectList(_ctx.Perfis, "Id", "Descricao", usuario.PerfilId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoginName,LoginPass,PerfilId,Email,Ativo")] Usuario usuario)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id != usuario.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _ctx.Update(usuario);
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PerfilId"] = new SelectList(_ctx.Perfis, "Id", "Descricao", usuario.PerfilId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var usuario = await _ctx.Usuarios.FindAsync(id);
            _ctx.Usuarios.Remove(usuario);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _ctx.Usuarios.Any(e => e.Id == id);
        }
    }
}
