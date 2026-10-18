using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using X.PagedList;
using NuGet.Protocol;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Services;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace SyncOSCore.Controllers
{

    [AllowAnonymous]
    public class ClienteHistoricosController : Controller
    {
        private readonly APPDbContext _ctx;
        private readonly INotyfService _notyf;
        private Usuario _usuarioLogado;
        public ClienteHistoricosController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador,Financeiro,Suporte,Visitante")]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,UsuarioId,Data,Historico,Arquivo")] ClienteHistorico hist)
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
                return RedirectToAction("index", new { pagina = 0, ClienteId = hist.ClienteId });
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
                var historico = new ClienteHistorico
                {
                    Data = DateTime.Now,
                    UsuarioId = _usuarioLogado.Id,
                    ClienteId = clienteId
                };
                ViewData["ClienteId"] = clienteId;
                return View(historico);
            }
            catch (Exception)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Ocorreu um erro ao tentar cadatrar um novo cliente</p>", 5);
                return RedirectToAction("index", "Clientes");
            }

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

            var historico = await _ctx.ClienteHistoricos
                .Include(c => c.Cliente)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historico == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = historico.ClienteId;
            return View(historico);
        }



        public async Task<IActionResult> Index(int pagina,  int clienteId)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }
            var cli = await _ctx.Clientes.FindAsync(clienteId);
            ViewData["NomeCliente"] = cli.Razao;
            ViewData["ClienteId"] = clienteId;
            ViewData["Perfil"] = _usuarioLogado.PerfilId;
            if (cli.IdClienteProsp as int? == null)
            {
                ViewData["ClienteProsp"] = 0;
            }
            else
            {
                ViewData["ClienteProsp"] = cli.IdClienteProsp;
            }
            
            IOrderedQueryable<ClienteHistorico> historicos =
                _ctx.ClienteHistoricos
                    .Include(c => c.Cliente)
                    .Include(c => c.Usuario)
                    .Where(w => w.ClienteId == clienteId)
                    .OrderByDescending(c => c.Id);
            var numeroPagina = (pagina == 0) ? 1 : pagina;
            return View(await historicos.ToPagedListAsync(numeroPagina, 20));
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


    }





}
