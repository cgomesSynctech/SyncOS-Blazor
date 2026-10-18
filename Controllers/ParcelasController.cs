using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using info;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Newtonsoft.Json;
using X.PagedList;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class ParcelasController : Controller
    {
        private readonly APPDbContext _ctx;
        private readonly INotyfService _notyf;
        private Usuario _usuarioLogado;

        public ParcelasController(APPDbContext context, INotyfService notyf)
        {
            _ctx = context;
            _notyf = notyf;
        }

        private Usuario SetUserLogado()
        {
            try
            {
                var cookieValue = Request.Cookies["UsuarioLogado"];
                if (string.IsNullOrEmpty(cookieValue))
                {
                    ViewData["Perfil"] = 0;
                    return new Usuario();
                }
                var userLogado = JsonConvert.DeserializeObject<Usuario>(cookieValue);
                ViewData["Perfil"] = userLogado?.PerfilId ?? 0;
                ViewData["NomeUsuario"] = userLogado?.LoginName ?? "";
                return userLogado ?? new Usuario();
            }
            catch (Exception)
            {
                ViewData["Perfil"] = 0;
                return new Usuario();
            }
        }

        private SelectList CarregarStatus(string statusSelecionado = null)
        {
            var lista = new[]
            {
                new { Id = "A", Descricao = "A Vencer" },
                new { Id = "V", Descricao = "Vencido" },
                new { Id = "L", Descricao = "Liquidado" }
            };
            return new SelectList(lista, "Id", "Descricao", statusSelecionado);
        }

        // GET: Parcelas
        public async Task<IActionResult> Index(int pagina, string filtro, string status)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }

            filtro ??= "";
            filtro = filtro.Trim();
            ViewData["Filtro"] = filtro;
            ViewData["Status"] = status;

            IQueryable<Parcela> query = _ctx.Parcelas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(p => p.NomeCliente.Contains(filtro) || p.CPFCNPJ.Contains(filtro));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(p => p.Status == status);
            }

            var parcelasOrdenadas = query.OrderBy(p => p.DataVencimento);
            var numeroPagina = (pagina == 0) ? 1 : pagina;

            return View(await parcelasOrdenadas.ToPagedListAsync(numeroPagina, 20));
        }

        // GET: Parcelas/Details/5
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

            var parcela = await _ctx.Parcelas.FirstOrDefaultAsync(m => m.Id == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // GET: Parcelas/Create
        public IActionResult Create()
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }

            ViewData["StatusList"] = CarregarStatus("A");
            return View(new Parcela { DataVencimento = DateTime.Today });
        }

        // POST: Parcelas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeCliente,CPFCNPJ,Fone,Email,DataVencimento,Valor,Status")] Parcela parcela)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }

            try
            {
                if (!string.IsNullOrEmpty(parcela.CPFCNPJ))
                {
                    parcela.CPFCNPJ = Regex.Replace(parcela.CPFCNPJ, "[^0-9]", "");
                }
                if (!string.IsNullOrEmpty(parcela.Fone))
                {
                    parcela.Fone = Regex.Replace(parcela.Fone, "[^0-9]", "");
                }

                if (ModelState.IsValid)
                {
                    _ctx.Add(parcela);
                    await _ctx.SaveChangesAsync();
                    _notyf.Success(string.Format("<h3>Aviso</h3><br/><p>Parcela de {0} cadastrada com sucesso.</p>", parcela.NomeCliente), 5);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var erros = string.Empty;
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        erros += error.ErrorMessage + "<br/>";
                    }
                    _notyf.Warning(string.Format("<h3>Aviso</h3><br/><p>Erro ao cadastrar parcela:<br/>{0}</p>", erros), 5);
                }
            }
            catch (Exception ex)
            {
                _notyf.Error(string.Format("<h3>Erro</h3><br/><p>Ocorreu um erro: {0}</p>", ex.Message), 5);
            }

            ViewData["StatusList"] = CarregarStatus(parcela.Status);
            return View(parcela);
        }

        // GET: Parcelas/Edit/5
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

            var parcela = await _ctx.Parcelas.FindAsync(id);
            if (parcela == null)
            {
                return NotFound();
            }

            ViewData["StatusList"] = CarregarStatus(parcela.Status);
            return View(parcela);
        }

        // POST: Parcelas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeCliente,CPFCNPJ,Fone,Email,DataVencimento,Valor,Status")] Parcela parcela)
        {
            _usuarioLogado = SetUserLogado();
            if (_usuarioLogado.PerfilId == 0)
            {
                _notyf.Warning("<h3>Aviso</h3><br/><p>Você não tem permissão para esta tarefa!</p>", 5);
                return RedirectToAction("Login", "Home");
            }

            if (id != parcela.Id)
            {
                return NotFound();
            }

            try
            {
                if (!string.IsNullOrEmpty(parcela.CPFCNPJ))
                {
                    parcela.CPFCNPJ = Regex.Replace(parcela.CPFCNPJ, "[^0-9]", "");
                }
                if (!string.IsNullOrEmpty(parcela.Fone))
                {
                    parcela.Fone = Regex.Replace(parcela.Fone, "[^0-9]", "");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _ctx.Update(parcela);
                        await _ctx.SaveChangesAsync();
                        _notyf.Success("<h3>Aviso</h3><br/><p>Parcela alterada com sucesso.</p>", 5);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_ctx.Parcelas.Any(e => e.Id == parcela.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _notyf.Error(string.Format("<h3>Erro</h3><br/><p>Ocorreu um erro: {0}</p>", ex.Message), 5);
            }

            ViewData["StatusList"] = CarregarStatus(parcela.Status);
            return View(parcela);
        }

        // GET: Parcelas/Delete/5
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

            var parcela = await _ctx.Parcelas.FirstOrDefaultAsync(m => m.Id == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // POST: Parcelas/Delete/5
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

            var parcela = await _ctx.Parcelas.FindAsync(id);
            if (parcela != null)
            {
                _ctx.Parcelas.Remove(parcela);
                await _ctx.SaveChangesAsync();
                _notyf.Success("<h3>Aviso</h3><br/><p>Parcela excluída com sucesso.</p>", 5);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
