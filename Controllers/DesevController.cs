using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Newtonsoft.Json;
using SyncOSCore.Migrations;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class DesevController : Controller
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
        public DesevController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }

        public IActionResult UploadArquivo(IFormFile file)
        {
            try
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + fileExtension;

                var filePath = Path.Combine(@"wwwroot/DesenvCard", newFileName);
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
        public async Task<IActionResult> Index(int pagina, int status, int modulo)
        {
            _usuarioLogado = SetUserLogado();

            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            IOrderedQueryable<DesenvCard> desnvCards;
            (int, int) teste = (modulo, status);
            switch (teste)
            {
                case (100, 100):
                    desnvCards = _ctx.DesenvCard
                        .Include(c => c.Usuario)
                        .Include(c => c.DesenvCardStatus)
                        .Include(c => c.ChamadoModulo)
                        .OrderByDescending(o => o.DesenvCardStatus);                        
                    //ViewData["Modulo"] = 100;
                    //ViewData["Status"] = 100;
                    
                    break;
                case (>0,100):
                    desnvCards = _ctx.DesenvCard
                        .Include(c => c.Usuario)
                        .Include(c => c.DesenvCardStatus)
                        .Include(c => c.ChamadoModulo)
                        .Where(w => w.ChamadoModuloId == modulo)
                        .OrderByDescending(o => o.DesenvCardStatus);
                    //ViewData["Modulo"] = 100;
                    //ViewData["Status"] = status;
                    break;
                case (100, > 0):
                    desnvCards = _ctx.DesenvCard
                        .Include(c => c.Usuario)
                        .Include(c => c.DesenvCardStatus)
                        .Include(c => c.ChamadoModulo)
                        .Where(w => w.DesenvCardStatusId == status)
                        .OrderByDescending(o => o.DesenvCardStatus);
                    //ViewData["Modulo"] = modulo;
                    //ViewData["Status"] = 100;
                    break;
                case (> 0, > 0):
                    desnvCards = _ctx.DesenvCard
                        .Include(c => c.Usuario)
                        .Include(c => c.DesenvCardStatus)
                        .Include(c => c.ChamadoModulo)
                        .Where(w => w.DesenvCardStatusId == status && w.ChamadoModuloId == modulo)
                        .OrderByDescending(o => o.DesenvCardStatus);
                    //ViewData["Modulo"] = modulo;
                    //ViewData["Status"] = status;
                    break;

                default:
                    desnvCards = _ctx.DesenvCard
                        .Include(c => c.Usuario)
                        .Include(c => c.DesenvCardStatus)
                        .Include(c => c.ChamadoModulo)
                        .OrderByDescending(o => o.DesenvCardStatus);
                    //ViewData["Modulo"] = modulo;
                    //ViewData["Status"] = status;
                    break;
            }
            
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.DesenvCardStatus, "Id", "Descricao", status);
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", modulo);
            ViewData["Status"] = status;
            ViewData["Modulo"] = modulo;
            
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await desnvCards.ToPagedListAsync(numeroPagina, 20));
        }

        public async Task<IActionResult> Create()
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            try
            {
                ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao");
                ViewData["Cliente"] = new SelectList(_ctx.Clientes, "Id", "Fantasia");

                var card = new DesenvCard
                {
                    ChamadoModuloId = 1, // Default value, can be changed as needed
                    DesenvCardStatusId = 1, // Default status
                    UsuarioId = _usuarioLogado.Id,
                    Data = DateTime.Now,
                    LinkBase = "https://suapp.com.br",
                    Rotulo = "Novo Card"
                };
                return View(card);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte,Visitante")]
        public async Task<IActionResult> Create([Bind("Id,ChamadoModuloId,DesenvCardStatusId,UsuarioId,Descricao,Detalhes,LinkBase,Arquivo,Rotulo,Observacao,ClienteId,ClienteNome")] DesenvCard desenvCard)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                desenvCard.UsuarioId = _usuarioLogado.Id;
                desenvCard.Data = DateTime.Now;
                desenvCard.DesenvCardStatusId = 1;
                if (desenvCard.ClienteId > 0)
                {
                    desenvCard.ClienteNome = _ctx.Clientes
                        .Where(c => c.Id == desenvCard.ClienteId)
                        .Select(c => c.Fantasia)
                        .FirstOrDefault();
                }
                else
                {
                    desenvCard.ClienteId = null;
                    desenvCard.ClienteNome = "Não Informado";
                }

                _ctx.Add(desenvCard);
                await _ctx.SaveChangesAsync();
                // Adiciona o histórico do card
                var historico = new DesenvCardHistorico
                {
                    DesenvCardId = desenvCard.Id,
                    DesenvCardStatusId = desenvCard.DesenvCardStatusId,
                    UsuarioId = desenvCard.UsuarioId,
                    Descricao = desenvCard.Descricao,
                    Detalhes = desenvCard.Detalhes,
                    LinkBase = desenvCard.LinkBase,
                    Arquivo = desenvCard.Arquivo,
                    Data = DateTime.Now
                };
                _ctx.DesenvCardHistorico.Add(historico);
                await _ctx.SaveChangesAsync();


                _notyf.Success("<h3>Sucesso</h3><br/><p>Registro salvo com sucesso!</p>", 5);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", desenvCard.ChamadoModuloId);
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", desenvCard.DesenvCardStatusId);
            return View(desenvCard);

        }
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
            var desenvCard = await _ctx.DesenvCard.FindAsync(id);
            if (desenvCard == null)
            {
                return NotFound();
            }
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", desenvCard.ChamadoModuloId);
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", desenvCard.DesenvCardStatusId);
            ViewData["Cliente"] = new SelectList(_ctx.Clientes, "Id", "Fantasia");
            return View(desenvCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChamadoModuloId,DesenvCardStatusId,UsuarioId,Descricao,Detalhes,LinkBase,Arquivo,Rotulo,Observacao,Solucao,Resolucao,VersaoSuapp,Arquivosolucao,Rotulo,ClienteId,ClienteNome")] DesenvCard desenvCard)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            if (id != desenvCard.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                   // desenvCard.UsuarioId = _usuarioLogado.Id;
                    desenvCard.Data = DateTime.Now;
                    if (desenvCard.ClienteId > 0)
                    {
                        desenvCard.ClienteNome = _ctx.Clientes
                            .Where(c => c.Id == desenvCard.ClienteId)
                            .Select(c => c.Fantasia)
                            .FirstOrDefault();
                    }
                    else
                    {
                        desenvCard.ClienteId = null;
                        desenvCard.ClienteNome = "Não Informado";
                    }
                    _ctx.Update(desenvCard);
                    await _ctx.SaveChangesAsync();
                    _notyf.Success("<h3>Sucesso</h3><br/><p>Registro atualizado com sucesso!</p>", 5);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesenvCardExists(desenvCard.Id))
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
            ViewData["ChamadoModuloId"] = new SelectList(_ctx.ChamadoModulo, "Id", "Descricao", desenvCard.ChamadoModuloId);
            ViewData["DesenvCardStatusId"] = new SelectList(_ctx.ChamadoStatus, "Id", "Descricao", desenvCard.DesenvCardStatusId);
            return View(desenvCard);
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
            var desenvCard = await _ctx.DesenvCard
                .Include(c => c.Usuario)
                .Include(c => c.DesenvCardStatus)
                .Include(c => c.ChamadoModulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desenvCard == null)
            {
                return NotFound();
            }
            return View(desenvCard);
        }
        public async Task<IActionResult> AlteraStatus(int desenvCardId)
        {
            _usuarioLogado = SetUserLogado();
            var card = await _ctx.DesenvCard.FindAsync(desenvCardId);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_ctx.DesenvCardStatus, "Id", "Descricao");
            return View(card);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlteraStatus(int id, [Bind("Id,DesenvCardStatusId")] DesenvCard desenvCard)
        {
            _usuarioLogado = SetUserLogado();
            if (id != desenvCard.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                var card = await _ctx.DesenvCard.FindAsync(id);
                var statusAntigo = await _ctx.DesenvCard.FindAsync(card.DesenvCardStatusId);
                var statusNovo = await _ctx.DesenvCardStatus.FindAsync(card.DesenvCardStatusId);
                card.DesenvCardStatusId = desenvCard.DesenvCardStatusId;               
                _ctx.Update(card);
                await _ctx.SaveChangesAsync();                

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesenvCardExists(desenvCard.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Card modificado com sucesso.</p>"), 5);
            //return View(cliente);
            return RedirectToAction(nameof(Index));
            //}

        }
        private bool DesenvCardExists(int id)
        {
            return _ctx.DesenvCard.Any(e => e.Id == id);
        }
    }
}
