using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class DesenvCardHistoricoController : Controller
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
        public DesenvCardHistoricoController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index(int pagina, int desenvcardId)
        {
            _usuarioLogado = SetUserLogado();
            //if (_usuarioLogado.PerfilId == 0)
            //{
            //    _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
            //    return RedirectToAction("Login", "Home");
            //}
            //var cli = await _ctx.DesenvCardHistorico.FindAsync(desenvcardId);
            var card = await _ctx.DesenvCard.FindAsync(desenvcardId);
            //ViewBag["modelo"] = cli;
            ViewData["RotuloCard"] = card.Rotulo;
            ViewData["DesenvcardId"] = desenvcardId;
            ViewData["Perfil"] = _usuarioLogado.PerfilId;
            IOrderedQueryable<DesenvCardHistorico> historicos =
                _ctx.DesenvCardHistorico
                    .Include(c => c.DesenvCardStatus)
                    .Include(c => c.Usuario)
                    .Include(c => c.DesenvCard)
                    .Where(w => w.DesenvCardId == desenvcardId)
                    .OrderByDescending(c => c.Id);
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await historicos.ToPagedListAsync(numeroPagina, 20));
        }

        public IActionResult Create(int desenvcardId)
        {
            _usuarioLogado = SetUserLogado();

            try
            {
                var card = _ctx.DesenvCard
                    .Where(c => c.Id == desenvcardId)
                    .FirstOrDefault();               


                var historico = new DesenvCardHistorico
                {
                    Data = DateTime.Now,
                    UsuarioId = _usuarioLogado.Id,
                    DesenvCardId = desenvcardId,
                    DesenvCardStatusId = 1, // Status inicial                   
                    
                };
                return View(historico);
            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo cliente</p>", 5);
                return RedirectToAction("index", "Clientes");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte,Visitante")]
        public async Task<IActionResult> Create([Bind("Id,DesenvCardId,DesenvCardStatusId,UsuarioId,Descricao,Detalhes,Observacao,LinkBase,Arquivo,Data")] DesenvCardHistorico hist)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                if (ModelState.IsValid)
                {
                    hist.DesenvCardStatusId = 1;
                    _ctx.Add(hist);
                    await _ctx.SaveChangesAsync();
                    _notyf.Information("<h3>Aviso</h3><br/><p>Novo historico cadastrado com sucesso.</p>", 5);
                }
                return RedirectToAction("index", new { pagina = 0, desenvcardId = hist.DesenvCardId });
            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo historico</p>", 5);
                return RedirectToAction("index", "DesenvCardHistorico");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var historico = await _ctx.DesenvCardHistorico
                .Include(c => c.DesenvCardStatus)
                .Include(c => c.Usuario)
                .Include(c => c.DesenvCard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historico == null)
            {
                return NotFound();
            }
            ViewData["DesenvCardId"] = new SelectList(_ctx.DesenvCard, "Id", "Titulo", historico.DesenvCardId);
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.DesenvCardStatus, "Id", "Descricao", historico.DesenvCardStatusId);
            return View(historico);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DesenvCardId,DesenvCardStatusId,UsuarioId,Descricao,Detalhes,Observacao,LinkBase,Arquivo,Data")] DesenvCardHistorico historico)
        {
            if (id != historico.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _ctx.Update(historico);
                    await _ctx.SaveChangesAsync();
                    _notyf.Information("<h3>Aviso</h3><br/><p>Historico atualizado com sucesso.</p>", 5);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index), new { pagina = 0, desenvcardId = historico.DesenvCardId });
            }
            ViewData["DesenvCardId"] = new SelectList(_ctx.DesenvCard, "Id", "Titulo", historico.DesenvCardId);
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.DesenvCardStatus, "Id", "Descricao", historico.DesenvCardStatusId);
            return View(historico);
        }

        public async Task<IActionResult> RespostaDesenv(int id)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var historico = await _ctx.DesenvCardHistorico
                .Include(c => c.DesenvCardStatus)
                .Include(c => c.Usuario)
                .Include(c => c.DesenvCard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historico == null)
            {
                return NotFound();
            }
            ViewData["DesenvCardId"] = new SelectList(_ctx.DesenvCard, "Id", "Titulo", historico.DesenvCardId);
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.DesenvCardStatus, "Id", "Descricao", historico.DesenvCardStatusId);
            return View(historico);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RespostaDesenv(int id, [Bind("Id,DesenvCardId,Arquivosolucao,Solucao,Resolucao,VersaoSuapp,DesenvCardStatusId,UsuarioRespostaId")] DesenvCardHistorico historico)
        {
            if (id != historico.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var devcarhist = await _ctx.DesenvCardHistorico
                        .Include(c => c.DesenvCardStatus)
                        .Include(c => c.Usuario)
                        .Include(c => c.DesenvCard)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (devcarhist == null)
                    {
                        _notyf.Warning("<h3>Aviso</h3><br/><p>O histórico não foi encontrado.</p>", 5);
                        return NotFound();
                    }
                    _usuarioLogado = SetUserLogado();
                    // Atualiza os campos necessários
                    devcarhist.Arquivosolucao = historico.Arquivosolucao;
                    devcarhist.Solucao = historico.Solucao;
                    devcarhist.Resolucao = historico.Resolucao;
                    devcarhist.VersaoSuapp = historico.VersaoSuapp;
                    devcarhist.DesenvCardStatusId = historico.DesenvCardStatusId;
                    devcarhist.UsuarioResposta = _usuarioLogado.LoginName; // Define o usuário logado
                    // Define o usuário logado e atualiza o status do card
                    _ctx.Entry(devcarhist).State = EntityState.Modified;
                    _ctx.Update(devcarhist);
                    await _ctx.SaveChangesAsync();
                    _notyf.Information("<h3>Aviso</h3><br/><p>Resposta do desenvolvedor registrada com sucesso.</p>", 5);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index), new { pagina = 0, desenvcardId = historico.DesenvCardId });
            }
            ViewData["DesenvCardId"] = new SelectList(_ctx.DesenvCard, "Id", "Titulo", historico.DesenvCardId);
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.DesenvCardStatus, "Id", "Descricao", historico.DesenvCardStatusId);
            return View(historico);
        }

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
            var desenvCard = await _ctx.DesenvCardHistorico
                .Include(c => c.Usuario)
                .Include(c => c.DesenvCardStatus)
                .Include(c => c.DesenvCard)                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desenvCard == null)
            {
                return NotFound();
            }
            return View(desenvCard);
        }
    }
}
