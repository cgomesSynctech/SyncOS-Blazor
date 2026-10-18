using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using info;
using AspNetCoreHero.ToastNotification.Abstractions;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Principal;
using Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting.Internal;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class AtualizacoesController : Controller
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


        public AtualizacoesController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;

        }


        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                if (_usuarioLogado.PerfilId != 1)
                {
                    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                    return RedirectToAction("Login", "Home");
                }
                var filePath = Path.Combine(@"Pacotes", file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var json = Json(new { success = file.FileName });
                return json;
            }
            catch (Exception e)
            {
                return Json(new { success = e.Message });
            }
        }
        public IActionResult UploadHistocioProsp(IFormFile file)
        {
            try
            {
                //_usuarioLogado = SetUserLogado();
                //if (_usuarioLogado.PerfilId != 1)
                //{
                //    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                //    return RedirectToAction("Login", "Home");
                //}
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + fileExtension;

                var filePath = Path.Combine(@"wwwroot/HistoricoProsp", newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var json = Json(new { success = newFileName });
                return json;
            }
            catch (Exception e)
            {
                return Json(new { success = e.Message });
            }
        }
        public IActionResult UploadHistorico(IFormFile file)
        {
            try
            {
                
                _usuarioLogado = SetUserLogado();
                //if (_usuarioLogado.PerfilId != 1)
                //{
                //    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                //    return RedirectToAction("Login", "Home");
                //}
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + fileExtension;

                var filePath = Path.Combine(@"wwwroot/Historico", newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                var json = Json(new { success = newFileName });
                return json;
            }
            catch (Exception e)
            {
                return Json(new { success = e.Message });
            }
        }
        [HttpGet()]
        public async Task<IActionResult> Index(int pagina,  int tipoApp)
        {
            _usuarioLogado = SetUserLogado();

            //if (_usuarioLogado.PerfilId != 1)
            //{
            //    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
            //    return RedirectToAction("Login", "Home");
            //}
            var nome = User.Identity.Name;
            var token = User.Identity.AuthenticationType;
            IOrderedQueryable<Atualizacao> atualizacoes;
            if (tipoApp == 999)
            {
                atualizacoes = _ctx.Atualizacoes
               .Include(i => i.TiposAPP)
               .OrderByDescending(o => o.Id);
            }
            else
            {
                atualizacoes = _ctx.Atualizacoes
              .Include(i => i.TiposAPP)
              .Where(w => w.TiposAPP.Id == tipoApp )
              .OrderByDescending(o => o.Id);
            }

            ViewData["tiposAPPId"] = new SelectList(_ctx.TiposAPP, "Id", "Descricao", tipoApp);
            ViewData["tipoApp"] = tipoApp;
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await atualizacoes.ToPagedListAsync(numeroPagina, 20));



        }


        
        public async Task<IActionResult> Lista(int pagina, int tipoApp)
        {
            _usuarioLogado = SetUserLogado();

            //if (_usuarioLogado.PerfilId != 1)
            //{
            //    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
            //    return RedirectToAction("Login", "Home");
            //}
            var nome = User.Identity.Name;
            var token = User.Identity.AuthenticationType;
            IOrderedQueryable<Atualizacao> atualizacoes;
            if (tipoApp == 100)
            {
                atualizacoes = _ctx.Atualizacoes
               .Include(i => i.TiposAPP)
               .OrderByDescending(o => o.Id);
            }
            else
            {
                atualizacoes = _ctx.Atualizacoes
              .Include(i => i.TiposAPP)
              .Where(w => w.TiposAPP.Id == tipoApp)
              .OrderByDescending(o => o.Id);
            }

            ViewData["tiposAPPId"] = new SelectList(_ctx.TiposAPP, "Id", "Descricao", tipoApp);
            ViewData["tipoApp"] = tipoApp;
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await atualizacoes.ToPagedListAsync(numeroPagina, 20));



        }

        // GET: Atualizacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId != 1)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null || _ctx.Atualizacoes == null)
            {
                return NotFound();
            }

            var atualizacao = await _ctx.Atualizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atualizacao == null)
            {
                return NotFound();
            }

            return View(atualizacao);
        }


        public IActionResult Create()
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId != 1)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var atu = new Atualizacao
            {
                DataVersao = DateTime.Now,
                CNPJ = "99999999999999",
                Backup = "S",
                Ativo = "S"
            };
            ViewData["TipoAtualizacaoId"] = new SelectList(_ctx.TiposAPP, "Id", "Descricao");
            return View(atu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataVersao,Versao,CNPJ,LeiaMe,Script,Pacote,PathDescompactar,TipoAtualizacaoId,Backup,Ativo")] Atualizacao atualizacao)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                if (_usuarioLogado.PerfilId != 1)
                {
                    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                    return RedirectToAction("Login", "Home");
                }
                if (ModelState.IsValid)
                {
                    //atualizacao.Pacote = fileNameUpload;
                    atualizacao.Id = await NextID.GetNextId("Atualizacao", _ctx);
                    _ctx.Add(atualizacao);
                    await _ctx.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["TipoAtualizacaoId"] = new SelectList(_ctx.TiposAPP, "Id", "Descricao", atualizacao.TipoAtualizacaoId);
                return View(atualizacao);
            }
            catch (Exception ex) 
            {
                return Json(new { success = ex.Message });
            }

        }


        public async Task<IActionResult> Edit(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId != 1)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null || _ctx.Atualizacoes == null)
            {
                return NotFound();
            }

            var atualizacao = await _ctx.Atualizacoes.FindAsync(id);
            if (atualizacao == null)
            {
                return NotFound();
            }
            ViewData["TipoAtualizacaoId"] = new SelectList(_ctx.TiposAPP, "Id", "Descricao", atualizacao.TipoAtualizacaoId);
            return View(atualizacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataVersao,Versao,CNPJ,LeiaMe,Script,Pacote,PathDescompactar,TipoAtualizacaoId,Backup,Ativo")] Atualizacao atualizacao)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId != 1)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id != atualizacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ctx.Update(atualizacao);
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtualizacaoExists(atualizacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["TipoAtualizacaoId"] = new SelectList(_ctx.TiposAPP, "Id", "Descricao", atualizacao.TipoAtualizacaoId);
                return RedirectToAction(nameof(Index));
            }
            return View(atualizacao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId != 1)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id == null || _ctx.Atualizacoes == null)
            {
                return NotFound();
            }

            var atualizacao = await _ctx.Atualizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atualizacao == null)
            {
                return NotFound();
            }
            ViewData["TipoAtualizacaoId"] = new SelectList(_ctx.TiposAPP, "Id", "Descricao", atualizacao.TipoAtualizacaoId);
            return View(atualizacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId != 1)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (_ctx.Atualizacoes == null)
            {
                return Problem("Entity set 'APPDbContext.Atualizacoes'  is null.");
            }
            var atualizacao = await _ctx.Atualizacoes.FindAsync(id);
            if (atualizacao != null)
            {
                _ctx.Atualizacoes.Remove(atualizacao);
            }
            
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtualizacaoExists(int id)
        {
          return _ctx.Atualizacoes.Any(e => e.Id == id);
        }
    }
}
