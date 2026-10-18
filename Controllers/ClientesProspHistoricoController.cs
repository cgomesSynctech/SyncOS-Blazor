using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class ClientesProspHistoricoController : Controller
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
        public ClientesProspHistoricoController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }
        public IActionResult Desisteancia(int id)
        {
            _usuarioLogado = SetUserLogado();

            try
            {
                var razao = _ctx.ClienteProsp
                    .Where(c => c.Id == id)
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Razao })
                    .FirstOrDefault();

                ViewData["Prosp"] = razao;


                var historico = new ClienteProspHistorico
                {
                    Data = DateTime.Now,
                    UsuarioId = _usuarioLogado.Id,
                    ClienteProspId = id
                };
                //ViewData["ClienteId"] = clienteId;
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
        public async Task<IActionResult> Desisteancia([Bind("Id,ClienteProspId,UsuarioId,Data,Historico,Arquivo")] ClienteProspHistorico hist)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                var prosp = await _ctx.ClienteProsp.FindAsync(hist.ClienteProspId);
                if (prosp != null)
                {
                    prosp.SetorId = 101;
                    prosp.StatusId = 6;
                    prosp.Flag = 2;
                }
                if (ModelState.IsValid)
                {                   
                    _ctx.Update(prosp);
                    _ctx.Add(hist);
                    await _ctx.SaveChangesAsync();
                    _notyf.Information("<h3>Aviso</h3><br/><p>Novo historico cadastrado com sucesso.</p>", 5);
                }
                return RedirectToAction("index", new { pagina = 0, clienteId = hist.ClienteProspId });
            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo historico</p>", 5);
                return RedirectToAction("index", "Clientes");
            }
        }

        public IActionResult Create(int clienteId)
        {
            _usuarioLogado = SetUserLogado();

            try
            {
                var razao = _ctx.ClienteProsp
                    .Where(c => c.Id == clienteId)
                    .Select(c => new SelectListItem { Value = c.Razao })
                    .FirstOrDefault();
                
                ViewData["Prosp"] = razao.Value;


                var historico = new ClienteProspHistorico
                {
                    Data = DateTime.Now,
                    UsuarioId = _usuarioLogado.Id,
                    ClienteProspId = clienteId
                };
                //ViewData["ClienteId"] = clienteId;
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
        public async Task<IActionResult> Create([Bind("Id,ClienteProspId,UsuarioId,Data,Historico,Arquivo")] ClienteProspHistorico hist)
        {
            try
            {
                _usuarioLogado = SetUserLogado();
                if (ModelState.IsValid)
                {
                    _ctx.Add(hist);
                    await _ctx.SaveChangesAsync();
                    _notyf.Information("<h3>Aviso</h3><br/><p>Novo historico cadastrado com sucesso.</p>", 5);
                }
                return RedirectToAction("index", new { pagina = 0, clienteId = hist.ClienteProspId });
            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo historico</p>", 5);
                return RedirectToAction("index", "Clientes");
            }
        }

        public async Task<IActionResult> Index(int pagina, int clienteId)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            
            var cli = await _ctx.ClienteProsp.FindAsync(clienteId);
            //ViewBag["modelo"] = cli;
            ViewData["NomeCliente"] = cli.Razao;
            ViewData["ClienteId"] = clienteId;
            ViewData["Perfil"] = _usuarioLogado.PerfilId;
            IOrderedQueryable<ClienteProspHistorico> historicos =
                _ctx.ClienteProspHistorico
                    .Include(c => c.ClienteProsp)
                    .Include(c => c.Usuario)
                    .Where(w => w.ClienteProspId == clienteId)
                    .OrderByDescending(c => c.Id);
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await historicos.ToPagedListAsync(numeroPagina, 20));
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
        

    }
}
